using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.IO;
using Dapper;

namespace Customer
{
    public static class DatabaseHelper
    {
        // هذا السطر يضمن أن البرنامج سيبحث عن القاعدة في نفس مجلد الـ exe مهما كان مكان المجلد
        private static string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mydatabase.db");

        // تأكد من تغيير الامتداد في السطر أعلاه لـ .db لو كان ملفك ينتهي بـ .db
        private static string ConnectionString = $"Data Source={DbPath};Version=3;";

        // --- 1. جلب المستخدمين لشاشة الدخول ---
        public static List<UserInfo> GetUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                // Dapper سيقوم أوتوماتيكياً بتحويل نتائج الاستعلام إلى قائمة من كلاس UserInfo
                return cnn.Query<UserInfo>("SELECT * FROM Users").ToList();
            }
        }

        // --- 2. جلب الإعدادات (اسم المتجر، الضريبة...) ---
        public static void LoadSettings()
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                var settings = cnn.QueryFirstOrDefault<AppData>("SELECT * FROM Settings WHERE Id = 1");
                if (settings != null)
                {
                    // نحدث القيم في الكلاس الموجود عندك
                    AppDataStore.Current.StoreName = settings.StoreName;
                    AppDataStore.Current.OwnerName = settings.OwnerName;
                    AppDataStore.Current.VatRate = settings.VatRate;
                    AppDataStore.Current.FooterMessage = settings.FooterMessage;
                    AppDataStore.Current.LastInvoiceNumber = settings.LastInvoiceNumber;
                    AppDataStore.Current.PrinterName = settings.PrinterName;
                }
            }
        }

        // --- 3. حفظ الفاتورة الجديدة (أهم دالة) ---
        public static void SaveInvoice(InvoiceRecord invoice)
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();
                // نستخدم Transaction عشان نضمن إن الفاتورة والأصناف ينحفظون سوا أو لا شيء ينحفظ
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        // أ. حفظ رأس الفاتورة
                        string sqlInvoice = @"
                            INSERT INTO Invoices (Number, Date, UserName, Role, SubTotal, Tax, GrandTotal, Status) 
                            VALUES (@Number, @Date, @UserName, @Role, @SubTotal, @Tax, @GrandTotal, @Status)";

                        // هنا Dapper يأخذ القيم من الاوبجكت invoice مباشرة
                        cnn.Execute(sqlInvoice, invoice, transaction: trans);

                        // ب. حفظ المنتجات (الأصناف)
                        string sqlItems = @"
                            INSERT INTO InvoiceItems (InvoiceNumber, Name, Price, Quantity) 
                            VALUES (@InvoiceNumber, @Name, @Price, @Quantity)";

                        foreach (var item in invoice.Items)
                        {
                            cnn.Execute(sqlItems, new
                            {
                                InvoiceNumber = invoice.Number,
                                Name = item.Name,
                                Price = item.Price,
                                Quantity = item.Quantity
                            }, transaction: trans);
                        }

                        // ج. تحديث رقم آخر فاتورة في الإعدادات
                        cnn.Execute("UPDATE Settings SET LastInvoiceNumber = @Num WHERE Id = 1",
                            new { Num = invoice.Number }, transaction: trans);

                        // اعتماد الحفظ
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        // لو صار خطأ نتراجع عن كل شيء
                        trans.Rollback();
                        throw; // نرمي الخطأ عشان يظهر لك في البرنامج وتعرف السبب
                    }
                }
            }
        }

        // --- 4. جلب التقارير ---
        public static List<InvoiceRecord> GetAllInvoices()
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                // نجلب الفواتير
                var invoices = cnn.Query<InvoiceRecord>("SELECT * FROM Invoices").ToList();

                // لكل فاتورة نجلب أصنافها (هذه الطريقة بسيطة ومناسبة للمشاريع الصغيرة والمتوسطة)
                foreach (var inv in invoices)
                {
                    string sqlItems = "SELECT Name, Price, Quantity FROM InvoiceItems WHERE InvoiceNumber = @Num";
                    var items = cnn.Query<Product>(sqlItems, new { Num = inv.Number }).ToList();
                    inv.Items = items;
                }

                return invoices;
            }
        }
    }
}
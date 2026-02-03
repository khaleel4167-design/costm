using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Customer
{
    public static class AppDataStore
    {
        public static AppData Current { get; private set; } = new AppData();

        /// <summary>
        /// تحميل البيانات من قاعدة البيانات
        /// </summary>
        public static void Load()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // إنشاء قاعدة البيانات إذا لم تكن موجودة
                    AppDbContext.InitializeDatabase();

                    // تحميل المستخدمين
                    Current.Users = db.Users.ToList();

                    // إذا لم يكن هناك مستخدمين، أضف الافتراضيين
                    if (!Current.Users.Any())
                    {
                        CreateDefaultUsers(db);
                        Current.Users = db.Users.ToList();
                    }

                    // تحميل الإعدادات
                    var settings = db.Settings.FirstOrDefault();
                    if (settings == null)
                    {
                        settings = CreateDefaultSettings(db);
                    }

                    // نسخ الإعدادات إلى Current
                    Current.StoreName = settings.StoreName;
                    Current.OwnerName = settings.OwnerName;
                    Current.VatRate = settings.VatRate;
                    Current.InvoiceStartNumber = settings.InvoiceStartNumber;
                    Current.FooterMessage = settings.FooterMessage;
                    Current.StoreLogoBase64 = settings.StoreLogoBase64;
                    Current.PrinterName = settings.PrinterName;
                    Current.EnableLogo = settings.EnableLogo;
                    Current.TouchMode = settings.TouchMode;
                    Current.LastInvoiceNumber = settings.LastInvoiceNumber;

                    // تحميل الفواتير حسب الحالة
                    var allInvoices = db.Invoices.ToList();
                    Current.Invoices = allInvoices.Where(i => i.Status == InvoiceStatus.Paid).ToList();
                    Current.SuspendedInvoices = allInvoices.Where(i => i.Status == InvoiceStatus.Suspended).ToList();
                    Current.InvoicesHistory = allInvoices.Where(i => i.Status == InvoiceStatus.Normal).ToList();
                    Current.CanceledInvoices = allInvoices.Where(i => i.Status == InvoiceStatus.Canceled).ToList();
                }
            }
            catch (Exception ex)
            {
                // عرض رسالة خطأ واضحة
                System.Windows.Forms.MessageBox.Show(
                    $"خطأ في تحميل قاعدة البيانات: {ex.Message}\n\nالرجاء التأكد من وجود قاعدة البيانات وصحة البيانات.",
                    "خطأ",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);

                // إنشاء بيانات افتراضية
                Current = CreateDefaultData();
            }
        }

        /// <summary>
        /// حفظ البيانات في قاعدة البيانات
        /// </summary>
        public static void Save()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // حفظ المستخدمين
                    foreach (var user in Current.Users)
                    {
                        var existingUser = db.Users.Find(user.UserName);
                        if (existingUser == null)
                        {
                            db.Users.Add(user);
                        }
                        else
                        {
                            db.Entry(existingUser).CurrentValues.SetValues(user);
                        }
                    }

                    // حفظ الإعدادات
                    var settings = db.Settings.FirstOrDefault();
                    if (settings == null)
                    {
                        settings = new AppSettings();
                        db.Settings.Add(settings);
                    }

                    settings.StoreName = Current.StoreName;
                    settings.OwnerName = Current.OwnerName;
                    settings.VatRate = Current.VatRate;
                    settings.InvoiceStartNumber = Current.InvoiceStartNumber;
                    settings.FooterMessage = Current.FooterMessage;
                    settings.StoreLogoBase64 = Current.StoreLogoBase64;
                    settings.PrinterName = Current.PrinterName;
                    settings.EnableLogo = Current.EnableLogo;
                    settings.TouchMode = Current.TouchMode;
                    settings.LastInvoiceNumber = Current.LastInvoiceNumber;

                    // حفظ جميع الفواتير
                    var allInvoicesToSave = new List<InvoiceRecord>();
                    allInvoicesToSave.AddRange(Current.Invoices);
                    allInvoicesToSave.AddRange(Current.SuspendedInvoices);
                    allInvoicesToSave.AddRange(Current.InvoicesHistory);
                    allInvoicesToSave.AddRange(Current.CanceledInvoices);

                    foreach (var invoice in allInvoicesToSave)
                    {
                        var existingInvoice = db.Invoices.Find(invoice.Number);
                        if (existingInvoice == null)
                        {
                            db.Invoices.Add(invoice);
                        }
                        else
                        {
                            db.Entry(existingInvoice).CurrentValues.SetValues(invoice);
                            existingInvoice.ItemsJson = invoice.ItemsJson; // مهم لتحديث الأصناف
                        }
                    }

                    db.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"خطأ في حفظ قاعدة البيانات: {ex.Message}",
                    "خطأ",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إنشاء المستخدمين الافتراضيين
        /// </summary>
        private static void CreateDefaultUsers(AppDbContext db)
        {
            db.Users.Add(new UserInfo
            {
                UserName = "Admin",
                Password = "2222",
                Role = UserRole.Admin
            });

            db.Users.Add(new UserInfo
            {
                UserName = "Employee1",
                Password = "1111",
                Role = UserRole.Employee
            });

            db.SaveChanges();
        }

        /// <summary>
        /// إنشاء الإعدادات الافتراضية
        /// </summary>
        private static AppSettings CreateDefaultSettings(AppDbContext db)
        {
            var settings = new AppSettings
            {
                VatRate = 0.15m,
                InvoiceStartNumber = 1,
                LastInvoiceNumber = 0,
            };

            db.Settings.Add(settings);
            db.SaveChanges();

            return settings;
        }



        /// <summary>
        /// إنشاء بيانات افتراضية
        /// </summary>
        private static AppData CreateDefaultData()
        {
            var data = new AppData();

            data.Users.Add(new UserInfo
            {
                UserName = "Admin",
                Password = "2222",
                Role = UserRole.Admin
            });

            data.Users.Add(new UserInfo
            {
                UserName = "Employee1",
                Password = "1111",
                Role = UserRole.Employee
            });

            return data;
        }
    }
}
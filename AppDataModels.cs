using System;
using System.Collections.Generic;
using System.Linq;

namespace Customer
{
    // 🔹 دور المستخدم
    public enum UserRole
    {
        Admin,
        Employee
    }

    // 🔹 المنتج
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public int Total => Price * Quantity;
    }

    // 🔹 حالة الفاتورة (أضفنا Suspended و Paid عشان الكود القديم)
    public enum InvoiceStatus
    {
        Normal,
        Suspended,
        Paid,
        Canceled
    }

    // 🔹 سجل الفاتورة المستخدم في التقارير والتاريخ
    public class InvoiceRecord
    {
        public int Number { get; set; }

        // بعض الأكواد تستخدم Date وبعضها Time، فخلّيناهم نفس الشي
        public DateTime Date { get; set; } = DateTime.Now;

        // توافق مع الكود القديم
        public DateTime Time
        {
            get => Date;
            set => Date = value;
        }

        public string UserName { get; set; } = "";
        public UserRole Role { get; set; }   // 👈 جديد

        // تفاصيل الأصناف في الفاتورة
        public List<Product> Items { get; set; } = new List<Product>();
        public decimal SubTotal { get; set; }   // المجموع قبل الضريبة
        public decimal Tax { get; set; }        // قيمة الضريبة
        public decimal GrandTotal { get; set; } // الإجمالي بعد الضريبة
        public int ItemsCount
        {
            get => Items?.Sum(p => p.Quantity) ?? 0;
        }
        // لو فيه كود قديم يستخدم Total فقط
        public decimal Total
        {
            get => GrandTotal;
            set => GrandTotal = value;
        }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Normal;
    }

    // 🔹 الفاتورة الكاملة المستخدمة داخل البرنامج (تعليق – استرجاع – ...إلخ)
/*    public class Invoice
    {
        public int Id { get; set; }

        // توافق مع الكود القديم اللي يستخدم Number
        public int Number
        {
            get => Id;
            set => Id = value;
        }
       

        public DateTime Time { get; set; } = DateTime.Now;
        public string UserName { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Employee;
        public List<Product> Items { get; set; } = new List<Product>();
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }    
        public decimal GrandTotal { get; set; }

        public decimal Total
        {
            get => GrandTotal;
            set => GrandTotal = value;
        }

        // 👈 تحويل تلقائي من InvoiceRecord إلى Invoice
        public static implicit operator Invoice(InvoiceRecord r)
        {
            if (r == null) return null;

            return new Invoice
            {
                Id = r.Number,
                Time = r.Date,
                UserName = r.UserName,
                Items = r.Items
                ?.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
                ?? new List<Product>(),

                SubTotal = r.SubTotal,
                Tax = r.Tax,
                GrandTotal = r.GrandTotal
            };
        }
    }
*/
    // 🔹 معلومات المستخدم
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    // 🔹 كل البيانات اللي تنحفظ في ملف JSON واحد
    public class AppData
    {
        public string StoreName { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public decimal VatRate { get; set; } = 0.15m;
        public int InvoiceStartNumber { get; set; } = 1;
        public string FooterMessage { get; set; } = "";
        public string StoreLogoBase64 { get; set; } = "";
        public string PrinterName { get; set; } = "";
        public string LogoBase64 { get; set; } = "";
        public bool EnableLogo { get; set; } = false;

        public bool TouchMode { get; set; } = false;
        public string Language { get; set; } = "Arabic";

        // المستخدمين
        public List<UserInfo> Users { get; set; } = new List<UserInfo>();


        // نسبة الضريبة
        //public decimal VatRate { get; set; } = 0.15m;

        // آخر رقم فاتورة
        public int LastInvoiceNumber { get; set; } = 0;

        // سجل الفواتير (للتقارير)
        public List<InvoiceRecord> Invoices { get; set; } = new List<InvoiceRecord>();

        // الفواتير المعلّقة
        public List<InvoiceRecord> SuspendedInvoices { get; set; } = new List<InvoiceRecord>();

        // تاريخ الفواتير العادية
        public List<InvoiceRecord> InvoicesHistory { get; set; } = new List<InvoiceRecord>();

        // الفواتير الملغاة
        public List<InvoiceRecord> CanceledInvoices { get; set; } = new List<InvoiceRecord>();
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

    // 🔹 حالة الفاتورة
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
        [Key]
        public int Number { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // توافق مع الكود القديم
        [NotMapped]
        public DateTime Time
        {
            get => Date;
            set => Date = value;
        }

        public string UserName { get; set; } = "";
        public UserRole Role { get; set; }

        // حفظ الأصناف كـ JSON في قاعدة البيانات
        public string ItemsJson { get; set; } = "[]";

        // الخاصية التي يستخدمها الكود
        [NotMapped]
        public List<Product> Items
        {
            get
            {
                if (string.IsNullOrEmpty(ItemsJson))
                    return new List<Product>();
                try
                {
                    return JsonConvert.DeserializeObject<List<Product>>(ItemsJson) ?? new List<Product>();
                }
                catch
                {
                    return new List<Product>();
                }
            }
            set
            {
                ItemsJson = JsonConvert.SerializeObject(value ?? new List<Product>());
            }
        }

        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }

        [NotMapped]
        public int ItemsCount
        {
            get => Items?.Sum(p => p.Quantity) ?? 0;
        }

        [NotMapped]
        public decimal Total
        {
            get => GrandTotal;
            set => GrandTotal = value;
        }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Normal;
    }

    // 🔹 معلومات المستخدم
    public class UserInfo
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    // 🔹 كلاس مساعد لإدارة المستخدمين في الجدول
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    // 🔹 كل البيانات (للتوافق مع الكود القديم - سيتم الاستغناء عنه تدريجياً)
    public class AppData
    {
        public string StoreName { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public decimal VatRate { get; set; } = 0.15m;
        public int InvoiceStartNumber { get; set; } = 1;
        public string FooterMessage { get; set; } = "";
        public string StoreLogoBase64 { get; set; } = "";
        public string PrinterName { get; set; } = "";
        public bool EnableLogo { get; set; } = false;
        public bool TouchMode { get; set; } = false;
        public List<UserInfo> Users { get; set; } = new List<UserInfo>();
        public int LastInvoiceNumber { get; set; } = 0;
        public List<InvoiceRecord> Invoices { get; set; } = new List<InvoiceRecord>();
        public List<InvoiceRecord> SuspendedInvoices { get; set; } = new List<InvoiceRecord>();
        public List<InvoiceRecord> CanceledInvoices { get; set; } = new List<InvoiceRecord>();
    }
}
using System.Data.Entity;
using System.Data.SQLite;

namespace Customer
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            // تعطيل Database Initializer لأن SQLite لا يدعمه بشكل جيد
            //Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext() : base("DefaultConnection")
        {
        }

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection("Data Source=AppDatabase.db;Version=3;"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    // إنشاء جدول المستخدمين
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS UserInfoes (
                            UserName TEXT PRIMARY KEY NOT NULL,
                            Password TEXT NOT NULL,
                            Role INTEGER NOT NULL
                        )";
                    command.ExecuteNonQuery();

                    // إنشاء جدول الفواتير
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS InvoiceRecords (
                            Number INTEGER PRIMARY KEY NOT NULL,
                            Date TEXT NOT NULL,
                            CustomerName TEXT,
                            UserName TEXT,
                            Role INTEGER NOT NULL,
                            ItemsJson TEXT NOT NULL DEFAULT '[]',
                            SubTotal REAL NOT NULL DEFAULT 0,
                            Tax REAL NOT NULL DEFAULT 0,
                            GrandTotal REAL NOT NULL DEFAULT 0,
                            Status INTEGER NOT NULL DEFAULT 0,
                            PaymentMethod TEXT
                        )";
                    command.ExecuteNonQuery();

                    // إنشاء جدول الإعدادات
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS AppSettings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            StoreName TEXT,
                            OwnerName TEXT,
                            VatRate REAL,
                            InvoiceStartNumber INTEGER,
                            FooterMessage TEXT,
                            StoreLogoBase64 TEXT,
                            PrinterName TEXT,
                            EnableLogo INTEGER,
                            TouchMode INTEGER,
                            LastInvoiceNumber INTEGER
                        )";
                    command.ExecuteNonQuery();
                }
            }
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<InvoiceRecord> Invoices { get; set; }
        public DbSet<AppSettings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تحديد UserName كـ Primary Key
            modelBuilder.Entity<UserInfo>()
                .HasKey(u => u.UserName);

            // تحديد Number كـ Primary Key للفواتير
            modelBuilder.Entity<InvoiceRecord>()
                .HasKey(i => i.Number);

            // تجاهل خاصية Items لأنها لا يمكن حفظها مباشرة في جدول بسيط
            modelBuilder.Entity<InvoiceRecord>()
                .Ignore(i => i.Items);
        }
    }

    // كلاس جديد للإعدادات
    public class AppSettings
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public decimal VatRate { get; set; } = 0.15m;
        public int InvoiceStartNumber { get; set; } = 1;
        public string FooterMessage { get; set; } = "";
        public string StoreLogoBase64 { get; set; } = "";
        public string PrinterName { get; set; } = "";
        public bool EnableLogo { get; set; } = false;
        public bool TouchMode { get; set; } = false;
        public int LastInvoiceNumber { get; set; } = 0;
    }
}
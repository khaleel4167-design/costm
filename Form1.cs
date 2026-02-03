using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Text;
using Microsoft.VisualBasic;
using System.Drawing.Printing;

namespace Customer
{
    public partial class Customer : Form
    {
        private string _lastInvoiceText = "";


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // إذا كان المستخدم يغلق النافذة عن طريق زر X
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // إلغاء الإغلاق
                e.Cancel = true;

                // إخفاء نموذج المبيعات
                this.Hide();

                // فتح شاشة تسجيل الدخول مرة أخرى
                LoginForm login = new LoginForm();
                login.Show();
            }
        }

        // Form1.cs (داخل الكلاس)
        private int? _resumedInvoiceNumber = null;

        private int? _currentInvoiceNumber = null;
        private bool _isAdminMenuExpanded = false; // مغلقة افتراضاً

        List<Product> products = new List<Product>();
        // 🧾 فاتورة معلّقة واحدة
        // 🧾 قائمة الفواتير المعلقة



        // تعريف كلاس الفاتورة
        private readonly UserRole _currentRole;
        private readonly string _userName;

        // 🔹 باني افتراضي للـ Designer
        public Customer() : this(UserRole.Employee, "Guest")
        {

        }

        // 🔹 الباني الحقيقي الذي يستقبل الدور واسم المستخدم
        public Customer(UserRole role, string userName)
        {

            _currentRole = role;
            _userName = userName;

            InitializeComponent();

            ApplyRolePermissions(); // نطبّق الصلاحيات حسب الدور
        }

        //public Customer()
        //{
        //    InitializeComponent();
        //}
        // 🛡️ تطبيق صلاحيات الدور
        private void ApplyRolePermissions()
        {
            // مثال: الموظف لا يقدر يلغي فاتورة كاملة أو يحذف أصناف
            if (_currentRole == UserRole.Employee)
            {
                buttonCancelInvoice.Enabled = false;
                buttonDeleteItem.Enabled = false;

                // لو عندك زر إدارة المستخدمين/التقارير:
                buttonManageUsers.Enabled = false;
                buttonReports.Enabled = false;
            }
            else if (_currentRole == UserRole.Admin)
            {
                buttonCancelInvoice.Enabled = true;
                buttonDeleteItem.Enabled = true;
                buttonManageUsers.Enabled = true;
                buttonReports.Enabled = true;
            }
        }
        private InvoiceRecord CreateInvoiceRecord(InvoiceStatus status)
        {
            var itemsCopy = products.Select(p => new Product
            {
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();

            decimal subtotal = itemsCopy.Sum(p => p.Total);
            decimal tax = subtotal * AppDataStore.Current.VatRate;
            decimal grandTotal = subtotal + tax;

            int number;

            // 1) إذا كنت تسترجع فاتورة معلقة → استخدم رقمها كما هو
            if (_currentInvoiceNumber.HasValue)
            {
                number = _currentInvoiceNumber.Value;
            }
            else
            {
                // 2) تعليق فاتورة → زِد الرقم مثل الفاتورة العادية
                if (status == InvoiceStatus.Suspended)
                {
                    AppDataStore.Current.LastInvoiceNumber++;
                    number = AppDataStore.Current.LastInvoiceNumber;
                }
                else
                {
                    // 3) فاتورة جديدة مدفوعة
                    AppDataStore.Current.LastInvoiceNumber++;
                    number = AppDataStore.Current.LastInvoiceNumber;
                }
            }

            return new InvoiceRecord
            {
                Number = number,
                Date = DateTime.Now,
                Items = itemsCopy,
                SubTotal = subtotal,
                Tax = tax,
                GrandTotal = grandTotal,
                Status = status,
                UserName = Program.LoggedInUserName,
                Role = Program.LoggedInUserRole
            };
        }

        private void ApplyModernTheme()
        {
            // خلفية الفورم
            this.BackColor = Color.FromArgb(15, 23, 42); // #0F172A

            // مثال: لو عندك Panel للفاتورة (panelInvoiceContainer)
            // غيّر الاسم حسب الموجود عندك
            TryStylePanel(panelInvoiceContainer);

            // زر طباعة الفاتورة (غير الاسم حسب زرّك)
            TryStylePrimaryButton(HibaDatesCakeSmallChocolate);

            // زر تعليق/استرجاع (غير الأسماء حسب عندك)
            TryStyleSecondaryButton(buttonSuspendInvoice);
            TryStyleSecondaryButton(buttonResumeInvoice);

            // لو عندك زر حذف/إلغاء
            TryStyleDangerButton(buttonDeleteItem);
            TryStyleDangerButton(buttonCancelInvoice);

            // لو عندك DataGridView جديد من Guna
            TryStyleGrid(dataGridView1);
        }

        private void TryStylePanel(Control ctrl)
        {
            if (ctrl is Guna.UI2.WinForms.Guna2Panel p)
            {
                p.FillColor = Color.FromArgb(30, 41, 59); // #1E293B
                p.BorderRadius = 16;
                p.ShadowDecoration.Enabled = true;
                p.ShadowDecoration.BorderRadius = 16;
                p.ShadowDecoration.Depth = 8;
            }
        }

        private void TryStylePrimaryButton(Control ctrl)
        {
            if (ctrl is Guna.UI2.WinForms.Guna2Button b)
            {
                b.FillColor = Color.FromArgb(59, 130, 246); // #3B82F6
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                b.BorderRadius = 14;
                b.Animated = true;
                b.HoverState.FillColor = Color.FromArgb(37, 99, 235);
                b.PressedColor = Color.FromArgb(30, 64, 175);
            }
        }

        private void TryStyleSecondaryButton(Control ctrl)
        {
            if (ctrl is Guna.UI2.WinForms.Guna2Button b)
            {
                b.FillColor = Color.FromArgb(51, 65, 85); // #334155
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                b.BorderRadius = 14;
                b.Animated = true;
                b.HoverState.FillColor = Color.FromArgb(71, 85, 105); // #475569
            }
        }

        private void TryStyleDangerButton(Control ctrl)
        {
            if (ctrl is Guna.UI2.WinForms.Guna2Button b)
            {
                b.FillColor = Color.FromArgb(239, 68, 68); // #EF4444
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                b.BorderRadius = 14;
                b.Animated = true;
                b.HoverState.FillColor = Color.FromArgb(220, 38, 38);
            }
        }

        private void TryStyleGrid(Control ctrl)
        {
            if (ctrl is Guna.UI2.WinForms.Guna2DataGridView g)
            {
                g.BackgroundColor = Color.FromArgb(15, 23, 42);
                g.GridColor = Color.FromArgb(51, 65, 85);

                g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                g.ColumnHeadersHeight = 45;

                g.DefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
                g.DefaultCellStyle.ForeColor = Color.White;
                g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
                g.DefaultCellStyle.SelectionForeColor = Color.White;
                g.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                g.RowTemplate.Height = 40;
                g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(17, 31, 55);
                g.EnableHeadersVisualStyles = false;
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {


            // التأكد من أن القائمة الجانبية في المقدمة حتى لا تغطيها العناصر الأخرى
            panelMenu.BringToFront();

            Image smallImage = new Bitmap(Properties.Resources.trash_red_24, new Size(24, 24));

            buttonCancelInvoice.Image = smallImage;
            dataGridView1.BackgroundColor = Color.FromArgb(15, 25, 35);
            dataGridView1.GridColor = Color.FromArgb(40, 50, 65);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(15, 25, 35);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            // ✅ إنشاء الأعمدة يدويًا قبل إضافة أي صف
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Name", "اسم المنتج");
            dataGridView1.Columns.Add("Quantity", "الكمية");
            dataGridView1.Columns.Add("Price", "السعر");
            dataGridView1.Columns.Add("Total", "الإجمالي");
            // ✅ إنشاء عمود زر أيقونة للحذف
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = " "; // بدون عنوان
            deleteColumn.Image = Properties.Resources.trash_red_24; // ← الأيقونة
            deleteColumn.Width = 40;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            deleteColumn.ToolTipText = "حذف الصنف";

            // إضافة العمود بعد الأعمدة الأخرى
            dataGridView1.Columns.Add(deleteColumn);
            dataGridView1.Columns["Name"].FillWeight = 220; // أوسع شيء
            dataGridView1.Columns["Quantity"].FillWeight = 60;
            dataGridView1.Columns["Price"].FillWeight = 80;
            dataGridView1.Columns["Total"].FillWeight = 80;
            dataGridView1.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Delete"].Width = 40;

            // 🔹 ربط الحدث يدوياً
            dataGridView1.Paint += new PaintEventHandler(DataGridView1_Paint);
            // 🌟 تأثير الإضاءة عند تمرير الماوس على الصف
            Color normalColor = Color.FromArgb(25, 35, 50);
            Color altColor = Color.FromArgb(35, 45, 60);
            Color hoverColor = Color.FromArgb(55, 75, 100);

            // عندما يدخل الماوس على الصف
            dataGridView1.CellMouseEnter += new DataGridViewCellEventHandler((sender2, e2) =>
            {
                if (e2.RowIndex >= 0)
                {
                    dataGridView1.Rows[e2.RowIndex].DefaultCellStyle.BackColor = hoverColor;
                }
            });

            // عندما يخرج الماوس من الصف
            dataGridView1.CellMouseLeave += new DataGridViewCellEventHandler((sender3, e3) =>
            {
                if (e3.RowIndex >= 0)
                {
                    // نعيد اللون حسب إذا كان الصف زوجي أو فردي
                    dataGridView1.Rows[e3.RowIndex].DefaultCellStyle.BackColor =
                        e3.RowIndex % 2 == 0 ? normalColor : altColor;
                }
            });
            // 🧭 تمكين اختيار الصف بالكامل عند الضغط
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 🧱 تعطيل التحديد الافتراضي الأزرق
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 70, 90);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // 🧩 جعل المستخدم يستطيع اختيار صف بالضغط عليه
            dataGridView1.MultiSelect = false;

            // 🔸 حذف التحديد عند تحميل النموذج
            dataGridView1.ClearSelection();
            // 🔁 تحديث القيم تلقائيًا عند حذف صف أو تعديل الخلايا
            dataGridView1.RowsRemoved += (s, ev1) => UpdateTotals();
            dataGridView1.CellValueChanged += (s, ev2) => UpdateTotals();
            dataGridView1.UserDeletedRow += (s, ev3) => UpdateTotals();
            ApplyModernTheme();
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            if (products.Count == 0)
            {
                MessageBox.Show("لا توجد فاتورة لحذفها.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "هل تريد إلغاء الفاتورة بالكامل؟",
                "تأكيد الإلغاء",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            // إنشاء سجل الفاتورة كملغاة (يحتفظ بالرقم الأصلي)
            var canceledRecord = CreateInvoiceRecord(InvoiceStatus.Canceled);

            // تخزينها في قائمة الفواتير الملغاة
            AppDataStore.Current.CanceledInvoices.Add(canceledRecord);
            AppDataStore.Save();

            int number = canceledRecord.Number;

            // مسح الفاتورة الحالية
            products.Clear();
            RefreshGrid();
            _currentInvoiceNumber = null;

            MessageBox.Show($"تم إلغاء الفاتورة رقم {number}.",
                "تم الإلغاء", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }





        // 🧩 إضافة منتج
        private void AddProduct(string name, int price)
        {
            var existing = products.FirstOrDefault(p => p.Name == name);

            if (existing == null)
            {
                existing = new Product
                {
                    Name = name,
                    Price = price,
                    Quantity = 1
                };
                products.Add(existing);
            }
            else
            {
                existing.Quantity++;
            }

            RefreshGrid();
        }

        // 🧩 تحديث البيانات في DataGridView
        private void RefreshGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (var p in products)
            {
                dataGridView1.Rows.Add(p.Name, p.Quantity, p.Price, p.Total);
            }

            // تحديث التصميم للتأكد من ظهورها فورًا
            dataGridView1.Refresh();
            UpdateTotals();
        }
        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                string message = "لا توجد عناصر";
                using (Font font = new Font("Segoe UI", 16, FontStyle.Bold))
                {
                    SizeF textSize = e.Graphics.MeasureString(message, font);
                    float x = (dataGridView1.Width - textSize.Width) / 2;
                    float y = (dataGridView1.Height - textSize.Height) / 2;
                    e.Graphics.DrawString(message, font, Brushes.Gray, x, y);
                }
            }
        }
        // 🔘 الأزرار


        private void button1_Click(object sender, EventArgs e)
        {
            Dessertspanel.Visible = false;
            Mojitopanel.Visible = false;
            DripCoffeePanel.Visible = false;
            panel3.Visible = false;
            panelHotDrinks.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dessertspanel.Visible = false;
            Mojitopanel.Visible = false;
            panelHotDrinks.Visible = false;
            DripCoffeePanel.Visible = false;
            panel3.Visible = true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            // 🎨 إعدادات الإطار
            int borderThickness = 4; // سمك الإطار
            int cornerRadius = 15;   // انحناء الزوايا
            Color borderColor = Color.FromArgb(180, 180, 180); // لون رمادي فاتح مريح للعين

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // يجعل الحواف ناعمة

            Rectangle rect = new Rectangle(
                borderThickness / 2,
                borderThickness / 2,
                panel1.Width - borderThickness,
                panel1.Height - borderThickness
            );

            using (GraphicsPath path = GetRoundedRectanglePath(rect, cornerRadius))
            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        // 🔹 دالة لإنشاء المسار المنحني
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rect.Location, size);

            // الزاوية العلوية اليسرى
            path.AddArc(arc, 180, 90);

            // الزاوية العلوية اليمنى
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // الزاوية السفلية اليمنى
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // الزاوية السفلية اليسرى
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد الصنف الذي تريد حذفه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // تأكيد الحذف
            DialogResult confirm = MessageBox.Show("هل تريد حذف الصنف المحدد؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                // الحصول على اسم المنتج من الصف المحدد
                string nameToDelete = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();

                // 🔹 حذف من القائمة الأصلية (وليس فقط من الجدول)
                var itemToRemove = products.FirstOrDefault(p => p.Name == nameToDelete);
                if (itemToRemove != null)
                {
                    products.Remove(itemToRemove);
                }

                // 🔹 تحديث الجدول بعد الحذف
                RefreshGrid();

                // إزالة التحديد
                dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // لو الضغط على عمود الحذف
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                // 🔐 منع الحذف للموظف
                if (_currentRole == UserRole.Employee)
                {
                    MessageBox.Show("غير مسموح لك بحذف الأصناف، الرجاء الرجوع للمسؤول.",
                        "صلاحيات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nameToDelete = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                DialogResult confirm = MessageBox.Show(
                    $"هل تريد حذف {nameToDelete} من الفاتورة؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    products.RemoveAll(p => p.Name == nameToDelete);
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }


        private void PrintInvoiceToPrinter()
        {

            if (products == null || products.Count == 0)
            {
                MessageBox.Show("لا يوجد أصناف للطباعة.", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PrintDocument pd = new PrintDocument();

            // اسم الطابعة المختار من الإعدادات
            if (!string.IsNullOrEmpty(AppDataStore.Current.PrinterName))
                pd.PrinterSettings.PrinterName = AppDataStore.Current.PrinterName;

            // حواف بسيطة
            pd.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

            // مقاس ورق حراري 80mm تقريب تقريباً
            pd.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 280, 1000);

            pd.PrintPage += Pd_PrintPage;

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الطباعة:\n" + ex.Message,
                    "خطأ طباعة", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 🖊 خطوط للطباعة (Tahoma يدعم العربي)
            using (Font headerFont = new Font("Tahoma", 11, FontStyle.Bold))
            using (Font normalFont = new Font("Tahoma", 8, FontStyle.Regular))
            using (Font boldFont = new Font("Tahoma", 8, FontStyle.Bold))
            {
                float y = e.MarginBounds.Top;
                float xLeft = e.MarginBounds.Left;
                float xRight = e.MarginBounds.Right;

                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat left = new StringFormat { Alignment = StringAlignment.Near };

                // 🏪 بيانات الهيدر (عدّل النصوص كما يناسبك أو خذها من الإعدادات)
                string storeName = AppDataStore.Current.StoreName; // اسم المتجر من الإعدادات
                string addr1 = "حي عليشة شارع الامام";
                string addr2 = "تركي مقابل مدينة الملك سعود الطبية";
                string city = "الرياض";
                string vatNumber = "رقم الضريبة: 310169336000003";
                string phone = "0551897832";

                RectangleF headerRect = new RectangleF(xLeft, y, e.MarginBounds.Width, headerFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(storeName, headerFont, Brushes.Black, headerRect, center);
                y += headerFont.GetHeight(e.Graphics) + 4;

                e.Graphics.DrawString(addr1, normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), center);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString(addr2, normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), center);
                y += normalFont.GetHeight(e.Graphics) + 4;

                e.Graphics.DrawString(city, normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), center);
                y += normalFont.GetHeight(e.Graphics) + 4;

                e.Graphics.DrawString(vatNumber, normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), center);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString(phone, normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), center);
                y += normalFont.GetHeight(e.Graphics) + 6;

                // خط فاصل
                string line = new string('-', 40);
                e.Graphics.DrawString(line, normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 2;

                // 🕒 بيانات الفاتورة
                DateTime now = DateTime.Now;
                string invoiceDate = now.ToString("dd/MM/yyyy HH:mm:ss");
                string userName = Program.LoggedInUserName ?? "User";

                e.Graphics.DrawString("رقم الفاتورة: " + AppDataStore.Current.LastInvoiceNumber,
                    normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString("التاريخ: " + invoiceDate,
                    normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString("المستخدم: " + userName,
                    normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 4;

                e.Graphics.DrawString(line, normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 2;

                // 🧾 الأصناف
                decimal totalWithVat = 0;
                int itemsCount = 0;

                foreach (var p in products)
                {
                    decimal lineTotal = p.Total;
                    totalWithVat += lineTotal;
                    itemsCount += p.Quantity;

                    // اسم المنتج سطر لوحده
                    e.Graphics.DrawString(p.Name, normalFont, Brushes.Black, xLeft, y, left);
                    y += normalFont.GetHeight(e.Graphics) + 2;

                    // سطر الكمية × السعر + الإجمالي يمين
                    string qtyPrice = $"{p.Quantity} x {p.Price:0.00}";
                    string totalText = $"S.R{lineTotal:0.00}";

                    e.Graphics.DrawString(qtyPrice, normalFont, Brushes.Black, xLeft, y, left);
                    e.Graphics.DrawString(totalText, normalFont, Brushes.Black,
                        new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), right);

                    y += normalFont.GetHeight(e.Graphics) + 4;
                }

                e.Graphics.DrawString("عدد المنتجات: " + itemsCount,
                    normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 4;

                e.Graphics.DrawString(line, normalFont, Brushes.Black, xLeft, y, left);
                y += normalFont.GetHeight(e.Graphics) + 2;

                // 🧮 الإجماليات
                decimal vatRate = AppDataStore.Current.VatRate;
                decimal subTotal = Math.Round(totalWithVat / (1 + vatRate), 2);
                decimal tax = Math.Round(totalWithVat - subTotal, 2);
                decimal grandTotal = Math.Round(totalWithVat, 2);

                e.Graphics.DrawString("الإجمالي قبل الضريبة:", normalFont, Brushes.Black, xLeft, y, left);
                e.Graphics.DrawString($"S.R{subTotal:0.00}", normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), right);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString($"الضريبة ({vatRate * 100:0}%):", normalFont, Brushes.Black, xLeft, y, left);
                e.Graphics.DrawString($"S.R{tax:0.00}", normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), right);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString("إجمالي:", boldFont, Brushes.Black, xLeft, y, left);
                e.Graphics.DrawString($"S.R{grandTotal:0.00}", boldFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, boldFont.GetHeight(e.Graphics)), right);
                y += boldFont.GetHeight(e.Graphics) + 4;

                // طريقة الدفع
                e.Graphics.DrawString("Cash:", normalFont, Brushes.Black, xLeft, y, left);
                e.Graphics.DrawString($"S.R{grandTotal:0.00}", normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), right);
                y += normalFont.GetHeight(e.Graphics) + 2;

                e.Graphics.DrawString("المبلغ المدفوع:", normalFont, Brushes.Black, xLeft, y, left);
                e.Graphics.DrawString($"S.R{grandTotal:0.00}", normalFont, Brushes.Black,
                    new RectangleF(xLeft, y, e.MarginBounds.Width, normalFont.GetHeight(e.Graphics)), right);
                y += normalFont.GetHeight(e.Graphics) + 6;

                // تقدر هنا لاحقاً تضيف باركود أو QR إن حبيت

                e.HasMorePages = false;
            }
        }


        private void buttonPrintInvoice_Click_1(object sender, EventArgs e)
        {
            if (products.Count == 0)
            {
                MessageBox.Show("لا توجد منتجات في الفاتورة.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StringBuilder invoice = new StringBuilder();

            // اسم المتجر من الإعدادات
            invoice.AppendLine(AppDataStore.Current.StoreName);
            invoice.AppendLine("-----------------------------");
            invoice.AppendLine("🧾 فاتورة المبيعات");
            invoice.AppendLine("-----------------------------");

            decimal subtotalWithVat = 0;

            foreach (var p in products)
            {
                decimal lineTotal = p.Total;
                subtotalWithVat += lineTotal;
                invoice.AppendLine($"{p.Name,-12}  x{p.Quantity}   = {lineTotal:0.00} ر.س");
            }

            // نسبة الضريبة من الإعدادات (مثلاً 0.15)
            decimal vatRate = AppDataStore.Current.VatRate;
            decimal subtotalBeforeVat = subtotalWithVat / (1 + vatRate);
            decimal tax = subtotalWithVat - subtotalBeforeVat;

            subtotalBeforeVat = Math.Round(subtotalBeforeVat, 2);
            tax = Math.Round(tax, 2);
            subtotalWithVat = Math.Round(subtotalWithVat, 2);

            invoice.AppendLine("-----------------------------");
            invoice.AppendLine($"الإجمالي قبل الضريبة: {subtotalBeforeVat:0.00} ر.س");
            invoice.AppendLine($"الضريبة {(vatRate * 100):0}%: {tax:0.00} ر.س");
            invoice.AppendLine($"الإجمالي الكلي: {subtotalWithVat:0.00} ر.س");
            invoice.AppendLine($"التاريخ: {DateTime.Now:yyyy/MM/dd  hh:mm tt}");

            // حفظ النص في المتغيّر عشان دالة الطباعة تستخدمه
            _lastInvoiceText = invoice.ToString();

            // طباعة على الطابعة
            PrintInvoiceToPrinter();

            // حفظ الفاتورة في قاعدة البيانات
            var record = CreateInvoiceRecord(InvoiceStatus.Paid);
            AppDataStore.Current.Invoices.Add(record);
            AppDataStore.Current.InvoicesHistory.Add(record);
            AppDataStore.Save();

            // تنظيف الفاتورة من الشاشة
            ClearCurrentInvoice();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelInvoiceContainer_Paint(object sender, PaintEventArgs e)

        {
            Panel pnl = sender as Panel;

            // 🖌️ إعدادات الشكل
            int borderRadius = 14;        // انحناء الزوايا
            int borderSize = 6;           // سماكة الإطار (أسمك من السابق)
            int shadowOffset = 10;         // بُعد الظل
            Color shadowColor = Color.FromArgb(200, 0, 0, 0); // ظل شفاف ناعم (رمادي غامق)
            Color borderColorLight = Color.FromArgb(100, 160, 230); // أزرق فاتح مريح
            Color borderColorDark = Color.FromArgb(50, 90, 170);    // أزرق غامق راقٍ

            Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 🎨 1. رسم الظل أولاً
            Rectangle shadowRect = new Rectangle(
                rect.X + shadowOffset,
                rect.Y + shadowOffset,
                rect.Width - shadowOffset,
                rect.Height - shadowOffset
            );

            using (SolidBrush shadowBrush = new SolidBrush(shadowColor))
            {
                e.Graphics.FillRoundedRectangle(shadowBrush, shadowRect, borderRadius + 2);
            }

            // 🎨 2. تدرج لوني للإطار (من الأزرق الفاتح للأزرق الغامق)
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                borderColorLight,
                borderColorDark,
                LinearGradientMode.Vertical))
            {
                using (Pen pen = new Pen(brush, borderSize))
                {
                    e.Graphics.DrawRoundedRectangle(pen, rect, borderRadius);
                }
            }

        }

        private void panelSummary_Paint_Paint(object sender, PaintEventArgs e)
        {

            // 👇 إعدادات الشكل
            int borderRadius = 12; // درجة التقوّس (كلما زاد الرقم زادت الانحناءات)
            int borderSize = 3;    // سماكة الحدود
            Color borderColor = Color.FromArgb(0, 180, 255); // لون الإطار (أزرق سماوي أنيق)
            Color backgroundColor = Color.FromArgb(25, 35, 50); // لون الخلفية الداكن

            // 📏 تحديد حجم المستطيل الداخلي
            Rectangle rect = new Rectangle(0, 0, panelSummary.Width - 1, panelSummary.Height - 1);

            // ⚙️ تحسين المظهر
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 🎨 تعبئة الخلفية
            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRoundedRectangle(brush, rect, borderRadius);
            }

            // 🖋️ رسم الحدود (اللمعة الزرقاء)
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                e.Graphics.DrawRoundedRectangle(pen, rect, borderRadius);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        // 🧮 دالة لحساب المجموع والضريبة والإجمالي
        private void UpdateTotals()
        {
            // إجمالي الفاتورة (شامل الضريبة)
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                    grandTotal += Convert.ToDecimal(row.Cells["Total"].Value);
            }

            // السعر قبل الضريبة
            decimal subtotalBeforeVat = grandTotal / 1.15m;

            // الضريبة
            decimal tax = grandTotal - subtotalBeforeVat;

            // تقريب
            subtotalBeforeVat = Math.Round(subtotalBeforeVat, 2);
            tax = Math.Round(tax, 2);
            grandTotal = Math.Round(grandTotal, 2);

            // عرضها في الواجهة
            labelSubtotal.Text = $"{subtotalBeforeVat:0.00} ر.س";
            labelTax.Text = $"{tax:0.00} ر.س";
            labelTotal.Text = $"{grandTotal:0.00} ر.س";

        }
        // 🧾 تعليق الفاتورة الحالية
        // 🧾 تعليق الفاتورة الحالية (يضيفها لقائمة الفواتير المعلقة)
        // 🧾 تعليق الفاتورة الحالية (يضيفها لقائمة الفواتير المعلقة)
        // تنظيف الفاتورة الحالية بعد الطباعة أو الإلغاء
        private void ClearCurrentInvoice()
        {
            // تفريغ قائمة المنتجات
            products.Clear();

            // تفريغ الجدول
            dataGridView1.Rows.Clear();

            // تحديث المجاميع (المجموع والضريبة والإجمالي)
            UpdateTotals();

            // إلغاء أي تحديد
            dataGridView1.ClearSelection();

            // اعتبار أن الفاتورة الجديدة ما لها رقم سابق
            _currentInvoiceNumber = null;
        }
        private void SuspendInvoice()
        {

            if (products.Count == 0)
            {
                MessageBox.Show("لا توجد أصناف لتعليقها.");
                return;
            }

            try
            {
                // ننشئ سجل فاتورة بالحالة "معلّقة"
                var invRecord = CreateInvoiceRecord(InvoiceStatus.Suspended);

                // حفظ الفاتورة في قاعدة البيانات
                using (var db = new AppDbContext())
                {
                    db.Invoices.Add(invRecord);
                    db.SaveChanges();
                }

                // تحديث AppDataStore للتوافق مع الكود القديم
                AppDataStore.Current.SuspendedInvoices.Add(invRecord);
                AppDataStore.Save();

                products.Clear();
                RefreshGrid();

                MessageBox.Show($"تم تعليق الفاتورة رقم {invRecord.Number}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تعليق الفاتورة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // 🔁 استرجاع الفاتورة المعلّقة
        // 🔁 استرجاع فاتورة معلّقة عبر الفورم الصغير
        private void ResumeSuspendedInvoice()
        {
            // 1) لو ما فيه فواتير معلّقة
            if (AppDataStore.Current.SuspendedInvoices == null ||
                AppDataStore.Current.SuspendedInvoices.Count == 0)
            {
                MessageBox.Show("لا توجد فواتير معلّقة.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) فتح فورم الفواتير المعلّقة
            using (var dlg = new SuspendedInvoicesForm(AppDataStore.Current.SuspendedInvoices))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedInvoice != null)
                {
                    // خزن رقم الفاتورة المسترجعة حتى نستخدمه عند الطباعة
                    _currentInvoiceNumber = dlg.SelectedInvoice.Number;
                    var inv = dlg.SelectedInvoice;

                    // لو في فاتورة حالية فيها أصناف نطلب تأكيد قبل الاستبدال
                    if (products.Count > 0)
                    {
                        var confirm = MessageBox.Show(
                            "سيتم استبدال الفاتورة الحالية بالفاتورة المعلّقة المختارة، هل تريد المتابعة؟",
                            "تأكيد",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (confirm == DialogResult.No)
                            return;
                    }

                    // 3) نسخ الأصناف من الفاتورة المعلّقة إلى الفاتورة الحالية
                    products = inv.Items
                        .Select(p => new Product
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Quantity = p.Quantity
                        })
                        .ToList();

                    // 4) حذف الفاتورة من قاعدة البيانات والقائمة المعلّقة
                    try
                    {
                        using (var db = new AppDbContext())
                        {
                            var invoiceToDelete = db.Invoices.Find(inv.Number);
                            if (invoiceToDelete != null)
                            {
                                db.Invoices.Remove(invoiceToDelete);
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"تحذير: لم يتم حذف الفاتورة من قاعدة البيانات: {ex.Message}",
                            "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    AppDataStore.Current.SuspendedInvoices
                        .RemoveAll(i => i.Number == inv.Number);

                    AppDataStore.Save();

                    // 5) تحديث الجدول والمجاميع
                    RefreshGrid();

                    MessageBox.Show($"تم استرجاع الفاتورة رقم {inv.Number}.", "تم",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                SuspendInvoice();
                return true;
            }
            else if (keyData == Keys.F10)
            {
                ResumeSuspendedInvoice();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void IcedLatte_Click(object sender, EventArgs e)
        {
            AddProduct("آيس لاتيه", 15);
        }

        private void IcedAmericano_Click(object sender, EventArgs e)
        {
            AddProduct("آيس أمريكانو", 12);
        }

        private void Hibiscus_Click(object sender, EventArgs e)
        {
            AddProduct("كركديه", 14);
        }

        private void PeachIcedTea_Click(object sender, EventArgs e)
        {
            AddProduct("آيس تي خوخ", 17);
        }

        private void IcedMatcha_Click(object sender, EventArgs e)
        {
            AddProduct("آيس ماتشا", 17);
        }

        private void OreoMilkshake_Click(object sender, EventArgs e)
        {
            AddProduct("ملك تشيك أوريو", 18);
        }

        private void StrawberryMilkshake_Click(object sender, EventArgs e)
        {
            AddProduct("ملك تشيك فراولة", 18);
        }

        private void CaramelMacchiato_Click(object sender, EventArgs e)
        {
            AddProduct("كراميل ميكاتو", 16);
        }

        private void IcedChocolate_Click(object sender, EventArgs e)
        {
            AddProduct("شوكولاتة باردة", 17);
        }

        private void VanillaMilkshake_Click(object sender, EventArgs e)
        {
            AddProduct("ملك تشيك فانيلا", 18);
        }

        private void IcedMocha_Click(object sender, EventArgs e)
        {
            AddProduct("آيس موكا", 18);
        }

        private void IcedPunchLatte_Click(object sender, EventArgs e)
        {
            AddProduct("بنش لاتيه بارد", 17);
        }



        private void RedTea_Click(object sender, EventArgs e)
        {
            AddProduct("شاي أحمر", 5);
        }

        private void Espresso_Click(object sender, EventArgs e)
        {
            AddProduct("اسبرسو", 8);
        }

        private void SmallTurkishCoffee_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة تركي صغير", 8);
        }

        private void Americano_Click(object sender, EventArgs e)
        {
            AddProduct("أمريكانو", 10);
        }

        private void Cortage_Click(object sender, EventArgs e)
        {
            AddProduct("كورتاج", 12);
        }



        private void Cappuccino_Click(object sender, EventArgs e)
        {
            AddProduct("كابتشينو", 12);
        }

        private void Micato_Click(object sender, EventArgs e)
        {
            AddProduct("ميكاتو", 12);
        }

        private void latte_Click(object sender, EventArgs e)
        {
            AddProduct("لاتيه", 13);
        }

        private void HotMocha_Click(object sender, EventArgs e)
        {
            AddProduct("هوت موكا", 14);
        }

        private void FlatWhite_Click(object sender, EventArgs e)
        {
            AddProduct("فلات وايت", 14);
        }

        private void HotMatcha_Click(object sender, EventArgs e)
        {
            AddProduct("هوت ماتشا", 15);
        }

        private void Chocolate_Click(object sender, EventArgs e)
        {
            AddProduct("شوكلاته", 15);
        }

        private void SensationLatte_Click(object sender, EventArgs e)
        {
            AddProduct("سينشن لاتيه", 15);
        }

        private void ItalianCoffee_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة ايطالي", 15);
        }

        private void FrenchCoffee_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة فرنسي", 15);
        }

        private void TeaThermos_Click(object sender, EventArgs e)
        {
            AddProduct("ترمس شاي", 18);
        }

        private void TeaKettle_Click(object sender, EventArgs e)
        {
            AddProduct("براد شاي", 20);
        }

        private void KarakTea_Click(object sender, EventArgs e)
        {
            AddProduct("شاي كرك", 20);
        }

        private void ArabicCoffeewithDates_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة عربي مع التمر", 25);
        }

        private void SaudiCoffeePot_Click(object sender, EventArgs e)
        {
            AddProduct("دلة قهوة سعودي", 30);
        }

        private void LargeTurkishCoffee_Click_1(object sender, EventArgs e)
        {
            AddProduct("قهوة تركي كبير", 12);
        }

        private void DripCoffee_Click(object sender, EventArgs e)
        {
            Dessertspanel.Visible = false;
            Mojitopanel.Visible = false;
            panelHotDrinks.Visible = false;
            panel3.Visible = false;
            DripCoffeePanel.Visible = true;
        }

        private void CoffeeoftheDayBox_Click(object sender, EventArgs e)
        {
            AddProduct("بوكس قهوة اليوم", 45);
        }

        private void IceDrip_Click(object sender, EventArgs e)
        {
            AddProduct("ايس دريب", 17);
        }

        private void V60Hot_Click(object sender, EventArgs e)
        {
            AddProduct("V60 حار", 16);
        }

        private void CoffeeoftheDayIced_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة اليوم بارد", 10);
        }

        private void CoffeeoftheDayHot_Click(object sender, EventArgs e)
        {
            AddProduct("قهوة اليوم حار", 7);
        }

        private void FreshJuice_Click(object sender, EventArgs e)
        {
            AddProduct("عصير", 15);
        }

        private void CodeRed_Click(object sender, EventArgs e)
        {
            AddProduct("كودرد", 14);
        }

        private void SevenUpMojito_Click(object sender, EventArgs e)
        {
            AddProduct("موهيتو سفن أب", 13);
        }

        private void Water_Click(object sender, EventArgs e)
        {
            AddProduct("ماء", 1);
        }

        private void Mojito_Click(object sender, EventArgs e)
        {
            panelHotDrinks.Visible = false;
            panel3.Visible = false;
            DripCoffeePanel.Visible = false;
            Dessertspanel.Visible = false;
            Mojitopanel.Visible = true;
        }

        private void Mojitopanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Desserts_Click(object sender, EventArgs e)
        {
            panelHotDrinks.Visible = false;
            panel3.Visible = false;
            DripCoffeePanel.Visible = false;
            Mojitopanel.Visible = false;
            Dessertspanel.Visible = true;
        }

        private void ChocolateBox_Click(object sender, EventArgs e)
        {
            AddProduct("بوكس شوكولاتة", 30);
        }

        private void HibaDatesCakeLargeChocolate_Click(object sender, EventArgs e)
        {
            AddProduct("هبة دبس كبير شوكولاتة", 28);
        }

        private void HibaDatesCakeSmallChocolate_Click(object sender, EventArgs e)
        {
            AddProduct("هبة دبس صغير شوكولاتة", 18);
        }

        private void SanSebastian_Click(object sender, EventArgs e)
        {
            AddProduct("سان سبستيان", 17);
        }

        private void ChocolateCake_Click(object sender, EventArgs e)
        {
            AddProduct("شوكليت كيك", 15);
        }

        private void Sandwich_Click(object sender, EventArgs e)
        {
            AddProduct("سندوتش", 15);
        }

        private void GlazedCake_Click(object sender, EventArgs e)
        {
            AddProduct("كيك جلز", 12);
        }

        private void TripleChocolateCake_Click(object sender, EventArgs e)
        {
            AddProduct("تربل شوكلت", 12);
        }

        private void RedVelvetCake_Click(object sender, EventArgs e)
        {
            AddProduct("رد فلفت", 12);
        }

        private void ChocolateCookies_Click(object sender, EventArgs e)
        {
            AddProduct("كوكيز شوكلاته", 10);
        }

        private void Cookies_Click(object sender, EventArgs e)
        {
            AddProduct("كوكيز", 10);
        }

        private void MiniCookies_Click(object sender, EventArgs e)
        {
            AddProduct("كوكيز صغير", 8);
        }

        private void Croissant_Click(object sender, EventArgs e)
        {
            AddProduct("كروسون", 8);
        }

        private void LemonCake_Click(object sender, EventArgs e)
        {
            AddProduct("كيك ليمون", 6);
        }

        private void buttonSuspendInvoice_Click(object sender, EventArgs e)
        {
            SuspendInvoice();
        }

        private void buttonResumeInvoice_Click(object sender, EventArgs e)
        {
            ResumeSuspendedInvoice();
        }

        private void menuAdmin_Click(object sender, EventArgs e)
        {

        }

        private void menuReports_Click(object sender, EventArgs e)
        {

            if (_currentRole != UserRole.Admin)
            {
                MessageBox.Show("هذه الخاصية متاحة للأدمن فقط.",
                    "صلاحيات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new ReportsForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
               "هل تريد تسجيل الخروج؟",
               "تأكيد",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var login = new LoginForm();
                login.Show();
            }
        }




        private void buttonManageUsers_Click(object sender, EventArgs e)
        {

            // تأكد أنه أدمن فقط
            if (_currentRole != UserRole.Admin)
            {
                MessageBox.Show("هذه الشاشة متاحة للمدير فقط.",
                    "صلاحيات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new UserManagementForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void buttonReports_Click(object sender, EventArgs e)
        {
            using (var frm = new ReportsForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            using (var frm = new SettingsForm())
            {
                frm.ShowDialog();
            }

        }
    }

    // 🧩 دوال مساعدة لرسم المستطيلات المنحنية
    public static class GraphicsExtensions
    {
        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using (var path = GetRoundedRectPath(rect, radius))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using (var path = GetRoundedRectPath(rect, radius))
            {
                g.FillPath(brush, path);
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rect.Location, size);

            // الزوايا الأربع
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }


    }

    // 🧩 تعريف المنتج
    //public class Product
    //{
    //    public string Name { get; set; }
    //    public int Price { get; set; }
    //    public int Quantity { get; set; }

    //    public int Total => Price * Quantity;
    //}
    //public class Invoice
    //{
    //    public int Id { get; set; }             // رقم الفاتورة
    //    public DateTime Time { get; set; }      // وقت التعليق
    //    public List<Product> Items { get; set; } // الأصناف
    //    public decimal Total { get; set; }      // إجمالي الفاتورة
    //}
    //public enum UserRole
    //{
    //    Admin,
    //    Employee
    //}


}

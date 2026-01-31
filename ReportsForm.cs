using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Customer
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {

            // نقرأ الفواتير من AppDataStore
            var list = AppDataStore.Current.Invoices
             .Concat(AppDataStore.Current.CanceledInvoices)   // 👈 دمج الفواتير الملغاة
             .OrderBy(i => i.Number)
             .Select(i => new
             {
                 i.Number,
                 Date = i.Date.ToShortDateString(),
                 Time = i.Date.ToString("HH:mm"),
                 i.UserName,

                 // ترجمة الدور
                 RoleText = i.Role == UserRole.Admin ? "مسؤول" : "موظف",

                 ItemsCount = i.ItemsCount,     // عدد الأصناف
                 i.SubTotal,
                 i.Tax,
                 i.GrandTotal,

                 // ترجمة الحالة (مدفوعة / ملغاة / معلّقة)
                 StatusText = i.Status == InvoiceStatus.Paid
                                 ? "مدفوعة"
                             : i.Status == InvoiceStatus.Canceled
                                 ? "ملغاة"
                             : i.Status == InvoiceStatus.Suspended
                                 ? "معلّقة"
                                 : ""
             })
             .ToList();

            dataGridViewReports.AutoGenerateColumns = true;
            dataGridViewReports.DataSource = list;

            // عناوين الأعمدة
            dataGridViewReports.Columns["Number"].HeaderText = "رقم الفاتورة";
            dataGridViewReports.Columns["Date"].HeaderText = "التاريخ";
            dataGridViewReports.Columns["Time"].HeaderText = "الوقت";
            dataGridViewReports.Columns["UserName"].HeaderText = "المستخدم";
            dataGridViewReports.Columns["RoleText"].HeaderText = "الدور";
            dataGridViewReports.Columns["ItemsCount"].HeaderText = "عدد الأصناف";
            dataGridViewReports.Columns["SubTotal"].HeaderText = "المجموع قبل الضريبة";
            dataGridViewReports.Columns["Tax"].HeaderText = "الضريبة";
            dataGridViewReports.Columns["GrandTotal"].HeaderText = "الإجمالي";
            dataGridViewReports.Columns["StatusText"].HeaderText = "الحالة";
        }

        // ✅ ترجمة حالة الفاتورة (Paid / Suspended / Canceled) للعربي
        private void dataGridViewReports_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewReports.Columns[e.ColumnIndex].DataPropertyName == "Status"
                && e.Value is InvoiceStatus status)
            {
                switch (status)
                {
                    case InvoiceStatus.Paid:
                        e.Value = "مدفوعة";
                        break;
                    case InvoiceStatus.Suspended:
                        e.Value = "معلّقة";
                        break;
                    case InvoiceStatus.Canceled:
                        e.Value = "ملغاة";
                        break;
                    default:
                        e.Value = "";
                        break;
                }

                e.FormattingApplied = true;
            }
           // ترجمة الدور(مسؤول / موظف)
    if (dataGridViewReports.Columns[e.ColumnIndex].DataPropertyName == "RoleText" &&
        e.Value is string role)
            {
                if (role == "Admin")
                    e.Value = "مسؤول";
                else if (role == "Employee")
                    e.Value = "موظف";

                e.FormattingApplied = true;
            }
        }

        private void buttonReprint_Click(object sender, EventArgs e)
        {

            if (dataGridViewReports.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار فاتورة من التقارير.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int number = Convert.ToInt32(
                dataGridViewReports.SelectedRows[0].Cells["Number"].Value);

            // البحث داخل "تاريخ الفواتير" فقط
            var invoice = AppDataStore.Current.InvoicesHistory
                .LastOrDefault(i => i.Number == number); // 👈 آخر نسخة هي الصحيحة

            if (invoice == null)
            {
                MessageBox.Show("تعذر العثور على بيانات الفاتورة.", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تجهيز نص الفاتورة
            StringBuilder invoiceText = new StringBuilder();
            invoiceText.AppendLine("🧾 فاتورة المبيعات");
            invoiceText.AppendLine("-----------------------------");

            foreach (var p in invoice.Items)
            {
                decimal lineTotal = p.Total;
                invoiceText.AppendLine($"{p.Name,-12} x{p.Quantity} = {lineTotal} ريال");
            }

            invoiceText.AppendLine("-----------------------------");
            invoiceText.AppendLine($"الإجمالي قبل الضريبة: {invoice.SubTotal:0.00} ريال");
            invoiceText.AppendLine($"الضريبة 15%: {invoice.Tax:0.00} ريال");
            invoiceText.AppendLine($"الإجمالي الكلي: {invoice.GrandTotal:0.00} ريال");
            invoiceText.AppendLine($"التاريخ: {invoice.Date:yyyy/MM/dd  hh:mm tt}");
            invoiceText.AppendLine($"رقم الفاتورة: {invoice.Number}");

            MessageBox.Show(invoiceText.ToString(), "إعادة طباعة الفاتورة",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

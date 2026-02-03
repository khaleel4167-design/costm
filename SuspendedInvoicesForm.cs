using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Customer
{
    public partial class SuspendedInvoicesForm : Form
    {
        // قائمة الفواتير المعلّقة القادمة من شاشة الكاشير
        private readonly List<InvoiceRecord> _invoices = new List<InvoiceRecord>();

        // الفاتورة التي يختارها المستخدم لاسترجاعها
        public InvoiceRecord SelectedInvoice { get; private set; }

        public SuspendedInvoicesForm(List<InvoiceRecord> invoices)
        {
            InitializeComponent();

            // تحميل الفواتير المعلقة من قاعدة البيانات
            LoadSuspendedInvoicesFromDatabase();

            InitializeGrid();
            LoadInvoices();
        }

        private void LoadSuspendedInvoicesFromDatabase()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // تحميل الفواتير المعلقة فقط
                    var suspendedInvoices = db.Invoices
                        .Where(i => i.Status == InvoiceStatus.Suspended)
                        .OrderByDescending(i => i.Date)
                        .ToList();

                    _invoices.Clear();
                    _invoices.AddRange(suspendedInvoices);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل الفواتير المعلقة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SuspendedInvoicesForm_Load(object sender, EventArgs e)
        {
            // تقدر تتركه فاضي، أو لو حاب:
            // InitializeGrid();
            // LoadInvoices();
        }


        // إعداد الـ DataGridView
        private void InitializeGrid()
        {
            dataGridViewInvoices.AutoGenerateColumns = false;
            dataGridViewInvoices.AllowUserToAddRows = false;
            dataGridViewInvoices.ReadOnly = true;
            dataGridViewInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInvoices.MultiSelect = false;

            dataGridViewInvoices.Columns.Clear();

            // رقم الفاتورة
            var colNumber = new DataGridViewTextBoxColumn
            {
                Name = "Number",
                HeaderText = "رقم الفاتورة",
                DataPropertyName = "Number",
                Width = 80
            };
            dataGridViewInvoices.Columns.Add(colNumber);

            // التاريخ والوقت
            var colTime = new DataGridViewTextBoxColumn
            {
                Name = "Time",
                HeaderText = "التاريخ / الوقت",
                DataPropertyName = "Time",
                Width = 150
            };
            dataGridViewInvoices.Columns.Add(colTime);

            // عدد الأصناف
            var colCount = new DataGridViewTextBoxColumn
            {
                Name = "ItemsCount",
                HeaderText = "عدد الأصناف",
                DataPropertyName = "ItemsCount",
                Width = 100
            };
            dataGridViewInvoices.Columns.Add(colCount);

            // الإجمالي
            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "الإجمالي",
                DataPropertyName = "Total",
                Width = 100
            };
            dataGridViewInvoices.Columns.Add(colTotal);
        }

        // تحميل الفواتير في الجدول
        private void LoadInvoices()
        {
            var data = _invoices.Select(i => new
            {
                Number = i.Number,
                Time = i.Date.ToString("yyyy/MM/dd  HH:mm"),
                ItemsCount = i.Items.Sum(p => p.Quantity),
                Total = i.GrandTotal.ToString("0.00")
            }).ToList();

            dataGridViewInvoices.DataSource = data;
        }

        // زر استرجاع الفاتورة
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (dataGridViewInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار فاتورة من القائمة.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int number = Convert.ToInt32(
     dataGridViewInvoices.SelectedRows[0].Cells["Number"].Value);

            SelectedInvoice = _invoices.FirstOrDefault(i => i.Number == number);

            if (SelectedInvoice == null)
            {
                MessageBox.Show("حدث خطأ في استرجاع الفاتورة.", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // زر إلغاء
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // دبل كليك على أي خلية = استرجاع الفاتورة
        private void dataGridViewInvoices_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                buttonOk_Click(sender, EventArgs.Empty);
            }
        }
    }
}

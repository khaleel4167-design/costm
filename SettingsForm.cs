using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace Customer
{
    public partial class SettingsForm : Form
    {
        private string _logoBase64 = "";

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtVatRate_TextChanged(object sender, EventArgs e)
        {

        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        // --------------- تحميل الإعدادات عند فتح الفورم ---------------
        private void LoadSettings()
        {
            labelPrinterName.Text = string.IsNullOrEmpty(AppDataStore.Current.PrinterName)
                ? "لم يتم اختيار طابعة"
                : AppDataStore.Current.PrinterName;

            txtStoreName.Text = AppDataStore.Current.StoreName;
            txtOwnerName.Text = AppDataStore.Current.OwnerName;

            txtVatRate.Text = (AppDataStore.Current.VatRate * 100).ToString(); // 0.15 → 15
            txtFooterMessage.Text = AppDataStore.Current.FooterMessage;

            txtInvoiceStart.Text = AppDataStore.Current.LastInvoiceNumber.ToString();

            _logoBase64 = AppDataStore.Current.StoreLogoBase64;

            if (!string.IsNullOrEmpty(_logoBase64))
            {
                pictureLogo.Image = Base64ToImage(_logoBase64);
            }
        }


        // --------------- زر حفظ الإعدادات ---------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AppDataStore.Current.StoreName = txtStoreName.Text;
                AppDataStore.Current.OwnerName = txtOwnerName.Text;

                // نسبة الضريبة
                if (decimal.TryParse(txtVatRate.Text, out decimal vat))
                    AppDataStore.Current.VatRate = vat / 100;

                // رسالة أسفل الفاتورة
                AppDataStore.Current.FooterMessage = txtFooterMessage.Text;

                // رقم بداية الفاتورة
                if (int.TryParse(txtInvoiceStart.Text, out int invStart))
                    AppDataStore.Current.LastInvoiceNumber = invStart;

                // حفظ اللوغو
                AppDataStore.Current.StoreLogoBase64 = _logoBase64;

                AppDataStore.Save();
                MessageBox.Show("تم حفظ JSON", "✔", MessageBoxButtons.OK);

                MessageBox.Show("✔ تم حفظ الإعدادات بنجاح",
                    "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الحفظ:\n" + ex.Message);
            }
        }


        // --------------- زر رفع الشعار (لوغو) ---------------
        private void btnUploadLogo_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(dlg.FileName);
                pictureLogo.Image = img;

                _logoBase64 = ImageToBase64(img);
            }
        }


        // --------------- تحويل صورة ← Base64 ---------------
        private string ImageToBase64(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bytes = ms.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

        // --------------- تحويل Base64 ← صورة ---------------
        private Image Base64ToImage(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void btnSelectPrinter_Click(object sender, EventArgs e)
        {   PrintDialog dlg = new PrintDialog();

            // لو فيه طابعة محفوظة من قبل نحطها افتراضياً
            if (!string.IsNullOrEmpty(AppDataStore.Current.PrinterName))
                dlg.PrinterSettings.PrinterName = AppDataStore.Current.PrinterName;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // حفظ اسم الطابعة
                AppDataStore.Current.PrinterName = dlg.PrinterSettings.PrinterName;
                AppDataStore.Save();

                MessageBox.Show("تم اختيار الطابعة: " + AppDataStore.Current.PrinterName);

                // عرض الاسم في الليبل
                labelPrinterName.Text = AppDataStore.Current.PrinterName;
            }
        }

    }


}

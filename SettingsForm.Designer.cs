namespace Customer
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtFooterMessage = new System.Windows.Forms.TextBox();
            this.txtInvoiceStart = new System.Windows.Forms.TextBox();
            this.txtVatRate = new System.Windows.Forms.TextBox();
            this.txtOwnerName = new System.Windows.Forms.TextBox();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPrinting = new System.Windows.Forms.TabPage();
            this.chkEnableLogo = new System.Windows.Forms.CheckBox();
            this.btnUploadLogo = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.chkTouchMode = new System.Windows.Forms.CheckBox();
            this.btnSelectPrinter = new System.Windows.Forms.Button();
            this.labelPrinterName = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabPrinting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.tabSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabPrinting);
            this.tabControl1.Controls.Add(this.tabSystem);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1419, 614);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.AccessibleName = "";
            this.tabGeneral.Controls.Add(this.txtFooterMessage);
            this.tabGeneral.Controls.Add(this.txtInvoiceStart);
            this.tabGeneral.Controls.Add(this.txtVatRate);
            this.tabGeneral.Controls.Add(this.txtOwnerName);
            this.tabGeneral.Controls.Add(this.txtStoreName);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 29);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(1411, 581);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtFooterMessage
            // 
            this.txtFooterMessage.Location = new System.Drawing.Point(715, 282);
            this.txtFooterMessage.Multiline = true;
            this.txtFooterMessage.Name = "txtFooterMessage";
            this.txtFooterMessage.Size = new System.Drawing.Size(251, 48);
            this.txtFooterMessage.TabIndex = 1;
            this.txtFooterMessage.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtInvoiceStart
            // 
            this.txtInvoiceStart.Location = new System.Drawing.Point(715, 226);
            this.txtInvoiceStart.Name = "txtInvoiceStart";
            this.txtInvoiceStart.Size = new System.Drawing.Size(251, 26);
            this.txtInvoiceStart.TabIndex = 1;
            this.txtInvoiceStart.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtVatRate
            // 
            this.txtVatRate.Location = new System.Drawing.Point(715, 165);
            this.txtVatRate.Name = "txtVatRate";
            this.txtVatRate.Size = new System.Drawing.Size(251, 26);
            this.txtVatRate.TabIndex = 1;
            this.txtVatRate.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Location = new System.Drawing.Point(715, 100);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(251, 26);
            this.txtOwnerName.TabIndex = 1;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(715, 28);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(251, 26);
            this.txtStoreName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(987, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = " : رسالة أسفل الفاتورة";
            this.label5.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(987, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 32);
            this.label4.TabIndex = 0;
            this.label4.Text = " : رقم بدء الفاتورة";
            this.label4.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(987, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = " : VAT الضريبة ";
            this.label3.Click += new System.EventHandler(this.label2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(987, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = " : اسم صاحب المتجر";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(987, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = " : اسم المتجر";
            // 
            // tabPrinting
            // 
            this.tabPrinting.Controls.Add(this.labelPrinterName);
            this.tabPrinting.Controls.Add(this.btnSelectPrinter);
            this.tabPrinting.Controls.Add(this.chkEnableLogo);
            this.tabPrinting.Controls.Add(this.btnUploadLogo);
            this.tabPrinting.Controls.Add(this.pictureLogo);
            this.tabPrinting.Controls.Add(this.cmbPrinters);
            this.tabPrinting.Controls.Add(this.label7);
            this.tabPrinting.Controls.Add(this.label6);
            this.tabPrinting.Location = new System.Drawing.Point(4, 29);
            this.tabPrinting.Name = "tabPrinting";
            this.tabPrinting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrinting.Size = new System.Drawing.Size(1411, 581);
            this.tabPrinting.TabIndex = 1;
            this.tabPrinting.Text = "tabPrinting";
            this.tabPrinting.UseVisualStyleBackColor = true;
            // 
            // chkEnableLogo
            // 
            this.chkEnableLogo.AutoSize = true;
            this.chkEnableLogo.Location = new System.Drawing.Point(965, 271);
            this.chkEnableLogo.Name = "chkEnableLogo";
            this.chkEnableLogo.Size = new System.Drawing.Size(100, 24);
            this.chkEnableLogo.TabIndex = 5;
            this.chkEnableLogo.Text = "تفعيل الشعار";
            this.chkEnableLogo.UseVisualStyleBackColor = true;
            // 
            // btnUploadLogo
            // 
            this.btnUploadLogo.Location = new System.Drawing.Point(798, 253);
            this.btnUploadLogo.Name = "btnUploadLogo";
            this.btnUploadLogo.Size = new System.Drawing.Size(139, 59);
            this.btnUploadLogo.TabIndex = 4;
            this.btnUploadLogo.Text = "تحميل الشعار";
            this.btnUploadLogo.UseVisualStyleBackColor = true;
            // 
            // pictureLogo
            // 
            this.pictureLogo.Location = new System.Drawing.Point(798, 120);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(253, 99);
            this.pictureLogo.TabIndex = 3;
            this.pictureLogo.TabStop = false;
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(798, 83);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(261, 28);
            this.cmbPrinters.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1057, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 32);
            this.label7.TabIndex = 1;
            this.label7.Text = " : شعار الفاتورة";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1057, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 32);
            this.label6.TabIndex = 1;
            this.label6.Text = " : الطابعة";
            // 
            // tabSystem
            // 
            this.tabSystem.Controls.Add(this.btnSave);
            this.tabSystem.Controls.Add(this.cmbLanguage);
            this.tabSystem.Controls.Add(this.chkTouchMode);
            this.tabSystem.Location = new System.Drawing.Point(4, 29);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystem.Size = new System.Drawing.Size(1411, 581);
            this.tabSystem.TabIndex = 2;
            this.tabSystem.Text = "tabSystem";
            this.tabSystem.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(608, 402);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(166, 84);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(608, 352);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(253, 28);
            this.cmbLanguage.TabIndex = 8;
            // 
            // chkTouchMode
            // 
            this.chkTouchMode.AutoSize = true;
            this.chkTouchMode.Location = new System.Drawing.Point(658, 278);
            this.chkTouchMode.Name = "chkTouchMode";
            this.chkTouchMode.Size = new System.Drawing.Size(95, 24);
            this.chkTouchMode.TabIndex = 7;
            this.chkTouchMode.Text = "وضع اللمس";
            this.chkTouchMode.UseVisualStyleBackColor = true;
            // 
            // btnSelectPrinter
            // 
            this.btnSelectPrinter.Location = new System.Drawing.Point(539, 159);
            this.btnSelectPrinter.Name = "btnSelectPrinter";
            this.btnSelectPrinter.Size = new System.Drawing.Size(219, 91);
            this.btnSelectPrinter.TabIndex = 6;
            this.btnSelectPrinter.Text = "btnSelectPrinter";
            this.btnSelectPrinter.UseVisualStyleBackColor = true;
            this.btnSelectPrinter.Click += new System.EventHandler(this.btnSelectPrinter_Click);
            // 
            // labelPrinterName
            // 
            this.labelPrinterName.AutoSize = true;
            this.labelPrinterName.Location = new System.Drawing.Point(331, 179);
            this.labelPrinterName.Name = "labelPrinterName";
            this.labelPrinterName.Size = new System.Drawing.Size(130, 20);
            this.labelPrinterName.TabIndex = 7;
            this.labelPrinterName.Text = "labelPrinterName";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 752);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabPrinting.ResumeLayout(false);
            this.tabPrinting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.tabSystem.ResumeLayout(false);
            this.tabSystem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabPrinting;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TextBox txtOwnerName;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVatRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFooterMessage;
        private System.Windows.Forms.TextBox txtInvoiceStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkEnableLogo;
        private System.Windows.Forms.Button btnUploadLogo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.CheckBox chkTouchMode;
        private System.Windows.Forms.Button btnSelectPrinter;
        private System.Windows.Forms.Label labelPrinterName;
    }
}
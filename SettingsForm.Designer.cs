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
            this.labelPrinterName = new System.Windows.Forms.Label();
            this.btnSelectPrinter = new System.Windows.Forms.Button();
            this.chkEnableLogo = new System.Windows.Forms.CheckBox();
            this.btnUploadLogo = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkTouchMode = new System.Windows.Forms.CheckBox();
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
            this.tabControl1.Location = new System.Drawing.Point(11, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1261, 491);
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
            this.tabGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabGeneral.Size = new System.Drawing.Size(1253, 462);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtFooterMessage
            // 
            this.txtFooterMessage.Location = new System.Drawing.Point(636, 226);
            this.txtFooterMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFooterMessage.Multiline = true;
            this.txtFooterMessage.Name = "txtFooterMessage";
            this.txtFooterMessage.Size = new System.Drawing.Size(224, 39);
            this.txtFooterMessage.TabIndex = 1;
            this.txtFooterMessage.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtInvoiceStart
            // 
            this.txtInvoiceStart.Location = new System.Drawing.Point(636, 181);
            this.txtInvoiceStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInvoiceStart.Name = "txtInvoiceStart";
            this.txtInvoiceStart.Size = new System.Drawing.Size(224, 22);
            this.txtInvoiceStart.TabIndex = 1;
            this.txtInvoiceStart.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtVatRate
            // 
            this.txtVatRate.Location = new System.Drawing.Point(636, 132);
            this.txtVatRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVatRate.Name = "txtVatRate";
            this.txtVatRate.Size = new System.Drawing.Size(224, 22);
            this.txtVatRate.TabIndex = 1;
            this.txtVatRate.TextChanged += new System.EventHandler(this.txtVatRate_TextChanged);
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Location = new System.Drawing.Point(636, 80);
            this.txtOwnerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(224, 22);
            this.txtOwnerName.TabIndex = 1;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(636, 22);
            this.txtStoreName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(224, 22);
            this.txtStoreName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(877, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = " : رسالة أسفل الفاتورة";
            this.label5.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(877, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = " : رقم بدء الفاتورة";
            this.label4.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(877, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = " : VAT الضريبة ";
            this.label3.Click += new System.EventHandler(this.label2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(877, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = " : اسم صاحب المتجر";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(877, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 29);
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
            this.tabPrinting.Location = new System.Drawing.Point(4, 25);
            this.tabPrinting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPrinting.Name = "tabPrinting";
            this.tabPrinting.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPrinting.Size = new System.Drawing.Size(1253, 462);
            this.tabPrinting.TabIndex = 1;
            this.tabPrinting.Text = "tabPrinting";
            this.tabPrinting.UseVisualStyleBackColor = true;
            // 
            // labelPrinterName
            // 
            this.labelPrinterName.AutoSize = true;
            this.labelPrinterName.Location = new System.Drawing.Point(294, 143);
            this.labelPrinterName.Name = "labelPrinterName";
            this.labelPrinterName.Size = new System.Drawing.Size(112, 16);
            this.labelPrinterName.TabIndex = 7;
            this.labelPrinterName.Text = "labelPrinterName";
            // 
            // btnSelectPrinter
            // 
            this.btnSelectPrinter.Location = new System.Drawing.Point(479, 127);
            this.btnSelectPrinter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectPrinter.Name = "btnSelectPrinter";
            this.btnSelectPrinter.Size = new System.Drawing.Size(195, 73);
            this.btnSelectPrinter.TabIndex = 6;
            this.btnSelectPrinter.Text = "btnSelectPrinter";
            this.btnSelectPrinter.UseVisualStyleBackColor = true;
            this.btnSelectPrinter.Click += new System.EventHandler(this.btnSelectPrinter_Click);
            // 
            // chkEnableLogo
            // 
            this.chkEnableLogo.AutoSize = true;
            this.chkEnableLogo.Location = new System.Drawing.Point(858, 217);
            this.chkEnableLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkEnableLogo.Name = "chkEnableLogo";
            this.chkEnableLogo.Size = new System.Drawing.Size(90, 20);
            this.chkEnableLogo.TabIndex = 5;
            this.chkEnableLogo.Text = "تفعيل الشعار";
            this.chkEnableLogo.UseVisualStyleBackColor = true;
            // 
            // btnUploadLogo
            // 
            this.btnUploadLogo.Location = new System.Drawing.Point(709, 202);
            this.btnUploadLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadLogo.Name = "btnUploadLogo";
            this.btnUploadLogo.Size = new System.Drawing.Size(124, 47);
            this.btnUploadLogo.TabIndex = 4;
            this.btnUploadLogo.Text = "تحميل الشعار";
            this.btnUploadLogo.UseVisualStyleBackColor = true;
            // 
            // pictureLogo
            // 
            this.pictureLogo.Location = new System.Drawing.Point(709, 96);
            this.pictureLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(225, 79);
            this.pictureLogo.TabIndex = 3;
            this.pictureLogo.TabStop = false;
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(709, 66);
            this.cmbPrinters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(232, 24);
            this.cmbPrinters.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(940, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = " : شعار الفاتورة";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(940, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = " : الطابعة";
            // 
            // tabSystem
            // 
            this.tabSystem.Controls.Add(this.btnSave);
            this.tabSystem.Controls.Add(this.chkTouchMode);
            this.tabSystem.Location = new System.Drawing.Point(4, 25);
            this.tabSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSystem.Size = new System.Drawing.Size(1253, 462);
            this.tabSystem.TabIndex = 2;
            this.tabSystem.Text = "tabSystem";
            this.tabSystem.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(540, 322);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(148, 67);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkTouchMode
            // 
            this.chkTouchMode.AutoSize = true;
            this.chkTouchMode.Location = new System.Drawing.Point(585, 222);
            this.chkTouchMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTouchMode.Name = "chkTouchMode";
            this.chkTouchMode.Size = new System.Drawing.Size(85, 20);
            this.chkTouchMode.TabIndex = 7;
            this.chkTouchMode.Text = "وضع اللمس";
            this.chkTouchMode.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 602);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.CheckBox chkTouchMode;
        private System.Windows.Forms.Button btnSelectPrinter;
        private System.Windows.Forms.Label labelPrinterName;
    }
}
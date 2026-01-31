namespace Customer
{
    partial class ReportsForm
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
            this.dataGridViewReports = new System.Windows.Forms.DataGridView();
            this.buttonReprint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewReports
            // 
            this.dataGridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReports.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewReports.Name = "dataGridViewReports";
            this.dataGridViewReports.RowHeadersWidth = 62;
            this.dataGridViewReports.RowTemplate.Height = 28;
            this.dataGridViewReports.Size = new System.Drawing.Size(1409, 499);
            this.dataGridViewReports.TabIndex = 0;
            this.dataGridViewReports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewReports_CellFormatting);
            // 
            // buttonReprint
            // 
            this.buttonReprint.Location = new System.Drawing.Point(69, 531);
            this.buttonReprint.Name = "buttonReprint";
            this.buttonReprint.Size = new System.Drawing.Size(257, 77);
            this.buttonReprint.TabIndex = 1;
            this.buttonReprint.Text = "طباعة الفاتورة";
            this.buttonReprint.UseVisualStyleBackColor = true;
            this.buttonReprint.Click += new System.EventHandler(this.buttonReprint_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 620);
            this.Controls.Add(this.buttonReprint);
            this.Controls.Add(this.dataGridViewReports);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewReports;
        private System.Windows.Forms.Button buttonReprint;
    }
}
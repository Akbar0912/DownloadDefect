namespace DownloadDefect
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            label4 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnWarranty = new Button();
            btnDefect = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Teal;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1233, 51);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Helvetica", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(610, 51);
            label3.TabIndex = 0;
            label3.Text = "DOWNLOAD DATA PRODUCTION";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Helvetica", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(619, 0);
            label4.Name = "label4";
            label4.Size = new Size(611, 51);
            label4.TabIndex = 1;
            label4.Text = "LAUNDRY SYSTEM BUSINESS UNIT";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(splitContainer1, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 51);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1233, 520);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);
            splitContainer1.Size = new Size(1233, 520);
            splitContainer1.SplitterDistance = 111;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Teal;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(btnWarranty, 0, 1);
            tableLayoutPanel2.Controls.Add(btnDefect, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.69329F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.69329F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.69329F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 49.92013F));
            tableLayoutPanel2.Size = new Size(111, 520);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // btnWarranty
            // 
            btnWarranty.Dock = DockStyle.Fill;
            btnWarranty.FlatAppearance.BorderSize = 0;
            btnWarranty.FlatStyle = FlatStyle.Flat;
            btnWarranty.Font = new Font("Helvetica", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnWarranty.ForeColor = Color.White;
            btnWarranty.Image = (Image)resources.GetObject("btnWarranty.Image");
            btnWarranty.Location = new Point(3, 86);
            btnWarranty.Margin = new Padding(3, 0, 3, 3);
            btnWarranty.Name = "btnWarranty";
            btnWarranty.Size = new Size(105, 83);
            btnWarranty.TabIndex = 4;
            btnWarranty.Text = "\r\nDownload Warranty";
            btnWarranty.TextAlign = ContentAlignment.BottomCenter;
            btnWarranty.TextImageRelation = TextImageRelation.ImageAboveText;
            btnWarranty.UseVisualStyleBackColor = true;
            // 
            // btnDefect
            // 
            btnDefect.BackColor = Color.FromArgb(0, 133, 181);
            btnDefect.Dock = DockStyle.Fill;
            btnDefect.FlatAppearance.BorderSize = 0;
            btnDefect.FlatStyle = FlatStyle.Flat;
            btnDefect.Font = new Font("Helvetica", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDefect.ForeColor = Color.White;
            btnDefect.Image = (Image)resources.GetObject("btnDefect.Image");
            btnDefect.Location = new Point(3, 0);
            btnDefect.Margin = new Padding(3, 0, 3, 3);
            btnDefect.Name = "btnDefect";
            btnDefect.Size = new Size(105, 83);
            btnDefect.TabIndex = 3;
            btnDefect.Text = "\r\nDownload Defect";
            btnDefect.TextAlign = ContentAlignment.BottomCenter;
            btnDefect.TextImageRelation = TextImageRelation.ImageAboveText;
            btnDefect.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1233, 571);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Download Data Defect Line";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label3;
        private Label label4;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnWarranty;
        private Button btnDefect;
    }
}

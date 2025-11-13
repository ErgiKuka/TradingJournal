namespace TradingJournal.Pl.PlaceHolder.Settings
{
    partial class DlgExportOptions
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
            lblExTitle = new Label();
            cbFormat = new ComboBox();
            lblFrom = new Label();
            lblTo = new Label();
            lblFormat = new Label();
            lblSaveTo = new Label();
            gpbExportSources = new GroupBox();
            chkSrcTransactions = new CheckBox();
            chkSrcStatistics = new CheckBox();
            chkSrcRecovery = new CheckBox();
            chkSrcJournal = new CheckBox();
            gpbExportDateRange = new GroupBox();
            dtTo = new DateTimePicker();
            dtFrom = new DateTimePicker();
            chkUseDateRange = new CheckBox();
            txtSavePath = new TextBox();
            btnBrowseSave = new FontAwesome.Sharp.IconButton();
            gpbExportFormat = new GroupBox();
            checkBox1 = new CheckBox();
            gpbExportPath = new GroupBox();
            btnExportRun = new FontAwesome.Sharp.IconButton();
            btnExportCancel = new FontAwesome.Sharp.IconButton();
            progressExport = new ProgressBar();
            saveFileDialog1 = new SaveFileDialog();
            gpbExportSources.SuspendLayout();
            gpbExportDateRange.SuspendLayout();
            gpbExportFormat.SuspendLayout();
            gpbExportPath.SuspendLayout();
            SuspendLayout();
            // 
            // lblExTitle
            // 
            lblExTitle.AutoSize = true;
            lblExTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblExTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblExTitle.Location = new Point(11, 9);
            lblExTitle.Margin = new Padding(2, 0, 2, 0);
            lblExTitle.Name = "lblExTitle";
            lblExTitle.Size = new Size(204, 32);
            lblExTitle.TabIndex = 5;
            lblExTitle.Text = "Export Options";
            // 
            // cbFormat
            // 
            cbFormat.BackColor = Color.FromArgb(30, 58, 95);
            cbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFormat.FlatStyle = FlatStyle.Flat;
            cbFormat.Font = new Font("Times New Roman", 13.8F);
            cbFormat.ForeColor = SystemColors.Window;
            cbFormat.FormattingEnabled = true;
            cbFormat.Items.AddRange(new object[] { "Excel (.xlsx)", "CSV (.csv)", "PDF (.pdf)" });
            cbFormat.Location = new Point(22, 81);
            cbFormat.Margin = new Padding(2);
            cbFormat.Name = "cbFormat";
            cbFormat.Size = new Size(163, 28);
            cbFormat.TabIndex = 30;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Times New Roman", 15.75F);
            lblFrom.ForeColor = Color.FromArgb(156, 163, 175);
            lblFrom.Location = new Point(29, 84);
            lblFrom.Margin = new Padding(2, 0, 2, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(56, 23);
            lblFrom.TabIndex = 31;
            lblFrom.Text = "From";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Times New Roman", 15.75F);
            lblTo.ForeColor = Color.FromArgb(156, 163, 175);
            lblTo.Location = new Point(265, 89);
            lblTo.Margin = new Padding(2, 0, 2, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(32, 23);
            lblTo.TabIndex = 32;
            lblTo.Text = "To";
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Font = new Font("Times New Roman", 15.75F);
            lblFormat.ForeColor = Color.FromArgb(156, 163, 175);
            lblFormat.Location = new Point(22, 41);
            lblFormat.Margin = new Padding(2, 0, 2, 0);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(71, 23);
            lblFormat.TabIndex = 33;
            lblFormat.Text = "Format";
            // 
            // lblSaveTo
            // 
            lblSaveTo.AutoSize = true;
            lblSaveTo.Font = new Font("Times New Roman", 15.75F);
            lblSaveTo.ForeColor = Color.FromArgb(156, 163, 175);
            lblSaveTo.Location = new Point(22, 41);
            lblSaveTo.Margin = new Padding(2, 0, 2, 0);
            lblSaveTo.Name = "lblSaveTo";
            lblSaveTo.Size = new Size(71, 23);
            lblSaveTo.TabIndex = 34;
            lblSaveTo.Text = "Save to";
            // 
            // gpbExportSources
            // 
            gpbExportSources.Controls.Add(chkSrcTransactions);
            gpbExportSources.Controls.Add(chkSrcStatistics);
            gpbExportSources.Controls.Add(chkSrcRecovery);
            gpbExportSources.Controls.Add(chkSrcJournal);
            gpbExportSources.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbExportSources.ForeColor = Color.FromArgb(156, 163, 175);
            gpbExportSources.Location = new Point(23, 56);
            gpbExportSources.Name = "gpbExportSources";
            gpbExportSources.Size = new Size(376, 202);
            gpbExportSources.TabIndex = 0;
            gpbExportSources.TabStop = false;
            gpbExportSources.Text = "Export Sources";
            // 
            // chkSrcTransactions
            // 
            chkSrcTransactions.AutoSize = true;
            chkSrcTransactions.Font = new Font("Times New Roman", 18F);
            chkSrcTransactions.ForeColor = Color.FromArgb(156, 163, 175);
            chkSrcTransactions.Location = new Point(22, 161);
            chkSrcTransactions.Name = "chkSrcTransactions";
            chkSrcTransactions.Size = new Size(240, 31);
            chkSrcTransactions.TabIndex = 35;
            chkSrcTransactions.Text = "Account Transactions";
            chkSrcTransactions.UseVisualStyleBackColor = true;
            // 
            // chkSrcStatistics
            // 
            chkSrcStatistics.AutoSize = true;
            chkSrcStatistics.Font = new Font("Times New Roman", 18F);
            chkSrcStatistics.ForeColor = Color.FromArgb(156, 163, 175);
            chkSrcStatistics.Location = new Point(24, 122);
            chkSrcStatistics.Name = "chkSrcStatistics";
            chkSrcStatistics.Size = new Size(116, 31);
            chkSrcStatistics.TabIndex = 34;
            chkSrcStatistics.Text = "Statistics";
            chkSrcStatistics.UseVisualStyleBackColor = true;
            // 
            // chkSrcRecovery
            // 
            chkSrcRecovery.AutoSize = true;
            chkSrcRecovery.Font = new Font("Times New Roman", 18F);
            chkSrcRecovery.ForeColor = Color.FromArgb(156, 163, 175);
            chkSrcRecovery.Location = new Point(24, 83);
            chkSrcRecovery.Name = "chkSrcRecovery";
            chkSrcRecovery.Size = new Size(336, 31);
            chkSrcRecovery.TabIndex = 33;
            chkSrcRecovery.Text = "Recovery (Cases + Allocations)";
            chkSrcRecovery.UseVisualStyleBackColor = true;
            // 
            // chkSrcJournal
            // 
            chkSrcJournal.AutoSize = true;
            chkSrcJournal.Font = new Font("Times New Roman", 18F);
            chkSrcJournal.ForeColor = Color.FromArgb(156, 163, 175);
            chkSrcJournal.Location = new Point(24, 44);
            chkSrcJournal.Name = "chkSrcJournal";
            chkSrcJournal.Size = new Size(187, 31);
            chkSrcJournal.TabIndex = 32;
            chkSrcJournal.Text = "Journal (Trades)";
            chkSrcJournal.UseVisualStyleBackColor = true;
            // 
            // gpbExportDateRange
            // 
            gpbExportDateRange.Controls.Add(dtTo);
            gpbExportDateRange.Controls.Add(dtFrom);
            gpbExportDateRange.Controls.Add(chkUseDateRange);
            gpbExportDateRange.Controls.Add(lblFrom);
            gpbExportDateRange.Controls.Add(lblTo);
            gpbExportDateRange.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbExportDateRange.ForeColor = Color.FromArgb(156, 163, 175);
            gpbExportDateRange.Location = new Point(427, 56);
            gpbExportDateRange.Name = "gpbExportDateRange";
            gpbExportDateRange.Size = new Size(497, 170);
            gpbExportDateRange.TabIndex = 35;
            gpbExportDateRange.TabStop = false;
            gpbExportDateRange.Text = "Date Range";
            // 
            // dtTo
            // 
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Location = new Point(265, 118);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(144, 32);
            dtTo.TabIndex = 34;
            // 
            // dtFrom
            // 
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Location = new Point(29, 118);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(148, 32);
            dtFrom.TabIndex = 33;
            // 
            // chkUseDateRange
            // 
            chkUseDateRange.AutoSize = true;
            chkUseDateRange.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkUseDateRange.ForeColor = Color.FromArgb(156, 163, 175);
            chkUseDateRange.Location = new Point(29, 35);
            chkUseDateRange.Name = "chkUseDateRange";
            chkUseDateRange.Size = new Size(188, 27);
            chkUseDateRange.TabIndex = 32;
            chkUseDateRange.Text = "Filter by date range";
            chkUseDateRange.UseVisualStyleBackColor = true;
            // 
            // txtSavePath
            // 
            txtSavePath.BackColor = Color.FromArgb(30, 58, 95);
            txtSavePath.BorderStyle = BorderStyle.None;
            txtSavePath.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSavePath.Location = new Point(22, 76);
            txtSavePath.Margin = new Padding(2);
            txtSavePath.Name = "txtSavePath";
            txtSavePath.PlaceholderText = "Place";
            txtSavePath.ReadOnly = true;
            txtSavePath.Size = new Size(207, 32);
            txtSavePath.TabIndex = 40;
            txtSavePath.Text = "   ";
            // 
            // btnBrowseSave
            // 
            btnBrowseSave.BackColor = Color.FromArgb(30, 58, 95);
            btnBrowseSave.FlatAppearance.BorderSize = 0;
            btnBrowseSave.FlatStyle = FlatStyle.Flat;
            btnBrowseSave.Font = new Font("Times New Roman", 12F);
            btnBrowseSave.IconChar = FontAwesome.Sharp.IconChar.None;
            btnBrowseSave.IconColor = Color.Black;
            btnBrowseSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBrowseSave.Location = new Point(29, 136);
            btnBrowseSave.Margin = new Padding(2);
            btnBrowseSave.Name = "btnBrowseSave";
            btnBrowseSave.Size = new Size(148, 43);
            btnBrowseSave.TabIndex = 41;
            btnBrowseSave.Text = "Browse…";
            btnBrowseSave.UseVisualStyleBackColor = false;
            // 
            // gpbExportFormat
            // 
            gpbExportFormat.Controls.Add(checkBox1);
            gpbExportFormat.Controls.Add(lblFormat);
            gpbExportFormat.Controls.Add(cbFormat);
            gpbExportFormat.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbExportFormat.ForeColor = Color.FromArgb(156, 163, 175);
            gpbExportFormat.Location = new Point(23, 264);
            gpbExportFormat.Name = "gpbExportFormat";
            gpbExportFormat.Size = new Size(376, 207);
            gpbExportFormat.TabIndex = 42;
            gpbExportFormat.TabStop = false;
            gpbExportFormat.Text = "Export Format";
            // 
            // checkBox1
            // 
            checkBox1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = Color.FromArgb(156, 163, 175);
            checkBox1.Location = new Point(24, 136);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(310, 53);
            checkBox1.TabIndex = 36;
            checkBox1.Text = "Include computed summaries (Avg Entry, Unrealized P/L)";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // gpbExportPath
            // 
            gpbExportPath.Controls.Add(btnBrowseSave);
            gpbExportPath.Controls.Add(lblSaveTo);
            gpbExportPath.Controls.Add(txtSavePath);
            gpbExportPath.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbExportPath.ForeColor = Color.FromArgb(156, 163, 175);
            gpbExportPath.Location = new Point(427, 264);
            gpbExportPath.Name = "gpbExportPath";
            gpbExportPath.Size = new Size(376, 207);
            gpbExportPath.TabIndex = 43;
            gpbExportPath.TabStop = false;
            gpbExportPath.Text = "Export Path";
            // 
            // btnExportRun
            // 
            btnExportRun.BackColor = Color.FromArgb(30, 58, 95);
            btnExportRun.Enabled = false;
            btnExportRun.FlatAppearance.BorderSize = 0;
            btnExportRun.FlatStyle = FlatStyle.Flat;
            btnExportRun.Font = new Font("Times New Roman", 12F);
            btnExportRun.IconChar = FontAwesome.Sharp.IconChar.None;
            btnExportRun.IconColor = Color.Black;
            btnExportRun.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportRun.Location = new Point(60, 512);
            btnExportRun.Margin = new Padding(2);
            btnExportRun.Name = "btnExportRun";
            btnExportRun.Size = new Size(148, 43);
            btnExportRun.TabIndex = 44;
            btnExportRun.Text = "Export";
            btnExportRun.UseVisualStyleBackColor = false;
            // 
            // btnExportCancel
            // 
            btnExportCancel.BackColor = Color.FromArgb(30, 58, 95);
            btnExportCancel.FlatAppearance.BorderSize = 0;
            btnExportCancel.FlatStyle = FlatStyle.Flat;
            btnExportCancel.Font = new Font("Times New Roman", 12F);
            btnExportCancel.IconChar = FontAwesome.Sharp.IconChar.None;
            btnExportCancel.IconColor = Color.Black;
            btnExportCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportCancel.Location = new Point(403, 581);
            btnExportCancel.Margin = new Padding(2);
            btnExportCancel.Name = "btnExportCancel";
            btnExportCancel.Size = new Size(148, 43);
            btnExportCancel.TabIndex = 45;
            btnExportCancel.Text = "Cancel";
            btnExportCancel.UseVisualStyleBackColor = false;
            // 
            // progressExport
            // 
            progressExport.Location = new Point(61, 581);
            progressExport.Name = "progressExport";
            progressExport.Size = new Size(322, 42);
            progressExport.TabIndex = 46;
            // 
            // DlgExportOptions
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(960, 641);
            Controls.Add(progressExport);
            Controls.Add(btnExportCancel);
            Controls.Add(btnExportRun);
            Controls.Add(gpbExportPath);
            Controls.Add(gpbExportFormat);
            Controls.Add(gpbExportDateRange);
            Controls.Add(gpbExportSources);
            Controls.Add(lblExTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "DlgExportOptions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Export";
            gpbExportSources.ResumeLayout(false);
            gpbExportSources.PerformLayout();
            gpbExportDateRange.ResumeLayout(false);
            gpbExportDateRange.PerformLayout();
            gpbExportFormat.ResumeLayout(false);
            gpbExportFormat.PerformLayout();
            gpbExportPath.ResumeLayout(false);
            gpbExportPath.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblExTitle;
        private ComboBox chkSrcRec;
        private ComboBox chkSrcStat;
        private ComboBox cbFormat;
        private Label lblFrom;
        private Label lblTo;
        private Label lblFormat;
        private Label lblSaveTo;
        private GroupBox gpbExportSources;
        private CheckBox chkSrcStatistics;
        private CheckBox chkSrcRecovery;
        private CheckBox chkSrcJournal;
        private GroupBox gpbExportDateRange;
        private CheckBox checkBox2;
        private CheckBox chkUseDateRange;
        private DateTimePicker dtTo;
        private DateTimePicker dtFrom;
        private TextBox txtSavePath;
        private FontAwesome.Sharp.IconButton btnBrowseSave;
        private GroupBox gpbExportFormat;
        private GroupBox gpbExportPath;
        private FontAwesome.Sharp.IconButton btnExportRun;
        private FontAwesome.Sharp.IconButton btnExportCancel;
        private ProgressBar progressExport;
        private CheckBox chkSrcTransactions;
        private SaveFileDialog saveFileDialog1;
        private CheckBox checkBox1;
    }
}
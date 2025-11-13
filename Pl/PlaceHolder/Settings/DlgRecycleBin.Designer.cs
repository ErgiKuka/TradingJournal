namespace TradingJournal.Pl.PlaceHolder.Settings
{
    partial class DlgRecycleBin
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
            panel1 = new Panel();
            pnlBottom = new Panel();
            lblCount = new Label();
            lblRetention = new Label();
            pnlTop = new Panel();
            lblSearch = new Label();
            chkUseDateRange = new CheckBox();
            btnEmptyBin = new FontAwesome.Sharp.IconButton();
            btnPurgeExpired = new FontAwesome.Sharp.IconButton();
            btnRefresh = new FontAwesome.Sharp.IconButton();
            chkShowExpired = new CheckBox();
            dtpTo = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            lblFrom = new Label();
            lblTo = new Label();
            txtSearch = new TextBox();
            cbType = new ComboBox();
            lblFilters = new Label();
            lblHint = new Label();
            lblTitle = new Label();
            dgvBin = new DataGridView();
            panel1.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBin).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pnlBottom);
            panel1.Controls.Add(pnlTop);
            panel1.Controls.Add(dgvBin);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(972, 865);
            panel1.TabIndex = 0;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(27, 38, 59);
            pnlBottom.Controls.Add(lblCount);
            pnlBottom.Controls.Add(lblRetention);
            pnlBottom.Location = new Point(13, 741);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(947, 113);
            pnlBottom.TabIndex = 85;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Times New Roman", 15.75F);
            lblCount.ForeColor = Color.FromArgb(156, 163, 175);
            lblCount.Location = new Point(21, 67);
            lblCount.Margin = new Padding(2, 0, 2, 0);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(184, 23);
            lblCount.TabIndex = 87;
            lblCount.Text = "Items: N (M expired)";
            // 
            // lblRetention
            // 
            lblRetention.AutoSize = true;
            lblRetention.Font = new Font("Times New Roman", 15.75F);
            lblRetention.ForeColor = Color.FromArgb(156, 163, 175);
            lblRetention.Location = new Point(21, 19);
            lblRetention.Margin = new Padding(2, 0, 2, 0);
            lblRetention.Name = "lblRetention";
            lblRetention.Size = new Size(242, 23);
            lblRetention.TabIndex = 86;
            lblRetention.Text = "Retention: X days (Settings)";
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(27, 38, 59);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Controls.Add(chkUseDateRange);
            pnlTop.Controls.Add(btnEmptyBin);
            pnlTop.Controls.Add(btnPurgeExpired);
            pnlTop.Controls.Add(btnRefresh);
            pnlTop.Controls.Add(chkShowExpired);
            pnlTop.Controls.Add(dtpTo);
            pnlTop.Controls.Add(dtpFrom);
            pnlTop.Controls.Add(lblFrom);
            pnlTop.Controls.Add(lblTo);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(cbType);
            pnlTop.Controls.Add(lblFilters);
            pnlTop.Controls.Add(lblHint);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Location = new Point(13, 12);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(947, 369);
            pnlTop.TabIndex = 84;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Times New Roman", 15.75F);
            lblSearch.ForeColor = Color.FromArgb(156, 163, 175);
            lblSearch.Location = new Point(31, 174);
            lblSearch.Margin = new Padding(2, 0, 2, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(65, 23);
            lblSearch.TabIndex = 97;
            lblSearch.Text = "Search";
            // 
            // chkUseDateRange
            // 
            chkUseDateRange.AutoSize = true;
            chkUseDateRange.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkUseDateRange.ForeColor = Color.FromArgb(156, 163, 175);
            chkUseDateRange.Location = new Point(444, 111);
            chkUseDateRange.Name = "chkUseDateRange";
            chkUseDateRange.Size = new Size(188, 27);
            chkUseDateRange.TabIndex = 96;
            chkUseDateRange.Text = "Filter by date range";
            chkUseDateRange.UseVisualStyleBackColor = true;
            // 
            // btnEmptyBin
            // 
            btnEmptyBin.BackColor = Color.FromArgb(30, 58, 95);
            btnEmptyBin.FlatAppearance.BorderSize = 0;
            btnEmptyBin.FlatStyle = FlatStyle.Flat;
            btnEmptyBin.Font = new Font("Times New Roman", 12F);
            btnEmptyBin.IconChar = FontAwesome.Sharp.IconChar.None;
            btnEmptyBin.IconColor = Color.Black;
            btnEmptyBin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEmptyBin.Location = new Point(524, 309);
            btnEmptyBin.Margin = new Padding(2);
            btnEmptyBin.Name = "btnEmptyBin";
            btnEmptyBin.Size = new Size(148, 43);
            btnEmptyBin.TabIndex = 95;
            btnEmptyBin.Text = "Empty Bin";
            btnEmptyBin.UseVisualStyleBackColor = false;
            // 
            // btnPurgeExpired
            // 
            btnPurgeExpired.BackColor = Color.FromArgb(30, 58, 95);
            btnPurgeExpired.FlatAppearance.BorderSize = 0;
            btnPurgeExpired.FlatStyle = FlatStyle.Flat;
            btnPurgeExpired.Font = new Font("Times New Roman", 12F);
            btnPurgeExpired.IconChar = FontAwesome.Sharp.IconChar.None;
            btnPurgeExpired.IconColor = Color.Black;
            btnPurgeExpired.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPurgeExpired.Location = new Point(752, 309);
            btnPurgeExpired.Margin = new Padding(2);
            btnPurgeExpired.Name = "btnPurgeExpired";
            btnPurgeExpired.Size = new Size(148, 43);
            btnPurgeExpired.TabIndex = 94;
            btnPurgeExpired.Text = "Purge Expired";
            btnPurgeExpired.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(30, 58, 95);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Times New Roman", 12F);
            btnRefresh.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRefresh.IconColor = Color.Black;
            btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefresh.Location = new Point(48, 309);
            btnRefresh.Margin = new Padding(2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(148, 43);
            btnRefresh.TabIndex = 93;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // chkShowExpired
            // 
            chkShowExpired.AutoSize = true;
            chkShowExpired.Font = new Font("Times New Roman", 18F);
            chkShowExpired.ForeColor = Color.FromArgb(156, 163, 175);
            chkShowExpired.Location = new Point(34, 252);
            chkShowExpired.Name = "chkShowExpired";
            chkShowExpired.Size = new Size(162, 31);
            chkShowExpired.TabIndex = 92;
            chkShowExpired.Text = "Show expired";
            chkShowExpired.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            dtpTo.Font = new Font("Times New Roman", 15.75F);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(680, 186);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(144, 32);
            dtpTo.TabIndex = 91;
            // 
            // dtpFrom
            // 
            dtpFrom.Font = new Font("Times New Roman", 15.75F);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(444, 186);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(148, 32);
            dtpFrom.TabIndex = 90;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Times New Roman", 15.75F);
            lblFrom.ForeColor = Color.FromArgb(156, 163, 175);
            lblFrom.Location = new Point(444, 152);
            lblFrom.Margin = new Padding(2, 0, 2, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(56, 23);
            lblFrom.TabIndex = 88;
            lblFrom.Text = "From";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Times New Roman", 15.75F);
            lblTo.ForeColor = Color.FromArgb(156, 163, 175);
            lblTo.Location = new Point(680, 157);
            lblTo.Margin = new Padding(2, 0, 2, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(32, 23);
            lblTo.TabIndex = 89;
            lblTo.Text = "To";
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(30, 58, 95);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(31, 199);
            txtSearch.Margin = new Padding(2);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search preview or source…";
            txtSearch.Size = new Size(207, 32);
            txtSearch.TabIndex = 87;
            txtSearch.Text = "   ";
            // 
            // cbType
            // 
            cbType.BackColor = Color.FromArgb(30, 58, 95);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FlatStyle = FlatStyle.Flat;
            cbType.Font = new Font("Times New Roman", 13.8F);
            cbType.ForeColor = SystemColors.Window;
            cbType.FormattingEnabled = true;
            cbType.Items.AddRange(new object[] { "All", "AccountTransaction", "Journal", "RecoveryCase", "Allocation" });
            cbType.Location = new Point(31, 125);
            cbType.Margin = new Padding(2);
            cbType.Name = "cbType";
            cbType.Size = new Size(163, 28);
            cbType.TabIndex = 86;
            // 
            // lblFilters
            // 
            lblFilters.AutoSize = true;
            lblFilters.Font = new Font("Times New Roman", 15.75F);
            lblFilters.ForeColor = Color.FromArgb(156, 163, 175);
            lblFilters.Location = new Point(31, 100);
            lblFilters.Margin = new Padding(2, 0, 2, 0);
            lblFilters.Name = "lblFilters";
            lblFilters.Size = new Size(64, 23);
            lblFilters.TabIndex = 85;
            lblFilters.Text = "Filters";
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Times New Roman", 15.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblHint.ForeColor = Color.FromArgb(156, 163, 175);
            lblHint.Location = new Point(21, 57);
            lblHint.Margin = new Padding(2, 0, 2, 0);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(813, 23);
            lblHint.TabIndex = 84;
            lblHint.Text = "Items deleted in the app stay here until they expire. You can restore or permanently delete them.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTitle.Location = new Point(11, 9);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(160, 32);
            lblTitle.TabIndex = 83;
            lblTitle.Text = "Recycle Bin";
            // 
            // dgvBin
            // 
            dgvBin.AllowUserToAddRows = false;
            dgvBin.AllowUserToDeleteRows = false;
            dgvBin.AllowUserToResizeColumns = false;
            dgvBin.AllowUserToResizeRows = false;
            dgvBin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBin.Location = new Point(12, 406);
            dgvBin.Name = "dgvBin";
            dgvBin.ReadOnly = true;
            dgvBin.Size = new Size(948, 314);
            dgvBin.TabIndex = 83;
            // 
            // DlgRecycleBin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(972, 865);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "DlgRecycleBin";
            Text = "Recycle Bin";
            panel1.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBin).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvBin;
        private Panel pnlTop;
        private FontAwesome.Sharp.IconButton btnEmptyBin;
        private FontAwesome.Sharp.IconButton btnPurgeExpired;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private CheckBox chkShowExpired;
        private DateTimePicker dtpTo;
        private DateTimePicker dtpFrom;
        private Label lblFrom;
        private Label lblTo;
        private TextBox txtSearch;
        private ComboBox cbType;
        private Label lblFilters;
        private Label lblHint;
        private Label lblTitle;
        private Panel pnlBottom;
        private Label lblCount;
        private Label lblRetention;
        private CheckBox chkUseDateRange;
        private Label lblSearch;
    }
}
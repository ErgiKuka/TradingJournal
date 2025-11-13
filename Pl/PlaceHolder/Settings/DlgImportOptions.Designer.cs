namespace TradingJournal.Pl.PlaceHolder.Settings
{
    partial class DlgImportOptions
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
            progressImport = new ProgressBar();
            gpbImportPath = new GroupBox();
            btnImBrowse = new FontAwesome.Sharp.IconButton();
            lblImFile = new Label();
            txtImFile = new TextBox();
            gpbImportFormat = new GroupBox();
            lblImFormat = new Label();
            cbImFormat = new ComboBox();
            gpbImportSources = new GroupBox();
            cbTarget = new ComboBox();
            lblTarget = new Label();
            lblImTitle = new Label();
            btnImportRun = new FontAwesome.Sharp.IconButton();
            btnValidate = new FontAwesome.Sharp.IconButton();
            btnImportCancel = new FontAwesome.Sharp.IconButton();
            gridPreview = new DataGridView();
            lblImStatus = new Label();
            openFileDialog1 = new OpenFileDialog();
            gpbImportPath.SuspendLayout();
            gpbImportFormat.SuspendLayout();
            gpbImportSources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPreview).BeginInit();
            SuspendLayout();
            // 
            // progressImport
            // 
            progressImport.Location = new Point(445, 388);
            progressImport.Name = "progressImport";
            progressImport.Size = new Size(322, 42);
            progressImport.TabIndex = 54;
            // 
            // gpbImportPath
            // 
            gpbImportPath.Controls.Add(btnImBrowse);
            gpbImportPath.Controls.Add(lblImFile);
            gpbImportPath.Controls.Add(txtImFile);
            gpbImportPath.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbImportPath.ForeColor = Color.FromArgb(156, 163, 175);
            gpbImportPath.Location = new Point(23, 252);
            gpbImportPath.Name = "gpbImportPath";
            gpbImportPath.Size = new Size(328, 207);
            gpbImportPath.TabIndex = 51;
            gpbImportPath.TabStop = false;
            gpbImportPath.Text = "Import Path";
            // 
            // btnImBrowse
            // 
            btnImBrowse.BackColor = Color.FromArgb(30, 58, 95);
            btnImBrowse.FlatAppearance.BorderSize = 0;
            btnImBrowse.FlatStyle = FlatStyle.Flat;
            btnImBrowse.Font = new Font("Times New Roman", 12F);
            btnImBrowse.IconChar = FontAwesome.Sharp.IconChar.None;
            btnImBrowse.IconColor = Color.Black;
            btnImBrowse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImBrowse.Location = new Point(29, 136);
            btnImBrowse.Margin = new Padding(2);
            btnImBrowse.Name = "btnImBrowse";
            btnImBrowse.Size = new Size(148, 43);
            btnImBrowse.TabIndex = 41;
            btnImBrowse.Text = "Browse…";
            btnImBrowse.UseVisualStyleBackColor = false;
            // 
            // lblImFile
            // 
            lblImFile.AutoSize = true;
            lblImFile.Font = new Font("Times New Roman", 15.75F);
            lblImFile.ForeColor = Color.FromArgb(156, 163, 175);
            lblImFile.Location = new Point(22, 41);
            lblImFile.Margin = new Padding(2, 0, 2, 0);
            lblImFile.Name = "lblImFile";
            lblImFile.Size = new Size(43, 23);
            lblImFile.TabIndex = 34;
            lblImFile.Text = "File";
            // 
            // txtImFile
            // 
            txtImFile.BackColor = Color.FromArgb(30, 58, 95);
            txtImFile.BorderStyle = BorderStyle.None;
            txtImFile.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtImFile.Location = new Point(22, 76);
            txtImFile.Margin = new Padding(2);
            txtImFile.Name = "txtImFile";
            txtImFile.PlaceholderText = "Place";
            txtImFile.ReadOnly = true;
            txtImFile.Size = new Size(207, 32);
            txtImFile.TabIndex = 40;
            txtImFile.Text = "   ";
            // 
            // gpbImportFormat
            // 
            gpbImportFormat.Controls.Add(lblImFormat);
            gpbImportFormat.Controls.Add(cbImFormat);
            gpbImportFormat.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbImportFormat.ForeColor = Color.FromArgb(156, 163, 175);
            gpbImportFormat.Location = new Point(463, 56);
            gpbImportFormat.Name = "gpbImportFormat";
            gpbImportFormat.Size = new Size(294, 170);
            gpbImportFormat.TabIndex = 50;
            gpbImportFormat.TabStop = false;
            gpbImportFormat.Text = "Import Format";
            // 
            // lblImFormat
            // 
            lblImFormat.AutoSize = true;
            lblImFormat.Font = new Font("Times New Roman", 15.75F);
            lblImFormat.ForeColor = Color.FromArgb(156, 163, 175);
            lblImFormat.Location = new Point(22, 41);
            lblImFormat.Margin = new Padding(2, 0, 2, 0);
            lblImFormat.Name = "lblImFormat";
            lblImFormat.Size = new Size(71, 23);
            lblImFormat.TabIndex = 33;
            lblImFormat.Text = "Format";
            // 
            // cbImFormat
            // 
            cbImFormat.BackColor = Color.FromArgb(30, 58, 95);
            cbImFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbImFormat.FlatStyle = FlatStyle.Flat;
            cbImFormat.Font = new Font("Times New Roman", 13.8F);
            cbImFormat.ForeColor = SystemColors.Window;
            cbImFormat.FormattingEnabled = true;
            cbImFormat.Items.AddRange(new object[] { "CSV (.csv)", "Excel (.xlsx)" });
            cbImFormat.Location = new Point(22, 81);
            cbImFormat.Margin = new Padding(2);
            cbImFormat.Name = "cbImFormat";
            cbImFormat.Size = new Size(163, 28);
            cbImFormat.TabIndex = 30;
            // 
            // gpbImportSources
            // 
            gpbImportSources.Controls.Add(cbTarget);
            gpbImportSources.Controls.Add(lblTarget);
            gpbImportSources.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gpbImportSources.ForeColor = Color.FromArgb(156, 163, 175);
            gpbImportSources.Location = new Point(23, 56);
            gpbImportSources.Name = "gpbImportSources";
            gpbImportSources.Size = new Size(328, 170);
            gpbImportSources.TabIndex = 47;
            gpbImportSources.TabStop = false;
            gpbImportSources.Text = "Import Target";
            // 
            // cbTarget
            // 
            cbTarget.BackColor = Color.FromArgb(30, 58, 95);
            cbTarget.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTarget.FlatStyle = FlatStyle.Flat;
            cbTarget.Font = new Font("Times New Roman", 13.8F);
            cbTarget.ForeColor = SystemColors.Window;
            cbTarget.FormattingEnabled = true;
            cbTarget.Items.AddRange(new object[] { "Journal (Trades)", "Recovery (Cases + Allocations)" });
            cbTarget.Location = new Point(22, 88);
            cbTarget.Margin = new Padding(2);
            cbTarget.Name = "cbTarget";
            cbTarget.Size = new Size(163, 28);
            cbTarget.TabIndex = 35;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Font = new Font("Times New Roman", 15.75F);
            lblTarget.ForeColor = Color.FromArgb(156, 163, 175);
            lblTarget.Location = new Point(22, 50);
            lblTarget.Margin = new Padding(2, 0, 2, 0);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(62, 23);
            lblTarget.TabIndex = 34;
            lblTarget.Text = "Target";
            // 
            // lblImTitle
            // 
            lblImTitle.AutoSize = true;
            lblImTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblImTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblImTitle.Location = new Point(11, 9);
            lblImTitle.Margin = new Padding(2, 0, 2, 0);
            lblImTitle.Name = "lblImTitle";
            lblImTitle.Size = new Size(205, 32);
            lblImTitle.TabIndex = 48;
            lblImTitle.Text = "Import Options";
            // 
            // btnImportRun
            // 
            btnImportRun.BackColor = Color.FromArgb(30, 58, 95);
            btnImportRun.Enabled = false;
            btnImportRun.FlatAppearance.BorderSize = 0;
            btnImportRun.FlatStyle = FlatStyle.Flat;
            btnImportRun.Font = new Font("Times New Roman", 12F);
            btnImportRun.IconChar = FontAwesome.Sharp.IconChar.None;
            btnImportRun.IconColor = Color.Black;
            btnImportRun.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImportRun.Location = new Point(617, 324);
            btnImportRun.Margin = new Padding(2);
            btnImportRun.Name = "btnImportRun";
            btnImportRun.Size = new Size(150, 44);
            btnImportRun.TabIndex = 42;
            btnImportRun.Text = "Import";
            btnImportRun.UseVisualStyleBackColor = false;
            // 
            // btnValidate
            // 
            btnValidate.BackColor = Color.FromArgb(30, 58, 95);
            btnValidate.FlatAppearance.BorderSize = 0;
            btnValidate.FlatStyle = FlatStyle.Flat;
            btnValidate.Font = new Font("Times New Roman", 12F);
            btnValidate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnValidate.IconColor = Color.Black;
            btnValidate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnValidate.Location = new Point(445, 324);
            btnValidate.Margin = new Padding(2);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(150, 44);
            btnValidate.TabIndex = 43;
            btnValidate.Text = "Validate";
            btnValidate.UseVisualStyleBackColor = false;
            // 
            // btnImportCancel
            // 
            btnImportCancel.BackColor = Color.FromArgb(30, 58, 95);
            btnImportCancel.FlatAppearance.BorderSize = 0;
            btnImportCancel.FlatStyle = FlatStyle.Flat;
            btnImportCancel.Font = new Font("Times New Roman", 12F);
            btnImportCancel.IconChar = FontAwesome.Sharp.IconChar.None;
            btnImportCancel.IconColor = Color.Black;
            btnImportCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImportCancel.Location = new Point(785, 387);
            btnImportCancel.Margin = new Padding(2);
            btnImportCancel.Name = "btnImportCancel";
            btnImportCancel.Size = new Size(150, 43);
            btnImportCancel.TabIndex = 44;
            btnImportCancel.Text = "Cancel";
            btnImportCancel.UseVisualStyleBackColor = false;
            // 
            // gridPreview
            // 
            gridPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPreview.Location = new Point(23, 540);
            gridPreview.Name = "gridPreview";
            gridPreview.RowHeadersWidth = 51;
            gridPreview.Size = new Size(968, 281);
            gridPreview.TabIndex = 55;
            // 
            // lblImStatus
            // 
            lblImStatus.Font = new Font("Times New Roman", 15.75F);
            lblImStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblImStatus.Location = new Point(23, 482);
            lblImStatus.Margin = new Padding(2, 0, 2, 0);
            lblImStatus.Name = "lblImStatus";
            lblImStatus.Size = new Size(968, 55);
            lblImStatus.TabIndex = 56;
            lblImStatus.Text = "--";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // DlgImportOptions
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1003, 839);
            Controls.Add(lblImStatus);
            Controls.Add(gridPreview);
            Controls.Add(btnImportCancel);
            Controls.Add(btnValidate);
            Controls.Add(progressImport);
            Controls.Add(btnImportRun);
            Controls.Add(gpbImportPath);
            Controls.Add(gpbImportFormat);
            Controls.Add(gpbImportSources);
            Controls.Add(lblImTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "DlgImportOptions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Import";
            gpbImportPath.ResumeLayout(false);
            gpbImportPath.PerformLayout();
            gpbImportFormat.ResumeLayout(false);
            gpbImportFormat.PerformLayout();
            gpbImportSources.ResumeLayout(false);
            gpbImportSources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressImport;
        private GroupBox gpbImportPath;
        private FontAwesome.Sharp.IconButton btnImBrowse;
        private Label lblImFile;
        private TextBox txtImFile;
        private GroupBox gpbImportFormat;
        private Label lblImFormat;
        private ComboBox cbImFormat;
        private GroupBox gpbImportSources;
        private Label lblImTitle;
        private Label lblTarget;
        private ComboBox cbTarget;
        private FontAwesome.Sharp.IconButton btnImportCancel;
        private FontAwesome.Sharp.IconButton btnValidate;
        private FontAwesome.Sharp.IconButton btnImportRun;
        private DataGridView gridPreview;
        private Label lblImStatus;
        private OpenFileDialog openFileDialog1;
    }
}
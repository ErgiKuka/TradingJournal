namespace TradingJournal.Pl.PlaceHolder.Settings
{
    partial class FrmSettings
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
            pnlAccountMoney_Max = new Panel();
            pnlAccountMoney = new Panel();
            lnkExportAllTransactions = new LinkLabel();
            dgvAccountHistory = new DataGridView();
            lblAcctHistoryNote = new Label();
            lblAcctHistoryHeader = new Label();
            rbWithdraw = new RadioButton();
            rbDeposit = new RadioButton();
            btnResetBalance = new FontAwesome.Sharp.IconButton();
            btnChangeBalance = new FontAwesome.Sharp.IconButton();
            txtNewAccountBalance = new TextBox();
            lblCurrentBalance = new Label();
            lblAccNew = new Label();
            lblAccCurrent = new Label();
            lblAccTitle = new Label();
            pnlThemeAndNotifications_Max = new Panel();
            pnlThemeAndNotifications = new Panel();
            groupBox2 = new GroupBox();
            rbMaximized = new RadioButton();
            rbNormal = new RadioButton();
            groupBox1 = new GroupBox();
            rbLightMode = new RadioButton();
            rbDarkMode = new RadioButton();
            lblBackupNotifyHint = new Label();
            btnTestNotifications = new FontAwesome.Sharp.IconButton();
            chkNotifyDailySummary = new CheckBox();
            chkNotifyBackupStatus = new CheckBox();
            chkNotifyRecoveryMilestone = new CheckBox();
            lblRecoveryMilestoneSuffix = new Label();
            numRecoveryMilestone = new NumericUpDown();
            lblRecoveryMilestoneCaption = new Label();
            lblThemeAndNotificationsHeader = new Label();
            pnlRecycleBin_Max = new Panel();
            pnlRecycleBin = new Panel();
            numRetentionDays = new NumericUpDown();
            btnOpenRecycleBin = new FontAwesome.Sharp.IconButton();
            btnEmptyRecycleBin = new FontAwesome.Sharp.IconButton();
            lblRecycleBinHeader = new Label();
            lblRecycleBinDesc = new Label();
            lblRecycleBinInfo = new Label();
            lblRetentionCaption = new Label();
            lblRetentionHint = new Label();
            pnlBackupRestore_Max = new Panel();
            pnlBackupRestore = new Panel();
            lblLastBackup = new Label();
            btnOpenBackupFolder = new FontAwesome.Sharp.IconButton();
            btnRestoreFrom = new FontAwesome.Sharp.IconButton();
            btnUndoLastRestore = new FontAwesome.Sharp.IconButton();
            btnBackupNow = new FontAwesome.Sharp.IconButton();
            btnRestoreLatest = new FontAwesome.Sharp.IconButton();
            numKeepLast = new NumericUpDown();
            dtpBackupTime = new DateTimePicker();
            cbBackupFrequency = new ComboBox();
            chkEnableAutoBackup = new CheckBox();
            lblKeepLast = new Label();
            lbllBackupFrequency = new Label();
            lblKeepSuffix = new Label();
            lbllBackupTime = new Label();
            lblBkTitle = new Label();
            pnlMaintenance_Max = new Panel();
            pnlMaintenance = new Panel();
            btnOpenLogsFolder = new FontAwesome.Sharp.IconButton();
            txtDbPath = new TextBox();
            txtBackupPath = new TextBox();
            btnCompactDatabase = new FontAwesome.Sharp.IconButton();
            btnValidateDatabase = new FontAwesome.Sharp.IconButton();
            lblBackupPath = new Label();
            lblDbPath = new Label();
            lblMaintTitle = new Label();
            pnlImportExport_Max = new Panel();
            pnlImportExport = new Panel();
            btnImport = new FontAwesome.Sharp.IconButton();
            btnExport = new FontAwesome.Sharp.IconButton();
            btnDownloadRecoveryTemplate = new FontAwesome.Sharp.IconButton();
            btnDownloadJournalTemplate = new FontAwesome.Sharp.IconButton();
            lblIEInfo = new Label();
            lblIETitle = new Label();
            pnlScroll = new Panel();
            pnlAccountMoney.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccountHistory).BeginInit();
            pnlThemeAndNotifications.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRecoveryMilestone).BeginInit();
            pnlRecycleBin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRetentionDays).BeginInit();
            pnlBackupRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepLast).BeginInit();
            pnlMaintenance.SuspendLayout();
            pnlImportExport.SuspendLayout();
            pnlScroll.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAccountMoney_Max
            // 
            pnlAccountMoney_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlAccountMoney_Max.Location = new Point(15, 16);
            pnlAccountMoney_Max.Margin = new Padding(2);
            pnlAccountMoney_Max.Name = "pnlAccountMoney_Max";
            pnlAccountMoney_Max.Size = new Size(1651, 801);
            pnlAccountMoney_Max.TabIndex = 17;
            // 
            // pnlAccountMoney
            // 
            pnlAccountMoney.BackColor = Color.FromArgb(27, 38, 59);
            pnlAccountMoney.Controls.Add(lnkExportAllTransactions);
            pnlAccountMoney.Controls.Add(lblAcctHistoryNote);
            pnlAccountMoney.Controls.Add(lblAcctHistoryHeader);
            pnlAccountMoney.Controls.Add(rbWithdraw);
            pnlAccountMoney.Controls.Add(rbDeposit);
            pnlAccountMoney.Controls.Add(btnResetBalance);
            pnlAccountMoney.Controls.Add(btnChangeBalance);
            pnlAccountMoney.Controls.Add(txtNewAccountBalance);
            pnlAccountMoney.Controls.Add(lblCurrentBalance);
            pnlAccountMoney.Controls.Add(lblAccNew);
            pnlAccountMoney.Controls.Add(lblAccCurrent);
            pnlAccountMoney.Controls.Add(lblAccTitle);
            pnlAccountMoney.Controls.Add(dgvAccountHistory);
            pnlAccountMoney.Location = new Point(15, 16);
            pnlAccountMoney.Margin = new Padding(2);
            pnlAccountMoney.Name = "pnlAccountMoney";
            pnlAccountMoney.Size = new Size(922, 801);
            pnlAccountMoney.TabIndex = 16;
            // 
            // lnkExportAllTransactions
            // 
            lnkExportAllTransactions.AutoSize = true;
            lnkExportAllTransactions.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkExportAllTransactions.ForeColor = Color.FromArgb(156, 163, 175);
            lnkExportAllTransactions.LinkColor = Color.FromArgb(156, 163, 175);
            lnkExportAllTransactions.Location = new Point(29, 747);
            lnkExportAllTransactions.Name = "lnkExportAllTransactions";
            lnkExportAllTransactions.Size = new Size(296, 23);
            lnkExportAllTransactions.TabIndex = 32;
            lnkExportAllTransactions.TabStop = true;
            lnkExportAllTransactions.Text = "Open full history in Export/Import";
            // 
            // dgvAccountHistory
            // 
            dgvAccountHistory.AllowUserToAddRows = false;
            dgvAccountHistory.AllowUserToDeleteRows = false;
            dgvAccountHistory.AllowUserToResizeColumns = false;
            dgvAccountHistory.AllowUserToResizeRows = false;
            dgvAccountHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccountHistory.Location = new Point(29, 356);
            dgvAccountHistory.Name = "dgvAccountHistory";
            dgvAccountHistory.ReadOnly = true;
            dgvAccountHistory.Size = new Size(869, 341);
            dgvAccountHistory.TabIndex = 31;
            // 
            // lblAcctHistoryNote
            // 
            lblAcctHistoryNote.AutoSize = true;
            lblAcctHistoryNote.Font = new Font("Times New Roman", 15.75F);
            lblAcctHistoryNote.ForeColor = Color.FromArgb(156, 163, 175);
            lblAcctHistoryNote.Location = new Point(29, 713);
            lblAcctHistoryNote.Margin = new Padding(2, 0, 2, 0);
            lblAcctHistoryNote.Name = "lblAcctHistoryNote";
            lblAcctHistoryNote.Size = new Size(473, 23);
            lblAcctHistoryNote.TabIndex = 30;
            lblAcctHistoryNote.Text = "Showing the last 180 days. Export to view older entries.";
            // 
            // lblAcctHistoryHeader
            // 
            lblAcctHistoryHeader.AutoSize = true;
            lblAcctHistoryHeader.Font = new Font("Times New Roman", 15.75F);
            lblAcctHistoryHeader.ForeColor = Color.FromArgb(156, 163, 175);
            lblAcctHistoryHeader.Location = new Point(26, 310);
            lblAcctHistoryHeader.Margin = new Padding(2, 0, 2, 0);
            lblAcctHistoryHeader.Name = "lblAcctHistoryHeader";
            lblAcctHistoryHeader.Size = new Size(298, 23);
            lblAcctHistoryHeader.TabIndex = 29;
            lblAcctHistoryHeader.Text = "Recent transactions (last 180 days)";
            // 
            // rbWithdraw
            // 
            rbWithdraw.AutoSize = true;
            rbWithdraw.Font = new Font("Times New Roman", 14.25F);
            rbWithdraw.ForeColor = Color.FromArgb(156, 163, 175);
            rbWithdraw.Location = new Point(137, 157);
            rbWithdraw.Name = "rbWithdraw";
            rbWithdraw.Size = new Size(100, 25);
            rbWithdraw.TabIndex = 28;
            rbWithdraw.Text = "Withdraw";
            rbWithdraw.UseVisualStyleBackColor = true;
            // 
            // rbDeposit
            // 
            rbDeposit.AutoSize = true;
            rbDeposit.Checked = true;
            rbDeposit.Font = new Font("Times New Roman", 14.25F);
            rbDeposit.ForeColor = Color.FromArgb(156, 163, 175);
            rbDeposit.Location = new Point(26, 157);
            rbDeposit.Name = "rbDeposit";
            rbDeposit.Size = new Size(86, 25);
            rbDeposit.TabIndex = 27;
            rbDeposit.TabStop = true;
            rbDeposit.Text = "Deposit";
            rbDeposit.UseVisualStyleBackColor = true;
            // 
            // btnResetBalance
            // 
            btnResetBalance.BackColor = Color.DarkRed;
            btnResetBalance.FlatAppearance.BorderSize = 0;
            btnResetBalance.FlatStyle = FlatStyle.Flat;
            btnResetBalance.Font = new Font("Times New Roman", 12F);
            btnResetBalance.IconChar = FontAwesome.Sharp.IconChar.None;
            btnResetBalance.IconColor = Color.Black;
            btnResetBalance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnResetBalance.Location = new Point(718, 248);
            btnResetBalance.Margin = new Padding(2);
            btnResetBalance.Name = "btnResetBalance";
            btnResetBalance.Size = new Size(150, 33);
            btnResetBalance.TabIndex = 26;
            btnResetBalance.Text = "Reset Balance";
            btnResetBalance.UseVisualStyleBackColor = false;
            // 
            // btnChangeBalance
            // 
            btnChangeBalance.BackColor = Color.FromArgb(30, 95, 58);
            btnChangeBalance.FlatAppearance.BorderSize = 0;
            btnChangeBalance.FlatStyle = FlatStyle.Flat;
            btnChangeBalance.Font = new Font("Times New Roman", 12F);
            btnChangeBalance.IconChar = FontAwesome.Sharp.IconChar.None;
            btnChangeBalance.IconColor = Color.Black;
            btnChangeBalance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnChangeBalance.Location = new Point(264, 247);
            btnChangeBalance.Margin = new Padding(2);
            btnChangeBalance.Name = "btnChangeBalance";
            btnChangeBalance.Size = new Size(150, 33);
            btnChangeBalance.TabIndex = 25;
            btnChangeBalance.Text = "Deposit";
            btnChangeBalance.UseVisualStyleBackColor = false;
            // 
            // txtNewAccountBalance
            // 
            txtNewAccountBalance.BackColor = Color.FromArgb(30, 58, 95);
            txtNewAccountBalance.BorderStyle = BorderStyle.None;
            txtNewAccountBalance.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNewAccountBalance.Location = new Point(26, 248);
            txtNewAccountBalance.Margin = new Padding(2);
            txtNewAccountBalance.Name = "txtNewAccountBalance";
            txtNewAccountBalance.PlaceholderText = "0.00";
            txtNewAccountBalance.Size = new Size(207, 32);
            txtNewAccountBalance.TabIndex = 11;
            txtNewAccountBalance.Text = "   ";
            // 
            // lblCurrentBalance
            // 
            lblCurrentBalance.AutoSize = true;
            lblCurrentBalance.Font = new Font("Times New Roman", 15.75F);
            lblCurrentBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblCurrentBalance.Location = new Point(26, 108);
            lblCurrentBalance.Margin = new Padding(2, 0, 2, 0);
            lblCurrentBalance.Name = "lblCurrentBalance";
            lblCurrentBalance.Size = new Size(24, 23);
            lblCurrentBalance.TabIndex = 6;
            lblCurrentBalance.Text = "--";
            // 
            // lblAccNew
            // 
            lblAccNew.AutoSize = true;
            lblAccNew.Font = new Font("Times New Roman", 15.75F);
            lblAccNew.ForeColor = Color.FromArgb(156, 163, 175);
            lblAccNew.Location = new Point(26, 205);
            lblAccNew.Margin = new Padding(2, 0, 2, 0);
            lblAccNew.Name = "lblAccNew";
            lblAccNew.Size = new Size(294, 23);
            lblAccNew.TabIndex = 5;
            lblAccNew.Text = "Enter amount to deposit/withdraw";
            // 
            // lblAccCurrent
            // 
            lblAccCurrent.AutoSize = true;
            lblAccCurrent.Font = new Font("Times New Roman", 15.75F);
            lblAccCurrent.ForeColor = Color.FromArgb(156, 163, 175);
            lblAccCurrent.Location = new Point(26, 72);
            lblAccCurrent.Margin = new Padding(2, 0, 2, 0);
            lblAccCurrent.Name = "lblAccCurrent";
            lblAccCurrent.Size = new Size(145, 23);
            lblAccCurrent.TabIndex = 4;
            lblAccCurrent.Text = "Current Balance";
            // 
            // lblAccTitle
            // 
            lblAccTitle.AutoSize = true;
            lblAccTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAccTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblAccTitle.Location = new Point(12, 11);
            lblAccTitle.Margin = new Padding(2, 0, 2, 0);
            lblAccTitle.Name = "lblAccTitle";
            lblAccTitle.Size = new Size(261, 32);
            lblAccTitle.TabIndex = 3;
            lblAccTitle.Text = "Account and Money";
            // 
            // pnlThemeAndNotifications_Max
            // 
            pnlThemeAndNotifications_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlThemeAndNotifications_Max.Location = new Point(15, 848);
            pnlThemeAndNotifications_Max.Margin = new Padding(2);
            pnlThemeAndNotifications_Max.Name = "pnlThemeAndNotifications_Max";
            pnlThemeAndNotifications_Max.Size = new Size(1651, 491);
            pnlThemeAndNotifications_Max.TabIndex = 19;
            pnlThemeAndNotifications_Max.Visible = false;
            // 
            // pnlThemeAndNotifications
            // 
            pnlThemeAndNotifications.BackColor = Color.FromArgb(27, 38, 59);
            pnlThemeAndNotifications.Controls.Add(groupBox2);
            pnlThemeAndNotifications.Controls.Add(groupBox1);
            pnlThemeAndNotifications.Controls.Add(lblBackupNotifyHint);
            pnlThemeAndNotifications.Controls.Add(btnTestNotifications);
            pnlThemeAndNotifications.Controls.Add(chkNotifyDailySummary);
            pnlThemeAndNotifications.Controls.Add(chkNotifyBackupStatus);
            pnlThemeAndNotifications.Controls.Add(chkNotifyRecoveryMilestone);
            pnlThemeAndNotifications.Controls.Add(lblRecoveryMilestoneSuffix);
            pnlThemeAndNotifications.Controls.Add(numRecoveryMilestone);
            pnlThemeAndNotifications.Controls.Add(lblRecoveryMilestoneCaption);
            pnlThemeAndNotifications.Controls.Add(lblThemeAndNotificationsHeader);
            pnlThemeAndNotifications.Location = new Point(15, 848);
            pnlThemeAndNotifications.Margin = new Padding(2);
            pnlThemeAndNotifications.Name = "pnlThemeAndNotifications";
            pnlThemeAndNotifications.Size = new Size(922, 489);
            pnlThemeAndNotifications.TabIndex = 18;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rbMaximized);
            groupBox2.Controls.Add(rbNormal);
            groupBox2.Font = new Font("Times New Roman", 18F);
            groupBox2.ForeColor = Color.FromArgb(156, 163, 175);
            groupBox2.Location = new Point(310, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(254, 92);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "App startup window:";
            // 
            // rbMaximized
            // 
            rbMaximized.AutoSize = true;
            rbMaximized.Font = new Font("Times New Roman", 14.25F);
            rbMaximized.ForeColor = Color.FromArgb(156, 163, 175);
            rbMaximized.Location = new Point(106, 45);
            rbMaximized.Name = "rbMaximized";
            rbMaximized.Size = new Size(109, 25);
            rbMaximized.TabIndex = 30;
            rbMaximized.Text = "Maximized";
            rbMaximized.UseVisualStyleBackColor = true;
            // 
            // rbNormal
            // 
            rbNormal.AutoSize = true;
            rbNormal.Checked = true;
            rbNormal.Font = new Font("Times New Roman", 14.25F);
            rbNormal.ForeColor = Color.FromArgb(156, 163, 175);
            rbNormal.Location = new Point(16, 45);
            rbNormal.Name = "rbNormal";
            rbNormal.Size = new Size(83, 25);
            rbNormal.TabIndex = 29;
            rbNormal.TabStop = true;
            rbNormal.Text = "Normal";
            rbNormal.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbLightMode);
            groupBox1.Controls.Add(rbDarkMode);
            groupBox1.Font = new Font("Times New Roman", 18F);
            groupBox1.ForeColor = Color.FromArgb(156, 163, 175);
            groupBox1.Location = new Point(27, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(244, 92);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Theme";
            // 
            // rbLightMode
            // 
            rbLightMode.AutoSize = true;
            rbLightMode.Font = new Font("Times New Roman", 14.25F);
            rbLightMode.ForeColor = Color.FromArgb(156, 163, 175);
            rbLightMode.Location = new Point(115, 43);
            rbLightMode.Name = "rbLightMode";
            rbLightMode.Size = new Size(66, 25);
            rbLightMode.TabIndex = 6;
            rbLightMode.Text = "Light";
            rbLightMode.UseVisualStyleBackColor = true;
            // 
            // rbDarkMode
            // 
            rbDarkMode.AutoSize = true;
            rbDarkMode.Checked = true;
            rbDarkMode.Font = new Font("Times New Roman", 14.25F);
            rbDarkMode.ForeColor = Color.FromArgb(156, 163, 175);
            rbDarkMode.Location = new Point(16, 43);
            rbDarkMode.Name = "rbDarkMode";
            rbDarkMode.Size = new Size(64, 25);
            rbDarkMode.TabIndex = 5;
            rbDarkMode.TabStop = true;
            rbDarkMode.Text = "Dark";
            rbDarkMode.UseVisualStyleBackColor = true;
            // 
            // lblBackupNotifyHint
            // 
            lblBackupNotifyHint.AutoSize = true;
            lblBackupNotifyHint.Font = new Font("Times New Roman", 15.75F);
            lblBackupNotifyHint.ForeColor = Color.FromArgb(156, 163, 175);
            lblBackupNotifyHint.Location = new Point(20, 338);
            lblBackupNotifyHint.Margin = new Padding(2, 0, 2, 0);
            lblBackupNotifyHint.Name = "lblBackupNotifyHint";
            lblBackupNotifyHint.Size = new Size(420, 23);
            lblBackupNotifyHint.TabIndex = 28;
            lblBackupNotifyHint.Text = "Show a notification when backups succeed or fail";
            // 
            // btnTestNotifications
            // 
            btnTestNotifications.BackColor = Color.FromArgb(30, 58, 95);
            btnTestNotifications.FlatAppearance.BorderSize = 0;
            btnTestNotifications.FlatStyle = FlatStyle.Flat;
            btnTestNotifications.Font = new Font("Times New Roman", 12F);
            btnTestNotifications.IconChar = FontAwesome.Sharp.IconChar.None;
            btnTestNotifications.IconColor = Color.Black;
            btnTestNotifications.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTestNotifications.Location = new Point(26, 432);
            btnTestNotifications.Margin = new Padding(2);
            btnTestNotifications.Name = "btnTestNotifications";
            btnTestNotifications.Size = new Size(156, 33);
            btnTestNotifications.TabIndex = 27;
            btnTestNotifications.Text = "Send test notification";
            btnTestNotifications.UseVisualStyleBackColor = false;
            // 
            // chkNotifyDailySummary
            // 
            chkNotifyDailySummary.AutoSize = true;
            chkNotifyDailySummary.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkNotifyDailySummary.ForeColor = Color.FromArgb(156, 163, 175);
            chkNotifyDailySummary.Location = new Point(25, 388);
            chkNotifyDailySummary.Name = "chkNotifyDailySummary";
            chkNotifyDailySummary.Size = new Size(376, 27);
            chkNotifyDailySummary.TabIndex = 11;
            chkNotifyDailySummary.Text = "Daily summary (win/loss, P/L, reminders)";
            chkNotifyDailySummary.UseVisualStyleBackColor = true;
            // 
            // chkNotifyBackupStatus
            // 
            chkNotifyBackupStatus.AutoSize = true;
            chkNotifyBackupStatus.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkNotifyBackupStatus.ForeColor = Color.FromArgb(156, 163, 175);
            chkNotifyBackupStatus.Location = new Point(26, 300);
            chkNotifyBackupStatus.Name = "chkNotifyBackupStatus";
            chkNotifyBackupStatus.Size = new Size(195, 27);
            chkNotifyBackupStatus.TabIndex = 10;
            chkNotifyBackupStatus.Text = "Backup status alerts";
            chkNotifyBackupStatus.UseVisualStyleBackColor = true;
            // 
            // chkNotifyRecoveryMilestone
            // 
            chkNotifyRecoveryMilestone.AutoSize = true;
            chkNotifyRecoveryMilestone.Checked = true;
            chkNotifyRecoveryMilestone.CheckState = CheckState.Checked;
            chkNotifyRecoveryMilestone.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkNotifyRecoveryMilestone.ForeColor = Color.FromArgb(156, 163, 175);
            chkNotifyRecoveryMilestone.Location = new Point(26, 167);
            chkNotifyRecoveryMilestone.Name = "chkNotifyRecoveryMilestone";
            chkNotifyRecoveryMilestone.Size = new Size(268, 27);
            chkNotifyRecoveryMilestone.TabIndex = 0;
            chkNotifyRecoveryMilestone.Text = "Recovery progress milestone";
            chkNotifyRecoveryMilestone.UseVisualStyleBackColor = true;
            // 
            // lblRecoveryMilestoneSuffix
            // 
            lblRecoveryMilestoneSuffix.AutoSize = true;
            lblRecoveryMilestoneSuffix.Font = new Font("Times New Roman", 15.75F);
            lblRecoveryMilestoneSuffix.ForeColor = Color.FromArgb(156, 163, 175);
            lblRecoveryMilestoneSuffix.Location = new Point(137, 249);
            lblRecoveryMilestoneSuffix.Margin = new Padding(2, 0, 2, 0);
            lblRecoveryMilestoneSuffix.Name = "lblRecoveryMilestoneSuffix";
            lblRecoveryMilestoneSuffix.Size = new Size(103, 23);
            lblRecoveryMilestoneSuffix.TabIndex = 9;
            lblRecoveryMilestoneSuffix.Text = "% progress";
            // 
            // numRecoveryMilestone
            // 
            numRecoveryMilestone.Location = new Point(34, 249);
            numRecoveryMilestone.Name = "numRecoveryMilestone";
            numRecoveryMilestone.Size = new Size(88, 23);
            numRecoveryMilestone.TabIndex = 8;
            numRecoveryMilestone.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // lblRecoveryMilestoneCaption
            // 
            lblRecoveryMilestoneCaption.AutoSize = true;
            lblRecoveryMilestoneCaption.Font = new Font("Times New Roman", 15.75F);
            lblRecoveryMilestoneCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRecoveryMilestoneCaption.Location = new Point(25, 212);
            lblRecoveryMilestoneCaption.Margin = new Padding(2, 0, 2, 0);
            lblRecoveryMilestoneCaption.Name = "lblRecoveryMilestoneCaption";
            lblRecoveryMilestoneCaption.Size = new Size(254, 23);
            lblRecoveryMilestoneCaption.TabIndex = 7;
            lblRecoveryMilestoneCaption.Text = "Notify when progress reaches";
            // 
            // lblThemeAndNotificationsHeader
            // 
            lblThemeAndNotificationsHeader.AutoSize = true;
            lblThemeAndNotificationsHeader.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThemeAndNotificationsHeader.ForeColor = Color.FromArgb(156, 163, 175);
            lblThemeAndNotificationsHeader.Location = new Point(12, 12);
            lblThemeAndNotificationsHeader.Margin = new Padding(2, 0, 2, 0);
            lblThemeAndNotificationsHeader.Name = "lblThemeAndNotificationsHeader";
            lblThemeAndNotificationsHeader.Size = new Size(315, 32);
            lblThemeAndNotificationsHeader.TabIndex = 4;
            lblThemeAndNotificationsHeader.Text = "Theme and Notifications";
            // 
            // pnlRecycleBin_Max
            // 
            pnlRecycleBin_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlRecycleBin_Max.Location = new Point(15, 1357);
            pnlRecycleBin_Max.Margin = new Padding(2);
            pnlRecycleBin_Max.Name = "pnlRecycleBin_Max";
            pnlRecycleBin_Max.Size = new Size(1651, 285);
            pnlRecycleBin_Max.TabIndex = 21;
            pnlRecycleBin_Max.Visible = false;
            // 
            // pnlRecycleBin
            // 
            pnlRecycleBin.BackColor = Color.FromArgb(27, 38, 59);
            pnlRecycleBin.Controls.Add(numRetentionDays);
            pnlRecycleBin.Controls.Add(btnOpenRecycleBin);
            pnlRecycleBin.Controls.Add(btnEmptyRecycleBin);
            pnlRecycleBin.Controls.Add(lblRecycleBinHeader);
            pnlRecycleBin.Controls.Add(lblRecycleBinDesc);
            pnlRecycleBin.Controls.Add(lblRecycleBinInfo);
            pnlRecycleBin.Controls.Add(lblRetentionCaption);
            pnlRecycleBin.Controls.Add(lblRetentionHint);
            pnlRecycleBin.Location = new Point(15, 1357);
            pnlRecycleBin.Margin = new Padding(2);
            pnlRecycleBin.Name = "pnlRecycleBin";
            pnlRecycleBin.Size = new Size(922, 285);
            pnlRecycleBin.TabIndex = 20;
            // 
            // numRetentionDays
            // 
            numRetentionDays.Location = new Point(34, 131);
            numRetentionDays.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numRetentionDays.Name = "numRetentionDays";
            numRetentionDays.Size = new Size(88, 23);
            numRetentionDays.TabIndex = 39;
            numRetentionDays.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnOpenRecycleBin
            // 
            btnOpenRecycleBin.BackColor = Color.FromArgb(30, 58, 95);
            btnOpenRecycleBin.FlatAppearance.BorderSize = 0;
            btnOpenRecycleBin.FlatStyle = FlatStyle.Flat;
            btnOpenRecycleBin.Font = new Font("Times New Roman", 12F);
            btnOpenRecycleBin.IconChar = FontAwesome.Sharp.IconChar.None;
            btnOpenRecycleBin.IconColor = Color.Black;
            btnOpenRecycleBin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenRecycleBin.Location = new Point(32, 176);
            btnOpenRecycleBin.Margin = new Padding(2);
            btnOpenRecycleBin.Name = "btnOpenRecycleBin";
            btnOpenRecycleBin.Size = new Size(150, 33);
            btnOpenRecycleBin.TabIndex = 38;
            btnOpenRecycleBin.Text = "Open Recycle Bin";
            btnOpenRecycleBin.UseVisualStyleBackColor = false;
            // 
            // btnEmptyRecycleBin
            // 
            btnEmptyRecycleBin.BackColor = Color.DarkRed;
            btnEmptyRecycleBin.FlatAppearance.BorderSize = 0;
            btnEmptyRecycleBin.FlatStyle = FlatStyle.Flat;
            btnEmptyRecycleBin.Font = new Font("Times New Roman", 12F);
            btnEmptyRecycleBin.IconChar = FontAwesome.Sharp.IconChar.None;
            btnEmptyRecycleBin.IconColor = Color.Black;
            btnEmptyRecycleBin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEmptyRecycleBin.Location = new Point(267, 176);
            btnEmptyRecycleBin.Margin = new Padding(2);
            btnEmptyRecycleBin.Name = "btnEmptyRecycleBin";
            btnEmptyRecycleBin.Size = new Size(150, 33);
            btnEmptyRecycleBin.TabIndex = 37;
            btnEmptyRecycleBin.Text = "Empty Recycle Bin";
            btnEmptyRecycleBin.UseVisualStyleBackColor = false;
            // 
            // lblRecycleBinHeader
            // 
            lblRecycleBinHeader.AutoSize = true;
            lblRecycleBinHeader.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRecycleBinHeader.ForeColor = Color.FromArgb(156, 163, 175);
            lblRecycleBinHeader.Location = new Point(19, 10);
            lblRecycleBinHeader.Margin = new Padding(2, 0, 2, 0);
            lblRecycleBinHeader.Name = "lblRecycleBinHeader";
            lblRecycleBinHeader.Size = new Size(323, 32);
            lblRecycleBinHeader.TabIndex = 36;
            lblRecycleBinHeader.Text = "Recycle Bin (soft deletes)";
            // 
            // lblRecycleBinDesc
            // 
            lblRecycleBinDesc.AutoSize = true;
            lblRecycleBinDesc.Font = new Font("Times New Roman", 15.75F);
            lblRecycleBinDesc.ForeColor = Color.FromArgb(156, 163, 175);
            lblRecycleBinDesc.Location = new Point(29, 60);
            lblRecycleBinDesc.Margin = new Padding(2, 0, 2, 0);
            lblRecycleBinDesc.Name = "lblRecycleBinDesc";
            lblRecycleBinDesc.Size = new Size(636, 23);
            lblRecycleBinDesc.TabIndex = 34;
            lblRecycleBinDesc.Text = "Items you delete are kept here for a while so you can restore them if needed.";
            // 
            // lblRecycleBinInfo
            // 
            lblRecycleBinInfo.AutoSize = true;
            lblRecycleBinInfo.Font = new Font("Times New Roman", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecycleBinInfo.ForeColor = Color.FromArgb(156, 163, 175);
            lblRecycleBinInfo.Location = new Point(26, 245);
            lblRecycleBinInfo.Margin = new Padding(2, 0, 2, 0);
            lblRecycleBinInfo.Name = "lblRecycleBinInfo";
            lblRecycleBinInfo.Size = new Size(391, 17);
            lblRecycleBinInfo.TabIndex = 33;
            lblRecycleBinInfo.Text = "Tip: You can restore items from the Recycle Bin until they expire.";
            // 
            // lblRetentionCaption
            // 
            lblRetentionCaption.AutoSize = true;
            lblRetentionCaption.Font = new Font("Times New Roman", 15.75F);
            lblRetentionCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRetentionCaption.Location = new Point(29, 93);
            lblRetentionCaption.Margin = new Padding(2, 0, 2, 0);
            lblRetentionCaption.Name = "lblRetentionCaption";
            lblRetentionCaption.Size = new Size(155, 23);
            lblRetentionCaption.TabIndex = 32;
            lblRetentionCaption.Text = "Retention period:";
            // 
            // lblRetentionHint
            // 
            lblRetentionHint.AutoSize = true;
            lblRetentionHint.Font = new Font("Times New Roman", 15.75F);
            lblRetentionHint.ForeColor = Color.FromArgb(156, 163, 175);
            lblRetentionHint.Location = new Point(134, 131);
            lblRetentionHint.Margin = new Padding(2, 0, 2, 0);
            lblRetentionHint.Name = "lblRetentionHint";
            lblRetentionHint.Size = new Size(267, 23);
            lblRetentionHint.TabIndex = 31;
            lblRetentionHint.Text = "days before permanent deletion";
            // 
            // pnlBackupRestore_Max
            // 
            pnlBackupRestore_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlBackupRestore_Max.Location = new Point(15, 1664);
            pnlBackupRestore_Max.Margin = new Padding(2);
            pnlBackupRestore_Max.Name = "pnlBackupRestore_Max";
            pnlBackupRestore_Max.Size = new Size(1651, 473);
            pnlBackupRestore_Max.TabIndex = 23;
            pnlBackupRestore_Max.Visible = false;
            // 
            // pnlBackupRestore
            // 
            pnlBackupRestore.BackColor = Color.FromArgb(27, 38, 59);
            pnlBackupRestore.Controls.Add(lblLastBackup);
            pnlBackupRestore.Controls.Add(btnOpenBackupFolder);
            pnlBackupRestore.Controls.Add(btnRestoreFrom);
            pnlBackupRestore.Controls.Add(btnUndoLastRestore);
            pnlBackupRestore.Controls.Add(btnBackupNow);
            pnlBackupRestore.Controls.Add(btnRestoreLatest);
            pnlBackupRestore.Controls.Add(numKeepLast);
            pnlBackupRestore.Controls.Add(dtpBackupTime);
            pnlBackupRestore.Controls.Add(cbBackupFrequency);
            pnlBackupRestore.Controls.Add(chkEnableAutoBackup);
            pnlBackupRestore.Controls.Add(lblKeepLast);
            pnlBackupRestore.Controls.Add(lbllBackupFrequency);
            pnlBackupRestore.Controls.Add(lblKeepSuffix);
            pnlBackupRestore.Controls.Add(lbllBackupTime);
            pnlBackupRestore.Controls.Add(lblBkTitle);
            pnlBackupRestore.Location = new Point(15, 1664);
            pnlBackupRestore.Margin = new Padding(2);
            pnlBackupRestore.Name = "pnlBackupRestore";
            pnlBackupRestore.Size = new Size(922, 463);
            pnlBackupRestore.TabIndex = 22;
            // 
            // lblLastBackup
            // 
            lblLastBackup.AutoSize = true;
            lblLastBackup.Font = new Font("Times New Roman", 15.75F);
            lblLastBackup.ForeColor = Color.FromArgb(156, 163, 175);
            lblLastBackup.Location = new Point(383, 391);
            lblLastBackup.Margin = new Padding(2, 0, 2, 0);
            lblLastBackup.Name = "lblLastBackup";
            lblLastBackup.Size = new Size(119, 23);
            lblLastBackup.TabIndex = 37;
            lblLastBackup.Text = "Last Backup:";
            // 
            // btnOpenBackupFolder
            // 
            btnOpenBackupFolder.BackColor = Color.FromArgb(30, 58, 95);
            btnOpenBackupFolder.FlatAppearance.BorderSize = 0;
            btnOpenBackupFolder.FlatStyle = FlatStyle.Flat;
            btnOpenBackupFolder.Font = new Font("Times New Roman", 12F);
            btnOpenBackupFolder.IconChar = FontAwesome.Sharp.IconChar.None;
            btnOpenBackupFolder.IconColor = Color.Black;
            btnOpenBackupFolder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenBackupFolder.Location = new Point(673, 129);
            btnOpenBackupFolder.Margin = new Padding(2);
            btnOpenBackupFolder.Name = "btnOpenBackupFolder";
            btnOpenBackupFolder.Size = new Size(150, 33);
            btnOpenBackupFolder.TabIndex = 36;
            btnOpenBackupFolder.Text = "Open Backup Folder";
            btnOpenBackupFolder.UseVisualStyleBackColor = false;
            // 
            // btnRestoreFrom
            // 
            btnRestoreFrom.BackColor = Color.FromArgb(30, 58, 95);
            btnRestoreFrom.FlatAppearance.BorderSize = 0;
            btnRestoreFrom.FlatStyle = FlatStyle.Flat;
            btnRestoreFrom.Font = new Font("Times New Roman", 12F);
            btnRestoreFrom.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRestoreFrom.IconColor = Color.Black;
            btnRestoreFrom.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRestoreFrom.Location = new Point(474, 310);
            btnRestoreFrom.Margin = new Padding(2);
            btnRestoreFrom.Name = "btnRestoreFrom";
            btnRestoreFrom.Size = new Size(150, 33);
            btnRestoreFrom.TabIndex = 35;
            btnRestoreFrom.Text = "Restore From…";
            btnRestoreFrom.UseVisualStyleBackColor = false;
            // 
            // btnUndoLastRestore
            // 
            btnUndoLastRestore.BackColor = Color.FromArgb(30, 58, 95);
            btnUndoLastRestore.FlatAppearance.BorderSize = 0;
            btnUndoLastRestore.FlatStyle = FlatStyle.Flat;
            btnUndoLastRestore.Font = new Font("Times New Roman", 12F);
            btnUndoLastRestore.IconChar = FontAwesome.Sharp.IconChar.None;
            btnUndoLastRestore.IconColor = Color.Black;
            btnUndoLastRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUndoLastRestore.Location = new Point(673, 223);
            btnUndoLastRestore.Margin = new Padding(2);
            btnUndoLastRestore.Name = "btnUndoLastRestore";
            btnUndoLastRestore.Size = new Size(150, 33);
            btnUndoLastRestore.TabIndex = 34;
            btnUndoLastRestore.Text = "Undo Last Restore";
            btnUndoLastRestore.UseVisualStyleBackColor = false;
            // 
            // btnBackupNow
            // 
            btnBackupNow.BackColor = Color.FromArgb(30, 58, 95);
            btnBackupNow.FlatAppearance.BorderSize = 0;
            btnBackupNow.FlatStyle = FlatStyle.Flat;
            btnBackupNow.Font = new Font("Times New Roman", 12F);
            btnBackupNow.IconChar = FontAwesome.Sharp.IconChar.None;
            btnBackupNow.IconColor = Color.Black;
            btnBackupNow.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBackupNow.Location = new Point(474, 129);
            btnBackupNow.Margin = new Padding(2);
            btnBackupNow.Name = "btnBackupNow";
            btnBackupNow.Size = new Size(150, 33);
            btnBackupNow.TabIndex = 33;
            btnBackupNow.Text = "Backup Now";
            btnBackupNow.UseVisualStyleBackColor = false;
            // 
            // btnRestoreLatest
            // 
            btnRestoreLatest.BackColor = Color.FromArgb(30, 58, 95);
            btnRestoreLatest.FlatAppearance.BorderSize = 0;
            btnRestoreLatest.FlatStyle = FlatStyle.Flat;
            btnRestoreLatest.Font = new Font("Times New Roman", 12F);
            btnRestoreLatest.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRestoreLatest.IconColor = Color.Black;
            btnRestoreLatest.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRestoreLatest.Location = new Point(474, 223);
            btnRestoreLatest.Margin = new Padding(2);
            btnRestoreLatest.Name = "btnRestoreLatest";
            btnRestoreLatest.Size = new Size(150, 33);
            btnRestoreLatest.TabIndex = 32;
            btnRestoreLatest.Text = "Restore Latest Backup";
            btnRestoreLatest.UseVisualStyleBackColor = false;
            // 
            // numKeepLast
            // 
            numKeepLast.Location = new Point(29, 368);
            numKeepLast.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numKeepLast.Name = "numKeepLast";
            numKeepLast.Size = new Size(120, 23);
            numKeepLast.TabIndex = 31;
            numKeepLast.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // dtpBackupTime
            // 
            dtpBackupTime.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpBackupTime.Format = DateTimePickerFormat.Time;
            dtpBackupTime.Location = new Point(29, 166);
            dtpBackupTime.Name = "dtpBackupTime";
            dtpBackupTime.ShowUpDown = true;
            dtpBackupTime.Size = new Size(121, 26);
            dtpBackupTime.TabIndex = 30;
            // 
            // cbBackupFrequency
            // 
            cbBackupFrequency.BackColor = Color.FromArgb(30, 58, 95);
            cbBackupFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBackupFrequency.FlatStyle = FlatStyle.Flat;
            cbBackupFrequency.Font = new Font("Times New Roman", 13.8F);
            cbBackupFrequency.FormattingEnabled = true;
            cbBackupFrequency.Items.AddRange(new object[] { "Daily", "Weekly", "Monthly" });
            cbBackupFrequency.Location = new Point(26, 265);
            cbBackupFrequency.Margin = new Padding(2);
            cbBackupFrequency.Name = "cbBackupFrequency";
            cbBackupFrequency.Size = new Size(163, 28);
            cbBackupFrequency.TabIndex = 29;
            // 
            // chkEnableAutoBackup
            // 
            chkEnableAutoBackup.AutoSize = true;
            chkEnableAutoBackup.Checked = true;
            chkEnableAutoBackup.CheckState = CheckState.Checked;
            chkEnableAutoBackup.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkEnableAutoBackup.ForeColor = Color.FromArgb(156, 163, 175);
            chkEnableAutoBackup.Location = new Point(29, 81);
            chkEnableAutoBackup.Name = "chkEnableAutoBackup";
            chkEnableAutoBackup.Size = new Size(194, 27);
            chkEnableAutoBackup.TabIndex = 11;
            chkEnableAutoBackup.Text = "Enable auto-backup";
            chkEnableAutoBackup.UseVisualStyleBackColor = true;
            // 
            // lblKeepLast
            // 
            lblKeepLast.AutoSize = true;
            lblKeepLast.Font = new Font("Times New Roman", 15.75F);
            lblKeepLast.ForeColor = Color.FromArgb(156, 163, 175);
            lblKeepLast.Location = new Point(26, 329);
            lblKeepLast.Margin = new Padding(2, 0, 2, 0);
            lblKeepLast.Name = "lblKeepLast";
            lblKeepLast.Size = new Size(103, 23);
            lblKeepLast.TabIndex = 10;
            lblKeepLast.Text = "Keep last : ";
            // 
            // lbllBackupFrequency
            // 
            lbllBackupFrequency.AutoSize = true;
            lbllBackupFrequency.Font = new Font("Times New Roman", 15.75F);
            lbllBackupFrequency.ForeColor = Color.FromArgb(156, 163, 175);
            lbllBackupFrequency.Location = new Point(26, 227);
            lbllBackupFrequency.Margin = new Padding(2, 0, 2, 0);
            lbllBackupFrequency.Name = "lbllBackupFrequency";
            lbllBackupFrequency.Size = new Size(96, 23);
            lbllBackupFrequency.TabIndex = 9;
            lbllBackupFrequency.Text = "Frequency";
            // 
            // lblKeepSuffix
            // 
            lblKeepSuffix.AutoSize = true;
            lblKeepSuffix.Font = new Font("Times New Roman", 15.75F);
            lblKeepSuffix.ForeColor = Color.FromArgb(156, 163, 175);
            lblKeepSuffix.Location = new Point(160, 368);
            lblKeepSuffix.Margin = new Padding(2, 0, 2, 0);
            lblKeepSuffix.Name = "lblKeepSuffix";
            lblKeepSuffix.Size = new Size(82, 23);
            lblKeepSuffix.TabIndex = 8;
            lblKeepSuffix.Text = "backups.";
            // 
            // lbllBackupTime
            // 
            lbllBackupTime.AutoSize = true;
            lbllBackupTime.Font = new Font("Times New Roman", 15.75F);
            lbllBackupTime.ForeColor = Color.FromArgb(156, 163, 175);
            lbllBackupTime.Location = new Point(26, 129);
            lbllBackupTime.Margin = new Padding(2, 0, 2, 0);
            lbllBackupTime.Name = "lbllBackupTime";
            lbllBackupTime.Size = new Size(120, 23);
            lbllBackupTime.TabIndex = 7;
            lbllBackupTime.Text = "Backup Time";
            // 
            // lblBkTitle
            // 
            lblBkTitle.AutoSize = true;
            lblBkTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBkTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblBkTitle.Location = new Point(12, 22);
            lblBkTitle.Margin = new Padding(2, 0, 2, 0);
            lblBkTitle.Name = "lblBkTitle";
            lblBkTitle.Size = new Size(272, 32);
            lblBkTitle.TabIndex = 6;
            lblBkTitle.Text = "Backups and Restore";
            // 
            // pnlMaintenance_Max
            // 
            pnlMaintenance_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlMaintenance_Max.Location = new Point(15, 2156);
            pnlMaintenance_Max.Margin = new Padding(2);
            pnlMaintenance_Max.Name = "pnlMaintenance_Max";
            pnlMaintenance_Max.Size = new Size(1651, 321);
            pnlMaintenance_Max.TabIndex = 25;
            pnlMaintenance_Max.Visible = false;
            // 
            // pnlMaintenance
            // 
            pnlMaintenance.BackColor = Color.FromArgb(27, 38, 59);
            pnlMaintenance.Controls.Add(btnOpenLogsFolder);
            pnlMaintenance.Controls.Add(txtDbPath);
            pnlMaintenance.Controls.Add(txtBackupPath);
            pnlMaintenance.Controls.Add(btnCompactDatabase);
            pnlMaintenance.Controls.Add(btnValidateDatabase);
            pnlMaintenance.Controls.Add(lblBackupPath);
            pnlMaintenance.Controls.Add(lblDbPath);
            pnlMaintenance.Controls.Add(lblMaintTitle);
            pnlMaintenance.Location = new Point(15, 2156);
            pnlMaintenance.Margin = new Padding(2);
            pnlMaintenance.Name = "pnlMaintenance";
            pnlMaintenance.Size = new Size(922, 321);
            pnlMaintenance.TabIndex = 24;
            // 
            // btnOpenLogsFolder
            // 
            btnOpenLogsFolder.BackColor = Color.FromArgb(30, 58, 95);
            btnOpenLogsFolder.FlatAppearance.BorderSize = 0;
            btnOpenLogsFolder.FlatStyle = FlatStyle.Flat;
            btnOpenLogsFolder.Font = new Font("Times New Roman", 12F);
            btnOpenLogsFolder.IconChar = FontAwesome.Sharp.IconChar.None;
            btnOpenLogsFolder.IconColor = Color.Black;
            btnOpenLogsFolder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenLogsFolder.Location = new Point(563, 96);
            btnOpenLogsFolder.Margin = new Padding(2);
            btnOpenLogsFolder.Name = "btnOpenLogsFolder";
            btnOpenLogsFolder.Size = new Size(150, 33);
            btnOpenLogsFolder.TabIndex = 40;
            btnOpenLogsFolder.Text = "Open Logs Folder";
            btnOpenLogsFolder.UseVisualStyleBackColor = false;
            // 
            // txtDbPath
            // 
            txtDbPath.BackColor = Color.FromArgb(30, 58, 95);
            txtDbPath.BorderStyle = BorderStyle.None;
            txtDbPath.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDbPath.Location = new Point(19, 108);
            txtDbPath.Margin = new Padding(2);
            txtDbPath.Name = "txtDbPath";
            txtDbPath.PlaceholderText = "Place";
            txtDbPath.ReadOnly = true;
            txtDbPath.Size = new Size(207, 32);
            txtDbPath.TabIndex = 39;
            txtDbPath.Text = "   ";
            // 
            // txtBackupPath
            // 
            txtBackupPath.BackColor = Color.FromArgb(30, 58, 95);
            txtBackupPath.BorderStyle = BorderStyle.None;
            txtBackupPath.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBackupPath.Location = new Point(19, 235);
            txtBackupPath.Margin = new Padding(2);
            txtBackupPath.Name = "txtBackupPath";
            txtBackupPath.PlaceholderText = "Place";
            txtBackupPath.ReadOnly = true;
            txtBackupPath.Size = new Size(207, 32);
            txtBackupPath.TabIndex = 38;
            txtBackupPath.Text = "   ";
            // 
            // btnCompactDatabase
            // 
            btnCompactDatabase.BackColor = Color.FromArgb(30, 58, 95);
            btnCompactDatabase.FlatAppearance.BorderSize = 0;
            btnCompactDatabase.FlatStyle = FlatStyle.Flat;
            btnCompactDatabase.Font = new Font("Times New Roman", 12F);
            btnCompactDatabase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCompactDatabase.IconColor = Color.Black;
            btnCompactDatabase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCompactDatabase.Location = new Point(563, 234);
            btnCompactDatabase.Margin = new Padding(2);
            btnCompactDatabase.Name = "btnCompactDatabase";
            btnCompactDatabase.Size = new Size(150, 33);
            btnCompactDatabase.TabIndex = 37;
            btnCompactDatabase.Text = "Compact Database";
            btnCompactDatabase.UseVisualStyleBackColor = false;
            // 
            // btnValidateDatabase
            // 
            btnValidateDatabase.BackColor = Color.FromArgb(30, 58, 95);
            btnValidateDatabase.FlatAppearance.BorderSize = 0;
            btnValidateDatabase.FlatStyle = FlatStyle.Flat;
            btnValidateDatabase.Font = new Font("Times New Roman", 12F);
            btnValidateDatabase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnValidateDatabase.IconColor = Color.Black;
            btnValidateDatabase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnValidateDatabase.Location = new Point(563, 165);
            btnValidateDatabase.Margin = new Padding(2);
            btnValidateDatabase.Name = "btnValidateDatabase";
            btnValidateDatabase.Size = new Size(150, 33);
            btnValidateDatabase.TabIndex = 36;
            btnValidateDatabase.Text = "Validate Database";
            btnValidateDatabase.UseVisualStyleBackColor = false;
            // 
            // lblBackupPath
            // 
            lblBackupPath.AutoSize = true;
            lblBackupPath.Font = new Font("Times New Roman", 15.75F);
            lblBackupPath.ForeColor = Color.FromArgb(156, 163, 175);
            lblBackupPath.Location = new Point(19, 187);
            lblBackupPath.Margin = new Padding(2, 0, 2, 0);
            lblBackupPath.Name = "lblBackupPath";
            lblBackupPath.Size = new Size(115, 23);
            lblBackupPath.TabIndex = 12;
            lblBackupPath.Text = "Backup Path";
            // 
            // lblDbPath
            // 
            lblDbPath.AutoSize = true;
            lblDbPath.Font = new Font("Times New Roman", 15.75F);
            lblDbPath.ForeColor = Color.FromArgb(156, 163, 175);
            lblDbPath.Location = new Point(19, 71);
            lblDbPath.Margin = new Padding(2, 0, 2, 0);
            lblDbPath.Name = "lblDbPath";
            lblDbPath.Size = new Size(127, 23);
            lblDbPath.TabIndex = 11;
            lblDbPath.Text = "Database Path";
            // 
            // lblMaintTitle
            // 
            lblMaintTitle.AutoSize = true;
            lblMaintTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMaintTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblMaintTitle.Location = new Point(14, 17);
            lblMaintTitle.Margin = new Padding(2, 0, 2, 0);
            lblMaintTitle.Name = "lblMaintTitle";
            lblMaintTitle.Size = new Size(359, 32);
            lblMaintTitle.TabIndex = 7;
            lblMaintTitle.Text = "Maintenance and Data Path";
            // 
            // pnlImportExport_Max
            // 
            pnlImportExport_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlImportExport_Max.Location = new Point(15, 2496);
            pnlImportExport_Max.Margin = new Padding(2);
            pnlImportExport_Max.Name = "pnlImportExport_Max";
            pnlImportExport_Max.Size = new Size(1651, 309);
            pnlImportExport_Max.TabIndex = 27;
            pnlImportExport_Max.Visible = false;
            // 
            // pnlImportExport
            // 
            pnlImportExport.BackColor = Color.FromArgb(27, 38, 59);
            pnlImportExport.Controls.Add(btnImport);
            pnlImportExport.Controls.Add(btnExport);
            pnlImportExport.Controls.Add(btnDownloadRecoveryTemplate);
            pnlImportExport.Controls.Add(btnDownloadJournalTemplate);
            pnlImportExport.Controls.Add(lblIEInfo);
            pnlImportExport.Controls.Add(lblIETitle);
            pnlImportExport.Location = new Point(15, 2496);
            pnlImportExport.Margin = new Padding(2);
            pnlImportExport.Name = "pnlImportExport";
            pnlImportExport.Size = new Size(922, 309);
            pnlImportExport.TabIndex = 26;
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(30, 58, 95);
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Times New Roman", 12F);
            btnImport.IconChar = FontAwesome.Sharp.IconChar.None;
            btnImport.IconColor = Color.Black;
            btnImport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImport.Location = new Point(332, 159);
            btnImport.Margin = new Padding(2);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(150, 33);
            btnImport.TabIndex = 44;
            btnImport.Text = "Import…";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(30, 58, 95);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Times New Roman", 12F);
            btnExport.IconChar = FontAwesome.Sharp.IconChar.None;
            btnExport.IconColor = Color.Black;
            btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExport.Location = new Point(56, 159);
            btnExport.Margin = new Padding(2);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 33);
            btnExport.TabIndex = 43;
            btnExport.Text = "Export…";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnDownloadRecoveryTemplate
            // 
            btnDownloadRecoveryTemplate.BackColor = Color.FromArgb(30, 58, 95);
            btnDownloadRecoveryTemplate.FlatAppearance.BorderSize = 0;
            btnDownloadRecoveryTemplate.FlatStyle = FlatStyle.Flat;
            btnDownloadRecoveryTemplate.Font = new Font("Times New Roman", 12F);
            btnDownloadRecoveryTemplate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDownloadRecoveryTemplate.IconColor = Color.Black;
            btnDownloadRecoveryTemplate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDownloadRecoveryTemplate.Location = new Point(298, 238);
            btnDownloadRecoveryTemplate.Margin = new Padding(2);
            btnDownloadRecoveryTemplate.Name = "btnDownloadRecoveryTemplate";
            btnDownloadRecoveryTemplate.Size = new Size(223, 33);
            btnDownloadRecoveryTemplate.TabIndex = 42;
            btnDownloadRecoveryTemplate.Text = "Download Recovery Templates";
            btnDownloadRecoveryTemplate.UseVisualStyleBackColor = false;
            // 
            // btnDownloadJournalTemplate
            // 
            btnDownloadJournalTemplate.BackColor = Color.FromArgb(30, 58, 95);
            btnDownloadJournalTemplate.FlatAppearance.BorderSize = 0;
            btnDownloadJournalTemplate.FlatStyle = FlatStyle.Flat;
            btnDownloadJournalTemplate.Font = new Font("Times New Roman", 12F);
            btnDownloadJournalTemplate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDownloadJournalTemplate.IconColor = Color.Black;
            btnDownloadJournalTemplate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDownloadJournalTemplate.Location = new Point(26, 238);
            btnDownloadJournalTemplate.Margin = new Padding(2);
            btnDownloadJournalTemplate.Name = "btnDownloadJournalTemplate";
            btnDownloadJournalTemplate.Size = new Size(211, 33);
            btnDownloadJournalTemplate.TabIndex = 41;
            btnDownloadJournalTemplate.Text = "Download Journal Template";
            btnDownloadJournalTemplate.UseVisualStyleBackColor = false;
            // 
            // lblIEInfo
            // 
            lblIEInfo.Font = new Font("Times New Roman", 15.75F);
            lblIEInfo.ForeColor = Color.FromArgb(156, 163, 175);
            lblIEInfo.Location = new Point(19, 76);
            lblIEInfo.Margin = new Padding(2, 0, 2, 0);
            lblIEInfo.Name = "lblIEInfo";
            lblIEInfo.Size = new Size(524, 59);
            lblIEInfo.TabIndex = 12;
            lblIEInfo.Text = "Export Journal / Recovery / Statistics to Excel, CSV, or PDF. Import Journal or Recovery from Excel/CSV.";
            // 
            // lblIETitle
            // 
            lblIETitle.AutoSize = true;
            lblIETitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIETitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblIETitle.Location = new Point(14, 16);
            lblIETitle.Margin = new Padding(2, 0, 2, 0);
            lblIETitle.Name = "lblIETitle";
            lblIETitle.Size = new Size(207, 32);
            lblIETitle.TabIndex = 8;
            lblIETitle.Text = "Import / Export";
            // 
            // pnlScroll
            // 
            pnlScroll.AutoScroll = true;
            pnlScroll.Controls.Add(pnlImportExport);
            pnlScroll.Controls.Add(pnlImportExport_Max);
            pnlScroll.Controls.Add(pnlMaintenance);
            pnlScroll.Controls.Add(pnlMaintenance_Max);
            pnlScroll.Controls.Add(pnlBackupRestore);
            pnlScroll.Controls.Add(pnlBackupRestore_Max);
            pnlScroll.Controls.Add(pnlRecycleBin);
            pnlScroll.Controls.Add(pnlRecycleBin_Max);
            pnlScroll.Controls.Add(pnlThemeAndNotifications);
            pnlScroll.Controls.Add(pnlThemeAndNotifications_Max);
            pnlScroll.Controls.Add(pnlAccountMoney);
            pnlScroll.Controls.Add(pnlAccountMoney_Max);
            pnlScroll.Dock = DockStyle.Fill;
            pnlScroll.Location = new Point(0, 0);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(1699, 2816);
            pnlScroll.TabIndex = 0;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlScroll);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmSettings";
            Size = new Size(1699, 2816);
            pnlAccountMoney.ResumeLayout(false);
            pnlAccountMoney.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccountHistory).EndInit();
            pnlThemeAndNotifications.ResumeLayout(false);
            pnlThemeAndNotifications.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRecoveryMilestone).EndInit();
            pnlRecycleBin.ResumeLayout(false);
            pnlRecycleBin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRetentionDays).EndInit();
            pnlBackupRestore.ResumeLayout(false);
            pnlBackupRestore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepLast).EndInit();
            pnlMaintenance.ResumeLayout(false);
            pnlMaintenance.PerformLayout();
            pnlImportExport.ResumeLayout(false);
            pnlImportExport.PerformLayout();
            pnlScroll.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAccountMoney_Max;
        private Panel pnlAccountMoney;
        private FontAwesome.Sharp.IconButton btnResetBalance;
        private FontAwesome.Sharp.IconButton btnChangeBalance;
        private TextBox txtNewAccountBalance;
        private Label lblCurrentBalance;
        private Label lblAccNew;
        private Label lblAccCurrent;
        private Label lblAccTitle;
        private Panel pnlThemeAndNotifications_Max;
        private Panel pnlThemeAndNotifications;
        private Label lblRecoveryMilestoneSuffix;
        private NumericUpDown numRecoveryMilestone;
        private Label lblRecoveryMilestoneCaption;
        private CheckBox chkNotifyRecoveryMilestone;
        private RadioButton rbLightMode;
        private RadioButton rbDarkMode;
        private Label lblThemeAndNotificationsHeader;
        private Panel pnlRecycleBin_Max;
        private Panel pnlRecycleBin;
        private Panel pnlBackupRestore_Max;
        private Panel pnlBackupRestore;
        private FontAwesome.Sharp.IconButton btnOpenBackupFolder;
        private FontAwesome.Sharp.IconButton btnRestoreFrom;
        private FontAwesome.Sharp.IconButton btnUndoLastRestore;
        private FontAwesome.Sharp.IconButton btnBackupNow;
        private FontAwesome.Sharp.IconButton btnRestoreLatest;
        private NumericUpDown numKeepLast;
        private DateTimePicker dtpBackupTime;
        private ComboBox cbBackupFrequency;
        private CheckBox chkEnableAutoBackup;
        private Label lblKeepLast;
        private Label lbllBackupFrequency;
        private Label lblKeepSuffix;
        private Label lbllBackupTime;
        private Label lblBkTitle;
        private Panel pnlMaintenance_Max;
        private Panel pnlMaintenance;
        private FontAwesome.Sharp.IconButton btnOpenLogsFolder;
        private TextBox txtDbPath;
        private TextBox txtBackupPath;
        private FontAwesome.Sharp.IconButton btnCompactDatabase;
        private FontAwesome.Sharp.IconButton btnValidateDatabase;
        private Label lblBackupPath;
        private Label lblDbPath;
        private Label lblMaintTitle;
        private Panel pnlImportExport_Max;
        private Panel pnlImportExport;
        private FontAwesome.Sharp.IconButton btnImport;
        private FontAwesome.Sharp.IconButton btnExport;
        private FontAwesome.Sharp.IconButton btnDownloadRecoveryTemplate;
        private FontAwesome.Sharp.IconButton btnDownloadJournalTemplate;
        private Label lblIEInfo;
        private Label lblIETitle;
        private Panel pnlScroll;
        private RadioButton rbWithdraw;
        private RadioButton rbDeposit;
        private DataGridView dgvAccountHistory;
        private Label lblAcctHistoryNote;
        private Label lblAcctHistoryHeader;
        private LinkLabel lnkExportAllTransactions;
        private Label lblRecycleBinHeader;
        private Label lblRecycleBinDesc;
        private Label lblRecycleBinInfo;
        private Label lblRetentionCaption;
        private Label lblRetentionHint;
        private FontAwesome.Sharp.IconButton btnOpenRecycleBin;
        private FontAwesome.Sharp.IconButton btnEmptyRecycleBin;
        private CheckBox chkNotifyDailySummary;
        private CheckBox chkNotifyBackupStatus;
        private Label lblBackupNotifyHint;
        private FontAwesome.Sharp.IconButton btnTestNotifications;
        private NumericUpDown numRetentionDays;
        private Label lblLastBackup;
        private RadioButton rbMaximized;
        private RadioButton rbNormal;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
    }
}
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
            label1 = new Label();
            panel1 = new Panel();
            btnRestoreFrom = new FontAwesome.Sharp.IconButton();
            btnRestoreLatest = new FontAwesome.Sharp.IconButton();
            btnBackupNow = new FontAwesome.Sharp.IconButton();
            btnReset = new FontAwesome.Sharp.IconButton();
            lblCurrentBalance = new Label();
            label2 = new Label();
            rbLightMode = new RadioButton();
            rbDarkMode = new RadioButton();
            btnChangeBalance = new FontAwesome.Sharp.IconButton();
            txtNewAccountBalance = new TextBox();
            panel2 = new Panel();
            btnUndoLastRestore = new FontAwesome.Sharp.IconButton();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(156, 163, 175);
            label1.Location = new Point(39, 159);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(378, 33);
            label1.TabIndex = 7;
            label1.Text = "Enter your new Account Balance";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(27, 38, 59);
            panel1.Controls.Add(btnUndoLastRestore);
            panel1.Controls.Add(btnRestoreFrom);
            panel1.Controls.Add(btnRestoreLatest);
            panel1.Controls.Add(btnBackupNow);
            panel1.Controls.Add(btnReset);
            panel1.Controls.Add(lblCurrentBalance);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(rbLightMode);
            panel1.Controls.Add(rbDarkMode);
            panel1.Controls.Add(btnChangeBalance);
            panel1.Controls.Add(txtNewAccountBalance);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(23, 28);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1054, 813);
            panel1.TabIndex = 14;
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
            btnRestoreFrom.Location = new Point(634, 264);
            btnRestoreFrom.Margin = new Padding(2, 3, 2, 3);
            btnRestoreFrom.Name = "btnRestoreFrom";
            btnRestoreFrom.Size = new Size(157, 43);
            btnRestoreFrom.TabIndex = 19;
            btnRestoreFrom.Text = "Restore From";
            btnRestoreFrom.UseVisualStyleBackColor = false;
            btnRestoreFrom.Click += btnRestoreFrom_Click;
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
            btnRestoreLatest.Location = new Point(634, 167);
            btnRestoreLatest.Margin = new Padding(2, 3, 2, 3);
            btnRestoreLatest.Name = "btnRestoreLatest";
            btnRestoreLatest.Size = new Size(200, 43);
            btnRestoreLatest.TabIndex = 18;
            btnRestoreLatest.Text = "Restore Latest Backup";
            btnRestoreLatest.UseVisualStyleBackColor = false;
            btnRestoreLatest.Click += btnRestoreLatest_Click;
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
            btnBackupNow.Location = new Point(634, 70);
            btnBackupNow.Margin = new Padding(2, 3, 2, 3);
            btnBackupNow.Name = "btnBackupNow";
            btnBackupNow.Size = new Size(157, 43);
            btnBackupNow.TabIndex = 17;
            btnBackupNow.Text = "Backup Now";
            btnBackupNow.UseVisualStyleBackColor = false;
            btnBackupNow.Click += btnBackupNow_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(30, 58, 95);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Times New Roman", 12F);
            btnReset.IconChar = FontAwesome.Sharp.IconChar.None;
            btnReset.IconColor = Color.Black;
            btnReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReset.Location = new Point(369, 71);
            btnReset.Margin = new Padding(2, 3, 2, 3);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(157, 43);
            btnReset.TabIndex = 16;
            btnReset.Text = "Reset Balance";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // lblCurrentBalance
            // 
            lblCurrentBalance.AutoSize = true;
            lblCurrentBalance.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblCurrentBalance.Location = new Point(55, 80);
            lblCurrentBalance.Margin = new Padding(2, 0, 2, 0);
            lblCurrentBalance.Name = "lblCurrentBalance";
            lblCurrentBalance.Size = new Size(33, 33);
            lblCurrentBalance.TabIndex = 15;
            lblCurrentBalance.Text = "--";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(156, 163, 175);
            label2.Location = new Point(39, 33);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(261, 33);
            label2.TabIndex = 14;
            label2.Text = "Current Balance Input";
            // 
            // rbLightMode
            // 
            rbLightMode.AutoSize = true;
            rbLightMode.Font = new Font("Times New Roman", 16.2F);
            rbLightMode.ForeColor = Color.FromArgb(156, 163, 175);
            rbLightMode.Location = new Point(150, 307);
            rbLightMode.Name = "rbLightMode";
            rbLightMode.Size = new Size(95, 37);
            rbLightMode.TabIndex = 13;
            rbLightMode.TabStop = true;
            rbLightMode.Text = "Light";
            rbLightMode.UseVisualStyleBackColor = true;
            rbLightMode.CheckedChanged += rbTheme_CheckedChanged;
            // 
            // rbDarkMode
            // 
            rbDarkMode.AutoSize = true;
            rbDarkMode.Font = new Font("Times New Roman", 16.2F);
            rbDarkMode.ForeColor = Color.FromArgb(156, 163, 175);
            rbDarkMode.Location = new Point(37, 307);
            rbDarkMode.Name = "rbDarkMode";
            rbDarkMode.Size = new Size(91, 37);
            rbDarkMode.TabIndex = 12;
            rbDarkMode.TabStop = true;
            rbDarkMode.Text = "Dark";
            rbDarkMode.UseVisualStyleBackColor = true;
            rbDarkMode.CheckedChanged += rbTheme_CheckedChanged;
            // 
            // btnChangeBalance
            // 
            btnChangeBalance.BackColor = Color.FromArgb(30, 58, 95);
            btnChangeBalance.FlatAppearance.BorderSize = 0;
            btnChangeBalance.FlatStyle = FlatStyle.Flat;
            btnChangeBalance.Font = new Font("Times New Roman", 12F);
            btnChangeBalance.IconChar = FontAwesome.Sharp.IconChar.None;
            btnChangeBalance.IconColor = Color.Black;
            btnChangeBalance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnChangeBalance.Location = new Point(369, 217);
            btnChangeBalance.Margin = new Padding(2, 3, 2, 3);
            btnChangeBalance.Name = "btnChangeBalance";
            btnChangeBalance.Size = new Size(157, 43);
            btnChangeBalance.TabIndex = 11;
            btnChangeBalance.Text = "Change Balance";
            btnChangeBalance.UseVisualStyleBackColor = false;
            btnChangeBalance.Click += btnChangeBalance_Click;
            // 
            // txtNewAccountBalance
            // 
            txtNewAccountBalance.BackColor = Color.FromArgb(30, 58, 95);
            txtNewAccountBalance.BorderStyle = BorderStyle.None;
            txtNewAccountBalance.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNewAccountBalance.Location = new Point(39, 221);
            txtNewAccountBalance.Margin = new Padding(3, 4, 3, 4);
            txtNewAccountBalance.Name = "txtNewAccountBalance";
            txtNewAccountBalance.PlaceholderText = "Account Balance";
            txtNewAccountBalance.Size = new Size(277, 39);
            txtNewAccountBalance.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(27, 38, 59);
            panel2.Location = new Point(23, 28);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1887, 1261);
            panel2.TabIndex = 15;
            panel2.Visible = false;
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
            btnUndoLastRestore.Location = new Point(867, 167);
            btnUndoLastRestore.Margin = new Padding(2, 3, 2, 3);
            btnUndoLastRestore.Name = "btnUndoLastRestore";
            btnUndoLastRestore.Size = new Size(170, 43);
            btnUndoLastRestore.TabIndex = 20;
            btnUndoLastRestore.Text = "Undo Last Restore";
            btnUndoLastRestore.UseVisualStyleBackColor = false;
            btnUndoLastRestore.Click += btnUndoLastRestore_Click;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1942, 1102);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmSettings";
            Text = "FrmSettings";
            Load += FrmSettings_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private TextBox txtNewAccountBalance;
        private FontAwesome.Sharp.IconButton btnChangeBalance;
        private RadioButton rbLightMode;
        private RadioButton rbDarkMode;
        private Panel panel2;
        private Label lblCurrentBalance;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnReset;
        private FontAwesome.Sharp.IconButton btnRestoreFrom;
        private FontAwesome.Sharp.IconButton btnRestoreLatest;
        private FontAwesome.Sharp.IconButton btnBackupNow;
        private FontAwesome.Sharp.IconButton btnUndoLastRestore;
    }
}
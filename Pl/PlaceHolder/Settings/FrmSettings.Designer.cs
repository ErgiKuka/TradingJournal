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
            rbLightMode = new RadioButton();
            rbDarkMode = new RadioButton();
            btnChangeBalance = new FontAwesome.Sharp.IconButton();
            txtNewAccountBalance = new TextBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(156, 163, 175);
            label1.Location = new Point(32, 29);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(300, 25);
            label1.TabIndex = 7;
            label1.Text = "Enter your new Account Balance";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(27, 38, 59);
            panel1.Controls.Add(rbLightMode);
            panel1.Controls.Add(rbDarkMode);
            panel1.Controls.Add(btnChangeBalance);
            panel1.Controls.Add(txtNewAccountBalance);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(20, 21);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(922, 610);
            panel1.TabIndex = 14;
            // 
            // rbLightMode
            // 
            rbLightMode.AutoSize = true;
            rbLightMode.Font = new Font("Times New Roman", 16.2F);
            rbLightMode.ForeColor = Color.FromArgb(156, 163, 175);
            rbLightMode.Location = new Point(131, 186);
            rbLightMode.Margin = new Padding(3, 2, 3, 2);
            rbLightMode.Name = "rbLightMode";
            rbLightMode.Size = new Size(74, 29);
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
            rbDarkMode.Location = new Point(32, 186);
            rbDarkMode.Margin = new Padding(3, 2, 3, 2);
            rbDarkMode.Name = "rbDarkMode";
            rbDarkMode.Size = new Size(74, 29);
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
            btnChangeBalance.Location = new Point(321, 73);
            btnChangeBalance.Margin = new Padding(2);
            btnChangeBalance.Name = "btnChangeBalance";
            btnChangeBalance.Size = new Size(137, 32);
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
            txtNewAccountBalance.Location = new Point(32, 76);
            txtNewAccountBalance.Name = "txtNewAccountBalance";
            txtNewAccountBalance.PlaceholderText = "Account Balance";
            txtNewAccountBalance.Size = new Size(242, 32);
            txtNewAccountBalance.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(27, 38, 59);
            panel2.Location = new Point(20, 21);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1651, 946);
            panel2.TabIndex = 15;
            panel2.Visible = false;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1707, 1003);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
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
    }
}
namespace TradingJournal.Pl.Skeleton
{
    partial class FrmHome
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
            pnlNavigation = new Panel();
            btnDashboard = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            pnlControll = new Panel();
            BtnMinimize = new Button();
            BtnExit = new Button();
            panel2 = new Panel();
            btnJournal = new FontAwesome.Sharp.IconButton();
            btnStatistics = new FontAwesome.Sharp.IconButton();
            panel3 = new Panel();
            btnSettings = new FontAwesome.Sharp.IconButton();
            panel4 = new Panel();
            pnlNavigation.SuspendLayout();
            pnlControll.SuspendLayout();
            SuspendLayout();
            // 
            // pnlNavigation
            // 
            pnlNavigation.Controls.Add(btnSettings);
            pnlNavigation.Controls.Add(panel4);
            pnlNavigation.Controls.Add(btnStatistics);
            pnlNavigation.Controls.Add(panel3);
            pnlNavigation.Controls.Add(btnJournal);
            pnlNavigation.Controls.Add(panel2);
            pnlNavigation.Controls.Add(btnDashboard);
            pnlNavigation.Controls.Add(panel1);
            pnlNavigation.Dock = DockStyle.Left;
            pnlNavigation.Location = new Point(0, 0);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Size = new Size(266, 900);
            pnlNavigation.TabIndex = 2;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(27, 38, 59);
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDashboard.ForeColor = Color.FromArgb(156, 163, 175);
            btnDashboard.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDashboard.IconColor = Color.Black;
            btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDashboard.Location = new Point(0, 85);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(266, 58);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.MouseEnter += MouseEnterEffect;
            btnDashboard.MouseLeave += MouseLeaveEffect;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(266, 85);
            panel1.TabIndex = 0;
            // 
            // pnlControll
            // 
            pnlControll.Controls.Add(BtnMinimize);
            pnlControll.Controls.Add(BtnExit);
            pnlControll.Dock = DockStyle.Fill;
            pnlControll.Location = new Point(266, 0);
            pnlControll.Name = "pnlControll";
            pnlControll.Size = new Size(1234, 900);
            pnlControll.TabIndex = 3;
            // 
            // BtnMinimize
            // 
            BtnMinimize.BackColor = Color.DarkGray;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Location = new Point(1127, 5);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(47, 27);
            BtnMinimize.TabIndex = 3;
            BtnMinimize.Text = "―";
            BtnMinimize.UseVisualStyleBackColor = false;
            BtnMinimize.Click += BtnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.BackColor = Color.Brown;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Location = new Point(1174, 5);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(47, 27);
            BtnExit.TabIndex = 2;
            BtnExit.Text = "X";
            BtnExit.UseVisualStyleBackColor = false;
            BtnExit.Click += BtnExit_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 143);
            panel2.Name = "panel2";
            panel2.Size = new Size(266, 10);
            panel2.TabIndex = 2;
            // 
            // btnJournal
            // 
            btnJournal.BackColor = Color.FromArgb(27, 38, 59);
            btnJournal.Dock = DockStyle.Top;
            btnJournal.FlatAppearance.BorderSize = 0;
            btnJournal.FlatStyle = FlatStyle.Flat;
            btnJournal.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnJournal.ForeColor = Color.FromArgb(156, 163, 175);
            btnJournal.IconChar = FontAwesome.Sharp.IconChar.None;
            btnJournal.IconColor = Color.Black;
            btnJournal.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnJournal.Location = new Point(0, 153);
            btnJournal.Margin = new Padding(3, 30, 3, 3);
            btnJournal.Name = "btnJournal";
            btnJournal.Size = new Size(266, 58);
            btnJournal.TabIndex = 3;
            btnJournal.Text = "Journal";
            btnJournal.UseVisualStyleBackColor = false;
            // 
            // btnStatistics
            // 
            btnStatistics.BackColor = Color.FromArgb(27, 38, 59);
            btnStatistics.Dock = DockStyle.Top;
            btnStatistics.FlatAppearance.BorderSize = 0;
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStatistics.ForeColor = Color.FromArgb(156, 163, 175);
            btnStatistics.IconChar = FontAwesome.Sharp.IconChar.None;
            btnStatistics.IconColor = Color.Black;
            btnStatistics.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStatistics.Location = new Point(0, 221);
            btnStatistics.Margin = new Padding(3, 30, 3, 3);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(266, 58);
            btnStatistics.TabIndex = 5;
            btnStatistics.Text = "Statistics";
            btnStatistics.UseVisualStyleBackColor = false;
            btnStatistics.MouseEnter += MouseEnterEffect;
            btnStatistics.MouseLeave += MouseLeaveEffect;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 211);
            panel3.Name = "panel3";
            panel3.Size = new Size(266, 10);
            panel3.TabIndex = 4;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(27, 38, 59);
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSettings.ForeColor = Color.FromArgb(156, 163, 175);
            btnSettings.IconChar = FontAwesome.Sharp.IconChar.None;
            btnSettings.IconColor = Color.Black;
            btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSettings.Location = new Point(0, 289);
            btnSettings.Margin = new Padding(3, 30, 3, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(266, 58);
            btnSettings.TabIndex = 7;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Enter += ActiveButtonEffect;
            btnSettings.MouseEnter += MouseEnterEffect;
            btnSettings.MouseLeave += MouseLeaveEffect;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 279);
            panel4.Name = "panel4";
            panel4.Size = new Size(266, 10);
            panel4.TabIndex = 6;
            // 
            // FrmHome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1500, 900);
            Controls.Add(pnlControll);
            Controls.Add(pnlNavigation);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmHome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmHome";
            Load += FrmHome_Load;
            pnlNavigation.ResumeLayout(false);
            pnlControll.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlNavigation;
        private Panel pnlControll;
        private Button BtnMinimize;
        private Button BtnExit;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnSettings;
        private Panel panel4;
        private FontAwesome.Sharp.IconButton btnStatistics;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton btnJournal;
        private Panel panel2;
    }
}
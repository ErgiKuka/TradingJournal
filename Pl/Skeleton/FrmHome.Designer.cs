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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            panel1 = new Panel();
            pnlTopBar = new Panel();
            panel6 = new Panel();
            btnMaximize = new FontAwesome.Sharp.IconButton();
            BtnExit = new Button();
            BtnMinimize = new Button();
            pnlNavigation = new Panel();
            btnSettings = new FontAwesome.Sharp.IconButton();
            panel4 = new Panel();
            btnStatistics = new FontAwesome.Sharp.IconButton();
            panel3 = new Panel();
            btnJournal = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            btnDashboard = new FontAwesome.Sharp.IconButton();
            panel5 = new Panel();
            pnlControls = new Panel();
            pnlTopBar.SuspendLayout();
            panel6.SuspendLayout();
            pnlNavigation.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2, 2, 2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(4, 720);
            panel1.TabIndex = 4;
            // 
            // pnlTopBar
            // 
            pnlTopBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTopBar.Controls.Add(panel6);
            pnlTopBar.Location = new Point(4, 0);
            pnlTopBar.Margin = new Padding(2, 2, 2, 2);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1196, 30);
            pnlTopBar.TabIndex = 8;
            // 
            // panel6
            // 
            panel6.Controls.Add(btnMaximize);
            panel6.Controls.Add(BtnExit);
            panel6.Controls.Add(BtnMinimize);
            panel6.Dock = DockStyle.Right;
            panel6.Location = new Point(1002, 0);
            panel6.Margin = new Padding(2, 2, 2, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(194, 30);
            panel6.TabIndex = 12;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnMaximize.BackColor = Color.DarkGray;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = FontAwesome.Sharp.IconChar.None;
            btnMaximize.IconColor = Color.Black;
            btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMaximize.Location = new Point(98, 4);
            btnMaximize.Margin = new Padding(2, 2, 2, 2);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(34, 22);
            btnMaximize.TabIndex = 11;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnExit.BackColor = Color.Brown;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Location = new Point(135, 4);
            BtnExit.Margin = new Padding(2, 2, 2, 2);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(34, 22);
            BtnExit.TabIndex = 12;
            BtnExit.Text = "X";
            BtnExit.UseVisualStyleBackColor = false;
            BtnExit.Click += BtnExit_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnMinimize.BackColor = Color.DarkGray;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Location = new Point(60, 4);
            BtnMinimize.Margin = new Padding(2, 2, 2, 2);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(34, 22);
            BtnMinimize.TabIndex = 9;
            BtnMinimize.Text = "―";
            BtnMinimize.UseVisualStyleBackColor = false;
            BtnMinimize.Click += BtnMinimize_Click;
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
            pnlNavigation.Controls.Add(panel5);
            pnlNavigation.Location = new Point(4, 30);
            pnlNavigation.Margin = new Padding(2, 2, 2, 2);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Size = new Size(213, 690);
            pnlNavigation.TabIndex = 9;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(27, 38, 59);
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Times New Roman", 13.8F);
            btnSettings.ForeColor = Color.FromArgb(156, 163, 175);
            btnSettings.IconChar = FontAwesome.Sharp.IconChar.None;
            btnSettings.IconColor = Color.Black;
            btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSettings.Location = new Point(0, 222);
            btnSettings.Margin = new Padding(2, 24, 2, 2);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(213, 46);
            btnSettings.TabIndex = 7;
            btnSettings.Text = "   Settings";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            btnSettings.MouseEnter += MouseEnterEffect;
            btnSettings.MouseLeave += MouseLeaveEffect;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 214);
            panel4.Margin = new Padding(2, 2, 2, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(213, 8);
            panel4.TabIndex = 6;
            // 
            // btnStatistics
            // 
            btnStatistics.BackColor = Color.FromArgb(27, 38, 59);
            btnStatistics.Dock = DockStyle.Top;
            btnStatistics.FlatAppearance.BorderSize = 0;
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Font = new Font("Times New Roman", 13.8F);
            btnStatistics.ForeColor = Color.FromArgb(156, 163, 175);
            btnStatistics.IconChar = FontAwesome.Sharp.IconChar.None;
            btnStatistics.IconColor = Color.Black;
            btnStatistics.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStatistics.Location = new Point(0, 168);
            btnStatistics.Margin = new Padding(2, 24, 2, 2);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(213, 46);
            btnStatistics.TabIndex = 5;
            btnStatistics.Text = "   Statistics";
            btnStatistics.UseVisualStyleBackColor = false;
            btnStatistics.Click += btnStatistics_Click;
            btnStatistics.MouseEnter += MouseEnterEffect;
            btnStatistics.MouseLeave += MouseLeaveEffect;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 160);
            panel3.Margin = new Padding(2, 2, 2, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(213, 8);
            panel3.TabIndex = 4;
            // 
            // btnJournal
            // 
            btnJournal.BackColor = Color.FromArgb(27, 38, 59);
            btnJournal.Dock = DockStyle.Top;
            btnJournal.FlatAppearance.BorderSize = 0;
            btnJournal.FlatStyle = FlatStyle.Flat;
            btnJournal.Font = new Font("Times New Roman", 13.8F);
            btnJournal.ForeColor = Color.FromArgb(156, 163, 175);
            btnJournal.IconChar = FontAwesome.Sharp.IconChar.None;
            btnJournal.IconColor = Color.Black;
            btnJournal.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnJournal.Location = new Point(0, 114);
            btnJournal.Margin = new Padding(2, 24, 2, 2);
            btnJournal.Name = "btnJournal";
            btnJournal.Size = new Size(213, 46);
            btnJournal.TabIndex = 3;
            btnJournal.Text = "   Journal";
            btnJournal.UseVisualStyleBackColor = false;
            btnJournal.Click += btnJournal_Click;
            btnJournal.MouseEnter += MouseEnterEffect;
            btnJournal.MouseLeave += MouseLeaveEffect;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 106);
            panel2.Margin = new Padding(2, 2, 2, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(213, 8);
            panel2.TabIndex = 2;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(27, 38, 59);
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Times New Roman", 13.8F);
            btnDashboard.ForeColor = Color.FromArgb(156, 163, 175);
            btnDashboard.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDashboard.IconColor = Color.Black;
            btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDashboard.Location = new Point(0, 60);
            btnDashboard.Margin = new Padding(2, 2, 2, 2);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(213, 46);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "   Dashboard";
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            btnDashboard.MouseEnter += MouseEnterEffect;
            btnDashboard.MouseLeave += MouseLeaveEffect;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(2, 2, 2, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(213, 60);
            panel5.TabIndex = 0;
            // 
            // pnlControls
            // 
            pnlControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlControls.AutoScroll = true;
            pnlControls.BackColor = Color.FromArgb(13, 27, 42);
            pnlControls.Location = new Point(217, 30);
            pnlControls.Margin = new Padding(2, 2, 2, 2);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(983, 690);
            pnlControls.TabIndex = 10;
            // 
            // FrmHome
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1200, 720);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlNavigation);
            Controls.Add(pnlControls);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 2, 2, 2);
            Name = "FrmHome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trading Journal";
            Load += FrmHome_Load;
            pnlTopBar.ResumeLayout(false);
            panel6.ResumeLayout(false);
            pnlNavigation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel pnlTopBar;
        private Button BtnMinimize;
        private Button BtnExit;
        private Panel pnlNavigation;
        private FontAwesome.Sharp.IconButton btnSettings;
        private Panel panel4;
        private FontAwesome.Sharp.IconButton btnStatistics;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton btnJournal;
        private Panel panel2;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private Panel panel5;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private Panel panel6;
        private Panel pnlControls;
    }
}
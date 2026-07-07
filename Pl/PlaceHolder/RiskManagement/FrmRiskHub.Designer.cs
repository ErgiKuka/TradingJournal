namespace TradingJournal.Pl.PlaceHolder.RiskManagement
{
    partial class FrmRiskHub
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
            menuStripNav = new MenuStrip();
            mnuSlTp = new ToolStripMenuItem();
            mnuPositionSize = new ToolStripMenuItem();
            mnuPartialTp = new ToolStripMenuItem();
            pnlContent = new Panel();
            menuStripNav.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripNav
            // 
            menuStripNav.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStripNav.ImageScalingSize = new Size(20, 20);
            menuStripNav.Items.AddRange(new ToolStripItem[] { mnuSlTp, mnuPositionSize, mnuPartialTp });
            menuStripNav.Location = new Point(0, 0);
            menuStripNav.Name = "menuStripNav";
            menuStripNav.Padding = new Padding(5, 2, 0, 2);
            menuStripNav.Size = new Size(1540, 29);
            menuStripNav.TabIndex = 0;
            menuStripNav.Text = "menuStrip";
            // 
            // mnuSlTp
            // 
            mnuSlTp.Name = "mnuSlTp";
            mnuSlTp.Size = new Size(117, 25);
            mnuSlTp.Text = "Sl,Tp Finder";
            mnuSlTp.Click += mnuSlTp_Click;
            // 
            // mnuPositionSize
            // 
            mnuPositionSize.Name = "mnuPositionSize";
            mnuPositionSize.Size = new Size(109, 25);
            mnuPositionSize.Text = "Risk Finder";
            mnuPositionSize.Click += mnuPositionSize_Click;
            // 
            // mnuPartialTp
            // 
            mnuPartialTp.Name = "mnuPartialTp";
            mnuPartialTp.Size = new Size(96, 25);
            mnuPartialTp.Text = "Partial TP";
            mnuPartialTp.Click += mnuPartialTp_Click;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 29);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1540, 815);
            pnlContent.TabIndex = 1;
            // 
            // FrmRiskHub
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlContent);
            Controls.Add(menuStripNav);
            Margin = new Padding(2);
            Name = "FrmRiskHub";
            Size = new Size(1540, 844);
            menuStripNav.ResumeLayout(false);
            menuStripNav.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStripNav;
        private ToolStripMenuItem mnuSlTp;
        private ToolStripMenuItem mnuPositionSize;
        private ToolStripMenuItem mnuPartialTp;
        private Panel pnlContent;
    }
}
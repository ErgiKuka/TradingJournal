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
            pnlContent = new Panel();
            menuStripNav = new MenuStrip();
            mnuSlTp = new ToolStripMenuItem();
            mnuPositionSize = new ToolStripMenuItem();
            menuStripNav.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 35);
            pnlContent.Margin = new Padding(4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1925, 1020);
            pnlContent.TabIndex = 3;
            // 
            // menuStripNav
            // 
            menuStripNav.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStripNav.ImageScalingSize = new Size(20, 20);
            menuStripNav.Items.AddRange(new ToolStripItem[] { mnuSlTp, mnuPositionSize });
            menuStripNav.Location = new Point(0, 0);
            menuStripNav.Name = "menuStripNav";
            menuStripNav.Size = new Size(1925, 35);
            menuStripNav.TabIndex = 2;
            menuStripNav.Text = "menuStrip";
            // 
            // mnuSlTp
            // 
            mnuSlTp.Name = "mnuSlTp";
            mnuSlTp.Size = new Size(144, 31);
            mnuSlTp.Text = "Sl,Tp Finder";
            // 
            // mnuPositionSize
            // 
            mnuPositionSize.Name = "mnuPositionSize";
            mnuPositionSize.Size = new Size(137, 31);
            mnuPositionSize.Text = "Risk Finder";
            // 
            // FrmRiskHub
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlContent);
            Controls.Add(menuStripNav);
            Margin = new Padding(2);
            Name = "FrmRiskHub";
            Size = new Size(1925, 1055);
            menuStripNav.ResumeLayout(false);
            menuStripNav.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlContent;
        private MenuStrip menuStripNav;
        private ToolStripMenuItem mnuSlTp;
        private ToolStripMenuItem mnuPositionSize;
    }
}
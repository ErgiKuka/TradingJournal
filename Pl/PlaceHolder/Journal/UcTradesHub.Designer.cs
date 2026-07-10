namespace TradingJournal.Pl.PlaceHolder.Journal
{
    partial class UcTradesHub
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlContent = new Panel();
            menuStripNav = new MenuStrip();
            mnuTrading = new ToolStripMenuItem();
            mnuJournal = new ToolStripMenuItem();
            mnuPlatforms = new ToolStripMenuItem();
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
            menuStripNav.Items.AddRange(new ToolStripItem[] { mnuTrading, mnuJournal, mnuPlatforms });
            menuStripNav.Location = new Point(0, 0);
            menuStripNav.Name = "menuStripNav";
            menuStripNav.Size = new Size(1925, 35);
            menuStripNav.TabIndex = 2;
            menuStripNav.Text = "menuStrip";
            // 
            // mnuTrading
            // 
            mnuTrading.Name = "mnuTrading";
            mnuTrading.Size = new Size(99, 31);
            mnuTrading.Text = "Trading";
            // 
            // mnuJournal
            // 
            mnuJournal.Name = "mnuJournal";
            mnuJournal.Size = new Size(96, 31);
            mnuJournal.Text = "Journal";
            // 
            // mnuPlatforms
            // 
            mnuPlatforms.Name = "mnuPlatforms";
            mnuPlatforms.Size = new Size(119, 31);
            mnuPlatforms.Text = "Platforms";
            // 
            // UcTradesHub
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlContent);
            Controls.Add(menuStripNav);
            Name = "UcTradesHub";
            Size = new Size(1925, 1055);
            menuStripNav.ResumeLayout(false);
            menuStripNav.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlContent;
        private MenuStrip menuStripNav;
        private ToolStripMenuItem mnuTrading;
        private ToolStripMenuItem mnuJournal;
        private ToolStripMenuItem mnuPlatforms;
    }
}

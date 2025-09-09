namespace TradingJournal.Pl.PlaceHolder.Journal
{
    partial class FrmImageViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImageViewer));
            pbFullImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbFullImage).BeginInit();
            SuspendLayout();
            // 
            // pbFullImage
            // 
            pbFullImage.BorderStyle = BorderStyle.FixedSingle;
            pbFullImage.Dock = DockStyle.Fill;
            pbFullImage.Location = new Point(0, 0);
            pbFullImage.Name = "pbFullImage";
            pbFullImage.Size = new Size(800, 450);
            pbFullImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbFullImage.TabIndex = 0;
            pbFullImage.TabStop = false;
            // 
            // FrmImageViewer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(800, 450);
            Controls.Add(pbFullImage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmImageViewer";
            ((System.ComponentModel.ISupportInitialize)pbFullImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbFullImage;
    }
}
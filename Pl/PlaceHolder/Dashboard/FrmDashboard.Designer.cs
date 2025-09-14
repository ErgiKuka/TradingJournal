namespace TradingJournal.Pl.PlaceHolder.Dashboard
{
    partial class FrmDashboard
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
            pnlPortfolio = new Panel();
            lblTodaysPnl = new Label();
            lblPortfolioValue = new Label();
            label1 = new Label();
            pnlLivePrices = new Panel();
            btnRefresh = new FontAwesome.Sharp.IconButton();
            lblCurrentDate = new Label();
            pnlPortfolio_Max = new Panel();
            pnlLivePrices_Max = new Panel();
            pnlPortfolio.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPortfolio
            // 
            pnlPortfolio.BackColor = Color.FromArgb(27, 38, 59);
            pnlPortfolio.Controls.Add(lblTodaysPnl);
            pnlPortfolio.Controls.Add(lblPortfolioValue);
            pnlPortfolio.Controls.Add(label1);
            pnlPortfolio.Location = new Point(37, 71);
            pnlPortfolio.Name = "pnlPortfolio";
            pnlPortfolio.Size = new Size(1152, 180);
            pnlPortfolio.TabIndex = 0;
            pnlPortfolio.Paint += panel1_Paint;
            // 
            // lblTodaysPnl
            // 
            lblTodaysPnl.AutoSize = true;
            lblTodaysPnl.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTodaysPnl.ForeColor = Color.FromArgb(156, 163, 175);
            lblTodaysPnl.Location = new Point(933, 97);
            lblTodaysPnl.Name = "lblTodaysPnl";
            lblTodaysPnl.Size = new Size(29, 33);
            lblTodaysPnl.TabIndex = 6;
            lblTodaysPnl.Text = "0";
            // 
            // lblPortfolioValue
            // 
            lblPortfolioValue.AutoSize = true;
            lblPortfolioValue.Font = new Font("Times New Roman", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPortfolioValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblPortfolioValue.Location = new Point(37, 97);
            lblPortfolioValue.Name = "lblPortfolioValue";
            lblPortfolioValue.Size = new Size(60, 46);
            lblPortfolioValue.TabIndex = 5;
            lblPortfolioValue.Text = "$0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(156, 163, 175);
            label1.Location = new Point(37, 37);
            label1.Name = "label1";
            label1.Size = new Size(144, 38);
            label1.TabIndex = 4;
            label1.Text = "Portfolio";
            // 
            // pnlLivePrices
            // 
            pnlLivePrices.BackColor = Color.FromArgb(27, 38, 59);
            pnlLivePrices.Location = new Point(37, 303);
            pnlLivePrices.Name = "pnlLivePrices";
            pnlLivePrices.Size = new Size(1152, 508);
            pnlLivePrices.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(13, 27, 42);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Times New Roman", 12F);
            btnRefresh.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRefresh.IconColor = Color.Black;
            btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefresh.Location = new Point(1139, 23);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(41, 34);
            btnRefresh.TabIndex = 11;
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblCurrentDate
            // 
            lblCurrentDate.AutoSize = true;
            lblCurrentDate.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentDate.ForeColor = Color.FromArgb(156, 163, 175);
            lblCurrentDate.Location = new Point(970, 21);
            lblCurrentDate.Name = "lblCurrentDate";
            lblCurrentDate.Size = new Size(68, 32);
            lblCurrentDate.TabIndex = 3;
            lblCurrentDate.Text = "------";
            // 
            // pnlPortfolio_Max
            // 
            pnlPortfolio_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlPortfolio_Max.Location = new Point(37, 71);
            pnlPortfolio_Max.Name = "pnlPortfolio_Max";
            pnlPortfolio_Max.Size = new Size(1577, 211);
            pnlPortfolio_Max.TabIndex = 12;
            pnlPortfolio_Max.Visible = false;
            // 
            // pnlLivePrices_Max
            // 
            pnlLivePrices_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlLivePrices_Max.Location = new Point(37, 303);
            pnlLivePrices_Max.Name = "pnlLivePrices_Max";
            pnlLivePrices_Max.Size = new Size(1577, 653);
            pnlLivePrices_Max.TabIndex = 13;
            pnlLivePrices_Max.Visible = false;
            // 
            // FrmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1645, 982);
            Controls.Add(btnRefresh);
            Controls.Add(lblCurrentDate);
            Controls.Add(pnlLivePrices);
            Controls.Add(pnlPortfolio);
            Controls.Add(pnlLivePrices_Max);
            Controls.Add(pnlPortfolio_Max);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDashboard";
            Text = "FrmDashboard";
            Load += FrmDashboard_Load;
            pnlPortfolio.ResumeLayout(false);
            pnlPortfolio.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlPortfolio;
        private Panel pnlLivePrices;
        private Label lblPortfolioValue;
        private Label label1;
        private Label lblCurrentDate;
        private Label lblTodaysPnl;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private Panel pnlPortfolio_Max;
        private Panel pnlLivePrices_Max;
    }
}
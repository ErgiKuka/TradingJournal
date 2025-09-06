namespace TradingJournal.Pl.PlaceHolder.Journal
{
    partial class FrmJournal
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
            pnlInformations = new Panel();
            txtScreenshotLink = new TextBox();
            lblMargin = new Label();
            lblProfitLoss = new Label();
            txtMargin = new TextBox();
            txtProfitLoss = new TextBox();
            pbScreenshot = new PictureBox();
            btnAddTrade = new FontAwesome.Sharp.IconButton();
            btnUploadScreenshot = new FontAwesome.Sharp.IconButton();
            lblTakeProfit = new Label();
            lblStopLoss = new Label();
            lblExitPrice = new Label();
            lblEntryPrice = new Label();
            lblTradeType = new Label();
            txtTakeProfit = new TextBox();
            txtStopLoss = new TextBox();
            txtExitPrice = new TextBox();
            txtEntryPrice = new TextBox();
            cbTradeType = new ComboBox();
            lblSymbol = new Label();
            cbSymbol = new ComboBox();
            pnlData = new Panel();
            openFileDialog = new OpenFileDialog();
            pnlInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).BeginInit();
            SuspendLayout();
            // 
            // pnlInformations
            // 
            pnlInformations.BackColor = Color.FromArgb(27, 38, 59);
            pnlInformations.Controls.Add(txtScreenshotLink);
            pnlInformations.Controls.Add(lblMargin);
            pnlInformations.Controls.Add(lblProfitLoss);
            pnlInformations.Controls.Add(txtMargin);
            pnlInformations.Controls.Add(txtProfitLoss);
            pnlInformations.Controls.Add(pbScreenshot);
            pnlInformations.Controls.Add(btnAddTrade);
            pnlInformations.Controls.Add(btnUploadScreenshot);
            pnlInformations.Controls.Add(lblTakeProfit);
            pnlInformations.Controls.Add(lblStopLoss);
            pnlInformations.Controls.Add(lblExitPrice);
            pnlInformations.Controls.Add(lblEntryPrice);
            pnlInformations.Controls.Add(lblTradeType);
            pnlInformations.Controls.Add(txtTakeProfit);
            pnlInformations.Controls.Add(txtStopLoss);
            pnlInformations.Controls.Add(txtExitPrice);
            pnlInformations.Controls.Add(txtEntryPrice);
            pnlInformations.Controls.Add(cbTradeType);
            pnlInformations.Controls.Add(lblSymbol);
            pnlInformations.Controls.Add(cbSymbol);
            pnlInformations.Location = new Point(32, 33);
            pnlInformations.Name = "pnlInformations";
            pnlInformations.Size = new Size(1167, 330);
            pnlInformations.TabIndex = 0;
            // 
            // txtScreenshotLink
            // 
            txtScreenshotLink.BackColor = Color.FromArgb(30, 58, 95);
            txtScreenshotLink.BorderStyle = BorderStyle.FixedSingle;
            txtScreenshotLink.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtScreenshotLink.Location = new Point(886, 108);
            txtScreenshotLink.Name = "txtScreenshotLink";
            txtScreenshotLink.PlaceholderText = "Upload link";
            txtScreenshotLink.Size = new Size(203, 30);
            txtScreenshotLink.TabIndex = 20;
            // 
            // lblMargin
            // 
            lblMargin.AutoSize = true;
            lblMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblMargin.Location = new Point(556, 122);
            lblMargin.Name = "lblMargin";
            lblMargin.Size = new Size(67, 22);
            lblMargin.TabIndex = 19;
            lblMargin.Text = "Margin";
            // 
            // lblProfitLoss
            // 
            lblProfitLoss.AutoSize = true;
            lblProfitLoss.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProfitLoss.ForeColor = Color.FromArgb(156, 163, 175);
            lblProfitLoss.Location = new Point(556, 20);
            lblProfitLoss.Name = "lblProfitLoss";
            lblProfitLoss.Size = new Size(108, 22);
            lblProfitLoss.TabIndex = 18;
            lblProfitLoss.Text = "Profit / Loss";
            // 
            // txtMargin
            // 
            txtMargin.BackColor = Color.FromArgb(30, 58, 95);
            txtMargin.BorderStyle = BorderStyle.FixedSingle;
            txtMargin.Font = new Font("Times New Roman", 13.8F);
            txtMargin.Location = new Point(555, 156);
            txtMargin.Name = "txtMargin";
            txtMargin.Size = new Size(203, 34);
            txtMargin.TabIndex = 17;
            // 
            // txtProfitLoss
            // 
            txtProfitLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtProfitLoss.BorderStyle = BorderStyle.FixedSingle;
            txtProfitLoss.Font = new Font("Times New Roman", 13.8F);
            txtProfitLoss.Location = new Point(556, 53);
            txtProfitLoss.Name = "txtProfitLoss";
            txtProfitLoss.Size = new Size(203, 34);
            txtProfitLoss.TabIndex = 16;
            // 
            // pbScreenshot
            // 
            pbScreenshot.Location = new Point(886, 161);
            pbScreenshot.Name = "pbScreenshot";
            pbScreenshot.Size = new Size(208, 133);
            pbScreenshot.TabIndex = 15;
            pbScreenshot.TabStop = false;
            // 
            // btnAddTrade
            // 
            btnAddTrade.BackColor = Color.FromArgb(30, 58, 95);
            btnAddTrade.FlatAppearance.BorderSize = 0;
            btnAddTrade.FlatStyle = FlatStyle.Flat;
            btnAddTrade.Font = new Font("Times New Roman", 12F);
            btnAddTrade.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAddTrade.IconColor = Color.Black;
            btnAddTrade.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddTrade.Location = new Point(547, 245);
            btnAddTrade.Name = "btnAddTrade";
            btnAddTrade.Size = new Size(219, 49);
            btnAddTrade.TabIndex = 14;
            btnAddTrade.Text = "Add Trade";
            btnAddTrade.UseVisualStyleBackColor = false;
            btnAddTrade.Click += btnAddTrade_Click;
            // 
            // btnUploadScreenshot
            // 
            btnUploadScreenshot.BackColor = Color.FromArgb(30, 58, 95);
            btnUploadScreenshot.FlatAppearance.BorderSize = 0;
            btnUploadScreenshot.FlatStyle = FlatStyle.Flat;
            btnUploadScreenshot.Font = new Font("Times New Roman", 12F);
            btnUploadScreenshot.IconChar = FontAwesome.Sharp.IconChar.None;
            btnUploadScreenshot.IconColor = Color.Black;
            btnUploadScreenshot.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUploadScreenshot.Location = new Point(870, 45);
            btnUploadScreenshot.Name = "btnUploadScreenshot";
            btnUploadScreenshot.Size = new Size(240, 49);
            btnUploadScreenshot.TabIndex = 13;
            btnUploadScreenshot.Text = "Upload Screenshot";
            btnUploadScreenshot.UseVisualStyleBackColor = false;
            btnUploadScreenshot.Click += btnUploadScreenshot_Click;
            // 
            // lblTakeProfit
            // 
            lblTakeProfit.AutoSize = true;
            lblTakeProfit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTakeProfit.ForeColor = Color.FromArgb(156, 163, 175);
            lblTakeProfit.Location = new Point(285, 224);
            lblTakeProfit.Name = "lblTakeProfit";
            lblTakeProfit.Size = new Size(98, 22);
            lblTakeProfit.TabIndex = 10;
            lblTakeProfit.Text = "Take Profit";
            // 
            // lblStopLoss
            // 
            lblStopLoss.AutoSize = true;
            lblStopLoss.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStopLoss.ForeColor = Color.FromArgb(156, 163, 175);
            lblStopLoss.Location = new Point(42, 224);
            lblStopLoss.Name = "lblStopLoss";
            lblStopLoss.Size = new Size(88, 22);
            lblStopLoss.TabIndex = 9;
            lblStopLoss.Text = "Stop Loss";
            // 
            // lblExitPrice
            // 
            lblExitPrice.AutoSize = true;
            lblExitPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExitPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblExitPrice.Location = new Point(285, 120);
            lblExitPrice.Name = "lblExitPrice";
            lblExitPrice.Size = new Size(89, 22);
            lblExitPrice.TabIndex = 9;
            lblExitPrice.Text = "Exit Price";
            // 
            // lblEntryPrice
            // 
            lblEntryPrice.AutoSize = true;
            lblEntryPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblEntryPrice.Location = new Point(42, 120);
            lblEntryPrice.Name = "lblEntryPrice";
            lblEntryPrice.Size = new Size(99, 22);
            lblEntryPrice.TabIndex = 8;
            lblEntryPrice.Text = "Entry Price";
            // 
            // lblTradeType
            // 
            lblTradeType.AutoSize = true;
            lblTradeType.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTradeType.ForeColor = Color.FromArgb(156, 163, 175);
            lblTradeType.Location = new Point(285, 25);
            lblTradeType.Name = "lblTradeType";
            lblTradeType.Size = new Size(100, 22);
            lblTradeType.TabIndex = 7;
            lblTradeType.Text = "Trade Type";
            // 
            // txtTakeProfit
            // 
            txtTakeProfit.BackColor = Color.FromArgb(30, 58, 95);
            txtTakeProfit.BorderStyle = BorderStyle.FixedSingle;
            txtTakeProfit.Font = new Font("Times New Roman", 13.8F);
            txtTakeProfit.Location = new Point(284, 260);
            txtTakeProfit.Name = "txtTakeProfit";
            txtTakeProfit.Size = new Size(203, 34);
            txtTakeProfit.TabIndex = 6;
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtStopLoss.BorderStyle = BorderStyle.FixedSingle;
            txtStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtStopLoss.Location = new Point(42, 260);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(203, 34);
            txtStopLoss.TabIndex = 5;
            // 
            // txtExitPrice
            // 
            txtExitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtExitPrice.BorderStyle = BorderStyle.FixedSingle;
            txtExitPrice.Font = new Font("Times New Roman", 13.8F);
            txtExitPrice.Location = new Point(285, 153);
            txtExitPrice.Name = "txtExitPrice";
            txtExitPrice.Size = new Size(203, 34);
            txtExitPrice.TabIndex = 4;
            // 
            // txtEntryPrice
            // 
            txtEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtEntryPrice.BorderStyle = BorderStyle.FixedSingle;
            txtEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtEntryPrice.Location = new Point(42, 153);
            txtEntryPrice.Name = "txtEntryPrice";
            txtEntryPrice.Size = new Size(203, 34);
            txtEntryPrice.TabIndex = 3;
            // 
            // cbTradeType
            // 
            cbTradeType.BackColor = Color.FromArgb(30, 58, 95);
            cbTradeType.FlatStyle = FlatStyle.Flat;
            cbTradeType.Font = new Font("Times New Roman", 13.8F);
            cbTradeType.FormattingEnabled = true;
            cbTradeType.Location = new Point(285, 52);
            cbTradeType.Name = "cbTradeType";
            cbTradeType.Size = new Size(202, 34);
            cbTradeType.TabIndex = 2;
            // 
            // lblSymbol
            // 
            lblSymbol.AutoSize = true;
            lblSymbol.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblSymbol.Location = new Point(42, 25);
            lblSymbol.Name = "lblSymbol";
            lblSymbol.Size = new Size(70, 22);
            lblSymbol.TabIndex = 1;
            lblSymbol.Text = "Symbol";
            // 
            // cbSymbol
            // 
            cbSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cbSymbol.FlatStyle = FlatStyle.Flat;
            cbSymbol.Font = new Font("Times New Roman", 13.8F);
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Location = new Point(42, 52);
            cbSymbol.Name = "cbSymbol";
            cbSymbol.Size = new Size(203, 34);
            cbSymbol.TabIndex = 0;
            // 
            // pnlData
            // 
            pnlData.BackColor = Color.FromArgb(27, 38, 59);
            pnlData.Location = new Point(32, 402);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(1167, 426);
            pnlData.TabIndex = 1;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // FrmJournal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1229, 862);
            Controls.Add(pnlData);
            Controls.Add(pnlInformations);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmJournal";
            Text = "FrmJournal";
            Load += FrmJournal_Load;
            pnlInformations.ResumeLayout(false);
            pnlInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlInformations;
        private Panel pnlData;
        private TextBox txtEntryPrice;
        private ComboBox cbTradeType;
        private Label lblSymbol;
        private ComboBox cbSymbol;
        private TextBox txtTakeProfit;
        private TextBox txtStopLoss;
        private TextBox txtExitPrice;
        private Label lblTakeProfit;
        private Label lblStopLoss;
        private Label lblExitPrice;
        private Label lblEntryPrice;
        private Label lblTradeType;
        private FontAwesome.Sharp.IconButton btnUploadScreenshot;
        private FontAwesome.Sharp.IconButton btnAddTrade;
        private PictureBox pbScreenshot;
        private Label lblMargin;
        private Label lblProfitLoss;
        private TextBox txtMargin;
        private TextBox txtProfitLoss;
        private TextBox txtScreenshotLink;
        private OpenFileDialog openFileDialog;
    }
}
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
            btnClearData = new FontAwesome.Sharp.IconButton();
            btnCancelUpdate = new FontAwesome.Sharp.IconButton();
            btnUpdateTrade = new FontAwesome.Sharp.IconButton();
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
            dtpFilterDate = new DateTimePicker();
            dgvData = new DataGridView();
            openFileDialog = new OpenFileDialog();
            pnlInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).BeginInit();
            pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // pnlInformations
            // 
            pnlInformations.BackColor = Color.FromArgb(27, 38, 59);
            pnlInformations.Controls.Add(btnClearData);
            pnlInformations.Controls.Add(btnCancelUpdate);
            pnlInformations.Controls.Add(btnUpdateTrade);
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
            // btnClearData
            // 
            btnClearData.BackColor = Color.IndianRed;
            btnClearData.FlatAppearance.BorderSize = 0;
            btnClearData.FlatStyle = FlatStyle.Flat;
            btnClearData.Font = new Font("Times New Roman", 12F);
            btnClearData.IconChar = FontAwesome.Sharp.IconChar.None;
            btnClearData.IconColor = Color.Black;
            btnClearData.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClearData.Location = new Point(560, 275);
            btnClearData.Name = "btnClearData";
            btnClearData.Size = new Size(190, 37);
            btnClearData.TabIndex = 13;
            btnClearData.Text = "Clear Data";
            btnClearData.UseVisualStyleBackColor = false;
            btnClearData.Click += btnClearData_Click;
            // 
            // btnCancelUpdate
            // 
            btnCancelUpdate.BackColor = Color.Crimson;
            btnCancelUpdate.FlatAppearance.BorderSize = 0;
            btnCancelUpdate.FlatStyle = FlatStyle.Flat;
            btnCancelUpdate.Font = new Font("Times New Roman", 12F);
            btnCancelUpdate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCancelUpdate.IconColor = Color.Black;
            btnCancelUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelUpdate.Location = new Point(680, 245);
            btnCancelUpdate.Name = "btnCancelUpdate";
            btnCancelUpdate.Size = new Size(131, 49);
            btnCancelUpdate.TabIndex = 12;
            btnCancelUpdate.Text = "Cancel";
            btnCancelUpdate.UseVisualStyleBackColor = false;
            btnCancelUpdate.Visible = false;
            btnCancelUpdate.Click += btnCancelUpdate_Click;
            // 
            // btnUpdateTrade
            // 
            btnUpdateTrade.BackColor = Color.DarkGreen;
            btnUpdateTrade.FlatAppearance.BorderSize = 0;
            btnUpdateTrade.FlatStyle = FlatStyle.Flat;
            btnUpdateTrade.Font = new Font("Times New Roman", 12F);
            btnUpdateTrade.IconChar = FontAwesome.Sharp.IconChar.None;
            btnUpdateTrade.IconColor = Color.Black;
            btnUpdateTrade.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUpdateTrade.Location = new Point(519, 245);
            btnUpdateTrade.Name = "btnUpdateTrade";
            btnUpdateTrade.Size = new Size(131, 49);
            btnUpdateTrade.TabIndex = 11;
            btnUpdateTrade.Text = "Update";
            btnUpdateTrade.UseVisualStyleBackColor = false;
            btnUpdateTrade.Visible = false;
            btnUpdateTrade.Click += btnUpdateTrade_Click;
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
            txtScreenshotLink.TabIndex = 8;
            // 
            // lblMargin
            // 
            lblMargin.AutoSize = true;
            lblMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblMargin.Location = new Point(556, 116);
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
            txtMargin.BorderStyle = BorderStyle.None;
            txtMargin.Font = new Font("Times New Roman", 13.8F);
            txtMargin.Location = new Point(555, 154);
            txtMargin.Name = "txtMargin";
            txtMargin.Size = new Size(203, 27);
            txtMargin.TabIndex = 7;
            txtMargin.Text = "   ";
            // 
            // txtProfitLoss
            // 
            txtProfitLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtProfitLoss.BorderStyle = BorderStyle.None;
            txtProfitLoss.Font = new Font("Times New Roman", 13.8F);
            txtProfitLoss.Location = new Point(556, 53);
            txtProfitLoss.Name = "txtProfitLoss";
            txtProfitLoss.Size = new Size(203, 27);
            txtProfitLoss.TabIndex = 6;
            txtProfitLoss.Text = "   ";
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
            btnAddTrade.Location = new Point(547, 211);
            btnAddTrade.Name = "btnAddTrade";
            btnAddTrade.Size = new Size(219, 49);
            btnAddTrade.TabIndex = 10;
            btnAddTrade.Text = "Add Trade";
            btnAddTrade.UseVisualStyleBackColor = false;
            btnAddTrade.Click += btnAddTrade_Click;
            // 
            // btnUploadScreenshot
            // 
            btnUploadScreenshot.AllowDrop = true;
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
            btnUploadScreenshot.TabIndex = 9;
            btnUploadScreenshot.Text = "Upload Screenshot";
            btnUploadScreenshot.UseVisualStyleBackColor = false;
            btnUploadScreenshot.Click += btnUploadScreenshot_Click;
            btnUploadScreenshot.DragDrop += btnUploadScreenshot_DragDrop;
            btnUploadScreenshot.DragEnter += btnUploadScreenshot_DragEnter;
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
            txtTakeProfit.BorderStyle = BorderStyle.None;
            txtTakeProfit.Font = new Font("Times New Roman", 13.8F);
            txtTakeProfit.Location = new Point(284, 260);
            txtTakeProfit.Name = "txtTakeProfit";
            txtTakeProfit.Size = new Size(203, 27);
            txtTakeProfit.TabIndex = 5;
            txtTakeProfit.Text = "   ";
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtStopLoss.BorderStyle = BorderStyle.None;
            txtStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtStopLoss.Location = new Point(42, 260);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(203, 27);
            txtStopLoss.TabIndex = 4;
            txtStopLoss.Text = "   ";
            // 
            // txtExitPrice
            // 
            txtExitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtExitPrice.BorderStyle = BorderStyle.None;
            txtExitPrice.Font = new Font("Times New Roman", 13.8F);
            txtExitPrice.Location = new Point(285, 153);
            txtExitPrice.Name = "txtExitPrice";
            txtExitPrice.Size = new Size(203, 27);
            txtExitPrice.TabIndex = 3;
            txtExitPrice.Text = "   ";
            // 
            // txtEntryPrice
            // 
            txtEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtEntryPrice.BorderStyle = BorderStyle.None;
            txtEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtEntryPrice.Location = new Point(42, 153);
            txtEntryPrice.Name = "txtEntryPrice";
            txtEntryPrice.Size = new Size(203, 27);
            txtEntryPrice.TabIndex = 2;
            txtEntryPrice.Text = "   ";
            // 
            // cbTradeType
            // 
            cbTradeType.BackColor = Color.FromArgb(30, 58, 95);
            cbTradeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTradeType.FlatStyle = FlatStyle.Flat;
            cbTradeType.Font = new Font("Times New Roman", 13.8F);
            cbTradeType.FormattingEnabled = true;
            cbTradeType.Items.AddRange(new object[] { "Long", "Short" });
            cbTradeType.Location = new Point(285, 52);
            cbTradeType.Name = "cbTradeType";
            cbTradeType.Size = new Size(202, 34);
            cbTradeType.TabIndex = 1;
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
            cbSymbol.Text = "  ";
            // 
            // pnlData
            // 
            pnlData.BackColor = Color.FromArgb(27, 38, 59);
            pnlData.Controls.Add(dtpFilterDate);
            pnlData.Controls.Add(dgvData);
            pnlData.Location = new Point(32, 402);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(1167, 426);
            pnlData.TabIndex = 1;
            // 
            // dtpFilterDate
            // 
            dtpFilterDate.CalendarFont = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFilterDate.CalendarForeColor = Color.IndianRed;
            dtpFilterDate.CalendarMonthBackground = Color.FromArgb(24, 30, 54);
            dtpFilterDate.CalendarTitleBackColor = Color.FromArgb(45, 51, 73);
            dtpFilterDate.CalendarTitleForeColor = Color.White;
            dtpFilterDate.CalendarTrailingForeColor = Color.Gray;
            dtpFilterDate.CustomFormat = "dd/MM/yyyy";
            dtpFilterDate.DropDownAlign = LeftRightAlignment.Right;
            dtpFilterDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFilterDate.Format = DateTimePickerFormat.Custom;
            dtpFilterDate.Location = new Point(961, 12);
            dtpFilterDate.MinDate = new DateTime(2025, 9, 6, 0, 0, 0, 0);
            dtpFilterDate.Name = "dtpFilterDate";
            dtpFilterDate.RightToLeft = RightToLeft.No;
            dtpFilterDate.Size = new Size(149, 30);
            dtpFilterDate.TabIndex = 14;
            dtpFilterDate.Value = new DateTime(2025, 9, 6, 16, 53, 29, 0);
            dtpFilterDate.ValueChanged += dtpFilterDate_ValueChanged;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Location = new Point(20, 49);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(1124, 357);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            dgvData.CellDoubleClick += dgvData_CellDoubleClick;
            dgvData.CellPainting += dgvData_CellPainting;
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
            pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
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
        private DataGridView dgvData;
        private DateTimePicker dtpFilterDate;
        private FontAwesome.Sharp.IconButton btnUpdateTrade;
        private FontAwesome.Sharp.IconButton btnCancelUpdate;
        private FontAwesome.Sharp.IconButton btnClearData;
    }
}
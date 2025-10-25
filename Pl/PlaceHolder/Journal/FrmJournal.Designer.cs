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
            openFileDialog1 = new OpenFileDialog();
            openFileDialog = new OpenFileDialog();
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
            pnlData = new Panel();
            chbAllTrades = new CheckBox();
            dtpFilterDate = new DateTimePicker();
            dgvData = new DataGridView();
            pnlInformations = new Panel();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
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
            pnlData_Max = new Panel();
            pnlInformations_Max = new Panel();
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).BeginInit();
            pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            pnlInformations.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog";
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
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
            btnClearData.Location = new Point(448, 220);
            btnClearData.Margin = new Padding(2);
            btnClearData.Name = "btnClearData";
            btnClearData.Size = new Size(152, 30);
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
            btnCancelUpdate.Location = new Point(544, 196);
            btnCancelUpdate.Margin = new Padding(2);
            btnCancelUpdate.Name = "btnCancelUpdate";
            btnCancelUpdate.Size = new Size(105, 39);
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
            btnUpdateTrade.Location = new Point(415, 196);
            btnUpdateTrade.Margin = new Padding(2);
            btnUpdateTrade.Name = "btnUpdateTrade";
            btnUpdateTrade.Size = new Size(105, 39);
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
            txtScreenshotLink.Location = new Point(709, 86);
            txtScreenshotLink.Margin = new Padding(2);
            txtScreenshotLink.Name = "txtScreenshotLink";
            txtScreenshotLink.PlaceholderText = "Upload link . . .";
            txtScreenshotLink.Size = new Size(163, 26);
            txtScreenshotLink.TabIndex = 8;
            // 
            // lblMargin
            // 
            lblMargin.AutoSize = true;
            lblMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblMargin.Location = new Point(445, 93);
            lblMargin.Margin = new Padding(2, 0, 2, 0);
            lblMargin.Name = "lblMargin";
            lblMargin.Size = new Size(52, 19);
            lblMargin.TabIndex = 19;
            lblMargin.Text = "Margin";
            // 
            // lblProfitLoss
            // 
            lblProfitLoss.AutoSize = true;
            lblProfitLoss.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProfitLoss.ForeColor = Color.FromArgb(156, 163, 175);
            lblProfitLoss.Location = new Point(445, 16);
            lblProfitLoss.Margin = new Padding(2, 0, 2, 0);
            lblProfitLoss.Name = "lblProfitLoss";
            lblProfitLoss.Size = new Size(83, 19);
            lblProfitLoss.TabIndex = 18;
            lblProfitLoss.Text = "Profit / Loss";
            // 
            // txtMargin
            // 
            txtMargin.BackColor = Color.FromArgb(30, 58, 95);
            txtMargin.BorderStyle = BorderStyle.None;
            txtMargin.Font = new Font("Times New Roman", 13.8F);
            txtMargin.Location = new Point(444, 123);
            txtMargin.Margin = new Padding(2);
            txtMargin.Name = "txtMargin";
            txtMargin.Size = new Size(162, 22);
            txtMargin.TabIndex = 7;
            txtMargin.Text = "   ";
            // 
            // txtProfitLoss
            // 
            txtProfitLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtProfitLoss.BorderStyle = BorderStyle.None;
            txtProfitLoss.Font = new Font("Times New Roman", 13.8F);
            txtProfitLoss.Location = new Point(445, 42);
            txtProfitLoss.Margin = new Padding(2);
            txtProfitLoss.Name = "txtProfitLoss";
            txtProfitLoss.Size = new Size(162, 22);
            txtProfitLoss.TabIndex = 6;
            txtProfitLoss.Text = "   ";
            // 
            // pbScreenshot
            // 
            pbScreenshot.BackgroundImageLayout = ImageLayout.Stretch;
            pbScreenshot.Location = new Point(709, 129);
            pbScreenshot.Margin = new Padding(2);
            pbScreenshot.Name = "pbScreenshot";
            pbScreenshot.Size = new Size(166, 106);
            pbScreenshot.SizeMode = PictureBoxSizeMode.Zoom;
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
            btnAddTrade.Location = new Point(438, 169);
            btnAddTrade.Margin = new Padding(2);
            btnAddTrade.Name = "btnAddTrade";
            btnAddTrade.Size = new Size(175, 39);
            btnAddTrade.TabIndex = 10;
            btnAddTrade.Text = "Add Trade";
            btnAddTrade.UseVisualStyleBackColor = false;
            btnAddTrade.Click += btnAddTrade_Click;
            // 
            // pnlData
            // 
            pnlData.BackColor = Color.FromArgb(27, 38, 59);
            pnlData.Controls.Add(chbAllTrades);
            pnlData.Controls.Add(dtpFilterDate);
            pnlData.Controls.Add(dgvData);
            pnlData.Location = new Point(25, 322);
            pnlData.Margin = new Padding(2);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(934, 341);
            pnlData.TabIndex = 11;
            // 
            // chbAllTrades
            // 
            chbAllTrades.AutoSize = true;
            chbAllTrades.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chbAllTrades.ForeColor = Color.FromArgb(156, 163, 175);
            chbAllTrades.Location = new Point(627, 12);
            chbAllTrades.Margin = new Padding(2);
            chbAllTrades.Name = "chbAllTrades";
            chbAllTrades.Size = new Size(121, 23);
            chbAllTrades.TabIndex = 15;
            chbAllTrades.Text = "Show all trades";
            chbAllTrades.UseVisualStyleBackColor = true;
            chbAllTrades.CheckedChanged += chbAllTrades_CheckedChanged;
            // 
            // dtpFilterDate
            // 
            dtpFilterDate.CalendarFont = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFilterDate.CalendarForeColor = Color.IndianRed;
            dtpFilterDate.CalendarMonthBackground = Color.FromArgb(24, 30, 54);
            dtpFilterDate.CalendarTitleBackColor = Color.FromArgb(45, 51, 73);
            dtpFilterDate.CalendarTitleForeColor = Color.White;
            dtpFilterDate.CalendarTrailingForeColor = Color.Gray;
            dtpFilterDate.CustomFormat = "";
            dtpFilterDate.DropDownAlign = LeftRightAlignment.Right;
            dtpFilterDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFilterDate.Format = DateTimePickerFormat.Short;
            dtpFilterDate.Location = new Point(769, 10);
            dtpFilterDate.Margin = new Padding(2);
            dtpFilterDate.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpFilterDate.Name = "dtpFilterDate";
            dtpFilterDate.RightToLeft = RightToLeft.No;
            dtpFilterDate.Size = new Size(120, 26);
            dtpFilterDate.TabIndex = 14;
            dtpFilterDate.Value = new DateTime(2025, 10, 25, 0, 0, 0, 0);
            dtpFilterDate.ValueChanged += dtpFilterDate_ValueChanged;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Location = new Point(16, 39);
            dgvData.Margin = new Padding(2);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(899, 286);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            dgvData.CellDoubleClick += dgvData_CellDoubleClick;
            dgvData.CellFormatting += dgvData_CellFormatting;
            dgvData.CellPainting += dgvData_CellPainting;
            // 
            // pnlInformations
            // 
            pnlInformations.BackColor = Color.FromArgb(27, 38, 59);
            pnlInformations.Controls.Add(lblDate);
            pnlInformations.Controls.Add(dtpDate);
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
            pnlInformations.Location = new Point(25, 27);
            pnlInformations.Margin = new Padding(2);
            pnlInformations.Name = "pnlInformations";
            pnlInformations.Size = new Size(934, 264);
            pnlInformations.TabIndex = 10;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(156, 163, 175);
            lblDate.Location = new Point(34, 206);
            lblDate.Margin = new Padding(2, 0, 2, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(38, 19);
            lblDate.TabIndex = 21;
            lblDate.Text = "Date";
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(34, 226);
            dtpDate.Margin = new Padding(2, 2, 2, 2);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(122, 26);
            dtpDate.TabIndex = 20;
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
            btnUploadScreenshot.Location = new Point(696, 36);
            btnUploadScreenshot.Margin = new Padding(2);
            btnUploadScreenshot.Name = "btnUploadScreenshot";
            btnUploadScreenshot.Size = new Size(192, 39);
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
            lblTakeProfit.Location = new Point(229, 143);
            lblTakeProfit.Margin = new Padding(2, 0, 2, 0);
            lblTakeProfit.Name = "lblTakeProfit";
            lblTakeProfit.Size = new Size(76, 19);
            lblTakeProfit.TabIndex = 10;
            lblTakeProfit.Text = "Take Profit";
            // 
            // lblStopLoss
            // 
            lblStopLoss.AutoSize = true;
            lblStopLoss.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStopLoss.ForeColor = Color.FromArgb(156, 163, 175);
            lblStopLoss.Location = new Point(34, 143);
            lblStopLoss.Margin = new Padding(2, 0, 2, 0);
            lblStopLoss.Name = "lblStopLoss";
            lblStopLoss.Size = new Size(71, 19);
            lblStopLoss.TabIndex = 9;
            lblStopLoss.Text = "Stop Loss";
            // 
            // lblExitPrice
            // 
            lblExitPrice.AutoSize = true;
            lblExitPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExitPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblExitPrice.Location = new Point(228, 82);
            lblExitPrice.Margin = new Padding(2, 0, 2, 0);
            lblExitPrice.Name = "lblExitPrice";
            lblExitPrice.Size = new Size(67, 19);
            lblExitPrice.TabIndex = 9;
            lblExitPrice.Text = "Exit Price";
            // 
            // lblEntryPrice
            // 
            lblEntryPrice.AutoSize = true;
            lblEntryPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblEntryPrice.Location = new Point(34, 82);
            lblEntryPrice.Margin = new Padding(2, 0, 2, 0);
            lblEntryPrice.Name = "lblEntryPrice";
            lblEntryPrice.Size = new Size(76, 19);
            lblEntryPrice.TabIndex = 8;
            lblEntryPrice.Text = "Entry Price";
            // 
            // lblTradeType
            // 
            lblTradeType.AutoSize = true;
            lblTradeType.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTradeType.ForeColor = Color.FromArgb(156, 163, 175);
            lblTradeType.Location = new Point(228, 15);
            lblTradeType.Margin = new Padding(2, 0, 2, 0);
            lblTradeType.Name = "lblTradeType";
            lblTradeType.Size = new Size(78, 19);
            lblTradeType.TabIndex = 7;
            lblTradeType.Text = "Trade Type";
            // 
            // txtTakeProfit
            // 
            txtTakeProfit.BackColor = Color.FromArgb(30, 58, 95);
            txtTakeProfit.BorderStyle = BorderStyle.None;
            txtTakeProfit.Font = new Font("Times New Roman", 13.8F);
            txtTakeProfit.Location = new Point(228, 172);
            txtTakeProfit.Margin = new Padding(2);
            txtTakeProfit.Name = "txtTakeProfit";
            txtTakeProfit.Size = new Size(162, 22);
            txtTakeProfit.TabIndex = 5;
            txtTakeProfit.Text = "   ";
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtStopLoss.BorderStyle = BorderStyle.None;
            txtStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtStopLoss.Location = new Point(34, 172);
            txtStopLoss.Margin = new Padding(2);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(162, 22);
            txtStopLoss.TabIndex = 4;
            txtStopLoss.Text = "   ";
            // 
            // txtExitPrice
            // 
            txtExitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtExitPrice.BorderStyle = BorderStyle.None;
            txtExitPrice.Font = new Font("Times New Roman", 13.8F);
            txtExitPrice.Location = new Point(228, 107);
            txtExitPrice.Margin = new Padding(2);
            txtExitPrice.Name = "txtExitPrice";
            txtExitPrice.Size = new Size(162, 22);
            txtExitPrice.TabIndex = 3;
            txtExitPrice.Text = "   ";
            // 
            // txtEntryPrice
            // 
            txtEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtEntryPrice.BorderStyle = BorderStyle.None;
            txtEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtEntryPrice.Location = new Point(34, 107);
            txtEntryPrice.Margin = new Padding(2);
            txtEntryPrice.Name = "txtEntryPrice";
            txtEntryPrice.Size = new Size(162, 22);
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
            cbTradeType.Location = new Point(228, 37);
            cbTradeType.Margin = new Padding(2);
            cbTradeType.Name = "cbTradeType";
            cbTradeType.Size = new Size(162, 28);
            cbTradeType.TabIndex = 1;
            // 
            // lblSymbol
            // 
            lblSymbol.AutoSize = true;
            lblSymbol.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblSymbol.Location = new Point(34, 15);
            lblSymbol.Margin = new Padding(2, 0, 2, 0);
            lblSymbol.Name = "lblSymbol";
            lblSymbol.Size = new Size(55, 19);
            lblSymbol.TabIndex = 1;
            lblSymbol.Text = "Symbol";
            // 
            // cbSymbol
            // 
            cbSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cbSymbol.FlatStyle = FlatStyle.Flat;
            cbSymbol.Font = new Font("Times New Roman", 13.8F);
            cbSymbol.FormattingEnabled = true;
            cbSymbol.Location = new Point(34, 37);
            cbSymbol.Margin = new Padding(2);
            cbSymbol.Name = "cbSymbol";
            cbSymbol.Size = new Size(163, 28);
            cbSymbol.TabIndex = 0;
            cbSymbol.Text = "  ";
            // 
            // pnlData_Max
            // 
            pnlData_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlData_Max.Location = new Point(25, 394);
            pnlData_Max.Margin = new Padding(2);
            pnlData_Max.Name = "pnlData_Max";
            pnlData_Max.Size = new Size(1651, 580);
            pnlData_Max.TabIndex = 12;
            pnlData_Max.Visible = false;
            // 
            // pnlInformations_Max
            // 
            pnlInformations_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlInformations_Max.Location = new Point(25, 18);
            pnlInformations_Max.Margin = new Padding(2);
            pnlInformations_Max.Name = "pnlInformations_Max";
            pnlInformations_Max.Size = new Size(1651, 357);
            pnlInformations_Max.TabIndex = 13;
            pnlInformations_Max.Visible = false;
            // 
            // FrmJournal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1552, 880);
            Controls.Add(pnlData);
            Controls.Add(pnlInformations);
            Controls.Add(pnlData_Max);
            Controls.Add(pnlInformations_Max);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "FrmJournal";
            Load += FrmJournal_Load;
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).EndInit();
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            pnlInformations.ResumeLayout(false);
            pnlInformations.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private OpenFileDialog openFileDialog;
        private FontAwesome.Sharp.IconButton btnClearData;
        private FontAwesome.Sharp.IconButton btnCancelUpdate;
        private FontAwesome.Sharp.IconButton btnUpdateTrade;
        private TextBox txtScreenshotLink;
        private Label lblMargin;
        private Label lblProfitLoss;
        private TextBox txtMargin;
        private TextBox txtProfitLoss;
        private PictureBox pbScreenshot;
        private FontAwesome.Sharp.IconButton btnAddTrade;
        private Panel pnlData;
        private CheckBox chbAllTrades;
        private DateTimePicker dtpFilterDate;
        private DataGridView dgvData;
        private Panel pnlInformations;
        private FontAwesome.Sharp.IconButton btnUploadScreenshot;
        private Label lblTakeProfit;
        private Label lblStopLoss;
        private Label lblExitPrice;
        private Label lblEntryPrice;
        private Label lblTradeType;
        private TextBox txtTakeProfit;
        private TextBox txtStopLoss;
        private TextBox txtExitPrice;
        private TextBox txtEntryPrice;
        private ComboBox cbTradeType;
        private Label lblSymbol;
        private ComboBox cbSymbol;
        private Panel pnlData_Max;
        private Panel pnlInformations_Max;
        private Label lblDate;
        private DateTimePicker dtpDate;
    }
}
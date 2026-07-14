using FontAwesome.Sharp;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    partial class FrmTrading
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
            pnlPlatforms = new Panel();
            lblStatus = new Label();
            btnConnect = new IconButton();
            lblTitle = new Label();
            cmbPlatform = new ComboBox();
            lblCapBalance = new Label();
            lblBalance = new Label();
            lblCapPlatName = new Label();
            pnlPlatforms_Max = new Panel();
            pnlActiveTrades = new Panel();
            lblTableTitle = new Label();
            dgvPositions = new DataGridView();
            colPosSymbol = new DataGridViewTextBoxColumn();
            colPosLiq = new DataGridViewTextBoxColumn();
            colPosRoi = new DataGridViewTextBoxColumn();
            colPosClose = new DataGridViewButtonColumn();
            colPosTp = new DataGridViewButtonColumn();
            colPosSide = new DataGridViewTextBoxColumn();
            colPosQty = new DataGridViewTextBoxColumn();
            colPosEntry = new DataGridViewTextBoxColumn();
            colPosMark = new DataGridViewTextBoxColumn();
            colPosPnl = new DataGridViewTextBoxColumn();
            colPosLev = new DataGridViewTextBoxColumn();
            colPosMargin = new DataGridViewTextBoxColumn();
            pnlActiveTrades_Max = new Panel();
            pnlAddTrades = new Panel();
            lblMaxTitle = new Label();
            lblCostTitle = new Label();
            lblLiqTitle = new Label();
            lblSellMax = new Label();
            lblSellCost = new Label();
            lblSellLiq = new Label();
            lblBuyMax = new Label();
            lblBuyCost = new Label();
            lblBuyLiq = new Label();
            btnSell = new IconButton();
            txtTakeProfit = new TextBox();
            lblCapTp = new Label();
            txtStopLoss = new TextBox();
            lblCapSl = new Label();
            txtTriggerPrice = new TextBox();
            lblCapTrigger = new Label();
            txtLimitPrice = new TextBox();
            lblCapLimit = new Label();
            txtUsdtAmount = new TextBox();
            txtLeverage = new TextBox();
            lblCapMargin = new Label();
            lblCapLeverage = new Label();
            cmbMarginMode = new ComboBox();
            lblCapUsdt = new Label();
            lblCapOrderType = new Label();
            lblCapSymbol = new Label();
            cmbSymbol = new ComboBox();
            btnBuy = new IconButton();
            lblTrading = new Label();
            cmbOrderType = new ComboBox();
            lblPrice = new Label();
            lblCapPrice = new Label();
            pnlAddTrades_Max = new Panel();
            pnlPlatforms.SuspendLayout();
            pnlActiveTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPositions).BeginInit();
            pnlAddTrades.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPlatforms
            // 
            pnlPlatforms.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatforms.Controls.Add(lblStatus);
            pnlPlatforms.Controls.Add(btnConnect);
            pnlPlatforms.Controls.Add(lblTitle);
            pnlPlatforms.Controls.Add(cmbPlatform);
            pnlPlatforms.Controls.Add(lblCapBalance);
            pnlPlatforms.Controls.Add(lblBalance);
            pnlPlatforms.Controls.Add(lblCapPlatName);
            pnlPlatforms.Location = new Point(17, 19);
            pnlPlatforms.Margin = new Padding(2);
            pnlPlatforms.Name = "pnlPlatforms";
            pnlPlatforms.Size = new Size(1008, 277);
            pnlPlatforms.TabIndex = 26;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblStatus.Location = new Point(347, 207);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(634, 55);
            lblStatus.TabIndex = 39;
            lblStatus.Text = "--";
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(30, 58, 95);
            btnConnect.FlatAppearance.BorderSize = 0;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Times New Roman", 12F);
            btnConnect.IconChar = IconChar.None;
            btnConnect.IconColor = Color.Black;
            btnConnect.IconFont = IconFont.Auto;
            btnConnect.Location = new Point(39, 197);
            btnConnect.Margin = new Padding(2);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(192, 37);
            btnConnect.TabIndex = 14;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTitle.Location = new Point(21, 16);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(242, 32);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Platform Selection";
            // 
            // cmbPlatform
            // 
            cmbPlatform.BackColor = Color.FromArgb(30, 58, 95);
            cmbPlatform.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatform.FlatStyle = FlatStyle.Flat;
            cmbPlatform.Font = new Font("Times New Roman", 13.8F);
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Location = new Point(39, 118);
            cmbPlatform.Margin = new Padding(2);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(177, 28);
            cmbPlatform.TabIndex = 11;
            // 
            // lblCapBalance
            // 
            lblCapBalance.AutoSize = true;
            lblCapBalance.Font = new Font("Times New Roman", 15.75F);
            lblCapBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapBalance.Location = new Point(338, 118);
            lblCapBalance.Margin = new Padding(4, 0, 4, 0);
            lblCapBalance.Name = "lblCapBalance";
            lblCapBalance.Size = new Size(172, 23);
            lblCapBalance.TabIndex = 3;
            lblCapBalance.Text = "Platform balance   :";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new Font("Times New Roman", 15.75F);
            lblBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblBalance.Location = new Point(541, 118);
            lblBalance.Margin = new Padding(4, 0, 4, 0);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(24, 23);
            lblBalance.TabIndex = 1;
            lblBalance.Text = "--";
            // 
            // lblCapPlatName
            // 
            lblCapPlatName.AutoSize = true;
            lblCapPlatName.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPlatName.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPlatName.Location = new Point(39, 87);
            lblCapPlatName.Margin = new Padding(4, 0, 4, 0);
            lblCapPlatName.Name = "lblCapPlatName";
            lblCapPlatName.Size = new Size(118, 21);
            lblCapPlatName.TabIndex = 0;
            lblCapPlatName.Text = "Platform name";
            // 
            // pnlPlatforms_Max
            // 
            pnlPlatforms_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatforms_Max.Location = new Point(17, 16);
            pnlPlatforms_Max.Margin = new Padding(2);
            pnlPlatforms_Max.Name = "pnlPlatforms_Max";
            pnlPlatforms_Max.Size = new Size(1660, 278);
            pnlPlatforms_Max.TabIndex = 27;
            pnlPlatforms_Max.Visible = false;
            // 
            // pnlActiveTrades
            // 
            pnlActiveTrades.BackColor = Color.FromArgb(27, 38, 59);
            pnlActiveTrades.Controls.Add(lblTableTitle);
            pnlActiveTrades.Controls.Add(dgvPositions);
            pnlActiveTrades.Location = new Point(17, 941);
            pnlActiveTrades.Margin = new Padding(2);
            pnlActiveTrades.Name = "pnlActiveTrades";
            pnlActiveTrades.Size = new Size(1008, 376);
            pnlActiveTrades.TabIndex = 28;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTableTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTableTitle.Location = new Point(22, 15);
            lblTableTitle.Margin = new Padding(2, 0, 2, 0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(261, 32);
            lblTableTitle.TabIndex = 13;
            lblTableTitle.Text = "Registerd Platforms";
            // 
            // dgvPositions
            // 
            dgvPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPositions.Columns.AddRange(new DataGridViewColumn[] { colPosSymbol, colPosLiq, colPosRoi, colPosClose, colPosTp, colPosSide, colPosQty, colPosEntry, colPosMark, colPosPnl, colPosLev, colPosMargin });
            dgvPositions.Location = new Point(18, 71);
            dgvPositions.Margin = new Padding(4, 3, 4, 3);
            dgvPositions.Name = "dgvPositions";
            dgvPositions.RowHeadersWidth = 51;
            dgvPositions.Size = new Size(963, 262);
            dgvPositions.TabIndex = 0;
            // 
            // colPosSymbol
            // 
            colPosSymbol.HeaderText = "Symbol";
            colPosSymbol.MinimumWidth = 6;
            colPosSymbol.Name = "colPosSymbol";
            colPosSymbol.ReadOnly = true;
            colPosSymbol.Width = 125;
            // 
            // colPosLiq
            // 
            colPosLiq.HeaderText = "Liq. Price";
            colPosLiq.MinimumWidth = 6;
            colPosLiq.Name = "colPosLiq";
            colPosLiq.ReadOnly = true;
            colPosLiq.Width = 125;
            // 
            // colPosRoi
            // 
            colPosRoi.HeaderText = "ROI %";
            colPosRoi.MinimumWidth = 6;
            colPosRoi.Name = "colPosRoi";
            colPosRoi.ReadOnly = true;
            colPosRoi.Width = 125;
            // 
            // colPosClose
            // 
            colPosClose.HeaderText = "";
            colPosClose.MinimumWidth = 6;
            colPosClose.Name = "colPosClose";
            colPosClose.ReadOnly = true;
            colPosClose.Text = "Close";
            colPosClose.UseColumnTextForButtonValue = true;
            colPosClose.Width = 125;
            // 
            // colPosTp
            // 
            colPosTp.HeaderText = "";
            colPosTp.MinimumWidth = 6;
            colPosTp.Name = "colPosTp";
            colPosTp.ReadOnly = true;
            colPosTp.Text = "TP";
            colPosTp.UseColumnTextForButtonValue = true;
            colPosTp.Width = 125;
            // 
            // colPosSide
            // 
            colPosSide.HeaderText = "Side";
            colPosSide.MinimumWidth = 6;
            colPosSide.Name = "colPosSide";
            colPosSide.ReadOnly = true;
            colPosSide.Width = 125;
            // 
            // colPosQty
            // 
            colPosQty.HeaderText = "Qty";
            colPosQty.MinimumWidth = 6;
            colPosQty.Name = "colPosQty";
            colPosQty.ReadOnly = true;
            colPosQty.Width = 125;
            // 
            // colPosEntry
            // 
            colPosEntry.HeaderText = "Entry";
            colPosEntry.MinimumWidth = 6;
            colPosEntry.Name = "colPosEntry";
            colPosEntry.ReadOnly = true;
            colPosEntry.Width = 125;
            // 
            // colPosMark
            // 
            colPosMark.HeaderText = "Mark";
            colPosMark.MinimumWidth = 6;
            colPosMark.Name = "colPosMark";
            colPosMark.ReadOnly = true;
            colPosMark.Width = 125;
            // 
            // colPosPnl
            // 
            colPosPnl.HeaderText = "uPnL";
            colPosPnl.MinimumWidth = 6;
            colPosPnl.Name = "colPosPnl";
            colPosPnl.ReadOnly = true;
            colPosPnl.Width = 125;
            // 
            // colPosLev
            // 
            colPosLev.HeaderText = "Lev";
            colPosLev.MinimumWidth = 6;
            colPosLev.Name = "colPosLev";
            colPosLev.ReadOnly = true;
            colPosLev.Width = 125;
            // 
            // colPosMargin
            // 
            colPosMargin.HeaderText = "Margin";
            colPosMargin.MinimumWidth = 6;
            colPosMargin.Name = "colPosMargin";
            colPosMargin.ReadOnly = true;
            colPosMargin.Width = 125;
            // 
            // pnlActiveTrades_Max
            // 
            pnlActiveTrades_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlActiveTrades_Max.Location = new Point(17, 940);
            pnlActiveTrades_Max.Margin = new Padding(2);
            pnlActiveTrades_Max.Name = "pnlActiveTrades_Max";
            pnlActiveTrades_Max.Size = new Size(1660, 376);
            pnlActiveTrades_Max.TabIndex = 25;
            pnlActiveTrades_Max.Visible = false;
            // 
            // pnlAddTrades
            // 
            pnlAddTrades.BackColor = Color.FromArgb(27, 38, 59);
            pnlAddTrades.Controls.Add(lblMaxTitle);
            pnlAddTrades.Controls.Add(lblCostTitle);
            pnlAddTrades.Controls.Add(lblLiqTitle);
            pnlAddTrades.Controls.Add(lblSellMax);
            pnlAddTrades.Controls.Add(lblSellCost);
            pnlAddTrades.Controls.Add(lblSellLiq);
            pnlAddTrades.Controls.Add(lblBuyMax);
            pnlAddTrades.Controls.Add(lblBuyCost);
            pnlAddTrades.Controls.Add(lblBuyLiq);
            pnlAddTrades.Controls.Add(btnSell);
            pnlAddTrades.Controls.Add(txtTakeProfit);
            pnlAddTrades.Controls.Add(lblCapTp);
            pnlAddTrades.Controls.Add(txtStopLoss);
            pnlAddTrades.Controls.Add(lblCapSl);
            pnlAddTrades.Controls.Add(txtTriggerPrice);
            pnlAddTrades.Controls.Add(lblCapTrigger);
            pnlAddTrades.Controls.Add(txtLimitPrice);
            pnlAddTrades.Controls.Add(lblCapLimit);
            pnlAddTrades.Controls.Add(txtUsdtAmount);
            pnlAddTrades.Controls.Add(txtLeverage);
            pnlAddTrades.Controls.Add(lblCapMargin);
            pnlAddTrades.Controls.Add(lblCapLeverage);
            pnlAddTrades.Controls.Add(cmbMarginMode);
            pnlAddTrades.Controls.Add(lblCapUsdt);
            pnlAddTrades.Controls.Add(lblCapOrderType);
            pnlAddTrades.Controls.Add(lblCapSymbol);
            pnlAddTrades.Controls.Add(cmbSymbol);
            pnlAddTrades.Controls.Add(btnBuy);
            pnlAddTrades.Controls.Add(lblTrading);
            pnlAddTrades.Controls.Add(cmbOrderType);
            pnlAddTrades.Controls.Add(lblPrice);
            pnlAddTrades.Controls.Add(lblCapPrice);
            pnlAddTrades.Location = new Point(17, 322);
            pnlAddTrades.Margin = new Padding(2);
            pnlAddTrades.Name = "pnlAddTrades";
            pnlAddTrades.Size = new Size(1008, 604);
            pnlAddTrades.TabIndex = 29;
            // 
            // lblMaxTitle
            // 
            lblMaxTitle.AutoSize = true;
            lblMaxTitle.Font = new Font("Times New Roman", 15F);
            lblMaxTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblMaxTitle.Location = new Point(55, 568);
            lblMaxTitle.Margin = new Padding(4, 0, 4, 0);
            lblMaxTitle.Name = "lblMaxTitle";
            lblMaxTitle.Size = new Size(101, 22);
            lblMaxTitle.TabIndex = 44;
            lblMaxTitle.Text = "Max          :";
            // 
            // lblCostTitle
            // 
            lblCostTitle.AutoSize = true;
            lblCostTitle.Font = new Font("Times New Roman", 15F);
            lblCostTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblCostTitle.Location = new Point(55, 538);
            lblCostTitle.Margin = new Padding(4, 0, 4, 0);
            lblCostTitle.Name = "lblCostTitle";
            lblCostTitle.Size = new Size(102, 22);
            lblCostTitle.TabIndex = 43;
            lblCostTitle.Text = "Cost          :";
            // 
            // lblLiqTitle
            // 
            lblLiqTitle.AutoSize = true;
            lblLiqTitle.Font = new Font("Times New Roman", 15F);
            lblLiqTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqTitle.Location = new Point(55, 504);
            lblLiqTitle.Margin = new Padding(4, 0, 4, 0);
            lblLiqTitle.Name = "lblLiqTitle";
            lblLiqTitle.Size = new Size(105, 22);
            lblLiqTitle.TabIndex = 42;
            lblLiqTitle.Text = "Liq Price   :";
            // 
            // lblSellMax
            // 
            lblSellMax.AutoSize = true;
            lblSellMax.Font = new Font("Times New Roman", 15F);
            lblSellMax.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellMax.Location = new Point(513, 568);
            lblSellMax.Margin = new Padding(4, 0, 4, 0);
            lblSellMax.Name = "lblSellMax";
            lblSellMax.Size = new Size(24, 22);
            lblSellMax.TabIndex = 41;
            lblSellMax.Text = "--";
            // 
            // lblSellCost
            // 
            lblSellCost.AutoSize = true;
            lblSellCost.Font = new Font("Times New Roman", 15F);
            lblSellCost.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellCost.Location = new Point(514, 538);
            lblSellCost.Margin = new Padding(4, 0, 4, 0);
            lblSellCost.Name = "lblSellCost";
            lblSellCost.Size = new Size(24, 22);
            lblSellCost.TabIndex = 40;
            lblSellCost.Text = "--";
            // 
            // lblSellLiq
            // 
            lblSellLiq.AutoSize = true;
            lblSellLiq.Font = new Font("Times New Roman", 15F);
            lblSellLiq.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellLiq.Location = new Point(514, 504);
            lblSellLiq.Margin = new Padding(4, 0, 4, 0);
            lblSellLiq.Name = "lblSellLiq";
            lblSellLiq.Size = new Size(24, 22);
            lblSellLiq.TabIndex = 39;
            lblSellLiq.Text = "--";
            // 
            // lblBuyMax
            // 
            lblBuyMax.AutoSize = true;
            lblBuyMax.Font = new Font("Times New Roman", 15F);
            lblBuyMax.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyMax.Location = new Point(259, 568);
            lblBuyMax.Margin = new Padding(4, 0, 4, 0);
            lblBuyMax.Name = "lblBuyMax";
            lblBuyMax.Size = new Size(24, 22);
            lblBuyMax.TabIndex = 38;
            lblBuyMax.Text = "--";
            // 
            // lblBuyCost
            // 
            lblBuyCost.AutoSize = true;
            lblBuyCost.Font = new Font("Times New Roman", 15F);
            lblBuyCost.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyCost.Location = new Point(259, 538);
            lblBuyCost.Margin = new Padding(4, 0, 4, 0);
            lblBuyCost.Name = "lblBuyCost";
            lblBuyCost.Size = new Size(24, 22);
            lblBuyCost.TabIndex = 37;
            lblBuyCost.Text = "--";
            // 
            // lblBuyLiq
            // 
            lblBuyLiq.AutoSize = true;
            lblBuyLiq.Font = new Font("Times New Roman", 15F);
            lblBuyLiq.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyLiq.Location = new Point(259, 504);
            lblBuyLiq.Margin = new Padding(4, 0, 4, 0);
            lblBuyLiq.Name = "lblBuyLiq";
            lblBuyLiq.Size = new Size(24, 22);
            lblBuyLiq.TabIndex = 36;
            lblBuyLiq.Text = "--";
            // 
            // btnSell
            // 
            btnSell.BackColor = Color.FromArgb(30, 58, 95);
            btnSell.FlatAppearance.BorderSize = 0;
            btnSell.FlatStyle = FlatStyle.Flat;
            btnSell.Font = new Font("Times New Roman", 12F);
            btnSell.IconChar = IconChar.None;
            btnSell.IconColor = Color.Black;
            btnSell.IconFont = IconFont.Auto;
            btnSell.Location = new Point(438, 448);
            btnSell.Margin = new Padding(2);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(192, 37);
            btnSell.TabIndex = 35;
            btnSell.Text = "Sell/Short";
            btnSell.UseVisualStyleBackColor = false;
            // 
            // txtTakeProfit
            // 
            txtTakeProfit.BackColor = Color.FromArgb(30, 58, 95);
            txtTakeProfit.BorderStyle = BorderStyle.None;
            txtTakeProfit.Font = new Font("Times New Roman", 13.8F);
            txtTakeProfit.Location = new Point(452, 376);
            txtTakeProfit.Margin = new Padding(2);
            txtTakeProfit.Name = "txtTakeProfit";
            txtTakeProfit.Size = new Size(177, 22);
            txtTakeProfit.TabIndex = 34;
            txtTakeProfit.Text = "   ";
            // 
            // lblCapTp
            // 
            lblCapTp.AutoSize = true;
            lblCapTp.Font = new Font("Times New Roman", 14.25F);
            lblCapTp.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTp.Location = new Point(452, 350);
            lblCapTp.Margin = new Padding(4, 0, 4, 0);
            lblCapTp.Name = "lblCapTp";
            lblCapTp.Size = new Size(170, 21);
            lblCapTp.TabIndex = 33;
            lblCapTp.Text = "Take-profit (optional)";
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtStopLoss.BorderStyle = BorderStyle.None;
            txtStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtStopLoss.Location = new Point(175, 376);
            txtStopLoss.Margin = new Padding(2);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(177, 22);
            txtStopLoss.TabIndex = 32;
            txtStopLoss.Text = "   ";
            // 
            // lblCapSl
            // 
            lblCapSl.AutoSize = true;
            lblCapSl.Font = new Font("Times New Roman", 14.25F);
            lblCapSl.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSl.Location = new Point(175, 350);
            lblCapSl.Margin = new Padding(4, 0, 4, 0);
            lblCapSl.Name = "lblCapSl";
            lblCapSl.Size = new Size(159, 21);
            lblCapSl.TabIndex = 31;
            lblCapSl.Text = "Stop-loss (optional)";
            // 
            // txtTriggerPrice
            // 
            txtTriggerPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtTriggerPrice.BorderStyle = BorderStyle.None;
            txtTriggerPrice.Font = new Font("Times New Roman", 13.8F);
            txtTriggerPrice.Location = new Point(452, 237);
            txtTriggerPrice.Margin = new Padding(2);
            txtTriggerPrice.Name = "txtTriggerPrice";
            txtTriggerPrice.Size = new Size(177, 22);
            txtTriggerPrice.TabIndex = 30;
            txtTriggerPrice.Text = "   ";
            // 
            // lblCapTrigger
            // 
            lblCapTrigger.AutoSize = true;
            lblCapTrigger.Font = new Font("Times New Roman", 14.25F);
            lblCapTrigger.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTrigger.Location = new Point(452, 211);
            lblCapTrigger.Margin = new Padding(4, 0, 4, 0);
            lblCapTrigger.Name = "lblCapTrigger";
            lblCapTrigger.Size = new Size(105, 21);
            lblCapTrigger.TabIndex = 29;
            lblCapTrigger.Text = "Trigger price";
            // 
            // txtLimitPrice
            // 
            txtLimitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtLimitPrice.BorderStyle = BorderStyle.None;
            txtLimitPrice.Font = new Font("Times New Roman", 13.8F);
            txtLimitPrice.Location = new Point(177, 304);
            txtLimitPrice.Margin = new Padding(2);
            txtLimitPrice.Name = "txtLimitPrice";
            txtLimitPrice.Size = new Size(177, 22);
            txtLimitPrice.TabIndex = 28;
            txtLimitPrice.Text = "   ";
            // 
            // lblCapLimit
            // 
            lblCapLimit.AutoSize = true;
            lblCapLimit.Font = new Font("Times New Roman", 14.25F);
            lblCapLimit.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLimit.Location = new Point(177, 278);
            lblCapLimit.Margin = new Padding(4, 0, 4, 0);
            lblCapLimit.Name = "lblCapLimit";
            lblCapLimit.Size = new Size(90, 21);
            lblCapLimit.TabIndex = 27;
            lblCapLimit.Text = "Limit price";
            // 
            // txtUsdtAmount
            // 
            txtUsdtAmount.BackColor = Color.FromArgb(30, 58, 95);
            txtUsdtAmount.BorderStyle = BorderStyle.None;
            txtUsdtAmount.Font = new Font("Times New Roman", 13.8F);
            txtUsdtAmount.Location = new Point(452, 304);
            txtUsdtAmount.Margin = new Padding(2);
            txtUsdtAmount.Name = "txtUsdtAmount";
            txtUsdtAmount.Size = new Size(177, 22);
            txtUsdtAmount.TabIndex = 26;
            txtUsdtAmount.Text = "   ";
            // 
            // txtLeverage
            // 
            txtLeverage.BackColor = Color.FromArgb(30, 58, 95);
            txtLeverage.BorderStyle = BorderStyle.None;
            txtLeverage.Font = new Font("Times New Roman", 13.8F);
            txtLeverage.Location = new Point(452, 166);
            txtLeverage.Margin = new Padding(2);
            txtLeverage.Name = "txtLeverage";
            txtLeverage.Size = new Size(92, 22);
            txtLeverage.TabIndex = 25;
            txtLeverage.Text = "   ";
            // 
            // lblCapMargin
            // 
            lblCapMargin.AutoSize = true;
            lblCapMargin.Font = new Font("Times New Roman", 14.25F);
            lblCapMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapMargin.Location = new Point(175, 141);
            lblCapMargin.Margin = new Padding(4, 0, 4, 0);
            lblCapMargin.Name = "lblCapMargin";
            lblCapMargin.Size = new Size(67, 21);
            lblCapMargin.TabIndex = 24;
            lblCapMargin.Text = "Margin ";
            // 
            // lblCapLeverage
            // 
            lblCapLeverage.AutoSize = true;
            lblCapLeverage.Font = new Font("Times New Roman", 14.25F);
            lblCapLeverage.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLeverage.Location = new Point(452, 141);
            lblCapLeverage.Margin = new Padding(4, 0, 4, 0);
            lblCapLeverage.Name = "lblCapLeverage";
            lblCapLeverage.Size = new Size(77, 21);
            lblCapLeverage.TabIndex = 22;
            lblCapLeverage.Text = "Leverage";
            // 
            // cmbMarginMode
            // 
            cmbMarginMode.BackColor = Color.FromArgb(30, 58, 95);
            cmbMarginMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarginMode.FlatStyle = FlatStyle.Flat;
            cmbMarginMode.Font = new Font("Times New Roman", 13.8F);
            cmbMarginMode.FormattingEnabled = true;
            cmbMarginMode.Location = new Point(175, 164);
            cmbMarginMode.Margin = new Padding(2);
            cmbMarginMode.Name = "cmbMarginMode";
            cmbMarginMode.Size = new Size(177, 28);
            cmbMarginMode.TabIndex = 20;
            // 
            // lblCapUsdt
            // 
            lblCapUsdt.AutoSize = true;
            lblCapUsdt.Font = new Font("Times New Roman", 14.25F);
            lblCapUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapUsdt.Location = new Point(452, 278);
            lblCapUsdt.Margin = new Padding(4, 0, 4, 0);
            lblCapUsdt.Name = "lblCapUsdt";
            lblCapUsdt.Size = new Size(136, 21);
            lblCapUsdt.TabIndex = 19;
            lblCapUsdt.Text = "Amount (USDT)";
            // 
            // lblCapOrderType
            // 
            lblCapOrderType.AutoSize = true;
            lblCapOrderType.Font = new Font("Times New Roman", 14.25F);
            lblCapOrderType.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapOrderType.Location = new Point(175, 207);
            lblCapOrderType.Margin = new Padding(4, 0, 4, 0);
            lblCapOrderType.Name = "lblCapOrderType";
            lblCapOrderType.Size = new Size(91, 21);
            lblCapOrderType.TabIndex = 17;
            lblCapOrderType.Text = "Order type";
            // 
            // lblCapSymbol
            // 
            lblCapSymbol.AutoSize = true;
            lblCapSymbol.Font = new Font("Times New Roman", 14.25F);
            lblCapSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSymbol.Location = new Point(176, 67);
            lblCapSymbol.Margin = new Padding(2, 0, 2, 0);
            lblCapSymbol.Name = "lblCapSymbol";
            lblCapSymbol.Size = new Size(68, 21);
            lblCapSymbol.TabIndex = 16;
            lblCapSymbol.Text = "Symbol";
            // 
            // cmbSymbol
            // 
            cmbSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cmbSymbol.FlatStyle = FlatStyle.Flat;
            cmbSymbol.Font = new Font("Times New Roman", 13.8F);
            cmbSymbol.FormattingEnabled = true;
            cmbSymbol.Location = new Point(176, 87);
            cmbSymbol.Margin = new Padding(2);
            cmbSymbol.Name = "cmbSymbol";
            cmbSymbol.Size = new Size(178, 28);
            cmbSymbol.TabIndex = 15;
            cmbSymbol.Text = "  ";
            // 
            // btnBuy
            // 
            btnBuy.BackColor = Color.FromArgb(30, 58, 95);
            btnBuy.FlatAppearance.BorderSize = 0;
            btnBuy.FlatStyle = FlatStyle.Flat;
            btnBuy.Font = new Font("Times New Roman", 12F);
            btnBuy.IconChar = IconChar.None;
            btnBuy.IconColor = Color.Black;
            btnBuy.IconFont = IconFont.Auto;
            btnBuy.Location = new Point(177, 448);
            btnBuy.Margin = new Padding(2);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(192, 37);
            btnBuy.TabIndex = 14;
            btnBuy.Text = "Buy/Long";
            btnBuy.UseVisualStyleBackColor = false;
            // 
            // lblTrading
            // 
            lblTrading.AutoSize = true;
            lblTrading.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTrading.ForeColor = Color.FromArgb(156, 163, 175);
            lblTrading.Location = new Point(21, 16);
            lblTrading.Margin = new Padding(2, 0, 2, 0);
            lblTrading.Name = "lblTrading";
            lblTrading.Size = new Size(154, 32);
            lblTrading.TabIndex = 12;
            lblTrading.Text = "Add Trades";
            // 
            // cmbOrderType
            // 
            cmbOrderType.BackColor = Color.FromArgb(30, 58, 95);
            cmbOrderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrderType.FlatStyle = FlatStyle.Flat;
            cmbOrderType.Font = new Font("Times New Roman", 13.8F);
            cmbOrderType.FormattingEnabled = true;
            cmbOrderType.Location = new Point(175, 237);
            cmbOrderType.Margin = new Padding(2);
            cmbOrderType.Name = "cmbOrderType";
            cmbOrderType.Size = new Size(204, 28);
            cmbOrderType.TabIndex = 11;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblPrice.Location = new Point(600, 89);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(32, 31);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "--";
            // 
            // lblCapPrice
            // 
            lblCapPrice.AutoSize = true;
            lblCapPrice.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPrice.Location = new Point(452, 89);
            lblCapPrice.Margin = new Padding(4, 0, 4, 0);
            lblCapPrice.Name = "lblCapPrice";
            lblCapPrice.Size = new Size(114, 23);
            lblCapPrice.TabIndex = 0;
            lblCapPrice.Text = "Live price   :";
            // 
            // pnlAddTrades_Max
            // 
            pnlAddTrades_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlAddTrades_Max.Location = new Point(17, 320);
            pnlAddTrades_Max.Margin = new Padding(2);
            pnlAddTrades_Max.Name = "pnlAddTrades_Max";
            pnlAddTrades_Max.Size = new Size(1660, 606);
            pnlAddTrades_Max.TabIndex = 30;
            pnlAddTrades_Max.Visible = false;
            // 
            // FrmTrading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlAddTrades);
            Controls.Add(pnlAddTrades_Max);
            Controls.Add(pnlActiveTrades);
            Controls.Add(pnlPlatforms);
            Controls.Add(pnlPlatforms_Max);
            Controls.Add(pnlActiveTrades_Max);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmTrading";
            Size = new Size(1699, 1376);
            pnlPlatforms.ResumeLayout(false);
            pnlPlatforms.PerformLayout();
            pnlActiveTrades.ResumeLayout(false);
            pnlActiveTrades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPositions).EndInit();
            pnlAddTrades.ResumeLayout(false);
            pnlAddTrades.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPlatforms;
        private FontAwesome.Sharp.IconButton btnConnect;
        private Label lblTitle;
        private ComboBox cmbPlatform;
        private TextBox txtCred1;
        private TextBox txtCred2;
        private TextBox txtPlatformName;
        private Label lblCapBalance;
        private Label lblBalance;
        private Label lblCapPlatName;
        private Panel pnlPlatforms_Max;
        private Panel pnlActiveTrades;
        private Panel pnlTpLevels;
        private Label lblTableTitle;
        private DataGridView dgvPositions;
        private Panel pnlActiveTrades_Max;
        private Panel pnlAddTrades;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Label lblTrading;
        private ComboBox cmbOrderType;
        private ComboBox comboBox1;
        private Label lblPrice;
        private Label lblCapPrice;
        private Panel pnlAddTrades_Max;
        private Label lblCapSymbol;
        private ComboBox cmbSymbol;
        private IconButton btnBuy;
        private ComboBox comboBox3;
        private ComboBox cmbMarginMode;
        private Label lblCapUsdt;
        private ComboBox cmbSide;
        private Label lblSellMax;
        private Label lblCapSide;
        private Label lblCapOrderType;
        private Label lblCapMargin;
        private Label lblCapLeverage;
        private TextBox txtLeverage;
        private TextBox txtUsdtAmount;
        private TextBox txtTakeProfit;
        private Label lblCapTp;
        private TextBox txtStopLoss;
        private Label lblCapSl;
        private TextBox txtTriggerPrice;
        private Label lblCapTrigger;
        private TextBox txtLimitPrice;
        private Label lblCapLimit;
        private FontAwesome.Sharp.IconButton btnSell;
        private Label lblSellCost;
        private Label lblSellLiq;
        private Label lblBuyMax;
        private Label lblBuyCost;
        private Label lblBuyLiq;
        private Label lblMaxTitle;
        private Label lblCostTitle;
        private Label lblLiqTitle;
        private Label lblStatus;
        private DataGridViewTextBoxColumn colPosSymbol;
        private DataGridViewTextBoxColumn colPosLiq;
        private DataGridViewTextBoxColumn colPosRoi;
        private DataGridViewButtonColumn colPosClose;
        private DataGridViewButtonColumn colPosTp;
        private DataGridViewTextBoxColumn colPosSide;
        private DataGridViewTextBoxColumn colPosQty;
        private DataGridViewTextBoxColumn colPosEntry;
        private DataGridViewTextBoxColumn colPosMark;
        private DataGridViewTextBoxColumn colPosPnl;
        private DataGridViewTextBoxColumn colPosLev;
        private DataGridViewTextBoxColumn colPosMargin;
    }
}
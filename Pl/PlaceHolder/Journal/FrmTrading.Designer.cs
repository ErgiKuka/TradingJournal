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
            pnlActiveTrades_Max = new Panel();
            panel1 = new Panel();
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
            panel2 = new Panel();
            colPosSymbol = new DataGridViewTextBoxColumn();
            colPosLiq = new DataGridViewTextBoxColumn();
            colPosRoi = new DataGridViewTextBoxColumn();
            colPosClose = new DataGridViewButtonColumn();
            colPosSide = new DataGridViewTextBoxColumn();
            colPosQty = new DataGridViewTextBoxColumn();
            colPosEntry = new DataGridViewTextBoxColumn();
            colPosMark = new DataGridViewTextBoxColumn();
            colPosPnl = new DataGridViewTextBoxColumn();
            colPosLev = new DataGridViewTextBoxColumn();
            colPosMargin = new DataGridViewTextBoxColumn();
            pnlPlatforms.SuspendLayout();
            pnlActiveTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPositions).BeginInit();
            panel1.SuspendLayout();
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
            pnlPlatforms.Location = new Point(19, 25);
            pnlPlatforms.Margin = new Padding(2);
            pnlPlatforms.Name = "pnlPlatforms";
            pnlPlatforms.Size = new Size(1152, 369);
            pnlPlatforms.TabIndex = 26;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblStatus.Location = new Point(397, 276);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(24, 22);
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
            btnConnect.Location = new Point(45, 263);
            btnConnect.Margin = new Padding(2);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(219, 49);
            btnConnect.TabIndex = 14;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTitle.Location = new Point(24, 21);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(307, 42);
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
            cmbPlatform.Location = new Point(45, 157);
            cmbPlatform.Margin = new Padding(2);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(202, 34);
            cmbPlatform.TabIndex = 11;
            // 
            // lblCapBalance
            // 
            lblCapBalance.AutoSize = true;
            lblCapBalance.Font = new Font("Times New Roman", 13.8F);
            lblCapBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapBalance.Location = new Point(386, 157);
            lblCapBalance.Margin = new Padding(4, 0, 4, 0);
            lblCapBalance.Name = "lblCapBalance";
            lblCapBalance.Size = new Size(193, 26);
            lblCapBalance.TabIndex = 3;
            lblCapBalance.Text = "Platform balance   :";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new Font("Times New Roman", 13.8F);
            lblBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblBalance.Location = new Point(618, 157);
            lblBalance.Margin = new Padding(4, 0, 4, 0);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(28, 26);
            lblBalance.TabIndex = 1;
            lblBalance.Text = "--";
            // 
            // lblCapPlatName
            // 
            lblCapPlatName.AutoSize = true;
            lblCapPlatName.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPlatName.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPlatName.Location = new Point(45, 116);
            lblCapPlatName.Margin = new Padding(4, 0, 4, 0);
            lblCapPlatName.Name = "lblCapPlatName";
            lblCapPlatName.Size = new Size(124, 22);
            lblCapPlatName.TabIndex = 0;
            lblCapPlatName.Text = "Platform name";
            // 
            // pnlPlatforms_Max
            // 
            pnlPlatforms_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatforms_Max.Location = new Point(19, 22);
            pnlPlatforms_Max.Margin = new Padding(2);
            pnlPlatforms_Max.Name = "pnlPlatforms_Max";
            pnlPlatforms_Max.Size = new Size(2064, 371);
            pnlPlatforms_Max.TabIndex = 27;
            pnlPlatforms_Max.Visible = false;
            // 
            // pnlActiveTrades
            // 
            pnlActiveTrades.BackColor = Color.FromArgb(27, 38, 59);
            pnlActiveTrades.Controls.Add(lblTableTitle);
            pnlActiveTrades.Controls.Add(dgvPositions);
            pnlActiveTrades.Location = new Point(19, 1255);
            pnlActiveTrades.Margin = new Padding(2);
            pnlActiveTrades.Name = "pnlActiveTrades";
            pnlActiveTrades.Size = new Size(1152, 501);
            pnlActiveTrades.TabIndex = 28;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTableTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTableTitle.Location = new Point(25, 20);
            lblTableTitle.Margin = new Padding(2, 0, 2, 0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(332, 42);
            lblTableTitle.TabIndex = 13;
            lblTableTitle.Text = "Registerd Platforms";
            // 
            // dgvPositions
            // 
            dgvPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPositions.Columns.AddRange(new DataGridViewColumn[] { colPosSymbol, colPosLiq, colPosRoi, colPosClose, colPosSide, colPosQty, colPosEntry, colPosMark, colPosPnl, colPosLev, colPosMargin });
            dgvPositions.Location = new Point(21, 95);
            dgvPositions.Margin = new Padding(4);
            dgvPositions.Name = "dgvPositions";
            dgvPositions.RowHeadersWidth = 51;
            dgvPositions.Size = new Size(1101, 349);
            dgvPositions.TabIndex = 0;
            // 
            // pnlActiveTrades_Max
            // 
            pnlActiveTrades_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlActiveTrades_Max.Location = new Point(19, 1253);
            pnlActiveTrades_Max.Margin = new Padding(2);
            pnlActiveTrades_Max.Name = "pnlActiveTrades_Max";
            pnlActiveTrades_Max.Size = new Size(2064, 501);
            pnlActiveTrades_Max.TabIndex = 25;
            pnlActiveTrades_Max.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(27, 38, 59);
            panel1.Controls.Add(lblMaxTitle);
            panel1.Controls.Add(lblCostTitle);
            panel1.Controls.Add(lblLiqTitle);
            panel1.Controls.Add(lblSellMax);
            panel1.Controls.Add(lblSellCost);
            panel1.Controls.Add(lblSellLiq);
            panel1.Controls.Add(lblBuyMax);
            panel1.Controls.Add(lblBuyCost);
            panel1.Controls.Add(lblBuyLiq);
            panel1.Controls.Add(btnSell);
            panel1.Controls.Add(txtTakeProfit);
            panel1.Controls.Add(lblCapTp);
            panel1.Controls.Add(txtStopLoss);
            panel1.Controls.Add(lblCapSl);
            panel1.Controls.Add(txtTriggerPrice);
            panel1.Controls.Add(lblCapTrigger);
            panel1.Controls.Add(txtLimitPrice);
            panel1.Controls.Add(lblCapLimit);
            panel1.Controls.Add(txtUsdtAmount);
            panel1.Controls.Add(txtLeverage);
            panel1.Controls.Add(lblCapMargin);
            panel1.Controls.Add(lblCapLeverage);
            panel1.Controls.Add(cmbMarginMode);
            panel1.Controls.Add(lblCapUsdt);
            panel1.Controls.Add(lblCapOrderType);
            panel1.Controls.Add(lblCapSymbol);
            panel1.Controls.Add(cmbSymbol);
            panel1.Controls.Add(btnBuy);
            panel1.Controls.Add(lblTrading);
            panel1.Controls.Add(cmbOrderType);
            panel1.Controls.Add(lblPrice);
            panel1.Controls.Add(lblCapPrice);
            panel1.Location = new Point(19, 429);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1152, 806);
            panel1.TabIndex = 29;
            // 
            // lblMaxTitle
            // 
            lblMaxTitle.AutoSize = true;
            lblMaxTitle.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMaxTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblMaxTitle.Location = new Point(63, 758);
            lblMaxTitle.Margin = new Padding(4, 0, 4, 0);
            lblMaxTitle.Name = "lblMaxTitle";
            lblMaxTitle.Size = new Size(101, 22);
            lblMaxTitle.TabIndex = 44;
            lblMaxTitle.Text = "Max          :";
            // 
            // lblCostTitle
            // 
            lblCostTitle.AutoSize = true;
            lblCostTitle.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCostTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblCostTitle.Location = new Point(65, 717);
            lblCostTitle.Margin = new Padding(4, 0, 4, 0);
            lblCostTitle.Name = "lblCostTitle";
            lblCostTitle.Size = new Size(102, 22);
            lblCostTitle.TabIndex = 43;
            lblCostTitle.Text = "Cost          :";
            // 
            // lblLiqTitle
            // 
            lblLiqTitle.AutoSize = true;
            lblLiqTitle.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLiqTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqTitle.Location = new Point(65, 672);
            lblLiqTitle.Margin = new Padding(4, 0, 4, 0);
            lblLiqTitle.Name = "lblLiqTitle";
            lblLiqTitle.Size = new Size(105, 22);
            lblLiqTitle.TabIndex = 42;
            lblLiqTitle.Text = "Liq Price   :";
            // 
            // lblSellMax
            // 
            lblSellMax.AutoSize = true;
            lblSellMax.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSellMax.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellMax.Location = new Point(563, 758);
            lblSellMax.Margin = new Padding(4, 0, 4, 0);
            lblSellMax.Name = "lblSellMax";
            lblSellMax.Size = new Size(24, 22);
            lblSellMax.TabIndex = 41;
            lblSellMax.Text = "--";
            // 
            // lblSellCost
            // 
            lblSellCost.AutoSize = true;
            lblSellCost.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSellCost.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellCost.Location = new Point(565, 717);
            lblSellCost.Margin = new Padding(4, 0, 4, 0);
            lblSellCost.Name = "lblSellCost";
            lblSellCost.Size = new Size(24, 22);
            lblSellCost.TabIndex = 40;
            lblSellCost.Text = "--";
            // 
            // lblSellLiq
            // 
            lblSellLiq.AutoSize = true;
            lblSellLiq.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSellLiq.ForeColor = Color.FromArgb(156, 163, 175);
            lblSellLiq.Location = new Point(565, 672);
            lblSellLiq.Margin = new Padding(4, 0, 4, 0);
            lblSellLiq.Name = "lblSellLiq";
            lblSellLiq.Size = new Size(24, 22);
            lblSellLiq.TabIndex = 39;
            lblSellLiq.Text = "--";
            // 
            // lblBuyMax
            // 
            lblBuyMax.AutoSize = true;
            lblBuyMax.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBuyMax.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyMax.Location = new Point(258, 758);
            lblBuyMax.Margin = new Padding(4, 0, 4, 0);
            lblBuyMax.Name = "lblBuyMax";
            lblBuyMax.Size = new Size(24, 22);
            lblBuyMax.TabIndex = 38;
            lblBuyMax.Text = "--";
            // 
            // lblBuyCost
            // 
            lblBuyCost.AutoSize = true;
            lblBuyCost.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBuyCost.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyCost.Location = new Point(260, 717);
            lblBuyCost.Margin = new Padding(4, 0, 4, 0);
            lblBuyCost.Name = "lblBuyCost";
            lblBuyCost.Size = new Size(24, 22);
            lblBuyCost.TabIndex = 37;
            lblBuyCost.Text = "--";
            // 
            // lblBuyLiq
            // 
            lblBuyLiq.AutoSize = true;
            lblBuyLiq.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBuyLiq.ForeColor = Color.FromArgb(156, 163, 175);
            lblBuyLiq.Location = new Point(260, 672);
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
            btnSell.Location = new Point(500, 598);
            btnSell.Margin = new Padding(2);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(219, 49);
            btnSell.TabIndex = 35;
            btnSell.Text = "Sell/Short";
            btnSell.UseVisualStyleBackColor = false;
            // 
            // txtTakeProfit
            // 
            txtTakeProfit.BackColor = Color.FromArgb(30, 58, 95);
            txtTakeProfit.BorderStyle = BorderStyle.None;
            txtTakeProfit.Font = new Font("Times New Roman", 13.8F);
            txtTakeProfit.Location = new Point(517, 501);
            txtTakeProfit.Margin = new Padding(2);
            txtTakeProfit.Name = "txtTakeProfit";
            txtTakeProfit.Size = new Size(202, 27);
            txtTakeProfit.TabIndex = 34;
            txtTakeProfit.Text = "   ";
            // 
            // lblCapTp
            // 
            lblCapTp.AutoSize = true;
            lblCapTp.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapTp.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTp.Location = new Point(517, 466);
            lblCapTp.Margin = new Padding(4, 0, 4, 0);
            lblCapTp.Name = "lblCapTp";
            lblCapTp.Size = new Size(183, 22);
            lblCapTp.TabIndex = 33;
            lblCapTp.Text = "Take-profit (optional)";
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 58, 95);
            txtStopLoss.BorderStyle = BorderStyle.None;
            txtStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtStopLoss.Location = new Point(200, 501);
            txtStopLoss.Margin = new Padding(2);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(202, 27);
            txtStopLoss.TabIndex = 32;
            txtStopLoss.Text = "   ";
            // 
            // lblCapSl
            // 
            lblCapSl.AutoSize = true;
            lblCapSl.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapSl.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSl.Location = new Point(200, 466);
            lblCapSl.Margin = new Padding(4, 0, 4, 0);
            lblCapSl.Name = "lblCapSl";
            lblCapSl.Size = new Size(169, 22);
            lblCapSl.TabIndex = 31;
            lblCapSl.Text = "Stop-loss (optional)";
            // 
            // txtTriggerPrice
            // 
            txtTriggerPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtTriggerPrice.BorderStyle = BorderStyle.None;
            txtTriggerPrice.Font = new Font("Times New Roman", 13.8F);
            txtTriggerPrice.Location = new Point(517, 316);
            txtTriggerPrice.Margin = new Padding(2);
            txtTriggerPrice.Name = "txtTriggerPrice";
            txtTriggerPrice.Size = new Size(202, 27);
            txtTriggerPrice.TabIndex = 30;
            txtTriggerPrice.Text = "   ";
            // 
            // lblCapTrigger
            // 
            lblCapTrigger.AutoSize = true;
            lblCapTrigger.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapTrigger.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTrigger.Location = new Point(517, 281);
            lblCapTrigger.Margin = new Padding(4, 0, 4, 0);
            lblCapTrigger.Name = "lblCapTrigger";
            lblCapTrigger.Size = new Size(114, 22);
            lblCapTrigger.TabIndex = 29;
            lblCapTrigger.Text = "Trigger price";
            // 
            // txtLimitPrice
            // 
            txtLimitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtLimitPrice.BorderStyle = BorderStyle.None;
            txtLimitPrice.Font = new Font("Times New Roman", 13.8F);
            txtLimitPrice.Location = new Point(202, 405);
            txtLimitPrice.Margin = new Padding(2);
            txtLimitPrice.Name = "txtLimitPrice";
            txtLimitPrice.Size = new Size(202, 27);
            txtLimitPrice.TabIndex = 28;
            txtLimitPrice.Text = "   ";
            // 
            // lblCapLimit
            // 
            lblCapLimit.AutoSize = true;
            lblCapLimit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapLimit.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLimit.Location = new Point(202, 370);
            lblCapLimit.Margin = new Padding(4, 0, 4, 0);
            lblCapLimit.Name = "lblCapLimit";
            lblCapLimit.Size = new Size(98, 22);
            lblCapLimit.TabIndex = 27;
            lblCapLimit.Text = "Limit price";
            // 
            // txtUsdtAmount
            // 
            txtUsdtAmount.BackColor = Color.FromArgb(30, 58, 95);
            txtUsdtAmount.BorderStyle = BorderStyle.None;
            txtUsdtAmount.Font = new Font("Times New Roman", 13.8F);
            txtUsdtAmount.Location = new Point(517, 405);
            txtUsdtAmount.Margin = new Padding(2);
            txtUsdtAmount.Name = "txtUsdtAmount";
            txtUsdtAmount.Size = new Size(202, 27);
            txtUsdtAmount.TabIndex = 26;
            txtUsdtAmount.Text = "   ";
            // 
            // txtLeverage
            // 
            txtLeverage.BackColor = Color.FromArgb(30, 58, 95);
            txtLeverage.BorderStyle = BorderStyle.None;
            txtLeverage.Font = new Font("Times New Roman", 13.8F);
            txtLeverage.Location = new Point(517, 221);
            txtLeverage.Margin = new Padding(2);
            txtLeverage.Name = "txtLeverage";
            txtLeverage.Size = new Size(105, 27);
            txtLeverage.TabIndex = 25;
            txtLeverage.Text = "   ";
            // 
            // lblCapMargin
            // 
            lblCapMargin.AutoSize = true;
            lblCapMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapMargin.Location = new Point(200, 188);
            lblCapMargin.Margin = new Padding(4, 0, 4, 0);
            lblCapMargin.Name = "lblCapMargin";
            lblCapMargin.Size = new Size(72, 22);
            lblCapMargin.TabIndex = 24;
            lblCapMargin.Text = "Margin ";
            // 
            // lblCapLeverage
            // 
            lblCapLeverage.AutoSize = true;
            lblCapLeverage.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapLeverage.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLeverage.Location = new Point(517, 188);
            lblCapLeverage.Margin = new Padding(4, 0, 4, 0);
            lblCapLeverage.Name = "lblCapLeverage";
            lblCapLeverage.Size = new Size(83, 22);
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
            cmbMarginMode.Location = new Point(200, 218);
            cmbMarginMode.Margin = new Padding(2);
            cmbMarginMode.Name = "cmbMarginMode";
            cmbMarginMode.Size = new Size(202, 34);
            cmbMarginMode.TabIndex = 20;
            // 
            // lblCapUsdt
            // 
            lblCapUsdt.AutoSize = true;
            lblCapUsdt.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapUsdt.Location = new Point(517, 370);
            lblCapUsdt.Margin = new Padding(4, 0, 4, 0);
            lblCapUsdt.Name = "lblCapUsdt";
            lblCapUsdt.Size = new Size(140, 22);
            lblCapUsdt.TabIndex = 19;
            lblCapUsdt.Text = "Amount (USDT)";
            // 
            // lblCapOrderType
            // 
            lblCapOrderType.AutoSize = true;
            lblCapOrderType.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapOrderType.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapOrderType.Location = new Point(200, 276);
            lblCapOrderType.Margin = new Padding(4, 0, 4, 0);
            lblCapOrderType.Name = "lblCapOrderType";
            lblCapOrderType.Size = new Size(95, 22);
            lblCapOrderType.TabIndex = 17;
            lblCapOrderType.Text = "Order type";
            // 
            // lblCapSymbol
            // 
            lblCapSymbol.AutoSize = true;
            lblCapSymbol.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSymbol.Location = new Point(201, 89);
            lblCapSymbol.Margin = new Padding(2, 0, 2, 0);
            lblCapSymbol.Name = "lblCapSymbol";
            lblCapSymbol.Size = new Size(70, 22);
            lblCapSymbol.TabIndex = 16;
            lblCapSymbol.Text = "Symbol";
            // 
            // cmbSymbol
            // 
            cmbSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cmbSymbol.FlatStyle = FlatStyle.Flat;
            cmbSymbol.Font = new Font("Times New Roman", 13.8F);
            cmbSymbol.FormattingEnabled = true;
            cmbSymbol.Location = new Point(201, 116);
            cmbSymbol.Margin = new Padding(2);
            cmbSymbol.Name = "cmbSymbol";
            cmbSymbol.Size = new Size(203, 34);
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
            btnBuy.Location = new Point(202, 598);
            btnBuy.Margin = new Padding(2);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(219, 49);
            btnBuy.TabIndex = 14;
            btnBuy.Text = "Buy/Long";
            btnBuy.UseVisualStyleBackColor = false;
            // 
            // lblTrading
            // 
            lblTrading.AutoSize = true;
            lblTrading.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTrading.ForeColor = Color.FromArgb(156, 163, 175);
            lblTrading.Location = new Point(24, 21);
            lblTrading.Margin = new Padding(2, 0, 2, 0);
            lblTrading.Name = "lblTrading";
            lblTrading.Size = new Size(203, 42);
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
            cmbOrderType.Location = new Point(200, 316);
            cmbOrderType.Margin = new Padding(2);
            cmbOrderType.Name = "cmbOrderType";
            cmbOrderType.Size = new Size(232, 34);
            cmbOrderType.TabIndex = 11;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Times New Roman", 13.8F);
            lblPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblPrice.Location = new Point(654, 119);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(28, 26);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "--";
            // 
            // lblCapPrice
            // 
            lblCapPrice.AutoSize = true;
            lblCapPrice.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPrice.Location = new Point(517, 119);
            lblCapPrice.Margin = new Padding(4, 0, 4, 0);
            lblCapPrice.Name = "lblCapPrice";
            lblCapPrice.Size = new Size(129, 26);
            lblCapPrice.TabIndex = 0;
            lblCapPrice.Text = "Live price   :";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(27, 38, 59);
            panel2.Location = new Point(19, 426);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(2064, 808);
            panel2.TabIndex = 30;
            panel2.Visible = false;
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
            // FrmTrading
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(pnlActiveTrades);
            Controls.Add(pnlPlatforms);
            Controls.Add(pnlPlatforms_Max);
            Controls.Add(pnlActiveTrades_Max);
            Name = "FrmTrading";
            Size = new Size(2124, 1774);
            pnlPlatforms.ResumeLayout(false);
            pnlPlatforms.PerformLayout();
            pnlActiveTrades.ResumeLayout(false);
            pnlActiveTrades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPositions).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Label lblTrading;
        private ComboBox cmbOrderType;
        private ComboBox comboBox1;
        private Label lblPrice;
        private Label lblCapPrice;
        private Panel panel2;
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
        private DataGridViewTextBoxColumn colPosSide;
        private DataGridViewTextBoxColumn colPosQty;
        private DataGridViewTextBoxColumn colPosEntry;
        private DataGridViewTextBoxColumn colPosMark;
        private DataGridViewTextBoxColumn colPosPnl;
        private DataGridViewTextBoxColumn colPosLev;
        private DataGridViewTextBoxColumn colPosMargin;
    }
}

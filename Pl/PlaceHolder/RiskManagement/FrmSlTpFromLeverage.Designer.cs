namespace TradingJournal.Pl.PlaceHolder.RiskManagement
{
    partial class FrmSlTpFromLeverage
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
            pnlSlnTp = new Panel();
            txtCoinQty = new TextBox();
            lblCapPosCoin = new Label();
            lblTitle = new Label();
            cmbDirection = new ComboBox();
            txtMargin = new TextBox();
            txtLeverage = new TextBox();
            txtRiskPercent = new TextBox();
            txtEntryPrice = new TextBox();
            txtBalance = new TextBox();
            lblCapLeverage = new Label();
            lblCapMargin = new Label();
            lblCapEntry = new Label();
            lblCapDirection = new Label();
            lblCapRisk = new Label();
            lblCapBalance = new Label();
            pnlTpLevels = new Panel();
            lblTableTitle = new Label();
            dgvTakeProfits = new DataGridView();
            colLevel = new DataGridViewTextBoxColumn();
            colMultiple = new DataGridViewTextBoxColumn();
            colPercent = new DataGridViewTextBoxColumn();
            colMovePct = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colPnl = new DataGridViewTextBoxColumn();
            pnlMetrics = new Panel();
            lblMetricsTitile = new Label();
            lblRewardToRisk = new Label();
            lblCapRr = new Label();
            lblReturnOnMargin = new Label();
            lblCapReturnMargin = new Label();
            lblTotalPnl = new Label();
            lblCapTotalPnl = new Label();
            lblLiqDistancePct = new Label();
            lblCapLiqDist = new Label();
            lblStopLossPrice = new Label();
            lblCapSlPrice = new Label();
            lblSlDistancePct = new Label();
            lblCapSlDist = new Label();
            lblPositionCoins = new Label();
            lblCapPositionCoins = new Label();
            lblPositionUsdt = new Label();
            lblCapPositionUsdt = new Label();
            lblCapRiskAmount = new Label();
            lblRiskAmount = new Label();
            pnlChecks = new Panel();
            llblChecksTitle = new Label();
            lblAllocationCheck = new Label();
            lblStatus = new Label();
            lblCapAllocation = new Label();
            lblLiqCheck = new Label();
            lblCapLiqCheck = new Label();
            lblCapMarginCheck = new Label();
            lblMarginCheck = new Label();
            pnlSlnTp_Max = new Panel();
            pnlTpLevels_Max = new Panel();
            pnlMetrics_Max = new Panel();
            pnlChecks_Max = new Panel();
            pnlSlnTp.SuspendLayout();
            pnlTpLevels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTakeProfits).BeginInit();
            pnlMetrics.SuspendLayout();
            pnlChecks.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSlnTp
            // 
            pnlSlnTp.BackColor = Color.FromArgb(27, 38, 59);
            pnlSlnTp.Controls.Add(txtCoinQty);
            pnlSlnTp.Controls.Add(lblCapPosCoin);
            pnlSlnTp.Controls.Add(lblTitle);
            pnlSlnTp.Controls.Add(cmbDirection);
            pnlSlnTp.Controls.Add(txtMargin);
            pnlSlnTp.Controls.Add(txtLeverage);
            pnlSlnTp.Controls.Add(txtRiskPercent);
            pnlSlnTp.Controls.Add(txtEntryPrice);
            pnlSlnTp.Controls.Add(txtBalance);
            pnlSlnTp.Controls.Add(lblCapLeverage);
            pnlSlnTp.Controls.Add(lblCapMargin);
            pnlSlnTp.Controls.Add(lblCapEntry);
            pnlSlnTp.Controls.Add(lblCapDirection);
            pnlSlnTp.Controls.Add(lblCapRisk);
            pnlSlnTp.Controls.Add(lblCapBalance);
            pnlSlnTp.Location = new Point(20, 15);
            pnlSlnTp.Margin = new Padding(2);
            pnlSlnTp.Name = "pnlSlnTp";
            pnlSlnTp.Size = new Size(922, 339);
            pnlSlnTp.TabIndex = 19;
            // 
            // txtCoinQty
            // 
            txtCoinQty.BackColor = Color.FromArgb(30, 58, 95);
            txtCoinQty.BorderStyle = BorderStyle.None;
            txtCoinQty.Font = new Font("Times New Roman", 13.8F);
            txtCoinQty.Location = new Point(242, 272);
            txtCoinQty.Margin = new Padding(2);
            txtCoinQty.Name = "txtCoinQty";
            txtCoinQty.Size = new Size(162, 22);
            txtCoinQty.TabIndex = 14;
            txtCoinQty.Text = "   ";
            // 
            // lblCapPosCoin
            // 
            lblCapPosCoin.AutoSize = true;
            lblCapPosCoin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPosCoin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPosCoin.Location = new Point(238, 251);
            lblCapPosCoin.Name = "lblCapPosCoin";
            lblCapPosCoin.Size = new Size(154, 19);
            lblCapPosCoin.TabIndex = 13;
            lblCapPosCoin.Text = "Margin per trade (Coin)";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTitle.Location = new Point(19, 17);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(498, 32);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Stop-Loss and Take-Profit from Margin";
            // 
            // cmbDirection
            // 
            cmbDirection.BackColor = Color.FromArgb(30, 58, 95);
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.FlatStyle = FlatStyle.Flat;
            cmbDirection.Font = new Font("Times New Roman", 13.8F);
            cmbDirection.FormattingEnabled = true;
            cmbDirection.Items.AddRange(new object[] { "Long", "Short" });
            cmbDirection.Location = new Point(240, 188);
            cmbDirection.Margin = new Padding(2);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(162, 28);
            cmbDirection.TabIndex = 11;
            // 
            // txtMargin
            // 
            txtMargin.BackColor = Color.FromArgb(30, 58, 95);
            txtMargin.BorderStyle = BorderStyle.None;
            txtMargin.Font = new Font("Times New Roman", 13.8F);
            txtMargin.Location = new Point(20, 272);
            txtMargin.Margin = new Padding(2);
            txtMargin.Name = "txtMargin";
            txtMargin.Size = new Size(162, 22);
            txtMargin.TabIndex = 10;
            txtMargin.Text = "   ";
            // 
            // txtLeverage
            // 
            txtLeverage.BackColor = Color.FromArgb(30, 58, 95);
            txtLeverage.BorderStyle = BorderStyle.None;
            txtLeverage.Font = new Font("Times New Roman", 13.8F);
            txtLeverage.Location = new Point(426, 111);
            txtLeverage.Margin = new Padding(2);
            txtLeverage.Name = "txtLeverage";
            txtLeverage.Size = new Size(162, 22);
            txtLeverage.TabIndex = 9;
            // 
            // txtRiskPercent
            // 
            txtRiskPercent.BackColor = Color.FromArgb(30, 58, 95);
            txtRiskPercent.BorderStyle = BorderStyle.None;
            txtRiskPercent.Font = new Font("Times New Roman", 13.8F);
            txtRiskPercent.Location = new Point(240, 111);
            txtRiskPercent.Margin = new Padding(2);
            txtRiskPercent.Name = "txtRiskPercent";
            txtRiskPercent.Size = new Size(162, 22);
            txtRiskPercent.TabIndex = 8;
            txtRiskPercent.Text = "   ";
            // 
            // txtEntryPrice
            // 
            txtEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtEntryPrice.BorderStyle = BorderStyle.None;
            txtEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtEntryPrice.Location = new Point(20, 188);
            txtEntryPrice.Margin = new Padding(2);
            txtEntryPrice.Name = "txtEntryPrice";
            txtEntryPrice.Size = new Size(162, 22);
            txtEntryPrice.TabIndex = 7;
            txtEntryPrice.Text = "   ";
            // 
            // txtBalance
            // 
            txtBalance.BackColor = Color.FromArgb(30, 58, 95);
            txtBalance.BorderStyle = BorderStyle.None;
            txtBalance.Font = new Font("Times New Roman", 13.8F);
            txtBalance.Location = new Point(20, 111);
            txtBalance.Margin = new Padding(2);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(162, 22);
            txtBalance.TabIndex = 6;
            txtBalance.Text = "   ";
            // 
            // lblCapLeverage
            // 
            lblCapLeverage.AutoSize = true;
            lblCapLeverage.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapLeverage.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLeverage.Location = new Point(422, 86);
            lblCapLeverage.Name = "lblCapLeverage";
            lblCapLeverage.Size = new Size(65, 19);
            lblCapLeverage.TabIndex = 5;
            lblCapLeverage.Text = "Leverage";
            // 
            // lblCapMargin
            // 
            lblCapMargin.AutoSize = true;
            lblCapMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapMargin.Location = new Point(17, 251);
            lblCapMargin.Name = "lblCapMargin";
            lblCapMargin.Size = new Size(165, 19);
            lblCapMargin.TabIndex = 4;
            lblCapMargin.Text = "Margin per trade (USDT)";
            // 
            // lblCapEntry
            // 
            lblCapEntry.AutoSize = true;
            lblCapEntry.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapEntry.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapEntry.Location = new Point(20, 162);
            lblCapEntry.Name = "lblCapEntry";
            lblCapEntry.Size = new Size(75, 19);
            lblCapEntry.TabIndex = 3;
            lblCapEntry.Text = "Entry price";
            // 
            // lblCapDirection
            // 
            lblCapDirection.AutoSize = true;
            lblCapDirection.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapDirection.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapDirection.Location = new Point(240, 162);
            lblCapDirection.Name = "lblCapDirection";
            lblCapDirection.Size = new Size(64, 19);
            lblCapDirection.TabIndex = 2;
            lblCapDirection.Text = "Direction";
            // 
            // lblCapRisk
            // 
            lblCapRisk.AutoSize = true;
            lblCapRisk.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapRisk.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapRisk.Location = new Point(242, 90);
            lblCapRisk.Name = "lblCapRisk";
            lblCapRisk.Size = new Size(122, 19);
            lblCapRisk.TabIndex = 1;
            lblCapRisk.Text = "Risk per trade (%)";
            // 
            // lblCapBalance
            // 
            lblCapBalance.AutoSize = true;
            lblCapBalance.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapBalance.Location = new Point(18, 85);
            lblCapBalance.Name = "lblCapBalance";
            lblCapBalance.Size = new Size(164, 19);
            lblCapBalance.TabIndex = 0;
            lblCapBalance.Text = "Account balance (USDT)";
            // 
            // pnlTpLevels
            // 
            pnlTpLevels.BackColor = Color.FromArgb(27, 38, 59);
            pnlTpLevels.Controls.Add(lblTableTitle);
            pnlTpLevels.Controls.Add(dgvTakeProfits);
            pnlTpLevels.Location = new Point(20, 386);
            pnlTpLevels.Margin = new Padding(2);
            pnlTpLevels.Name = "pnlTpLevels";
            pnlTpLevels.Size = new Size(922, 280);
            pnlTpLevels.TabIndex = 20;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTableTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTableTitle.Location = new Point(20, 16);
            lblTableTitle.Margin = new Padding(2, 0, 2, 0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(224, 32);
            lblTableTitle.TabIndex = 13;
            lblTableTitle.Text = "Take-profit levels";
            // 
            // dgvTakeProfits
            // 
            dgvTakeProfits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTakeProfits.Columns.AddRange(new DataGridViewColumn[] { colLevel, colMultiple, colPercent, colMovePct, colPrice, colQty, colPnl });
            dgvTakeProfits.Location = new Point(17, 76);
            dgvTakeProfits.Name = "dgvTakeProfits";
            dgvTakeProfits.RowHeadersWidth = 51;
            dgvTakeProfits.Size = new Size(881, 204);
            dgvTakeProfits.TabIndex = 0;
            // 
            // colLevel
            // 
            colLevel.HeaderText = "Level";
            colLevel.MinimumWidth = 6;
            colLevel.Name = "colLevel";
            colLevel.ReadOnly = true;
            colLevel.Width = 125;
            // 
            // colMultiple
            // 
            colMultiple.HeaderText = "× SL distance";
            colMultiple.MinimumWidth = 6;
            colMultiple.Name = "colMultiple";
            colMultiple.Width = 125;
            // 
            // colPercent
            // 
            colPercent.HeaderText = "% of position";
            colPercent.MinimumWidth = 6;
            colPercent.Name = "colPercent";
            colPercent.Width = 125;
            // 
            // colMovePct
            // 
            colMovePct.HeaderText = "% move";
            colMovePct.MinimumWidth = 6;
            colMovePct.Name = "colMovePct";
            colMovePct.ReadOnly = true;
            colMovePct.Width = 125;
            // 
            // colPrice
            // 
            colPrice.HeaderText = "TP price";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            colPrice.Width = 125;
            // 
            // colQty
            // 
            colQty.HeaderText = "Qty (USDT)";
            colQty.MinimumWidth = 6;
            colQty.Name = "colQty";
            colQty.ReadOnly = true;
            colQty.Width = 125;
            // 
            // colPnl
            // 
            colPnl.HeaderText = "PnL";
            colPnl.MinimumWidth = 6;
            colPnl.Name = "colPnl";
            colPnl.ReadOnly = true;
            colPnl.Width = 125;
            // 
            // pnlMetrics
            // 
            pnlMetrics.BackColor = Color.FromArgb(27, 38, 59);
            pnlMetrics.Controls.Add(lblMetricsTitile);
            pnlMetrics.Controls.Add(lblRewardToRisk);
            pnlMetrics.Controls.Add(lblCapRr);
            pnlMetrics.Controls.Add(lblReturnOnMargin);
            pnlMetrics.Controls.Add(lblCapReturnMargin);
            pnlMetrics.Controls.Add(lblTotalPnl);
            pnlMetrics.Controls.Add(lblCapTotalPnl);
            pnlMetrics.Controls.Add(lblLiqDistancePct);
            pnlMetrics.Controls.Add(lblCapLiqDist);
            pnlMetrics.Controls.Add(lblStopLossPrice);
            pnlMetrics.Controls.Add(lblCapSlPrice);
            pnlMetrics.Controls.Add(lblSlDistancePct);
            pnlMetrics.Controls.Add(lblCapSlDist);
            pnlMetrics.Controls.Add(lblPositionCoins);
            pnlMetrics.Controls.Add(lblCapPositionCoins);
            pnlMetrics.Controls.Add(lblPositionUsdt);
            pnlMetrics.Controls.Add(lblCapPositionUsdt);
            pnlMetrics.Controls.Add(lblCapRiskAmount);
            pnlMetrics.Controls.Add(lblRiskAmount);
            pnlMetrics.Location = new Point(20, 700);
            pnlMetrics.Margin = new Padding(2);
            pnlMetrics.Name = "pnlMetrics";
            pnlMetrics.Size = new Size(922, 335);
            pnlMetrics.TabIndex = 21;
            // 
            // lblMetricsTitile
            // 
            lblMetricsTitile.AutoSize = true;
            lblMetricsTitile.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMetricsTitile.ForeColor = Color.FromArgb(156, 163, 175);
            lblMetricsTitile.Location = new Point(17, 16);
            lblMetricsTitile.Margin = new Padding(2, 0, 2, 0);
            lblMetricsTitile.Name = "lblMetricsTitile";
            lblMetricsTitile.Size = new Size(109, 32);
            lblMetricsTitile.TabIndex = 18;
            lblMetricsTitile.Text = "Metrics";
            // 
            // lblRewardToRisk
            // 
            lblRewardToRisk.AutoSize = true;
            lblRewardToRisk.Font = new Font("Times New Roman", 15F);
            lblRewardToRisk.ForeColor = Color.FromArgb(156, 163, 175);
            lblRewardToRisk.Location = new Point(715, 240);
            lblRewardToRisk.Name = "lblRewardToRisk";
            lblRewardToRisk.Size = new Size(24, 22);
            lblRewardToRisk.TabIndex = 17;
            lblRewardToRisk.Text = "--";
            // 
            // lblCapRr
            // 
            lblCapRr.AutoSize = true;
            lblCapRr.Font = new Font("Times New Roman", 15F);
            lblCapRr.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapRr.Location = new Point(462, 240);
            lblCapRr.Name = "lblCapRr";
            lblCapRr.Size = new Size(238, 22);
            lblCapRr.TabIndex = 16;
            lblCapRr.Text = "Reward-to-risk                    :";
            // 
            // lblReturnOnMargin
            // 
            lblReturnOnMargin.AutoSize = true;
            lblReturnOnMargin.Font = new Font("Times New Roman", 15F);
            lblReturnOnMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblReturnOnMargin.Location = new Point(715, 191);
            lblReturnOnMargin.Name = "lblReturnOnMargin";
            lblReturnOnMargin.Size = new Size(24, 22);
            lblReturnOnMargin.TabIndex = 15;
            lblReturnOnMargin.Text = "--";
            // 
            // lblCapReturnMargin
            // 
            lblCapReturnMargin.AutoSize = true;
            lblCapReturnMargin.Font = new Font("Times New Roman", 15F);
            lblCapReturnMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapReturnMargin.Location = new Point(462, 191);
            lblCapReturnMargin.Name = "lblCapReturnMargin";
            lblCapReturnMargin.Size = new Size(236, 22);
            lblCapReturnMargin.TabIndex = 14;
            lblCapReturnMargin.Text = "Return on margin                 :";
            // 
            // lblTotalPnl
            // 
            lblTotalPnl.AutoSize = true;
            lblTotalPnl.Font = new Font("Times New Roman", 15F);
            lblTotalPnl.ForeColor = Color.FromArgb(156, 163, 175);
            lblTotalPnl.Location = new Point(715, 139);
            lblTotalPnl.Name = "lblTotalPnl";
            lblTotalPnl.Size = new Size(24, 22);
            lblTotalPnl.TabIndex = 13;
            lblTotalPnl.Text = "--";
            // 
            // lblCapTotalPnl
            // 
            lblCapTotalPnl.AutoSize = true;
            lblCapTotalPnl.Font = new Font("Times New Roman", 15F);
            lblCapTotalPnl.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTotalPnl.Location = new Point(462, 139);
            lblCapTotalPnl.Name = "lblCapTotalPnl";
            lblCapTotalPnl.Size = new Size(238, 22);
            lblCapTotalPnl.TabIndex = 12;
            lblCapTotalPnl.Text = "Total PnL (all TPs)              :";
            // 
            // lblLiqDistancePct
            // 
            lblLiqDistancePct.AutoSize = true;
            lblLiqDistancePct.Font = new Font("Times New Roman", 15F);
            lblLiqDistancePct.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqDistancePct.Location = new Point(715, 85);
            lblLiqDistancePct.Name = "lblLiqDistancePct";
            lblLiqDistancePct.Size = new Size(24, 22);
            lblLiqDistancePct.TabIndex = 11;
            lblLiqDistancePct.Text = "--";
            // 
            // lblCapLiqDist
            // 
            lblCapLiqDist.AutoSize = true;
            lblCapLiqDist.Font = new Font("Times New Roman", 15F);
            lblCapLiqDist.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLiqDist.Location = new Point(462, 85);
            lblCapLiqDist.Name = "lblCapLiqDist";
            lblCapLiqDist.Size = new Size(244, 22);
            lblCapLiqDist.TabIndex = 10;
            lblCapLiqDist.Text = "Liquidation distance %         :";
            // 
            // lblStopLossPrice
            // 
            lblStopLossPrice.AutoSize = true;
            lblStopLossPrice.Font = new Font("Times New Roman", 15F);
            lblStopLossPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblStopLossPrice.Location = new Point(264, 292);
            lblStopLossPrice.Name = "lblStopLossPrice";
            lblStopLossPrice.Size = new Size(24, 22);
            lblStopLossPrice.TabIndex = 9;
            lblStopLossPrice.Text = "--";
            // 
            // lblCapSlPrice
            // 
            lblCapSlPrice.AutoSize = true;
            lblCapSlPrice.Font = new Font("Times New Roman", 15F);
            lblCapSlPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSlPrice.Location = new Point(30, 292);
            lblCapSlPrice.Name = "lblCapSlPrice";
            lblCapSlPrice.Size = new Size(227, 22);
            lblCapSlPrice.TabIndex = 8;
            lblCapSlPrice.Text = "Stop-loss price                  :";
            // 
            // lblSlDistancePct
            // 
            lblSlDistancePct.AutoSize = true;
            lblSlDistancePct.Font = new Font("Times New Roman", 15F);
            lblSlDistancePct.ForeColor = Color.FromArgb(156, 163, 175);
            lblSlDistancePct.Location = new Point(264, 240);
            lblSlDistancePct.Name = "lblSlDistancePct";
            lblSlDistancePct.Size = new Size(24, 22);
            lblSlDistancePct.TabIndex = 7;
            lblSlDistancePct.Text = "--";
            // 
            // lblCapSlDist
            // 
            lblCapSlDist.AutoSize = true;
            lblCapSlDist.Font = new Font("Times New Roman", 15F);
            lblCapSlDist.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSlDist.Location = new Point(30, 240);
            lblCapSlDist.Name = "lblCapSlDist";
            lblCapSlDist.Size = new Size(224, 22);
            lblCapSlDist.TabIndex = 6;
            lblCapSlDist.Text = "SL distance %                   :";
            // 
            // lblPositionCoins
            // 
            lblPositionCoins.AutoSize = true;
            lblPositionCoins.Font = new Font("Times New Roman", 15F);
            lblPositionCoins.ForeColor = Color.FromArgb(156, 163, 175);
            lblPositionCoins.Location = new Point(264, 191);
            lblPositionCoins.Name = "lblPositionCoins";
            lblPositionCoins.Size = new Size(24, 22);
            lblPositionCoins.TabIndex = 5;
            lblPositionCoins.Text = "--";
            // 
            // lblCapPositionCoins
            // 
            lblCapPositionCoins.AutoSize = true;
            lblCapPositionCoins.Font = new Font("Times New Roman", 15F);
            lblCapPositionCoins.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPositionCoins.Location = new Point(30, 191);
            lblCapPositionCoins.Name = "lblCapPositionCoins";
            lblCapPositionCoins.Size = new Size(228, 22);
            lblCapPositionCoins.TabIndex = 4;
            lblCapPositionCoins.Text = "Position size (coins)          :";
            // 
            // lblPositionUsdt
            // 
            lblPositionUsdt.AutoSize = true;
            lblPositionUsdt.Font = new Font("Times New Roman", 15F);
            lblPositionUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblPositionUsdt.Location = new Point(264, 139);
            lblPositionUsdt.Name = "lblPositionUsdt";
            lblPositionUsdt.Size = new Size(24, 22);
            lblPositionUsdt.TabIndex = 3;
            lblPositionUsdt.Text = "--";
            // 
            // lblCapPositionUsdt
            // 
            lblCapPositionUsdt.AutoSize = true;
            lblCapPositionUsdt.Font = new Font("Times New Roman", 15F);
            lblCapPositionUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPositionUsdt.Location = new Point(30, 139);
            lblCapPositionUsdt.Name = "lblCapPositionUsdt";
            lblCapPositionUsdt.Size = new Size(226, 22);
            lblCapPositionUsdt.TabIndex = 2;
            lblCapPositionUsdt.Text = "Position size (USDT)        :";
            // 
            // lblCapRiskAmount
            // 
            lblCapRiskAmount.AutoSize = true;
            lblCapRiskAmount.Font = new Font("Times New Roman", 15F);
            lblCapRiskAmount.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapRiskAmount.Location = new Point(30, 85);
            lblCapRiskAmount.Name = "lblCapRiskAmount";
            lblCapRiskAmount.Size = new Size(223, 22);
            lblCapRiskAmount.TabIndex = 1;
            lblCapRiskAmount.Text = "Risk amount                      :";
            // 
            // lblRiskAmount
            // 
            lblRiskAmount.AutoSize = true;
            lblRiskAmount.Font = new Font("Times New Roman", 15F);
            lblRiskAmount.ForeColor = Color.FromArgb(156, 163, 175);
            lblRiskAmount.Location = new Point(264, 85);
            lblRiskAmount.Name = "lblRiskAmount";
            lblRiskAmount.Size = new Size(24, 22);
            lblRiskAmount.TabIndex = 1;
            lblRiskAmount.Text = "--";
            // 
            // pnlChecks
            // 
            pnlChecks.BackColor = Color.FromArgb(27, 38, 59);
            pnlChecks.Controls.Add(llblChecksTitle);
            pnlChecks.Controls.Add(lblAllocationCheck);
            pnlChecks.Controls.Add(lblStatus);
            pnlChecks.Controls.Add(lblCapAllocation);
            pnlChecks.Controls.Add(lblLiqCheck);
            pnlChecks.Controls.Add(lblCapLiqCheck);
            pnlChecks.Controls.Add(lblCapMarginCheck);
            pnlChecks.Controls.Add(lblMarginCheck);
            pnlChecks.Location = new Point(20, 1079);
            pnlChecks.Margin = new Padding(2);
            pnlChecks.Name = "pnlChecks";
            pnlChecks.Size = new Size(922, 293);
            pnlChecks.TabIndex = 22;
            // 
            // llblChecksTitle
            // 
            llblChecksTitle.AutoSize = true;
            llblChecksTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            llblChecksTitle.ForeColor = Color.FromArgb(156, 163, 175);
            llblChecksTitle.Location = new Point(19, 16);
            llblChecksTitle.Margin = new Padding(2, 0, 2, 0);
            llblChecksTitle.Name = "llblChecksTitle";
            llblChecksTitle.Size = new Size(110, 32);
            llblChecksTitle.TabIndex = 19;
            llblChecksTitle.Text = "Checks ";
            // 
            // lblAllocationCheck
            // 
            lblAllocationCheck.AutoSize = true;
            lblAllocationCheck.Font = new Font("Times New Roman", 15F);
            lblAllocationCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblAllocationCheck.Location = new Point(253, 182);
            lblAllocationCheck.Name = "lblAllocationCheck";
            lblAllocationCheck.Size = new Size(24, 22);
            lblAllocationCheck.TabIndex = 16;
            lblAllocationCheck.Text = "--";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Times New Roman", 15F);
            lblStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblStatus.Location = new Point(30, 237);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(57, 22);
            lblStatus.TabIndex = 14;
            lblStatus.Text = "Status";
            // 
            // lblCapAllocation
            // 
            lblCapAllocation.AutoSize = true;
            lblCapAllocation.Font = new Font("Times New Roman", 15F);
            lblCapAllocation.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapAllocation.Location = new Point(30, 182);
            lblCapAllocation.Name = "lblCapAllocation";
            lblCapAllocation.Size = new Size(215, 22);
            lblCapAllocation.TabIndex = 4;
            lblCapAllocation.Text = "Allocation                       :";
            // 
            // lblLiqCheck
            // 
            lblLiqCheck.AutoSize = true;
            lblLiqCheck.Font = new Font("Times New Roman", 15F);
            lblLiqCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqCheck.Location = new Point(253, 130);
            lblLiqCheck.Name = "lblLiqCheck";
            lblLiqCheck.Size = new Size(24, 22);
            lblLiqCheck.TabIndex = 3;
            lblLiqCheck.Text = "--";
            // 
            // lblCapLiqCheck
            // 
            lblCapLiqCheck.AutoSize = true;
            lblCapLiqCheck.Font = new Font("Times New Roman", 15F);
            lblCapLiqCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLiqCheck.Location = new Point(30, 130);
            lblCapLiqCheck.Name = "lblCapLiqCheck";
            lblCapLiqCheck.Size = new Size(217, 22);
            lblCapLiqCheck.TabIndex = 2;
            lblCapLiqCheck.Text = "Liquidation check            :";
            // 
            // lblCapMarginCheck
            // 
            lblCapMarginCheck.AutoSize = true;
            lblCapMarginCheck.Font = new Font("Times New Roman", 15F);
            lblCapMarginCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapMarginCheck.Location = new Point(32, 75);
            lblCapMarginCheck.Name = "lblCapMarginCheck";
            lblCapMarginCheck.Size = new Size(213, 22);
            lblCapMarginCheck.TabIndex = 1;
            lblCapMarginCheck.Text = "Margin check                  :";
            // 
            // lblMarginCheck
            // 
            lblMarginCheck.AutoSize = true;
            lblMarginCheck.Font = new Font("Times New Roman", 15F);
            lblMarginCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblMarginCheck.Location = new Point(253, 75);
            lblMarginCheck.Name = "lblMarginCheck";
            lblMarginCheck.Size = new Size(24, 22);
            lblMarginCheck.TabIndex = 1;
            lblMarginCheck.Text = "--";
            // 
            // pnlSlnTp_Max
            // 
            pnlSlnTp_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlSlnTp_Max.Location = new Point(20, 13);
            pnlSlnTp_Max.Margin = new Padding(2);
            pnlSlnTp_Max.Name = "pnlSlnTp_Max";
            pnlSlnTp_Max.Size = new Size(1651, 341);
            pnlSlnTp_Max.TabIndex = 23;
            pnlSlnTp_Max.Visible = false;
            // 
            // pnlTpLevels_Max
            // 
            pnlTpLevels_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlTpLevels_Max.Location = new Point(20, 386);
            pnlTpLevels_Max.Margin = new Padding(2);
            pnlTpLevels_Max.Name = "pnlTpLevels_Max";
            pnlTpLevels_Max.Size = new Size(1651, 280);
            pnlTpLevels_Max.TabIndex = 24;
            pnlTpLevels_Max.Visible = false;
            // 
            // pnlMetrics_Max
            // 
            pnlMetrics_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlMetrics_Max.Location = new Point(20, 700);
            pnlMetrics_Max.Margin = new Padding(2);
            pnlMetrics_Max.Name = "pnlMetrics_Max";
            pnlMetrics_Max.Size = new Size(1651, 335);
            pnlMetrics_Max.TabIndex = 25;
            pnlMetrics_Max.Visible = false;
            // 
            // pnlChecks_Max
            // 
            pnlChecks_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlChecks_Max.Location = new Point(20, 1079);
            pnlChecks_Max.Margin = new Padding(2);
            pnlChecks_Max.Name = "pnlChecks_Max";
            pnlChecks_Max.Size = new Size(1651, 293);
            pnlChecks_Max.TabIndex = 26;
            pnlChecks_Max.Visible = false;
            // 
            // FrmSlTpFromLeverage
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlChecks);
            Controls.Add(pnlMetrics);
            Controls.Add(pnlTpLevels);
            Controls.Add(pnlSlnTp);
            Controls.Add(pnlSlnTp_Max);
            Controls.Add(pnlTpLevels_Max);
            Controls.Add(pnlMetrics_Max);
            Controls.Add(pnlChecks_Max);
            Margin = new Padding(2);
            Name = "FrmSlTpFromLeverage";
            Size = new Size(1699, 1376);
            pnlSlnTp.ResumeLayout(false);
            pnlSlnTp.PerformLayout();
            pnlTpLevels.ResumeLayout(false);
            pnlTpLevels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTakeProfits).EndInit();
            pnlMetrics.ResumeLayout(false);
            pnlMetrics.PerformLayout();
            pnlChecks.ResumeLayout(false);
            pnlChecks.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSlnTp;
        private TextBox txtCoinQty;
        private Panel pnlTpLevels;
        private Panel pnlMetrics;
        private Panel pnlChecks;

        private Label lblTitle;
        private Label lblTableTitle;
        private Label lblMetricsTitile;
        private Label llblChecksTitle;

        private Label lblCapBalance;
        private Label lblCapRisk;
        private Label lblCapDirection;
        private Label lblCapEntry;
        private Label lblCapMargin;
        private Label lblCapLeverage;
        private TextBox txtBalance;
        private TextBox txtRiskPercent;
        private TextBox txtEntryPrice;
        private TextBox txtMargin;
        private TextBox txtLeverage;
        private ComboBox cmbDirection;

        private DataGridView dgvTakeProfits;
        private DataGridViewTextBoxColumn colLevel;
        private DataGridViewTextBoxColumn colMultiple;
        private DataGridViewTextBoxColumn colPercent;
        private DataGridViewTextBoxColumn colMovePct;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colPnl;

        private Label lblCapRiskAmount; private Label lblRiskAmount;
        private Label lblCapPositionUsdt; private Label lblPositionUsdt;
        private Label lblCapPositionCoins; private Label lblPositionCoins;
        private Label lblCapSlDist; private Label lblSlDistancePct;
        private Label lblCapSlPrice; private Label lblStopLossPrice;
        private Label lblCapLiqDist; private Label lblLiqDistancePct;
        private Label lblCapTotalPnl; private Label lblTotalPnl;
        private Label lblCapReturnMargin; private Label lblReturnOnMargin;
        private Label lblCapRr; private Label lblRewardToRisk;

        private Label lblCapMarginCheck; private Label lblMarginCheck;
        private Label lblCapLiqCheck; private Label lblLiqCheck;
        private Label lblCapAllocation; private Label lblAllocationCheck;
        private Label lblStatus;
        private Panel pnlSlnTp_Max;
        private Panel pnlTpLevels_Max;
        private Panel pnlMetrics_Max;
        private Panel pnlChecks_Max;
        private TextBox textBox1;
        private Label lblCapPosCoin;
    }
}
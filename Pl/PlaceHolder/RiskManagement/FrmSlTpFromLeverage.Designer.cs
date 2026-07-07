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
            pnlThemeAndNotifications = new Panel();
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
            panel1 = new Panel();
            lblTableTitle = new Label();
            dgvTakeProfits = new DataGridView();
            colLevel = new DataGridViewTextBoxColumn();
            colMultiple = new DataGridViewTextBoxColumn();
            colPercent = new DataGridViewTextBoxColumn();
            colMovePct = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colPnl = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
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
            panel3 = new Panel();
            llblChecksTitle = new Label();
            lblAllocationCheck = new Label();
            lblStatus = new Label();
            lblCapAllocation = new Label();
            lblLiqCheck = new Label();
            lblCapLiqCheck = new Label();
            lblCapMarginCheck = new Label();
            lblMarginCheck = new Label();
            pnlThemeAndNotifications.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTakeProfits).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pnlThemeAndNotifications
            // 
            pnlThemeAndNotifications.BackColor = Color.FromArgb(27, 38, 59);
            pnlThemeAndNotifications.Controls.Add(lblTitle);
            pnlThemeAndNotifications.Controls.Add(cmbDirection);
            pnlThemeAndNotifications.Controls.Add(txtMargin);
            pnlThemeAndNotifications.Controls.Add(txtLeverage);
            pnlThemeAndNotifications.Controls.Add(txtRiskPercent);
            pnlThemeAndNotifications.Controls.Add(txtEntryPrice);
            pnlThemeAndNotifications.Controls.Add(txtBalance);
            pnlThemeAndNotifications.Controls.Add(lblCapLeverage);
            pnlThemeAndNotifications.Controls.Add(lblCapMargin);
            pnlThemeAndNotifications.Controls.Add(lblCapEntry);
            pnlThemeAndNotifications.Controls.Add(lblCapDirection);
            pnlThemeAndNotifications.Controls.Add(lblCapRisk);
            pnlThemeAndNotifications.Controls.Add(lblCapBalance);
            pnlThemeAndNotifications.Location = new Point(20, 15);
            pnlThemeAndNotifications.Margin = new Padding(2);
            pnlThemeAndNotifications.Name = "pnlThemeAndNotifications";
            pnlThemeAndNotifications.Size = new Size(922, 339);
            pnlThemeAndNotifications.TabIndex = 19;
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
            txtLeverage.Location = new Point(240, 272);
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
            lblCapLeverage.Location = new Point(237, 246);
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
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(27, 38, 59);
            panel1.Controls.Add(lblTableTitle);
            panel1.Controls.Add(dgvTakeProfits);
            panel1.Location = new Point(20, 386);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(922, 280);
            panel1.TabIndex = 20;
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
            dgvTakeProfits.Size = new Size(881, 204);
            dgvTakeProfits.TabIndex = 0;
            // 
            // colLevel
            // 
            colLevel.HeaderText = "Level";
            colLevel.Name = "colLevel";
            colLevel.ReadOnly = true;
            // 
            // colMultiple
            // 
            colMultiple.HeaderText = "× SL distance";
            colMultiple.Name = "colMultiple";
            // 
            // colPercent
            // 
            colPercent.HeaderText = "% of position";
            colPercent.Name = "colPercent";
            // 
            // colMovePct
            // 
            colMovePct.HeaderText = "% move";
            colMovePct.Name = "colMovePct";
            colMovePct.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.HeaderText = "TP price";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colQty
            // 
            colQty.HeaderText = "Qty (USDT)";
            colQty.Name = "colQty";
            colQty.ReadOnly = true;
            // 
            // colPnl
            // 
            colPnl.HeaderText = "PnL";
            colPnl.Name = "colPnl";
            colPnl.ReadOnly = true;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(27, 38, 59);
            panel2.Controls.Add(lblMetricsTitile);
            panel2.Controls.Add(lblRewardToRisk);
            panel2.Controls.Add(lblCapRr);
            panel2.Controls.Add(lblReturnOnMargin);
            panel2.Controls.Add(lblCapReturnMargin);
            panel2.Controls.Add(lblTotalPnl);
            panel2.Controls.Add(lblCapTotalPnl);
            panel2.Controls.Add(lblLiqDistancePct);
            panel2.Controls.Add(lblCapLiqDist);
            panel2.Controls.Add(lblStopLossPrice);
            panel2.Controls.Add(lblCapSlPrice);
            panel2.Controls.Add(lblSlDistancePct);
            panel2.Controls.Add(lblCapSlDist);
            panel2.Controls.Add(lblPositionCoins);
            panel2.Controls.Add(lblCapPositionCoins);
            panel2.Controls.Add(lblPositionUsdt);
            panel2.Controls.Add(lblCapPositionUsdt);
            panel2.Controls.Add(lblCapRiskAmount);
            panel2.Controls.Add(lblRiskAmount);
            panel2.Location = new Point(20, 700);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(922, 335);
            panel2.TabIndex = 21;
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
            lblRewardToRisk.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRewardToRisk.ForeColor = Color.FromArgb(156, 163, 175);
            lblRewardToRisk.Location = new Point(654, 240);
            lblRewardToRisk.Name = "lblRewardToRisk";
            lblRewardToRisk.Size = new Size(21, 19);
            lblRewardToRisk.TabIndex = 17;
            lblRewardToRisk.Text = "--";
            // 
            // lblCapRr
            // 
            lblCapRr.AutoSize = true;
            lblCapRr.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapRr.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapRr.Location = new Point(462, 240);
            lblCapRr.Name = "lblCapRr";
            lblCapRr.Size = new Size(182, 19);
            lblCapRr.TabIndex = 16;
            lblCapRr.Text = "Reward-to-risk                   :";
            // 
            // lblReturnOnMargin
            // 
            lblReturnOnMargin.AutoSize = true;
            lblReturnOnMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReturnOnMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblReturnOnMargin.Location = new Point(654, 191);
            lblReturnOnMargin.Name = "lblReturnOnMargin";
            lblReturnOnMargin.Size = new Size(21, 19);
            lblReturnOnMargin.TabIndex = 15;
            lblReturnOnMargin.Text = "--";
            // 
            // lblCapReturnMargin
            // 
            lblCapReturnMargin.AutoSize = true;
            lblCapReturnMargin.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapReturnMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapReturnMargin.Location = new Point(462, 191);
            lblCapReturnMargin.Name = "lblCapReturnMargin";
            lblCapReturnMargin.Size = new Size(183, 19);
            lblCapReturnMargin.TabIndex = 14;
            lblCapReturnMargin.Text = "Return on margin                 :";
            // 
            // lblTotalPnl
            // 
            lblTotalPnl.AutoSize = true;
            lblTotalPnl.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalPnl.ForeColor = Color.FromArgb(156, 163, 175);
            lblTotalPnl.Location = new Point(654, 139);
            lblTotalPnl.Name = "lblTotalPnl";
            lblTotalPnl.Size = new Size(21, 19);
            lblTotalPnl.TabIndex = 13;
            lblTotalPnl.Text = "--";
            // 
            // lblCapTotalPnl
            // 
            lblCapTotalPnl.AutoSize = true;
            lblCapTotalPnl.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapTotalPnl.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapTotalPnl.Location = new Point(462, 139);
            lblCapTotalPnl.Name = "lblCapTotalPnl";
            lblCapTotalPnl.Size = new Size(181, 19);
            lblCapTotalPnl.TabIndex = 12;
            lblCapTotalPnl.Text = "Total PnL (all TPs)              :";
            // 
            // lblLiqDistancePct
            // 
            lblLiqDistancePct.AutoSize = true;
            lblLiqDistancePct.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLiqDistancePct.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqDistancePct.Location = new Point(654, 85);
            lblLiqDistancePct.Name = "lblLiqDistancePct";
            lblLiqDistancePct.Size = new Size(21, 19);
            lblLiqDistancePct.TabIndex = 11;
            lblLiqDistancePct.Text = "--";
            // 
            // lblCapLiqDist
            // 
            lblCapLiqDist.AutoSize = true;
            lblCapLiqDist.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapLiqDist.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLiqDist.Location = new Point(462, 85);
            lblCapLiqDist.Name = "lblCapLiqDist";
            lblCapLiqDist.Size = new Size(185, 19);
            lblCapLiqDist.TabIndex = 10;
            lblCapLiqDist.Text = "Liquidation distance %         :";
            // 
            // lblStopLossPrice
            // 
            lblStopLossPrice.AutoSize = true;
            lblStopLossPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStopLossPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblStopLossPrice.Location = new Point(209, 292);
            lblStopLossPrice.Name = "lblStopLossPrice";
            lblStopLossPrice.Size = new Size(21, 19);
            lblStopLossPrice.TabIndex = 9;
            lblStopLossPrice.Text = "--";
            // 
            // lblCapSlPrice
            // 
            lblCapSlPrice.AutoSize = true;
            lblCapSlPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapSlPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSlPrice.Location = new Point(30, 292);
            lblCapSlPrice.Name = "lblCapSlPrice";
            lblCapSlPrice.Size = new Size(172, 19);
            lblCapSlPrice.TabIndex = 8;
            lblCapSlPrice.Text = "Stop-loss price                 :";
            // 
            // lblSlDistancePct
            // 
            lblSlDistancePct.AutoSize = true;
            lblSlDistancePct.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlDistancePct.ForeColor = Color.FromArgb(156, 163, 175);
            lblSlDistancePct.Location = new Point(209, 240);
            lblSlDistancePct.Name = "lblSlDistancePct";
            lblSlDistancePct.Size = new Size(21, 19);
            lblSlDistancePct.TabIndex = 7;
            lblSlDistancePct.Text = "--";
            // 
            // lblCapSlDist
            // 
            lblCapSlDist.AutoSize = true;
            lblCapSlDist.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapSlDist.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapSlDist.Location = new Point(30, 240);
            lblCapSlDist.Name = "lblCapSlDist";
            lblCapSlDist.Size = new Size(171, 19);
            lblCapSlDist.TabIndex = 6;
            lblCapSlDist.Text = "SL distance %                  :";
            // 
            // lblPositionCoins
            // 
            lblPositionCoins.AutoSize = true;
            lblPositionCoins.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPositionCoins.ForeColor = Color.FromArgb(156, 163, 175);
            lblPositionCoins.Location = new Point(209, 191);
            lblPositionCoins.Name = "lblPositionCoins";
            lblPositionCoins.Size = new Size(21, 19);
            lblPositionCoins.TabIndex = 5;
            lblPositionCoins.Text = "--";
            // 
            // lblCapPositionCoins
            // 
            lblCapPositionCoins.AutoSize = true;
            lblCapPositionCoins.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPositionCoins.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPositionCoins.Location = new Point(30, 191);
            lblCapPositionCoins.Name = "lblCapPositionCoins";
            lblCapPositionCoins.Size = new Size(171, 19);
            lblCapPositionCoins.TabIndex = 4;
            lblCapPositionCoins.Text = "Position size (coins)          :";
            // 
            // lblPositionUsdt
            // 
            lblPositionUsdt.AutoSize = true;
            lblPositionUsdt.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPositionUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblPositionUsdt.Location = new Point(209, 139);
            lblPositionUsdt.Name = "lblPositionUsdt";
            lblPositionUsdt.Size = new Size(21, 19);
            lblPositionUsdt.TabIndex = 3;
            lblPositionUsdt.Text = "--";
            // 
            // lblCapPositionUsdt
            // 
            lblCapPositionUsdt.AutoSize = true;
            lblCapPositionUsdt.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPositionUsdt.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPositionUsdt.Location = new Point(30, 139);
            lblCapPositionUsdt.Name = "lblCapPositionUsdt";
            lblCapPositionUsdt.Size = new Size(172, 19);
            lblCapPositionUsdt.TabIndex = 2;
            lblCapPositionUsdt.Text = "Position size (USDT)        :";
            // 
            // lblCapRiskAmount
            // 
            lblCapRiskAmount.AutoSize = true;
            lblCapRiskAmount.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapRiskAmount.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapRiskAmount.Location = new Point(28, 85);
            lblCapRiskAmount.Name = "lblCapRiskAmount";
            lblCapRiskAmount.Size = new Size(175, 19);
            lblCapRiskAmount.TabIndex = 1;
            lblCapRiskAmount.Text = "Risk amount                      :";
            // 
            // lblRiskAmount
            // 
            lblRiskAmount.AutoSize = true;
            lblRiskAmount.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRiskAmount.ForeColor = Color.FromArgb(156, 163, 175);
            lblRiskAmount.Location = new Point(209, 85);
            lblRiskAmount.Name = "lblRiskAmount";
            lblRiskAmount.Size = new Size(21, 19);
            lblRiskAmount.TabIndex = 1;
            lblRiskAmount.Text = "--";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(27, 38, 59);
            panel3.Controls.Add(llblChecksTitle);
            panel3.Controls.Add(lblAllocationCheck);
            panel3.Controls.Add(lblStatus);
            panel3.Controls.Add(lblCapAllocation);
            panel3.Controls.Add(lblLiqCheck);
            panel3.Controls.Add(lblCapLiqCheck);
            panel3.Controls.Add(lblCapMarginCheck);
            panel3.Controls.Add(lblMarginCheck);
            panel3.Location = new Point(20, 1079);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(922, 293);
            panel3.TabIndex = 22;
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
            lblAllocationCheck.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAllocationCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblAllocationCheck.Location = new Point(209, 182);
            lblAllocationCheck.Name = "lblAllocationCheck";
            lblAllocationCheck.Size = new Size(21, 19);
            lblAllocationCheck.TabIndex = 16;
            lblAllocationCheck.Text = "--";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblStatus.Location = new Point(30, 237);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(46, 19);
            lblStatus.TabIndex = 14;
            lblStatus.Text = "Status";
            // 
            // lblCapAllocation
            // 
            lblCapAllocation.AutoSize = true;
            lblCapAllocation.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapAllocation.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapAllocation.Location = new Point(30, 182);
            lblCapAllocation.Name = "lblCapAllocation";
            lblCapAllocation.Size = new Size(165, 19);
            lblCapAllocation.TabIndex = 4;
            lblCapAllocation.Text = "Allocation                       :";
            // 
            // lblLiqCheck
            // 
            lblLiqCheck.AutoSize = true;
            lblLiqCheck.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLiqCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqCheck.Location = new Point(209, 130);
            lblLiqCheck.Name = "lblLiqCheck";
            lblLiqCheck.Size = new Size(21, 19);
            lblLiqCheck.TabIndex = 3;
            lblLiqCheck.Text = "--";
            // 
            // lblCapLiqCheck
            // 
            lblCapLiqCheck.AutoSize = true;
            lblCapLiqCheck.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapLiqCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapLiqCheck.Location = new Point(30, 130);
            lblCapLiqCheck.Name = "lblCapLiqCheck";
            lblCapLiqCheck.Size = new Size(167, 19);
            lblCapLiqCheck.TabIndex = 2;
            lblCapLiqCheck.Text = "Liquidation check            :";
            // 
            // lblCapMarginCheck
            // 
            lblCapMarginCheck.AutoSize = true;
            lblCapMarginCheck.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapMarginCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapMarginCheck.Location = new Point(30, 76);
            lblCapMarginCheck.Name = "lblCapMarginCheck";
            lblCapMarginCheck.Size = new Size(167, 19);
            lblCapMarginCheck.TabIndex = 1;
            lblCapMarginCheck.Text = "Margin check                  :";
            // 
            // lblMarginCheck
            // 
            lblMarginCheck.AutoSize = true;
            lblMarginCheck.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMarginCheck.ForeColor = Color.FromArgb(156, 163, 175);
            lblMarginCheck.Location = new Point(209, 76);
            lblMarginCheck.Name = "lblMarginCheck";
            lblMarginCheck.Size = new Size(21, 19);
            lblMarginCheck.TabIndex = 1;
            lblMarginCheck.Text = "--";
            // 
            // FrmSlTpFromLeverage
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pnlThemeAndNotifications);
            Margin = new Padding(2);
            Name = "FrmSlTpFromLeverage";
            Size = new Size(1540, 1393);
            pnlThemeAndNotifications.ResumeLayout(false);
            pnlThemeAndNotifications.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTakeProfits).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlThemeAndNotifications;
        private Panel panel1;
        private Label lblCapBalance;
        private Label lblCapLeverage;
        private Label lblCapMargin;
        private Label lblCapEntry;
        private Label lblCapDirection;
        private Label lblCapRisk;
        private TextBox txtMargin;
        private TextBox txtLeverage;
        private TextBox txtRiskPercent;
        private TextBox txtEntryPrice;
        private TextBox txtBalance;
        private ComboBox cmbDirection;
        private DataGridView dgvTakeProfits;
        private DataGridViewTextBoxColumn colLevel;
        private DataGridViewTextBoxColumn colMultiple;
        private DataGridViewTextBoxColumn colPercent;
        private DataGridViewTextBoxColumn colMovePct;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colPnl;
        private Label lblTitle;
        private Panel panel2;
        private Label lblRewardToRisk;
        private Label lblCapRr;
        private Label lblReturnOnMargin;
        private Label lblCapReturnMargin;
        private Label lblTotalPnl;
        private Label lblCapTotalPnl;
        private Label lblLiqDistancePct;
        private Label lblCapLiqDist;
        private Label lblStopLossPrice;
        private Label lblCapSlPrice;
        private Label lblSlDistancePct;
        private Label lblCapSlDist;
        private Label lblPositionCoins;
        private Label lblCapPositionCoins;
        private Label lblPositionUsdt;
        private Label lblCapPositionUsdt;
        private Label lblCapRiskAmount;
        private Label lblRiskAmount;
        private Panel panel3;
        private Label lblMetricsTitile;
        private Label label2;
        private Label lblStatus;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label lblCapAllocation;
        private Label lblLiqCheck;
        private Label lblCapLiqCheck;
        private Label lblCapMarginCheck;
        private Label lblMarginCheck;
        private Label lblAllocationCheck;
        private Label lblTableTitle;
        private Label llblChecksTitle;
    }
}
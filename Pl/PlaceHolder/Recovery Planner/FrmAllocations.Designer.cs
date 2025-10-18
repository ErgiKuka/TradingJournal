namespace TradingJournal.Pl.PlaceHolder.Recovery_Planner
{
    partial class FrmAllocations
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
            pnlAlcEditor = new Panel();
            btnAlcClear = new FontAwesome.Sharp.IconButton();
            btnAlcCancelUpdate = new FontAwesome.Sharp.IconButton();
            btnAlcUpdate = new FontAwesome.Sharp.IconButton();
            btnAlcAdd = new FontAwesome.Sharp.IconButton();
            this.txtAlcEntryPrice = new TextBox();
            this.lblAlcEntryPrice = new Label();
            this.lblAlcTradeDate = new Label();
            dtpAlcTradeDate = new DateTimePicker();
            pnlAlcGrid = new Panel();
            pnlAlcKpis = new Panel();
            txtAlcExitPrice = new TextBox();
            lblAlcExitPrice = new Label();
            txtAlcMargin = new TextBox();
            lblAlcMargin = new Label();
            grpAlcAmountMode = new GroupBox();
            rbAlcModeQuantity = new RadioButton();
            rbAlcModeMargin = new RadioButton();
            txtAlcQuantity = new TextBox();
            lblAlcQuantity = new Label();
            txtAlcProfit = new TextBox();
            lblAlcProfit = new Label();
            lblAlcListCaption = new Label();
            dataGridView1 = new DataGridView();
            lblKpiQuantity = new Label();
            lblKpiQuantityCaption = new Label();
            lblKpiAvgCost = new Label();
            lblKpiAvgCostCaption = new Label();
            lblKpiEntryPrice = new Label();
            lblKpiEntryPriceCaption = new Label();
            lblKpiSymbol = new Label();
            lblKpiSymbolCaption = new Label();
            lblKpiRecovered = new Label();
            lblKpiRecoveredCaption = new Label();
            lblKpiCurrentValue = new Label();
            lblKpiCurrentValueCaption = new Label();
            lblKpiInvested = new Label();
            lblKpiInvestedCaption = new Label();
            lblKpiCurrentPrice = new Label();
            lblKpiCurrentPriceCaption = new Label();
            lblKpiNeeded = new Label();
            lblKpiNeededCaption = new Label();
            lblKpiProgressPct = new Label();
            lblKpiProgressCaption = new Label();
            prgKpiProgress = new ProgressBar();
            pnlAlcEditor.SuspendLayout();
            pnlAlcGrid.SuspendLayout();
            pnlAlcKpis.SuspendLayout();
            grpAlcAmountMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pnlAlcEditor
            // 
            pnlAlcEditor.BackColor = Color.FromArgb(27, 38, 59);
            pnlAlcEditor.Controls.Add(txtAlcProfit);
            pnlAlcEditor.Controls.Add(lblAlcProfit);
            pnlAlcEditor.Controls.Add(grpAlcAmountMode);
            pnlAlcEditor.Controls.Add(txtAlcExitPrice);
            pnlAlcEditor.Controls.Add(lblAlcExitPrice);
            pnlAlcEditor.Controls.Add(btnAlcClear);
            pnlAlcEditor.Controls.Add(btnAlcCancelUpdate);
            pnlAlcEditor.Controls.Add(btnAlcUpdate);
            pnlAlcEditor.Controls.Add(btnAlcAdd);
            pnlAlcEditor.Controls.Add(this.txtAlcEntryPrice);
            pnlAlcEditor.Controls.Add(this.lblAlcEntryPrice);
            pnlAlcEditor.Controls.Add(this.lblAlcTradeDate);
            pnlAlcEditor.Controls.Add(dtpAlcTradeDate);
            pnlAlcEditor.Controls.Add(txtAlcMargin);
            pnlAlcEditor.Controls.Add(lblAlcMargin);
            pnlAlcEditor.Controls.Add(txtAlcQuantity);
            pnlAlcEditor.Controls.Add(lblAlcQuantity);
            pnlAlcEditor.Location = new Point(11, 11);
            pnlAlcEditor.Margin = new Padding(2);
            pnlAlcEditor.Name = "pnlAlcEditor";
            pnlAlcEditor.Size = new Size(1263, 230);
            pnlAlcEditor.TabIndex = 20;
            // 
            // btnAlcClear
            // 
            btnAlcClear.BackColor = Color.IndianRed;
            btnAlcClear.FlatAppearance.BorderSize = 0;
            btnAlcClear.FlatStyle = FlatStyle.Flat;
            btnAlcClear.Font = new Font("Times New Roman", 12F);
            btnAlcClear.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAlcClear.IconColor = Color.Black;
            btnAlcClear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAlcClear.Location = new Point(739, 131);
            btnAlcClear.Margin = new Padding(2);
            btnAlcClear.Name = "btnAlcClear";
            btnAlcClear.Size = new Size(152, 30);
            btnAlcClear.TabIndex = 27;
            btnAlcClear.Text = "Clear Fields";
            btnAlcClear.UseVisualStyleBackColor = false;
            // 
            // btnAlcCancelUpdate
            // 
            btnAlcCancelUpdate.BackColor = Color.Crimson;
            btnAlcCancelUpdate.FlatAppearance.BorderSize = 0;
            btnAlcCancelUpdate.FlatStyle = FlatStyle.Flat;
            btnAlcCancelUpdate.Font = new Font("Times New Roman", 12F);
            btnAlcCancelUpdate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAlcCancelUpdate.IconColor = Color.Black;
            btnAlcCancelUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAlcCancelUpdate.Location = new Point(835, 107);
            btnAlcCancelUpdate.Margin = new Padding(2);
            btnAlcCancelUpdate.Name = "btnAlcCancelUpdate";
            btnAlcCancelUpdate.Size = new Size(105, 39);
            btnAlcCancelUpdate.TabIndex = 26;
            btnAlcCancelUpdate.Text = "Cancel";
            btnAlcCancelUpdate.UseVisualStyleBackColor = false;
            btnAlcCancelUpdate.Visible = false;
            // 
            // btnAlcUpdate
            // 
            btnAlcUpdate.BackColor = Color.DarkGreen;
            btnAlcUpdate.FlatAppearance.BorderSize = 0;
            btnAlcUpdate.FlatStyle = FlatStyle.Flat;
            btnAlcUpdate.Font = new Font("Times New Roman", 12F);
            btnAlcUpdate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAlcUpdate.IconColor = Color.Black;
            btnAlcUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAlcUpdate.Location = new Point(679, 107);
            btnAlcUpdate.Margin = new Padding(2);
            btnAlcUpdate.Name = "btnAlcUpdate";
            btnAlcUpdate.Size = new Size(132, 39);
            btnAlcUpdate.TabIndex = 25;
            btnAlcUpdate.Text = "Update Allocation";
            btnAlcUpdate.UseVisualStyleBackColor = false;
            btnAlcUpdate.Visible = false;
            // 
            // btnAlcAdd
            // 
            btnAlcAdd.BackColor = Color.FromArgb(30, 58, 95);
            btnAlcAdd.FlatAppearance.BorderSize = 0;
            btnAlcAdd.FlatStyle = FlatStyle.Flat;
            btnAlcAdd.Font = new Font("Times New Roman", 12F);
            btnAlcAdd.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAlcAdd.IconColor = Color.Black;
            btnAlcAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAlcAdd.Location = new Point(729, 80);
            btnAlcAdd.Margin = new Padding(2);
            btnAlcAdd.Name = "btnAlcAdd";
            btnAlcAdd.Size = new Size(175, 39);
            btnAlcAdd.TabIndex = 24;
            btnAlcAdd.Text = "Add Allocation";
            btnAlcAdd.UseVisualStyleBackColor = false;
            // 
            // txtAlcEntryPrice
            // 
            this.txtAlcEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            this.txtAlcEntryPrice.BorderStyle = BorderStyle.None;
            this.txtAlcEntryPrice.Font = new Font("Times New Roman", 18F);
            this.txtAlcEntryPrice.Location = new Point(252, 58);
            this.txtAlcEntryPrice.Margin = new Padding(2);
            this.txtAlcEntryPrice.Name = "txtAlcEntryPrice";
            this.txtAlcEntryPrice.Size = new Size(162, 28);
            this.txtAlcEntryPrice.TabIndex = 10;
            this.txtAlcEntryPrice.Text = "   ";
            // 
            // lblAlcEntryPrice
            // 
            this.lblAlcEntryPrice.AutoSize = true;
            this.lblAlcEntryPrice.Font = new Font("Times New Roman", 14.25F);
            this.lblAlcEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            this.lblAlcEntryPrice.Location = new Point(250, 29);
            this.lblAlcEntryPrice.Margin = new Padding(2, 0, 2, 0);
            this.lblAlcEntryPrice.Name = "lblAlcEntryPrice";
            this.lblAlcEntryPrice.Size = new Size(93, 21);
            this.lblAlcEntryPrice.TabIndex = 9;
            this.lblAlcEntryPrice.Text = "Entry Price";
            // 
            // lblAlcTradeDate
            // 
            this.lblAlcTradeDate.AutoSize = true;
            this.lblAlcTradeDate.Font = new Font("Times New Roman", 14.25F);
            this.lblAlcTradeDate.ForeColor = Color.FromArgb(156, 163, 175);
            this.lblAlcTradeDate.Location = new Point(43, 31);
            this.lblAlcTradeDate.Margin = new Padding(2, 0, 2, 0);
            this.lblAlcTradeDate.Name = "lblAlcTradeDate";
            this.lblAlcTradeDate.Size = new Size(92, 21);
            this.lblAlcTradeDate.TabIndex = 7;
            this.lblAlcTradeDate.Text = "Trade Date";
            // 
            // dtpAlcTradeDate
            // 
            dtpAlcTradeDate.CalendarMonthBackground = SystemColors.MenuHighlight;
            dtpAlcTradeDate.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpAlcTradeDate.Format = DateTimePickerFormat.Short;
            dtpAlcTradeDate.Location = new Point(45, 55);
            dtpAlcTradeDate.Name = "dtpAlcTradeDate";
            dtpAlcTradeDate.Size = new Size(128, 29);
            dtpAlcTradeDate.TabIndex = 6;
            // 
            // pnlAlcGrid
            // 
            pnlAlcGrid.BackColor = Color.FromArgb(27, 38, 59);
            pnlAlcGrid.Controls.Add(dataGridView1);
            pnlAlcGrid.Controls.Add(lblAlcListCaption);
            pnlAlcGrid.Location = new Point(11, 262);
            pnlAlcGrid.Margin = new Padding(2);
            pnlAlcGrid.Name = "pnlAlcGrid";
            pnlAlcGrid.Size = new Size(1263, 349);
            pnlAlcGrid.TabIndex = 21;
            // 
            // pnlAlcKpis
            // 
            pnlAlcKpis.BackColor = Color.FromArgb(27, 38, 59);
            pnlAlcKpis.Controls.Add(prgKpiProgress);
            pnlAlcKpis.Controls.Add(lblKpiProgressPct);
            pnlAlcKpis.Controls.Add(lblKpiProgressCaption);
            pnlAlcKpis.Controls.Add(lblKpiNeeded);
            pnlAlcKpis.Controls.Add(lblKpiNeededCaption);
            pnlAlcKpis.Controls.Add(lblKpiRecovered);
            pnlAlcKpis.Controls.Add(lblKpiRecoveredCaption);
            pnlAlcKpis.Controls.Add(lblKpiCurrentValue);
            pnlAlcKpis.Controls.Add(lblKpiCurrentValueCaption);
            pnlAlcKpis.Controls.Add(lblKpiInvested);
            pnlAlcKpis.Controls.Add(lblKpiInvestedCaption);
            pnlAlcKpis.Controls.Add(lblKpiCurrentPrice);
            pnlAlcKpis.Controls.Add(lblKpiCurrentPriceCaption);
            pnlAlcKpis.Controls.Add(lblKpiQuantity);
            pnlAlcKpis.Controls.Add(lblKpiQuantityCaption);
            pnlAlcKpis.Controls.Add(lblKpiAvgCost);
            pnlAlcKpis.Controls.Add(lblKpiAvgCostCaption);
            pnlAlcKpis.Controls.Add(lblKpiEntryPrice);
            pnlAlcKpis.Controls.Add(lblKpiEntryPriceCaption);
            pnlAlcKpis.Controls.Add(lblKpiSymbol);
            pnlAlcKpis.Controls.Add(lblKpiSymbolCaption);
            pnlAlcKpis.Location = new Point(11, 629);
            pnlAlcKpis.Margin = new Padding(2);
            pnlAlcKpis.Name = "pnlAlcKpis";
            pnlAlcKpis.Size = new Size(1263, 207);
            pnlAlcKpis.TabIndex = 22;
            // 
            // txtAlcExitPrice
            // 
            txtAlcExitPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtAlcExitPrice.BorderStyle = BorderStyle.None;
            txtAlcExitPrice.Font = new Font("Times New Roman", 18F);
            txtAlcExitPrice.Location = new Point(496, 58);
            txtAlcExitPrice.Margin = new Padding(2);
            txtAlcExitPrice.Name = "txtAlcExitPrice";
            txtAlcExitPrice.Size = new Size(162, 28);
            txtAlcExitPrice.TabIndex = 29;
            txtAlcExitPrice.Text = "   ";
            // 
            // lblAlcExitPrice
            // 
            lblAlcExitPrice.AutoSize = true;
            lblAlcExitPrice.Font = new Font("Times New Roman", 14.25F);
            lblAlcExitPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcExitPrice.Location = new Point(494, 29);
            lblAlcExitPrice.Margin = new Padding(2, 0, 2, 0);
            lblAlcExitPrice.Name = "lblAlcExitPrice";
            lblAlcExitPrice.Size = new Size(82, 21);
            lblAlcExitPrice.TabIndex = 28;
            lblAlcExitPrice.Text = "Exit Price";
            // 
            // txtAlcMargin
            // 
            txtAlcMargin.BackColor = Color.FromArgb(30, 58, 95);
            txtAlcMargin.BorderStyle = BorderStyle.None;
            txtAlcMargin.Font = new Font("Times New Roman", 18F);
            txtAlcMargin.Location = new Point(252, 155);
            txtAlcMargin.Margin = new Padding(2);
            txtAlcMargin.Name = "txtAlcMargin";
            txtAlcMargin.Size = new Size(162, 28);
            txtAlcMargin.TabIndex = 32;
            txtAlcMargin.Text = "   ";
            // 
            // lblAlcMargin
            // 
            lblAlcMargin.AutoSize = true;
            lblAlcMargin.Font = new Font("Times New Roman", 14.25F);
            lblAlcMargin.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcMargin.Location = new Point(250, 126);
            lblAlcMargin.Margin = new Padding(2, 0, 2, 0);
            lblAlcMargin.Name = "lblAlcMargin";
            lblAlcMargin.Size = new Size(128, 21);
            lblAlcMargin.TabIndex = 31;
            lblAlcMargin.Text = "Margin (USDT)";
            // 
            // grpAlcAmountMode
            // 
            grpAlcAmountMode.Controls.Add(rbAlcModeQuantity);
            grpAlcAmountMode.Controls.Add(rbAlcModeMargin);
            grpAlcAmountMode.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpAlcAmountMode.ForeColor = Color.FromArgb(156, 163, 175);
            grpAlcAmountMode.Location = new Point(36, 108);
            grpAlcAmountMode.Name = "grpAlcAmountMode";
            grpAlcAmountMode.Size = new Size(177, 85);
            grpAlcAmountMode.TabIndex = 30;
            grpAlcAmountMode.TabStop = false;
            grpAlcAmountMode.Text = "Amount Entered";
            // 
            // rbAlcModeQuantity
            // 
            rbAlcModeQuantity.AutoSize = true;
            rbAlcModeQuantity.Location = new Point(15, 47);
            rbAlcModeQuantity.Name = "rbAlcModeQuantity";
            rbAlcModeQuantity.Size = new Size(122, 23);
            rbAlcModeQuantity.TabIndex = 1;
            rbAlcModeQuantity.Text = "Quantity (Base)";
            rbAlcModeQuantity.UseVisualStyleBackColor = true;
            // 
            // rbAlcModeMargin
            // 
            rbAlcModeMargin.AutoSize = true;
            rbAlcModeMargin.Checked = true;
            rbAlcModeMargin.Location = new Point(15, 22);
            rbAlcModeMargin.Name = "rbAlcModeMargin";
            rbAlcModeMargin.Size = new Size(124, 23);
            rbAlcModeMargin.TabIndex = 0;
            rbAlcModeMargin.TabStop = true;
            rbAlcModeMargin.Text = "Margin (USDT)";
            rbAlcModeMargin.UseVisualStyleBackColor = true;
            // 
            // txtAlcQuantity
            // 
            txtAlcQuantity.BackColor = Color.FromArgb(30, 58, 95);
            txtAlcQuantity.BorderStyle = BorderStyle.None;
            txtAlcQuantity.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAlcQuantity.Location = new Point(252, 155);
            txtAlcQuantity.Margin = new Padding(2);
            txtAlcQuantity.Name = "txtAlcQuantity";
            txtAlcQuantity.Size = new Size(162, 28);
            txtAlcQuantity.TabIndex = 34;
            txtAlcQuantity.Text = "   ";
            txtAlcQuantity.Visible = false;
            // 
            // lblAlcQuantity
            // 
            lblAlcQuantity.AutoSize = true;
            lblAlcQuantity.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAlcQuantity.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcQuantity.Location = new Point(252, 126);
            lblAlcQuantity.Margin = new Padding(2, 0, 2, 0);
            lblAlcQuantity.Name = "lblAlcQuantity";
            lblAlcQuantity.Size = new Size(73, 21);
            lblAlcQuantity.TabIndex = 33;
            lblAlcQuantity.Text = "Quantity";
            lblAlcQuantity.Visible = false;
            // 
            // txtAlcProfit
            // 
            txtAlcProfit.BackColor = Color.FromArgb(30, 58, 95);
            txtAlcProfit.BorderStyle = BorderStyle.None;
            txtAlcProfit.Font = new Font("Times New Roman", 18F);
            txtAlcProfit.Location = new Point(496, 155);
            txtAlcProfit.Margin = new Padding(2);
            txtAlcProfit.Name = "txtAlcProfit";
            txtAlcProfit.Size = new Size(162, 28);
            txtAlcProfit.TabIndex = 36;
            txtAlcProfit.Text = "   ";
            // 
            // lblAlcProfit
            // 
            lblAlcProfit.AutoSize = true;
            lblAlcProfit.Font = new Font("Times New Roman", 14.25F);
            lblAlcProfit.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcProfit.Location = new Point(494, 126);
            lblAlcProfit.Margin = new Padding(2, 0, 2, 0);
            lblAlcProfit.Name = "lblAlcProfit";
            lblAlcProfit.Size = new Size(118, 21);
            lblAlcProfit.TabIndex = 35;
            lblAlcProfit.Text = "Profit (USDT)";
            // 
            // lblAlcListCaption
            // 
            lblAlcListCaption.AutoSize = true;
            lblAlcListCaption.Font = new Font("Times New Roman", 14.25F);
            lblAlcListCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcListCaption.Location = new Point(17, 12);
            lblAlcListCaption.Margin = new Padding(2, 0, 2, 0);
            lblAlcListCaption.Name = "lblAlcListCaption";
            lblAlcListCaption.Size = new Size(194, 21);
            lblAlcListCaption.TabIndex = 32;
            lblAlcListCaption.Text = "Allocations for this Case";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(17, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1226, 295);
            dataGridView1.TabIndex = 33;
            // 
            // lblKpiQuantity
            // 
            lblKpiQuantity.AutoSize = true;
            lblKpiQuantity.Font = new Font("Times New Roman", 15.75F);
            lblKpiQuantity.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiQuantity.Location = new Point(384, 67);
            lblKpiQuantity.Margin = new Padding(2, 0, 2, 0);
            lblKpiQuantity.Name = "lblKpiQuantity";
            lblKpiQuantity.Size = new Size(24, 23);
            lblKpiQuantity.TabIndex = 31;
            lblKpiQuantity.Text = "--";
            // 
            // lblKpiQuantityCaption
            // 
            lblKpiQuantityCaption.AutoSize = true;
            lblKpiQuantityCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiQuantityCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiQuantityCaption.Location = new Point(384, 36);
            lblKpiQuantityCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiQuantityCaption.Name = "lblKpiQuantityCaption";
            lblKpiQuantityCaption.Size = new Size(88, 23);
            lblKpiQuantityCaption.TabIndex = 30;
            lblKpiQuantityCaption.Text = "Quantity:";
            // 
            // lblKpiAvgCost
            // 
            lblKpiAvgCost.AutoSize = true;
            lblKpiAvgCost.Font = new Font("Times New Roman", 15.75F);
            lblKpiAvgCost.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiAvgCost.Location = new Point(264, 67);
            lblKpiAvgCost.Margin = new Padding(2, 0, 2, 0);
            lblKpiAvgCost.Name = "lblKpiAvgCost";
            lblKpiAvgCost.Size = new Size(24, 23);
            lblKpiAvgCost.TabIndex = 29;
            lblKpiAvgCost.Text = "--";
            // 
            // lblKpiAvgCostCaption
            // 
            lblKpiAvgCostCaption.AutoSize = true;
            lblKpiAvgCostCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiAvgCostCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiAvgCostCaption.Location = new Point(264, 36);
            lblKpiAvgCostCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiAvgCostCaption.Name = "lblKpiAvgCostCaption";
            lblKpiAvgCostCaption.Size = new Size(92, 23);
            lblKpiAvgCostCaption.TabIndex = 28;
            lblKpiAvgCostCaption.Text = "Avg Cost:";
            // 
            // lblKpiEntryPrice
            // 
            lblKpiEntryPrice.AutoSize = true;
            lblKpiEntryPrice.Font = new Font("Times New Roman", 15.75F);
            lblKpiEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiEntryPrice.Location = new Point(135, 67);
            lblKpiEntryPrice.Margin = new Padding(2, 0, 2, 0);
            lblKpiEntryPrice.Name = "lblKpiEntryPrice";
            lblKpiEntryPrice.Size = new Size(24, 23);
            lblKpiEntryPrice.TabIndex = 27;
            lblKpiEntryPrice.Text = "--";
            // 
            // lblKpiEntryPriceCaption
            // 
            lblKpiEntryPriceCaption.AutoSize = true;
            lblKpiEntryPriceCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiEntryPriceCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiEntryPriceCaption.Location = new Point(132, 36);
            lblKpiEntryPriceCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiEntryPriceCaption.Name = "lblKpiEntryPriceCaption";
            lblKpiEntryPriceCaption.Size = new Size(109, 23);
            lblKpiEntryPriceCaption.TabIndex = 26;
            lblKpiEntryPriceCaption.Text = "Entry Price:";
            // 
            // lblKpiSymbol
            // 
            lblKpiSymbol.AutoSize = true;
            lblKpiSymbol.Font = new Font("Times New Roman", 15.75F);
            lblKpiSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiSymbol.Location = new Point(27, 67);
            lblKpiSymbol.Margin = new Padding(2, 0, 2, 0);
            lblKpiSymbol.Name = "lblKpiSymbol";
            lblKpiSymbol.Size = new Size(24, 23);
            lblKpiSymbol.TabIndex = 25;
            lblKpiSymbol.Text = "--";
            // 
            // lblKpiSymbolCaption
            // 
            lblKpiSymbolCaption.AutoSize = true;
            lblKpiSymbolCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiSymbolCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiSymbolCaption.Location = new Point(25, 36);
            lblKpiSymbolCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiSymbolCaption.Name = "lblKpiSymbolCaption";
            lblKpiSymbolCaption.Size = new Size(79, 23);
            lblKpiSymbolCaption.TabIndex = 24;
            lblKpiSymbolCaption.Text = "Symbol:";
            // 
            // lblKpiRecovered
            // 
            lblKpiRecovered.AutoSize = true;
            lblKpiRecovered.Font = new Font("Times New Roman", 15.75F);
            lblKpiRecovered.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiRecovered.Location = new Point(909, 67);
            lblKpiRecovered.Margin = new Padding(2, 0, 2, 0);
            lblKpiRecovered.Name = "lblKpiRecovered";
            lblKpiRecovered.Size = new Size(24, 23);
            lblKpiRecovered.TabIndex = 39;
            lblKpiRecovered.Text = "--";
            // 
            // lblKpiRecoveredCaption
            // 
            lblKpiRecoveredCaption.AutoSize = true;
            lblKpiRecoveredCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiRecoveredCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiRecoveredCaption.Location = new Point(906, 36);
            lblKpiRecoveredCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiRecoveredCaption.Name = "lblKpiRecoveredCaption";
            lblKpiRecoveredCaption.Size = new Size(104, 23);
            lblKpiRecoveredCaption.TabIndex = 38;
            lblKpiRecoveredCaption.Text = "Recovered:";
            // 
            // lblKpiCurrentValue
            // 
            lblKpiCurrentValue.AutoSize = true;
            lblKpiCurrentValue.Font = new Font("Times New Roman", 15.75F);
            lblKpiCurrentValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiCurrentValue.Location = new Point(756, 67);
            lblKpiCurrentValue.Margin = new Padding(2, 0, 2, 0);
            lblKpiCurrentValue.Name = "lblKpiCurrentValue";
            lblKpiCurrentValue.Size = new Size(24, 23);
            lblKpiCurrentValue.TabIndex = 37;
            lblKpiCurrentValue.Text = "--";
            // 
            // lblKpiCurrentValueCaption
            // 
            lblKpiCurrentValueCaption.AutoSize = true;
            lblKpiCurrentValueCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiCurrentValueCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiCurrentValueCaption.Location = new Point(756, 36);
            lblKpiCurrentValueCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiCurrentValueCaption.Name = "lblKpiCurrentValueCaption";
            lblKpiCurrentValueCaption.Size = new Size(133, 23);
            lblKpiCurrentValueCaption.TabIndex = 36;
            lblKpiCurrentValueCaption.Text = "Current Value:";
            // 
            // lblKpiInvested
            // 
            lblKpiInvested.AutoSize = true;
            lblKpiInvested.Font = new Font("Times New Roman", 15.75F);
            lblKpiInvested.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiInvested.Location = new Point(644, 67);
            lblKpiInvested.Margin = new Padding(2, 0, 2, 0);
            lblKpiInvested.Name = "lblKpiInvested";
            lblKpiInvested.Size = new Size(24, 23);
            lblKpiInvested.TabIndex = 35;
            lblKpiInvested.Text = "--";
            // 
            // lblKpiInvestedCaption
            // 
            lblKpiInvestedCaption.AutoSize = true;
            lblKpiInvestedCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiInvestedCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiInvestedCaption.Location = new Point(644, 36);
            lblKpiInvestedCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiInvestedCaption.Name = "lblKpiInvestedCaption";
            lblKpiInvestedCaption.Size = new Size(84, 23);
            lblKpiInvestedCaption.TabIndex = 34;
            lblKpiInvestedCaption.Text = "Invested:";
            // 
            // lblKpiCurrentPrice
            // 
            lblKpiCurrentPrice.AutoSize = true;
            lblKpiCurrentPrice.Font = new Font("Times New Roman", 15.75F);
            lblKpiCurrentPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiCurrentPrice.Location = new Point(496, 67);
            lblKpiCurrentPrice.Margin = new Padding(2, 0, 2, 0);
            lblKpiCurrentPrice.Name = "lblKpiCurrentPrice";
            lblKpiCurrentPrice.Size = new Size(24, 23);
            lblKpiCurrentPrice.TabIndex = 33;
            lblKpiCurrentPrice.Text = "--";
            // 
            // lblKpiCurrentPriceCaption
            // 
            lblKpiCurrentPriceCaption.AutoSize = true;
            lblKpiCurrentPriceCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiCurrentPriceCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiCurrentPriceCaption.Location = new Point(496, 36);
            lblKpiCurrentPriceCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiCurrentPriceCaption.Name = "lblKpiCurrentPriceCaption";
            lblKpiCurrentPriceCaption.Size = new Size(128, 23);
            lblKpiCurrentPriceCaption.TabIndex = 32;
            lblKpiCurrentPriceCaption.Text = "Current Price:";
            // 
            // lblKpiNeeded
            // 
            lblKpiNeeded.AutoSize = true;
            lblKpiNeeded.Font = new Font("Times New Roman", 15.75F);
            lblKpiNeeded.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiNeeded.Location = new Point(1036, 67);
            lblKpiNeeded.Margin = new Padding(2, 0, 2, 0);
            lblKpiNeeded.Name = "lblKpiNeeded";
            lblKpiNeeded.Size = new Size(24, 23);
            lblKpiNeeded.TabIndex = 41;
            lblKpiNeeded.Text = "--";
            // 
            // lblKpiNeededCaption
            // 
            lblKpiNeededCaption.AutoSize = true;
            lblKpiNeededCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiNeededCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiNeededCaption.Location = new Point(1036, 36);
            lblKpiNeededCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiNeededCaption.Name = "lblKpiNeededCaption";
            lblKpiNeededCaption.Size = new Size(201, 23);
            lblKpiNeededCaption.TabIndex = 40;
            lblKpiNeededCaption.Text = "Needed to Break Even:";
            // 
            // lblKpiProgressPct
            // 
            lblKpiProgressPct.AutoSize = true;
            lblKpiProgressPct.Font = new Font("Times New Roman", 14.25F);
            lblKpiProgressPct.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiProgressPct.Location = new Point(205, 149);
            lblKpiProgressPct.Margin = new Padding(2, 0, 2, 0);
            lblKpiProgressPct.Name = "lblKpiProgressPct";
            lblKpiProgressPct.Size = new Size(22, 21);
            lblKpiProgressPct.TabIndex = 43;
            lblKpiProgressPct.Text = "--";
            // 
            // lblKpiProgressCaption
            // 
            lblKpiProgressCaption.AutoSize = true;
            lblKpiProgressCaption.Font = new Font("Times New Roman", 15.75F);
            lblKpiProgressCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiProgressCaption.Location = new Point(25, 118);
            lblKpiProgressCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiProgressCaption.Name = "lblKpiProgressCaption";
            lblKpiProgressCaption.Size = new Size(88, 23);
            lblKpiProgressCaption.TabIndex = 42;
            lblKpiProgressCaption.Text = "Progress:";
            // 
            // prgKpiProgress
            // 
            prgKpiProgress.Location = new Point(25, 147);
            prgKpiProgress.Name = "prgKpiProgress";
            prgKpiProgress.Size = new Size(175, 23);
            prgKpiProgress.Style = ProgressBarStyle.Marquee;
            prgKpiProgress.TabIndex = 44;
            // 
            // FrmAllocations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1285, 852);
            Controls.Add(pnlAlcKpis);
            Controls.Add(pnlAlcGrid);
            Controls.Add(pnlAlcEditor);
            Name = "FrmAllocations";
            Text = "FrmAllocations";
            pnlAlcEditor.ResumeLayout(false);
            pnlAlcEditor.PerformLayout();
            pnlAlcGrid.ResumeLayout(false);
            pnlAlcGrid.PerformLayout();
            pnlAlcKpis.ResumeLayout(false);
            pnlAlcKpis.PerformLayout();
            grpAlcAmountMode.ResumeLayout(false);
            grpAlcAmountMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAlcEditor;
        private FontAwesome.Sharp.IconButton btnAlcClear;
        private FontAwesome.Sharp.IconButton btnAlcCancelUpdate;
        private FontAwesome.Sharp.IconButton btnAlcUpdate;
        private FontAwesome.Sharp.IconButton btnAlcAdd;
        private GroupBox grpRcAmountMode;
        private RadioButton rbRcModeInvested;
        private TextBox txtRcEntryPrice;
        private Label lblRcEntryPrice;
        private Label lblRcEntryDate;
        private DateTimePicker dtpAlcTradeDate;
        private Label lblRcCaseType;
        private ComboBox cbRcCaseType;
        private Label lblRcSymbol;
        private ComboBox cbRcSymbol;
        private Panel pnlAlcGrid;
        private Panel pnlAlcKpis;
        private TextBox txtAlcExitPrice;
        private Label lblAlcExitPrice;
        private TextBox txtAlcMargin;
        private Label lblAlcMargin;
        private GroupBox grpAlcAmountMode;
        private RadioButton rbAlcModeQuantity;
        private RadioButton rbAlcModeMargin;
        private TextBox txtAlcQuantity;
        private Label lblAlcQuantity;
        private TextBox txtAlcProfit;
        private Label lblAlcProfit;
        private Label lblAlcListCaption;
        private DataGridView dataGridView1;
        private Label lblKpiRecovered;
        private Label lblKpiRecoveredCaption;
        private Label lblKpiCurrentValue;
        private Label lblKpiCurrentValueCaption;
        private Label lblKpiInvested;
        private Label lblKpiInvestedCaption;
        private Label lblKpiCurrentPrice;
        private Label lblKpiCurrentPriceCaption;
        private Label lblKpiQuantity;
        private Label lblKpiQuantityCaption;
        private Label lblKpiAvgCost;
        private Label lblKpiAvgCostCaption;
        private Label lblKpiEntryPrice;
        private Label lblKpiEntryPriceCaption;
        private Label lblKpiSymbol;
        private Label lblKpiSymbolCaption;
        private Label lblKpiProgressPct;
        private Label lblKpiProgressCaption;
        private Label lblKpiNeeded;
        private Label lblKpiNeededCaption;
        private ProgressBar prgKpiProgress;
    }
}
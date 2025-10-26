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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAllocations));
            pnlAlcEditor = new Panel();
            grpAlcAmountMode = new GroupBox();
            rbAlcModeQuantity = new RadioButton();
            rbAlcModeMargin = new RadioButton();
            btnAlcClear = new FontAwesome.Sharp.IconButton();
            btnAlcCancelUpdate = new FontAwesome.Sharp.IconButton();
            btnAlcUpdate = new FontAwesome.Sharp.IconButton();
            btnAlcAdd = new FontAwesome.Sharp.IconButton();
            txtAlcEntryPrice = new TextBox();
            lblAlcEntryPrice = new Label();
            lblAlcTradeDate = new Label();
            dtpAlcTradeDate = new DateTimePicker();
            txtAlcQuantity = new TextBox();
            lblAlcQuantity = new Label();
            txtAlcMargin = new TextBox();
            lblAlcMargin = new Label();
            pnlAlcGrid = new Panel();
            dataGridView1 = new DataGridView();
            lblAlcListCaption = new Label();
            pnlAlcKpis = new Panel();
            prgKpiProgress = new ProgressBar();
            lblKpiProgressPct = new Label();
            lblKpiProgressCaption = new Label();
            lblKpiNeeded = new Label();
            lblKpiNeededCaption = new Label();
            lblKpiRecovered = new Label();
            lblKpiRecoveredCaption = new Label();
            lblKpiCurrentValue = new Label();
            lblKpiCurrentValueCaption = new Label();
            lblKpiInvested = new Label();
            lblKpiInvestedCaption = new Label();
            lblKpiCurrentPrice = new Label();
            lblKpiCurrentPriceCaption = new Label();
            lblKpiQuantity = new Label();
            lblKpiQuantityCaption = new Label();
            lblKpiAvgCost = new Label();
            lblKpiAvgCostCaption = new Label();
            lblKpiEntryPrice = new Label();
            lblKpiEntryPriceCaption = new Label();
            lblKpiSymbol = new Label();
            lblKpiSymbolCaption = new Label();
            pnlAlcEditor.SuspendLayout();
            grpAlcAmountMode.SuspendLayout();
            pnlAlcGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            pnlAlcKpis.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAlcEditor
            // 
            pnlAlcEditor.BackColor = Color.FromArgb(27, 38, 59);
            pnlAlcEditor.Controls.Add(grpAlcAmountMode);
            pnlAlcEditor.Controls.Add(btnAlcClear);
            pnlAlcEditor.Controls.Add(btnAlcCancelUpdate);
            pnlAlcEditor.Controls.Add(btnAlcUpdate);
            pnlAlcEditor.Controls.Add(btnAlcAdd);
            pnlAlcEditor.Controls.Add(txtAlcEntryPrice);
            pnlAlcEditor.Controls.Add(lblAlcEntryPrice);
            pnlAlcEditor.Controls.Add(lblAlcTradeDate);
            pnlAlcEditor.Controls.Add(dtpAlcTradeDate);
            pnlAlcEditor.Controls.Add(txtAlcQuantity);
            pnlAlcEditor.Controls.Add(lblAlcQuantity);
            pnlAlcEditor.Controls.Add(txtAlcMargin);
            pnlAlcEditor.Controls.Add(lblAlcMargin);
            pnlAlcEditor.Location = new Point(11, 11);
            pnlAlcEditor.Margin = new Padding(2);
            pnlAlcEditor.Name = "pnlAlcEditor";
            pnlAlcEditor.Size = new Size(1263, 230);
            pnlAlcEditor.TabIndex = 20;
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
            rbAlcModeMargin.Size = new Size(132, 23);
            rbAlcModeMargin.TabIndex = 0;
            rbAlcModeMargin.TabStop = true;
            rbAlcModeMargin.Text = "Invested (USDT)";
            rbAlcModeMargin.UseVisualStyleBackColor = true;
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
            btnAlcClear.Location = new Point(563, 126);
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
            btnAlcCancelUpdate.Location = new Point(659, 102);
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
            btnAlcUpdate.Location = new Point(503, 102);
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
            btnAlcAdd.Location = new Point(553, 75);
            btnAlcAdd.Margin = new Padding(2);
            btnAlcAdd.Name = "btnAlcAdd";
            btnAlcAdd.Size = new Size(175, 39);
            btnAlcAdd.TabIndex = 24;
            btnAlcAdd.Text = "Add Allocation";
            btnAlcAdd.UseVisualStyleBackColor = false;
            btnAlcAdd.Click += btnAlcAdd_Click;
            // 
            // txtAlcEntryPrice
            // 
            txtAlcEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtAlcEntryPrice.BorderStyle = BorderStyle.None;
            txtAlcEntryPrice.Font = new Font("Times New Roman", 18F);
            txtAlcEntryPrice.Location = new Point(252, 58);
            txtAlcEntryPrice.Margin = new Padding(2);
            txtAlcEntryPrice.Name = "txtAlcEntryPrice";
            txtAlcEntryPrice.Size = new Size(162, 28);
            txtAlcEntryPrice.TabIndex = 10;
            txtAlcEntryPrice.Text = "   ";
            // 
            // lblAlcEntryPrice
            // 
            lblAlcEntryPrice.AutoSize = true;
            lblAlcEntryPrice.Font = new Font("Times New Roman", 14.25F);
            lblAlcEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcEntryPrice.Location = new Point(250, 29);
            lblAlcEntryPrice.Margin = new Padding(2, 0, 2, 0);
            lblAlcEntryPrice.Name = "lblAlcEntryPrice";
            lblAlcEntryPrice.Size = new Size(93, 21);
            lblAlcEntryPrice.TabIndex = 9;
            lblAlcEntryPrice.Text = "Entry Price";
            // 
            // lblAlcTradeDate
            // 
            lblAlcTradeDate.AutoSize = true;
            lblAlcTradeDate.Font = new Font("Times New Roman", 14.25F);
            lblAlcTradeDate.ForeColor = Color.FromArgb(156, 163, 175);
            lblAlcTradeDate.Location = new Point(43, 31);
            lblAlcTradeDate.Margin = new Padding(2, 0, 2, 0);
            lblAlcTradeDate.Name = "lblAlcTradeDate";
            lblAlcTradeDate.Size = new Size(92, 21);
            lblAlcTradeDate.TabIndex = 7;
            lblAlcTradeDate.Text = "Trade Date";
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
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(17, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1226, 295);
            dataGridView1.TabIndex = 33;
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
            // prgKpiProgress
            // 
            prgKpiProgress.Location = new Point(43, 148);
            prgKpiProgress.Name = "prgKpiProgress";
            prgKpiProgress.Size = new Size(175, 23);
            prgKpiProgress.Style = ProgressBarStyle.Marquee;
            prgKpiProgress.TabIndex = 44;
            // 
            // lblKpiProgressPct
            // 
            lblKpiProgressPct.AutoSize = true;
            lblKpiProgressPct.Font = new Font("Times New Roman", 14.25F);
            lblKpiProgressPct.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiProgressPct.Location = new Point(223, 150);
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
            lblKpiProgressCaption.Location = new Point(43, 119);
            lblKpiProgressCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiProgressCaption.Name = "lblKpiProgressCaption";
            lblKpiProgressCaption.Size = new Size(88, 23);
            lblKpiProgressCaption.TabIndex = 42;
            lblKpiProgressCaption.Text = "Progress:";
            // 
            // lblKpiNeeded
            // 
            lblKpiNeeded.AutoSize = true;
            lblKpiNeeded.Font = new Font("Times New Roman", 15.75F);
            lblKpiNeeded.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiNeeded.Location = new Point(1086, 67);
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
            lblKpiNeededCaption.Location = new Point(1086, 36);
            lblKpiNeededCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiNeededCaption.Name = "lblKpiNeededCaption";
            lblKpiNeededCaption.Size = new Size(134, 23);
            lblKpiNeededCaption.TabIndex = 40;
            lblKpiNeededCaption.Text = "Current Profit:";
            // 
            // lblKpiRecovered
            // 
            lblKpiRecovered.AutoSize = true;
            lblKpiRecovered.Font = new Font("Times New Roman", 15.75F);
            lblKpiRecovered.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiRecovered.Location = new Point(917, 67);
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
            lblKpiRecoveredCaption.Location = new Point(914, 36);
            lblKpiRecoveredCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiRecoveredCaption.Name = "lblKpiRecoveredCaption";
            lblKpiRecoveredCaption.Size = new Size(158, 23);
            lblKpiRecoveredCaption.TabIndex = 38;
            lblKpiRecoveredCaption.Text = "Break-even Price:";
            // 
            // lblKpiCurrentValue
            // 
            lblKpiCurrentValue.AutoSize = true;
            lblKpiCurrentValue.Font = new Font("Times New Roman", 15.75F);
            lblKpiCurrentValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblKpiCurrentValue.Location = new Point(759, 67);
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
            lblKpiCurrentValueCaption.Location = new Point(759, 36);
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
            lblKpiInvested.Location = new Point(659, 67);
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
            lblKpiInvestedCaption.Location = new Point(659, 36);
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
            lblKpiCurrentPrice.Location = new Point(511, 67);
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
            lblKpiCurrentPriceCaption.Location = new Point(511, 36);
            lblKpiCurrentPriceCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiCurrentPriceCaption.Name = "lblKpiCurrentPriceCaption";
            lblKpiCurrentPriceCaption.Size = new Size(128, 23);
            lblKpiCurrentPriceCaption.TabIndex = 32;
            lblKpiCurrentPriceCaption.Text = "Current Price:";
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
            lblKpiAvgCost.Location = new Point(268, 67);
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
            lblKpiAvgCostCaption.Location = new Point(268, 36);
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
            lblKpiEntryPrice.Location = new Point(149, 67);
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
            lblKpiEntryPriceCaption.Location = new Point(146, 36);
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
            lblKpiSymbol.Location = new Point(42, 67);
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
            lblKpiSymbolCaption.Location = new Point(43, 36);
            lblKpiSymbolCaption.Margin = new Padding(2, 0, 2, 0);
            lblKpiSymbolCaption.Name = "lblKpiSymbolCaption";
            lblKpiSymbolCaption.Size = new Size(79, 23);
            lblKpiSymbolCaption.TabIndex = 24;
            lblKpiSymbolCaption.Text = "Symbol:";
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmAllocations";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAllocations";
            pnlAlcEditor.ResumeLayout(false);
            pnlAlcEditor.PerformLayout();
            grpAlcAmountMode.ResumeLayout(false);
            grpAlcAmountMode.PerformLayout();
            pnlAlcGrid.ResumeLayout(false);
            pnlAlcGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            pnlAlcKpis.ResumeLayout(false);
            pnlAlcKpis.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        #region Designer fields

        private System.Windows.Forms.Panel pnlAlcEditor;
        private FontAwesome.Sharp.IconButton btnAlcClear;
        private FontAwesome.Sharp.IconButton btnAlcCancelUpdate;
        private FontAwesome.Sharp.IconButton btnAlcUpdate;
        private FontAwesome.Sharp.IconButton btnAlcAdd;

        private System.Windows.Forms.TextBox txtAlcEntryPrice;
        private System.Windows.Forms.Label lblAlcEntryPrice;
        private System.Windows.Forms.Label lblAlcTradeDate;
        private System.Windows.Forms.DateTimePicker dtpAlcTradeDate;

        private System.Windows.Forms.TextBox txtAlcMargin;
        private System.Windows.Forms.Label lblAlcMargin;

        private System.Windows.Forms.GroupBox grpAlcAmountMode;
        private System.Windows.Forms.RadioButton rbAlcModeQuantity;
        private System.Windows.Forms.RadioButton rbAlcModeMargin;

        private System.Windows.Forms.TextBox txtAlcQuantity;
        private System.Windows.Forms.Label lblAlcQuantity;

        private System.Windows.Forms.Panel pnlAlcGrid;
        private System.Windows.Forms.Label lblAlcListCaption;
        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.Panel pnlAlcKpis;
        private System.Windows.Forms.Label lblKpiSymbolCaption;
        private System.Windows.Forms.Label lblKpiSymbol;
        private System.Windows.Forms.Label lblKpiEntryPriceCaption;
        private System.Windows.Forms.Label lblKpiEntryPrice;
        private System.Windows.Forms.Label lblKpiAvgCostCaption;
        private System.Windows.Forms.Label lblKpiAvgCost;
        private System.Windows.Forms.Label lblKpiQuantityCaption;
        private System.Windows.Forms.Label lblKpiQuantity;
        private System.Windows.Forms.Label lblKpiCurrentPriceCaption;
        private System.Windows.Forms.Label lblKpiCurrentPrice;
        private System.Windows.Forms.Label lblKpiInvestedCaption;
        private System.Windows.Forms.Label lblKpiInvested;
        private System.Windows.Forms.Label lblKpiCurrentValueCaption;
        private System.Windows.Forms.Label lblKpiCurrentValue;
        private System.Windows.Forms.Label lblKpiRecoveredCaption;
        private System.Windows.Forms.Label lblKpiRecovered;
        private System.Windows.Forms.Label lblKpiNeededCaption;
        private System.Windows.Forms.Label lblKpiNeeded;
        private System.Windows.Forms.Label lblKpiProgressCaption;
        private System.Windows.Forms.Label lblKpiProgressPct;
        private System.Windows.Forms.ProgressBar prgKpiProgress;

        #endregion
    }
}
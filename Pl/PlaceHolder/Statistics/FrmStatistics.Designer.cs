namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    partial class FrmStatistics
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
            pnlTotalPnl = new Panel();
            lblTotalPnlValue = new Label();
            lblSymbol = new Label();
            pnlWinRate = new Panel();
            lblWinRateValue = new Label();
            label5 = new Label();
            pnlProfitFactor = new Panel();
            lblProfitFactorValue = new Label();
            label2 = new Label();
            pnlTotalTrades = new Panel();
            lblTotalTradesValue = new Label();
            label7 = new Label();
            pnlAvgWinning = new Panel();
            lblAvgWinValue = new Label();
            label9 = new Label();
            pnlAvgLosing = new Panel();
            lblAvgLossValue = new Label();
            label11 = new Label();
            formsPlotPnl = new ScottPlot.WinForms.FormsPlot();
            pnlChart = new Panel();
            rbAllTime = new RadioButton();
            rbMonthly = new RadioButton();
            rbWeekly = new RadioButton();
            rbDaily = new RadioButton();
            btnOpenCalculator = new FontAwesome.Sharp.IconButton();
            pnlCalculator = new Panel();
            pnlChart_Max = new Panel();
            pnlCalculator_Max = new Panel();
            pnlTotalPnl_Max = new Panel();
            pnlProfitFactor_Max = new Panel();
            pnlWinRate_Max = new Panel();
            pnlAvgWinning_Max = new Panel();
            pnlTotalTrades_Max = new Panel();
            pnlAvgLosing_Max = new Panel();
            dtpStatsDate = new DateTimePicker();
            pnlTotalPnl.SuspendLayout();
            pnlWinRate.SuspendLayout();
            pnlProfitFactor.SuspendLayout();
            pnlTotalTrades.SuspendLayout();
            pnlAvgWinning.SuspendLayout();
            pnlAvgLosing.SuspendLayout();
            pnlChart.SuspendLayout();
            pnlCalculator.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTotalPnl
            // 
            pnlTotalPnl.BackColor = Color.FromArgb(27, 38, 59);
            pnlTotalPnl.Controls.Add(lblTotalPnlValue);
            pnlTotalPnl.Controls.Add(lblSymbol);
            pnlTotalPnl.Location = new Point(48, 32);
            pnlTotalPnl.Margin = new Padding(2);
            pnlTotalPnl.Name = "pnlTotalPnl";
            pnlTotalPnl.Size = new Size(165, 148);
            pnlTotalPnl.TabIndex = 0;
            // 
            // lblTotalPnlValue
            // 
            lblTotalPnlValue.AutoSize = true;
            lblTotalPnlValue.Font = new Font("Times New Roman", 12F);
            lblTotalPnlValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblTotalPnlValue.Location = new Point(61, 84);
            lblTotalPnlValue.Margin = new Padding(2, 0, 2, 0);
            lblTotalPnlValue.Name = "lblTotalPnlValue";
            lblTotalPnlValue.Size = new Size(20, 22);
            lblTotalPnlValue.TabIndex = 3;
            lblTotalPnlValue.Text = "0";
            // 
            // lblSymbol
            // 
            lblSymbol.AutoSize = true;
            lblSymbol.Font = new Font("Times New Roman", 12F);
            lblSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblSymbol.Location = new Point(44, 40);
            lblSymbol.Margin = new Padding(2, 0, 2, 0);
            lblSymbol.Name = "lblSymbol";
            lblSymbol.Size = new Size(82, 22);
            lblSymbol.TabIndex = 2;
            lblSymbol.Text = "Total Pnl";
            // 
            // pnlWinRate
            // 
            pnlWinRate.BackColor = Color.FromArgb(27, 38, 59);
            pnlWinRate.Controls.Add(lblWinRateValue);
            pnlWinRate.Controls.Add(label5);
            pnlWinRate.Location = new Point(242, 32);
            pnlWinRate.Margin = new Padding(2);
            pnlWinRate.Name = "pnlWinRate";
            pnlWinRate.Size = new Size(165, 148);
            pnlWinRate.TabIndex = 3;
            // 
            // lblWinRateValue
            // 
            lblWinRateValue.AutoSize = true;
            lblWinRateValue.Font = new Font("Times New Roman", 12F);
            lblWinRateValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblWinRateValue.Location = new Point(64, 84);
            lblWinRateValue.Margin = new Padding(2, 0, 2, 0);
            lblWinRateValue.Name = "lblWinRateValue";
            lblWinRateValue.Size = new Size(20, 22);
            lblWinRateValue.TabIndex = 3;
            lblWinRateValue.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F);
            label5.ForeColor = Color.FromArgb(156, 163, 175);
            label5.Location = new Point(42, 40);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(83, 22);
            label5.TabIndex = 2;
            label5.Text = "Win Rate";
            // 
            // pnlProfitFactor
            // 
            pnlProfitFactor.BackColor = Color.FromArgb(27, 38, 59);
            pnlProfitFactor.Controls.Add(lblProfitFactorValue);
            pnlProfitFactor.Controls.Add(label2);
            pnlProfitFactor.Location = new Point(438, 32);
            pnlProfitFactor.Margin = new Padding(2);
            pnlProfitFactor.Name = "pnlProfitFactor";
            pnlProfitFactor.Size = new Size(165, 148);
            pnlProfitFactor.TabIndex = 4;
            // 
            // lblProfitFactorValue
            // 
            lblProfitFactorValue.AutoSize = true;
            lblProfitFactorValue.Font = new Font("Times New Roman", 12F);
            lblProfitFactorValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblProfitFactorValue.Location = new Point(69, 84);
            lblProfitFactorValue.Margin = new Padding(2, 0, 2, 0);
            lblProfitFactorValue.Name = "lblProfitFactorValue";
            lblProfitFactorValue.Size = new Size(20, 22);
            lblProfitFactorValue.TabIndex = 3;
            lblProfitFactorValue.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F);
            label2.ForeColor = Color.FromArgb(156, 163, 175);
            label2.Location = new Point(29, 40);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 22);
            label2.TabIndex = 2;
            label2.Text = "Profit Factor";
            // 
            // pnlTotalTrades
            // 
            pnlTotalTrades.BackColor = Color.FromArgb(27, 38, 59);
            pnlTotalTrades.Controls.Add(lblTotalTradesValue);
            pnlTotalTrades.Controls.Add(label7);
            pnlTotalTrades.Location = new Point(632, 32);
            pnlTotalTrades.Margin = new Padding(2);
            pnlTotalTrades.Name = "pnlTotalTrades";
            pnlTotalTrades.Size = new Size(165, 148);
            pnlTotalTrades.TabIndex = 5;
            // 
            // lblTotalTradesValue
            // 
            lblTotalTradesValue.AutoSize = true;
            lblTotalTradesValue.Font = new Font("Times New Roman", 12F);
            lblTotalTradesValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblTotalTradesValue.Location = new Point(75, 84);
            lblTotalTradesValue.Margin = new Padding(2, 0, 2, 0);
            lblTotalTradesValue.Name = "lblTotalTradesValue";
            lblTotalTradesValue.Size = new Size(20, 22);
            lblTotalTradesValue.TabIndex = 3;
            lblTotalTradesValue.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F);
            label7.ForeColor = Color.FromArgb(156, 163, 175);
            label7.Location = new Point(30, 40);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(110, 22);
            label7.TabIndex = 2;
            label7.Text = "Total Trades";
            // 
            // pnlAvgWinning
            // 
            pnlAvgWinning.BackColor = Color.FromArgb(27, 38, 59);
            pnlAvgWinning.Controls.Add(lblAvgWinValue);
            pnlAvgWinning.Controls.Add(label9);
            pnlAvgWinning.Location = new Point(828, 32);
            pnlAvgWinning.Margin = new Padding(2);
            pnlAvgWinning.Name = "pnlAvgWinning";
            pnlAvgWinning.Size = new Size(165, 148);
            pnlAvgWinning.TabIndex = 6;
            // 
            // lblAvgWinValue
            // 
            lblAvgWinValue.AutoSize = true;
            lblAvgWinValue.Font = new Font("Times New Roman", 12F);
            lblAvgWinValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblAvgWinValue.Location = new Point(65, 84);
            lblAvgWinValue.Margin = new Padding(2, 0, 2, 0);
            lblAvgWinValue.Name = "lblAvgWinValue";
            lblAvgWinValue.Size = new Size(20, 22);
            lblAvgWinValue.TabIndex = 3;
            lblAvgWinValue.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 12F);
            label9.ForeColor = Color.FromArgb(156, 163, 175);
            label9.Location = new Point(5, 40);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(163, 22);
            label9.TabIndex = 2;
            label9.Text = "Avg Winning Trade";
            // 
            // pnlAvgLosing
            // 
            pnlAvgLosing.BackColor = Color.FromArgb(27, 38, 59);
            pnlAvgLosing.Controls.Add(lblAvgLossValue);
            pnlAvgLosing.Controls.Add(label11);
            pnlAvgLosing.Location = new Point(1022, 32);
            pnlAvgLosing.Margin = new Padding(2);
            pnlAvgLosing.Name = "pnlAvgLosing";
            pnlAvgLosing.Size = new Size(158, 148);
            pnlAvgLosing.TabIndex = 7;
            // 
            // lblAvgLossValue
            // 
            lblAvgLossValue.AutoSize = true;
            lblAvgLossValue.Font = new Font("Times New Roman", 12F);
            lblAvgLossValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblAvgLossValue.Location = new Point(56, 84);
            lblAvgLossValue.Margin = new Padding(2, 0, 2, 0);
            lblAvgLossValue.Name = "lblAvgLossValue";
            lblAvgLossValue.Size = new Size(20, 22);
            lblAvgLossValue.TabIndex = 3;
            lblAvgLossValue.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 12F);
            label11.ForeColor = Color.FromArgb(156, 163, 175);
            label11.Location = new Point(6, 40);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(151, 22);
            label11.TabIndex = 2;
            label11.Text = "Avg Losing Trade";
            // 
            // formsPlotPnl
            // 
            formsPlotPnl.DisplayScale = 1.25F;
            formsPlotPnl.Location = new Point(41, 54);
            formsPlotPnl.Margin = new Padding(2);
            formsPlotPnl.Name = "formsPlotPnl";
            formsPlotPnl.Size = new Size(1092, 346);
            formsPlotPnl.TabIndex = 8;
            // 
            // pnlChart
            // 
            pnlChart.BackColor = Color.FromArgb(27, 38, 59);
            pnlChart.Controls.Add(dtpStatsDate);
            pnlChart.Controls.Add(rbAllTime);
            pnlChart.Controls.Add(rbMonthly);
            pnlChart.Controls.Add(rbWeekly);
            pnlChart.Controls.Add(rbDaily);
            pnlChart.Controls.Add(formsPlotPnl);
            pnlChart.Location = new Point(22, 225);
            pnlChart.Margin = new Padding(2);
            pnlChart.Name = "pnlChart";
            pnlChart.Size = new Size(1172, 452);
            pnlChart.TabIndex = 9;
            // 
            // rbAllTime
            // 
            rbAllTime.AutoSize = true;
            rbAllTime.ForeColor = Color.FromArgb(156, 163, 175);
            rbAllTime.Location = new Point(772, 10);
            rbAllTime.Margin = new Padding(2);
            rbAllTime.Name = "rbAllTime";
            rbAllTime.Size = new Size(82, 24);
            rbAllTime.TabIndex = 4;
            rbAllTime.TabStop = true;
            rbAllTime.Text = "All time";
            rbAllTime.UseVisualStyleBackColor = true;
            rbAllTime.CheckedChanged += rbAllTime_CheckedChanged;
            // 
            // rbMonthly
            // 
            rbMonthly.AutoSize = true;
            rbMonthly.ForeColor = Color.FromArgb(156, 163, 175);
            rbMonthly.Location = new Point(649, 10);
            rbMonthly.Margin = new Padding(2);
            rbMonthly.Name = "rbMonthly";
            rbMonthly.Size = new Size(84, 24);
            rbMonthly.TabIndex = 3;
            rbMonthly.TabStop = true;
            rbMonthly.Text = "Monthly";
            rbMonthly.UseVisualStyleBackColor = true;
            rbMonthly.CheckedChanged += rbMonthly_CheckedChanged;
            // 
            // rbWeekly
            // 
            rbWeekly.AutoSize = true;
            rbWeekly.ForeColor = Color.FromArgb(156, 163, 175);
            rbWeekly.Location = new Point(518, 10);
            rbWeekly.Margin = new Padding(2);
            rbWeekly.Name = "rbWeekly";
            rbWeekly.Size = new Size(77, 24);
            rbWeekly.TabIndex = 2;
            rbWeekly.TabStop = true;
            rbWeekly.Text = "Weekly";
            rbWeekly.UseVisualStyleBackColor = true;
            rbWeekly.CheckedChanged += rbWeekly_CheckedChanged;
            // 
            // rbDaily
            // 
            rbDaily.AutoSize = true;
            rbDaily.Checked = true;
            rbDaily.ForeColor = Color.FromArgb(156, 163, 175);
            rbDaily.Location = new Point(398, 10);
            rbDaily.Margin = new Padding(2);
            rbDaily.Name = "rbDaily";
            rbDaily.Size = new Size(64, 24);
            rbDaily.TabIndex = 1;
            rbDaily.TabStop = true;
            rbDaily.Text = "Daily";
            rbDaily.UseVisualStyleBackColor = true;
            rbDaily.CheckedChanged += rbDaily_CheckedChanged;
            // 
            // btnOpenCalculator
            // 
            btnOpenCalculator.BackColor = Color.FromArgb(30, 58, 95);
            btnOpenCalculator.FlatAppearance.BorderSize = 0;
            btnOpenCalculator.FlatStyle = FlatStyle.Flat;
            btnOpenCalculator.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOpenCalculator.IconChar = FontAwesome.Sharp.IconChar.None;
            btnOpenCalculator.IconColor = Color.Black;
            btnOpenCalculator.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenCalculator.Location = new Point(492, 25);
            btnOpenCalculator.Margin = new Padding(2);
            btnOpenCalculator.Name = "btnOpenCalculator";
            btnOpenCalculator.Size = new Size(290, 65);
            btnOpenCalculator.TabIndex = 11;
            btnOpenCalculator.Text = "Calculator";
            btnOpenCalculator.UseVisualStyleBackColor = false;
            btnOpenCalculator.Click += btnOpenCalculator_Click;
            // 
            // pnlCalculator
            // 
            pnlCalculator.BackColor = Color.FromArgb(27, 38, 59);
            pnlCalculator.Controls.Add(btnOpenCalculator);
            pnlCalculator.Location = new Point(22, 706);
            pnlCalculator.Margin = new Padding(2);
            pnlCalculator.Name = "pnlCalculator";
            pnlCalculator.Size = new Size(1172, 124);
            pnlCalculator.TabIndex = 12;
            // 
            // pnlChart_Max
            // 
            pnlChart_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlChart_Max.Location = new Point(22, 225);
            pnlChart_Max.Margin = new Padding(2);
            pnlChart_Max.Name = "pnlChart_Max";
            pnlChart_Max.Size = new Size(2065, 728);
            pnlChart_Max.TabIndex = 14;
            pnlChart_Max.Visible = false;
            // 
            // pnlCalculator_Max
            // 
            pnlCalculator_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlCalculator_Max.Location = new Point(22, 996);
            pnlCalculator_Max.Margin = new Padding(2);
            pnlCalculator_Max.Name = "pnlCalculator_Max";
            pnlCalculator_Max.Size = new Size(2065, 224);
            pnlCalculator_Max.TabIndex = 15;
            pnlCalculator_Max.Visible = false;
            // 
            // pnlTotalPnl_Max
            // 
            pnlTotalPnl_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlTotalPnl_Max.Location = new Point(131, 32);
            pnlTotalPnl_Max.Margin = new Padding(2);
            pnlTotalPnl_Max.Name = "pnlTotalPnl_Max";
            pnlTotalPnl_Max.Size = new Size(235, 168);
            pnlTotalPnl_Max.TabIndex = 16;
            pnlTotalPnl_Max.Visible = false;
            // 
            // pnlProfitFactor_Max
            // 
            pnlProfitFactor_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlProfitFactor_Max.Location = new Point(785, 32);
            pnlProfitFactor_Max.Margin = new Padding(2);
            pnlProfitFactor_Max.Name = "pnlProfitFactor_Max";
            pnlProfitFactor_Max.Size = new Size(235, 168);
            pnlProfitFactor_Max.TabIndex = 18;
            pnlProfitFactor_Max.Visible = false;
            // 
            // pnlWinRate_Max
            // 
            pnlWinRate_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlWinRate_Max.Location = new Point(459, 32);
            pnlWinRate_Max.Margin = new Padding(2);
            pnlWinRate_Max.Name = "pnlWinRate_Max";
            pnlWinRate_Max.Size = new Size(235, 168);
            pnlWinRate_Max.TabIndex = 17;
            pnlWinRate_Max.Visible = false;
            // 
            // pnlAvgWinning_Max
            // 
            pnlAvgWinning_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlAvgWinning_Max.Location = new Point(1440, 32);
            pnlAvgWinning_Max.Margin = new Padding(2);
            pnlAvgWinning_Max.Name = "pnlAvgWinning_Max";
            pnlAvgWinning_Max.Size = new Size(235, 168);
            pnlAvgWinning_Max.TabIndex = 20;
            pnlAvgWinning_Max.Visible = false;
            // 
            // pnlTotalTrades_Max
            // 
            pnlTotalTrades_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlTotalTrades_Max.Location = new Point(1112, 32);
            pnlTotalTrades_Max.Margin = new Padding(2);
            pnlTotalTrades_Max.Name = "pnlTotalTrades_Max";
            pnlTotalTrades_Max.Size = new Size(235, 168);
            pnlTotalTrades_Max.TabIndex = 19;
            pnlTotalTrades_Max.Visible = false;
            // 
            // pnlAvgLosing_Max
            // 
            pnlAvgLosing_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlAvgLosing_Max.Location = new Point(1766, 32);
            pnlAvgLosing_Max.Margin = new Padding(2);
            pnlAvgLosing_Max.Name = "pnlAvgLosing_Max";
            pnlAvgLosing_Max.Size = new Size(235, 168);
            pnlAvgLosing_Max.TabIndex = 21;
            pnlAvgLosing_Max.Visible = false;
            // 
            // dtpStatsDate
            // 
            dtpStatsDate.Format = DateTimePickerFormat.Short;
            dtpStatsDate.Location = new Point(971, 10);
            dtpStatsDate.Name = "dtpStatsDate";
            dtpStatsDate.Size = new Size(162, 27);
            dtpStatsDate.TabIndex = 9;
            // 
            // FrmStatistics
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlCalculator);
            Controls.Add(pnlChart);
            Controls.Add(pnlChart_Max);
            Controls.Add(pnlCalculator_Max);
            Controls.Add(pnlAvgLosing_Max);
            Controls.Add(pnlAvgWinning_Max);
            Controls.Add(pnlTotalPnl);
            Controls.Add(pnlAvgWinning);
            Controls.Add(pnlAvgLosing);
            Controls.Add(pnlTotalTrades);
            Controls.Add(pnlProfitFactor);
            Controls.Add(pnlWinRate);
            Controls.Add(pnlTotalTrades_Max);
            Controls.Add(pnlProfitFactor_Max);
            Controls.Add(pnlWinRate_Max);
            Controls.Add(pnlTotalPnl_Max);
            Margin = new Padding(2);
            Name = "FrmStatistics";
            Size = new Size(2134, 1254);
            Load += FrmStatistics_Load;
            pnlTotalPnl.ResumeLayout(false);
            pnlTotalPnl.PerformLayout();
            pnlWinRate.ResumeLayout(false);
            pnlWinRate.PerformLayout();
            pnlProfitFactor.ResumeLayout(false);
            pnlProfitFactor.PerformLayout();
            pnlTotalTrades.ResumeLayout(false);
            pnlTotalTrades.PerformLayout();
            pnlAvgWinning.ResumeLayout(false);
            pnlAvgWinning.PerformLayout();
            pnlAvgLosing.ResumeLayout(false);
            pnlAvgLosing.PerformLayout();
            pnlChart.ResumeLayout(false);
            pnlChart.PerformLayout();
            pnlCalculator.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTotalPnl;
        private Label lblSymbol;
        private Label lblTotalPnlValue;
        private Panel pnlWinRate;
        private Label lblWinRateValue;
        private Label label5;
        private Panel pnlProfitFactor;
        private Label lblProfitFactorValue;
        private Label label2;
        private Panel pnlTotalTrades;
        private Label lblTotalTradesValue;
        private Label label7;
        private Panel pnlAvgWinning;
        private Label lblAvgWinValue;
        private Label label9;
        private Panel pnlAvgLosing;
        private Label lblAvgLossValue;
        private Label label11;
        private ScottPlot.WinForms.FormsPlot formsPlotPnl;
        private Panel pnlChart;
        private RadioButton rbDaily;
        private RadioButton rbAllTime;
        private RadioButton rbMonthly;
        private RadioButton rbWeekly;
        private FontAwesome.Sharp.IconButton btnOpenCalculator;
        private Panel pnlCalculator;
        private Panel pnlChart_Max;
        private Panel pnlCalculator_Max;
        private Panel pnlTotalPnl_Max;
        private Panel pnlProfitFactor_Max;
        private Panel pnlWinRate_Max;
        private Panel pnlAvgWinning_Max;
        private Panel pnlTotalTrades_Max;
        private Panel pnlAvgLosing_Max;
        private DateTimePicker dtpStatsDate;
    }
}
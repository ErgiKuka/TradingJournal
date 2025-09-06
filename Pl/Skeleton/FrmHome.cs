using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using TradingJournal.Pl.PlaceHolder.Dashboard;
using TradingJournal.Pl.PlaceHolder.Journal;
using TradingJournal.Pl.PlaceHolder.Settings;
using TradingJournal.Pl.PlaceHolder.Statistics;

namespace TradingJournal.Pl.Skeleton
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();

            RoundedFormHelper.ApplyRoundedCorners(this, 80);
            RoundedFormHelper.EnableDrag(this, pnlTopBar);

            RoundedFormHelper.MakeButtonRounded(btnDashboard, 40);
            RoundedFormHelper.MakeButtonRounded(btnJournal, 40);
            RoundedFormHelper.MakeButtonRounded(btnStatistics, 40);
            RoundedFormHelper.MakeButtonRounded(btnSettings, 40);

        }
        private void LoadPanels(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlControls.Controls.Clear();
            pnlControls.Controls.Add(form);
            form.Show();
            form.BringToFront();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnDashboard.IconChar = IconChar.Home;
            btnDashboard.IconColor = Color.White;
            btnDashboard.IconSize = 30;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            //btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            //btnDashboard.TextAlign = ContentAlignment.MiddleLeft;

            btnJournal.IconChar = IconChar.Book;
            btnJournal.IconColor = Color.White;
            btnJournal.IconSize = 30;
            btnJournal.TextImageRelation = TextImageRelation.ImageBeforeText;
            //btnJournal.ImageAlign = ContentAlignment.MiddleLeft;
            //btnJournal.TextAlign = ContentAlignment.MiddleLeft;

            btnStatistics.IconChar = IconChar.ChartLine;
            btnStatistics.IconColor = Color.White;
            btnStatistics.IconSize = 30;
            btnStatistics.TextImageRelation = TextImageRelation.ImageBeforeText;
            //btnStatistics.ImageAlign = ContentAlignment.MiddleLeft;
            //btnStatistics.TextAlign = ContentAlignment.MiddleLeft;

            btnSettings.IconChar = IconChar.Cog;
            btnSettings.IconColor = Color.White;
            btnSettings.IconSize = 30;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            //btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            //btnSettings.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            FrmHome.ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
        private void MouseEnterEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(35, 52, 74);
        }
        private void MouseLeaveEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(27, 38, 59);
        }
        private void ActiveButtonEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(30, 58, 95);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmDashboard());
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmJournal());
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmStatistics());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmSettings());
        }
    }
}

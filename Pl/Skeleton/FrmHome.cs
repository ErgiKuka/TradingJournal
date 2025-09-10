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
        private Button activeButton = null;
        private Color activeColor = Color.FromArgb(30, 58, 95); // #1E3A5F
        private Color normalColor = Color.FromArgb(27, 38, 59);
        private Color hoverColor = Color.FromArgb(35, 52, 74);

        public FrmHome()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint,
              true);
            this.UpdateStyles();

            pnlControls.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
    ?.SetValue(pnlControls, true, null);

            RoundedFormHelper.ApplyRoundedCorners(this, 80);
            RoundedFormHelper.EnableDrag(this, pnlTopBar);

            RoundedFormHelper.MakeButtonRounded(btnDashboard, 40);
            RoundedFormHelper.MakeButtonRounded(btnJournal, 40);
            RoundedFormHelper.MakeButtonRounded(btnStatistics, 40);
            RoundedFormHelper.MakeButtonRounded(btnSettings, 40);

            // Set up event handlers for all buttons
            SetupButtonEvents();
        }

        private void SetupButtonEvents()
        {
            // Dashboard button
            btnDashboard.MouseEnter += MouseEnterEffect;
            btnDashboard.MouseLeave += MouseLeaveEffect;
            btnDashboard.Click += btnDashboard_Click;

            // Journal button
            btnJournal.MouseEnter += MouseEnterEffect;
            btnJournal.MouseLeave += MouseLeaveEffect;
            btnJournal.Click += btnJournal_Click;

            // Statistics button
            btnStatistics.MouseEnter += MouseEnterEffect;
            btnStatistics.MouseLeave += MouseLeaveEffect;
            btnStatistics.Click += btnStatistics_Click;

            // Settings button
            btnSettings.MouseEnter += MouseEnterEffect;
            btnSettings.MouseLeave += MouseLeaveEffect;
            btnSettings.Click += btnSettings_Click;
        }

        private void SetActiveButton(Button button)
        {
            // Reset previous active button
            if (activeButton != null)
            {
                activeButton.BackColor = normalColor;
            }

            // Set new active button
            activeButton = button;
            activeButton.BackColor = activeColor;
        }

        private void LoadPanels(Form form)
        {
            pnlControls.SuspendLayout();

            pnlControls.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            pnlControls.Controls.Add(form);
            form.Show();
            form.BringToFront();

            pnlControls.ResumeLayout();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnDashboard.IconChar = IconChar.Home;
            btnDashboard.IconColor = Color.White;
            btnDashboard.IconSize = 30;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnJournal.IconChar = IconChar.Book;
            btnJournal.IconColor = Color.White;
            btnJournal.IconSize = 30;
            btnJournal.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnStatistics.IconChar = IconChar.ChartLine;
            btnStatistics.IconColor = Color.White;
            btnStatistics.IconSize = 30;
            btnStatistics.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnSettings.IconChar = IconChar.Cog;
            btnSettings.IconColor = Color.White;
            btnSettings.IconSize = 30;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;

            pnlControls.SuspendLayout();
            pnlControls.Visible = false;
            LoadPanels(new FrmDashboard());
            SetActiveButton(btnDashboard);

            pnlControls.ResumeLayout();
            pnlControls.Visible = true;
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
            if (btn != activeButton)
            {
                btn.BackColor = hoverColor;
            }
        }

        private void MouseLeaveEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != activeButton)
            {
                btn.BackColor = normalColor;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmDashboard());
            SetActiveButton(btnDashboard);
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmJournal());
            SetActiveButton(btnJournal);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmStatistics());
            SetActiveButton(btnStatistics);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmSettings());
            SetActiveButton(btnSettings);
        }
    }
}

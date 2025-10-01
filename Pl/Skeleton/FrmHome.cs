using FontAwesome.Sharp;
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
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Pl.PlaceHolder.Dashboard;
using TradingJournal.Pl.PlaceHolder.Journal;
using TradingJournal.Pl.PlaceHolder.RiskManagement;
using TradingJournal.Pl.PlaceHolder.Settings;
using TradingJournal.Pl.PlaceHolder.Statistics;

namespace TradingJournal.Pl.Skeleton
{
    public partial class FrmHome : Form
    {
        private Button activeButton = null;
        private Color activeColor => ThemeManager.ActiveButtonColor;
        private Color normalColor => ThemeManager.ButtonColor;
        private Color hoverColor => ThemeManager.ButtonHoverColor;

        private Rectangle _restoreBounds;
        private bool _isCustomMaximized = false;
        private const int FAKE_MAX_MARGIN = 12;

        public FrmHome()
        {
            InitializeComponent();

            btnMaximize.IconChar = IconChar.Square;
            btnMaximize.IconColor = Color.Black;
            btnMaximize.IconSize = 20;

            btnMaximize.Click += BtnMaximize_Click;
            pnlTopBar.DoubleClick += PnlTopBar_DoubleClick;
            this.Resize += FrmHome_Resize;
            this.MinimumSize = new Size(1024, 720);
            this.AutoScaleMode = AutoScaleMode.Dpi;

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
            RoundedFormHelper.MakeButtonRounded(btnRiskManagement, 40);

            // Set up event handlers for all buttons
            SetupButtonEvents();
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }
        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;
            pnlTopBar.BackColor = ThemeManager.BackgroundColor;
            pnlNavigation.BackColor = ThemeManager.BackgroundColor;
            pnlControls.BackColor = ThemeManager.BackgroundColor;
            panel1.BackColor = ThemeManager.BackgroundColor;

            btnDashboard.BackColor = ThemeManager.ButtonColor;
            btnDashboard.ForeColor = ThemeManager.TextColor;
            btnJournal.BackColor = ThemeManager.ButtonColor;
            btnJournal.ForeColor = ThemeManager.TextColor;
            btnStatistics.BackColor = ThemeManager.ButtonColor;
            btnStatistics.ForeColor = ThemeManager.TextColor;
            btnSettings.BackColor = ThemeManager.ButtonColor;
            btnSettings.ForeColor = ThemeManager.TextColor;
            btnRiskManagement.BackColor = ThemeManager.ButtonColor;
            btnRiskManagement.ForeColor = ThemeManager.TextColor;

            if (activeButton != null)
                SetActiveButton(activeButton);
        }


        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            ToggleMaximize(false); // pass true to use fake maximize (keeps rounded corner margin)
        }

        private void PnlTopBar_DoubleClick(object sender, EventArgs e)
        {
            ToggleMaximize(false); // double-click top bar toggles maximize
        }

        private void ToggleMaximize(bool useFakeMaximize)
        {
            FormWindowStateExtended newState;
            if (!_isCustomMaximized)
            {
                _restoreBounds = this.Bounds;
                var wa = Screen.FromHandle(this.Handle).WorkingArea;
                this.Bounds = useFakeMaximize
                    ? new Rectangle(wa.X + FAKE_MAX_MARGIN, wa.Y + FAKE_MAX_MARGIN, wa.Width - FAKE_MAX_MARGIN * 2, wa.Height - FAKE_MAX_MARGIN * 2)
                    : wa;

                if (!useFakeMaximize) this.Region = null;

                _isCustomMaximized = true;
                newState = FormWindowStateExtended.Maximized;
            }
            else
            {
                this.Bounds = _restoreBounds;
                _isCustomMaximized = false;
                if (this.WindowState == FormWindowState.Normal)
                    RoundedFormHelper.ApplyRoundedCorners(this, 80);

                newState = FormWindowStateExtended.Normal;
            }

            // Notify the active child form of the state change.
            var activeResponsiveForm = pnlControls.Controls.OfType<IResponsiveChildForm>().FirstOrDefault();
            activeResponsiveForm?.SetWindowState(newState);
        }

        private void FrmHome_Resize(object sender, EventArgs e)
        {
            // If manually maximized or minimized via keyboard / OS, keep visuals consistent
            if (this.WindowState == FormWindowState.Maximized)
            {
                // When truly maximized by Windows, remove rounding so edges don't get cut
                this.Region = null;
                _isCustomMaximized = true;
            }
            else if (this.WindowState == FormWindowState.Normal && !_isCustomMaximized)
            {
                // Reapply rounding when returning to normal state (if not in custom maximized)
                RoundedFormHelper.ApplyRoundedCorners(this, 80);
            }
        }

        public static void RemoveRoundedCorners(Form form)
        {
            form.Region = null;
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

            // **CRITICAL:** After loading, immediately set the form to the current window state.
            if (form is IResponsiveChildForm responsiveForm)
            {
                var currentState = _isCustomMaximized ? FormWindowStateExtended.Maximized : FormWindowStateExtended.Normal;
                responsiveForm.SetWindowState(currentState);
            }

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

            btnRiskManagement.IconChar = IconChar.ShieldAlt;
            btnRiskManagement.IconColor = Color.White;
            btnRiskManagement.IconSize = 30;
            btnRiskManagement.TextImageRelation = TextImageRelation.ImageBeforeText;

            pnlControls.SuspendLayout();
            pnlControls.Visible = false;
            LoadPanels(new FrmDashboard());
            SetActiveButton(btnDashboard);

            pnlControls.ResumeLayout();
            pnlControls.Visible = true;

            ApplyTheme();
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

        private void btnRiskManagement_Click(object sender, EventArgs e)
        {
            LoadPanels(new FrmRiskManagement());
            SetActiveButton(btnRiskManagement);
        }
    }
}

using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Pl.PlaceHolder.Dashboard;
using TradingJournal.Pl.PlaceHolder.Journal;
using TradingJournal.Pl.PlaceHolder.Recovery_Planner;
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
            this.Load += FrmHome_Load;

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
            RoundedFormHelper.MakeButtonRounded(btnRecoveryPlanner, 40);

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

            btnDashboard.BackColor = ThemeManager.ButtonColor; btnDashboard.ForeColor = ThemeManager.TextColor;
            btnJournal.BackColor = ThemeManager.ButtonColor; btnJournal.ForeColor = ThemeManager.TextColor;
            btnStatistics.BackColor = ThemeManager.ButtonColor; btnStatistics.ForeColor = ThemeManager.TextColor;
            btnSettings.BackColor = ThemeManager.ButtonColor; btnSettings.ForeColor = ThemeManager.TextColor;
            btnRiskManagement.BackColor = ThemeManager.ButtonColor; btnRiskManagement.ForeColor = ThemeManager.TextColor;
            btnRecoveryPlanner.BackColor = ThemeManager.ButtonColor; btnRecoveryPlanner.ForeColor = ThemeManager.TextColor;

            if (activeButton != null) SetActiveButton(activeButton);
        }

        private void BtnMaximize_Click(object sender, EventArgs e) => ToggleMaximize(false);
        private void PnlTopBar_DoubleClick(object sender, EventArgs e) => ToggleMaximize(false);

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

            // Notify currently visible child so it can reparent to *_Max panels (or back)
            var activeResponsiveForm = pnlControls.Controls.OfType<IResponsiveChildForm>().FirstOrDefault();
            activeResponsiveForm?.SetWindowState(newState);
        }

        private void FrmHome_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.Region = null;
                _isCustomMaximized = true;
            }
            else if (this.WindowState == FormWindowState.Normal && !_isCustomMaximized)
            {
                RoundedFormHelper.ApplyRoundedCorners(this, 80);
            }
        }

        public static void RemoveRoundedCorners(Form form) => form.Region = null;

        private void SetupButtonEvents()
        {
            btnDashboard.MouseEnter += MouseEnterEffect; btnDashboard.MouseLeave += MouseLeaveEffect; btnDashboard.Click += btnDashboard_Click;
            btnJournal.MouseEnter += MouseEnterEffect; btnJournal.MouseLeave += MouseLeaveEffect; btnJournal.Click += btnJournal_Click;
            btnStatistics.MouseEnter += MouseEnterEffect; btnStatistics.MouseLeave += MouseLeaveEffect; btnStatistics.Click += btnStatistics_Click;
            btnSettings.MouseEnter += MouseEnterEffect; btnSettings.MouseLeave += MouseLeaveEffect; btnSettings.Click += btnSettings_Click;
            btnRiskManagement.Click += btnRiskManagement_Click;
            btnRecoveryPlanner.Click += btnRecoveryPlanner_Click;
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null) activeButton.BackColor = normalColor;
            activeButton = button;
            activeButton.BackColor = activeColor;
        }

        private void LoadControl(UserControl control)
        {
            pnlControls.SuspendLayout();
            pnlControls.Controls.Clear();

            control.Dock = DockStyle.Fill;
            pnlControls.Controls.Add(control);
            control.BringToFront();

            // Push current state to child
            if (control is IResponsiveChildForm responsive)
            {
                var currentState = _isCustomMaximized
                    ? FormWindowStateExtended.Maximized
                    : FormWindowStateExtended.Normal;
                responsive.SetWindowState(currentState);
            }

            pnlControls.ResumeLayout();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnDashboard.IconChar = IconChar.Home; btnDashboard.IconColor = System.Drawing.Color.White; btnDashboard.IconSize = 30; btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnJournal.IconChar = IconChar.Book; btnJournal.IconColor = System.Drawing.Color.White; btnJournal.IconSize = 30; btnJournal.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStatistics.IconChar = IconChar.ChartLine; btnStatistics.IconColor = System.Drawing.Color.White; btnStatistics.IconSize = 30; btnStatistics.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.IconChar = IconChar.Cog; btnSettings.IconColor = System.Drawing.Color.White; btnSettings.IconSize = 30; btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRiskManagement.IconChar = IconChar.ShieldAlt; btnRiskManagement.IconColor = System.Drawing.Color.White; btnRiskManagement.IconSize = 30; btnRiskManagement.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRecoveryPlanner.IconChar = IconChar.Heartbeat; btnRecoveryPlanner.IconColor = System.Drawing.Color.White; btnRecoveryPlanner.IconSize = 30; btnRecoveryPlanner.TextImageRelation = TextImageRelation.ImageBeforeText;

            pnlControls.SuspendLayout();
            pnlControls.Visible = false;
            LoadControl(new FrmDashboard());
            SetActiveButton(btnDashboard);
            pnlControls.ResumeLayout();
            pnlControls.Visible = true;

            TradingJournal.Core.Logic.Helpers.AppNotifier.Initialize(this, null, "TradingJournal");

            // >>> STARTUP LAYOUT: read user preference and apply once
            var desired = ResponsiveStartupPrefs.GetDesired();
            if (desired == FormWindowStateExtended.Maximized && !_isCustomMaximized)
            {
                // Use your existing custom maximize flow so children get notified and reparent
                ToggleMaximize(false);
            }

            ApplyTheme();

            AutoBackupCoordinator.BackupCompleted -= OnAutoBackupCompleted;
            AutoBackupCoordinator.BackupCompleted += OnAutoBackupCompleted;

            // Run immediate check + minute scheduler
            AutoBackupCoordinator.StartScheduler();
        }

        private void OnAutoBackupCompleted(string createdPath)
        {
            try
            {
                // If Settings is the current page, refresh its "Last Backup" label
                var settingsUc = pnlControls.Controls.OfType<TradingJournal.Pl.PlaceHolder.Settings.FrmSettings>()
                                                     .FirstOrDefault();
                settingsUc?.RefreshLastBackupLabelForAuto();
            }
            catch { /* ignore UI race conditions */ }
        }


        private void BtnMinimize_Click(object sender, EventArgs e) => FrmHome.ActiveForm.WindowState = FormWindowState.Minimized;
        private void BtnExit_Click(object sender, EventArgs e) => this.Close();

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void MouseEnterEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != activeButton) btn.BackColor = hoverColor;
        }

        private void MouseLeaveEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != activeButton) btn.BackColor = normalColor;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmDashboard());
            SetActiveButton(btnDashboard);
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmJournal());
            SetActiveButton(btnJournal);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmStatistics());
            SetActiveButton(btnStatistics);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmSettings());
            SetActiveButton(btnSettings);
        }

        private void btnRiskManagement_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmRiskManagement());
            SetActiveButton(btnRiskManagement);
        }

        private void btnRecoveryPlanner_Click(object sender, EventArgs e)
        {
            LoadControl(new FrmRecoveryPlanner());
            SetActiveButton(btnRecoveryPlanner);
        }
    }
}

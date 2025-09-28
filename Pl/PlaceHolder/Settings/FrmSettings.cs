using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    // Note: Make sure your form implements IResponsiveChildForm if it doesn't already
    public partial class FrmSettings : Form, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private SettingsManager _settings;

        public FrmSettings()
        {
            InitializeComponent();
            _settings = SettingsManager.Load();

            // Round the corners of the panels
            RoundedFormHelper.RoundPanel(panel1, 20);
            RoundedFormHelper.RoundPanel(panel2, 20);

            // Initialize the layout manager and define responsive behavior
            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            // Subscribe to theme changes and apply the current theme
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }

        private void InitializeResponsiveLayouts()
        {
            // --- Register all controls that need to move between panels ---
            // For each control, specify its normal parent (panel1) and maximized parent (panel2),
            // along with its location and size in the maximized state.

            _layoutManager.RegisterControl(label1, panel1, panel2,
                new Point(32, 29), new Size(300, 25));

            _layoutManager.RegisterControl(txtNewAccountBalance, panel1, panel2,
                new Point(32, 76), new Size(400, 45));

            _layoutManager.RegisterControl(btnChangeBalance, panel1, panel2,
                new Point(450, 76), new Size(180, 38));

            _layoutManager.RegisterControl(rbDarkMode, panel1, panel2,
                new Point(32, 180), new Size(100, 35));

            _layoutManager.RegisterControl(rbLightMode, panel1, panel2,
                new Point(160, 180), new Size(100, 35));

            // --- Register actions to apply styles for each state ---
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, ApplyNormalStyle);
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, ApplyMaximizedStyle);
        }

        /// <summary>
        /// Implements the IResponsiveChildForm interface.
        /// This method is the single entry point for changing the form's state.
        /// It delegates all work to the ResponsiveLayoutManager.
        /// </summary>
        public void SetWindowState(FormWindowStateExtended newState)
        {
            // The layout manager will automatically handle showing/hiding panel1 and panel2
            // and moving all registered child controls between them.
            _layoutManager.SetWindowState(newState);
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            // Set the initial theme radio button state
            if (_settings.Theme == AppTheme.Dark)
                rbDarkMode.Checked = true;
            else
                rbLightMode.Checked = true;

            // **CRITICAL:** Ensure the maximized panel is hidden at startup.
            panel2.Visible = false;

            // Apply the initial "Normal" state.
            SetWindowState(FormWindowStateExtended.Normal);
        }

        #region --- Styling and Themeing ---

        private void ApplyTheme()
        {
            // Form background
            this.BackColor = ThemeManager.BackgroundColor;

            // Panels
            panel1.BackColor = ThemeManager.PanelColor;
            panel2.BackColor = ThemeManager.PanelColor;

            // Label
            label1.ForeColor = ThemeManager.TextColor;

            // Textbox
            txtNewAccountBalance.BackColor = ThemeManager.TextBoxColor;
            txtNewAccountBalance.ForeColor = ThemeManager.TextColor;

            // Radio buttons
            rbDarkMode.ForeColor = ThemeManager.TextColor;
            rbLightMode.ForeColor = ThemeManager.TextColor;

            // Button
            btnChangeBalance.BackColor = ThemeManager.DarkButtonColor;
            btnChangeBalance.ForeColor = ThemeManager.ActionButtonTextColor;
        }

        private void ApplyNormalStyle()
        {
            // Apply fonts for the normal, smaller state
            label1.Font = new Font("Times New Roman", 16f, FontStyle.Regular);
            txtNewAccountBalance.Font = new Font("Times New Roman", 20f, FontStyle.Bold);
            rbDarkMode.Font = new Font("Times New Roman", 16f);
            rbLightMode.Font = new Font("Times New Roman", 16f);
            btnChangeBalance.Font = new Font("Times New Roman", 12f);
        }

        private void ApplyMaximizedStyle()
        {
            // Apply larger fonts for the maximized state
            label1.Font = new Font("Times New Roman", 20f, FontStyle.Regular);
            txtNewAccountBalance.Font = new Font("Times New Roman", 24f, FontStyle.Bold);
            rbDarkMode.Font = new Font("Times New Roman", 18f);
            rbLightMode.Font = new Font("Times New Roman", 18f);
            btnChangeBalance.Font = new Font("Times New Roman", 14f);
        }

        #endregion

        #region --- Control Event Handlers ---

        private void btnChangeBalance_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtNewAccountBalance.Text, out decimal newBalance))
            {
                _settings.AccountBalance = newBalance;
                _settings.Save();
                MessageBox.Show("Account balance updated!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtNewAccountBalance.Clear();
        }

        private void rbTheme_CheckedChanged(object sender, EventArgs e)
        {
            // This handler is called whenever a radio button's state changes.
            // We only act if the button is being checked.
            if (((RadioButton)sender).Checked)
            {
                if (rbDarkMode.Checked)
                {
                    ThemeManager.SetTheme(AppTheme.Dark);
                    _settings.Theme = AppTheme.Dark;
                }
                else // rbLightMode must be checked
                {
                    ThemeManager.SetTheme(AppTheme.Light);
                    _settings.Theme = AppTheme.Light;
                }

                _settings.Save();
            }
        }

        #endregion
    }
}

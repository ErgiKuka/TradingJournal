using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    // Note: Make sure your form implements IResponsiveChildForm if it doesn't already
    public partial class FrmSettings : Form, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private SettingsManager _settings;
        private readonly DatabaseBackupService _bkp;
        private static readonly TimeSpan UNDO_TTL = TimeSpan.FromHours(24);

        public FrmSettings()
        {
            InitializeComponent();
            _settings = SettingsManager.Load();
            _bkp = new DatabaseBackupService(msg => Console.WriteLine(msg));

            // Round the corners of the panels
            RoundedFormHelper.RoundPanel(panel1, 20);
            RoundedFormHelper.RoundPanel(panel2, 20);

            // Initialize the layout manager and define responsive behavior
            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();
            UpdateBalance();

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
                new Point(32, 129), new Size(300, 25));

            _layoutManager.RegisterControl(txtNewAccountBalance, panel1, panel2,
                new Point(32, 176), new Size(300, 45));

            _layoutManager.RegisterControl(btnChangeBalance, panel1, panel2,
                new Point(450, 176), new Size(180, 38));

            _layoutManager.RegisterControl(rbDarkMode, panel1, panel2,
                new Point(32, 280), new Size(100, 35));

            _layoutManager.RegisterControl(rbLightMode, panel1, panel2,
                new Point(160, 280), new Size(100, 35));
            _layoutManager.RegisterControl(lblCurrentBalance, panel1, panel2,
                new Point(45, 65), new Size(300, 35));
            _layoutManager.RegisterControl(label2, panel1, panel2,
                new Point(32, 30), new Size(300, 25));

            _layoutManager.RegisterControl(btnReset, panel1, panel2,
                new Point(450, 65), new Size(180, 38));
            _layoutManager.RegisterControl(btnBackupNow, panel1, panel2,
                new Point(750, 65), new Size(180, 38));
            _layoutManager.RegisterControl(btnRestoreLatest, panel1, panel2,
                new Point(750, 165), new Size(230, 38));
            _layoutManager.RegisterControl(btnRestoreFrom, panel1, panel2,
                new Point(750, 265), new Size(180, 38));
            _layoutManager.RegisterControl(btnUndoLastRestore, panel1, panel2,
                new Point(1050, 165), new Size(200, 38));

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

        public void UpdateBalance()
        {
            lblCurrentBalance.Text = _settings.AccountBalance.ToString("C");
        }

        #region --- Styling and Themeing ---

        private void ApplyTheme()
        {
            // Form background
            this.BackColor = ThemeManager.BackgroundColor;
            label1.ForeColor = ThemeManager.TextColor;
            label2.ForeColor = ThemeManager.TextColor;
            lblCurrentBalance.ForeColor = ThemeManager.TextColor;

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

            btnReset.BackColor = ThemeManager.DarkButtonColor;
            btnReset.ForeColor = ThemeManager.ActionButtonTextColor;
            btnBackupNow.BackColor = ThemeManager.DarkButtonColor;
            btnBackupNow.ForeColor = ThemeManager.ActionButtonTextColor;
            btnRestoreLatest.BackColor = ThemeManager.DarkButtonColor;
            btnRestoreLatest.ForeColor = ThemeManager.ActionButtonTextColor;
            btnRestoreFrom.BackColor = ThemeManager.DarkButtonColor;
            btnRestoreFrom.ForeColor = ThemeManager.ActionButtonTextColor;
            btnUndoLastRestore.BackColor = ThemeManager.DarkButtonColor;
            btnUndoLastRestore.ForeColor = ThemeManager.ActionButtonTextColor;

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

            UpdateBalance();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to reset all settings to default values?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _settings = new SettingsManager
                {
                    AccountBalance = 0,
                    Theme = AppTheme.Dark
                };
                _settings.Save();
                UpdateBalance();
                txtNewAccountBalance.Clear();
                MessageBox.Show("Settings have been reset to default values.", "Reset Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            if (_bkp.BackupNow(db, out var path))
                MessageBox.Show($"Backup created:\n{path}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Backup failed.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRestoreLatest_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();

            // 1st confirmation
            var latest = _bkp.GetBackups().FirstOrDefault();
            var msg = latest == null
                ? "No backups found."
                : $"Latest backup:\n{latest.Name}\nCreated: {latest.CreationTime}\n\n" +
                  "Restoring will overwrite current data.\n\nContinue?";
            if (latest == null)
            {
                MessageBox.Show(msg, "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(msg, "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // 2nd confirmation
            if (MessageBox.Show("Final confirmation: Are you absolutely sure you want to restore?\n" +
                                "This will restart the app. An undo snapshot will be created.",
                                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_bkp.RestoreFromFileWithUndo(db, latest.FullName, $"Latest: {latest.Name}"))
            {
                MessageBox.Show("Restored. The app will now restart.\nYou can use 'Undo Last Restore' if needed.",
                                "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Restore failed.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreFrom_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();

            using var ofd = new OpenFileDialog
            {
                InitialDirectory = _bkp.BackupRoot,
                Filter = "TradingJournal backups (*.sqlite.bak)|*.sqlite.bak|All files (*.*)|*.*",
                Title = "Choose a backup to restore"
            };
            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            // 1st confirmation
            if (MessageBox.Show($"Restore from:\n{ofd.FileName}\n\nThis will overwrite current data. Continue?",
                                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // 2nd confirmation
            if (MessageBox.Show("Final confirmation: Are you absolutely sure?\n" +
                                "This will restart the app. An undo snapshot will be created.",
                                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_bkp.RestoreFromFileWithUndo(db, ofd.FileName, $"Chosen: {Path.GetFileName(ofd.FileName)}"))
            {
                MessageBox.Show("Restored. The app will now restart.\nYou can use 'Undo Last Restore' if needed.",
                                "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Restore failed.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndoLastRestore_Click(object sender, EventArgs e)
        {
            // Show what we’re undoing (path, age, source), enforce TTL, double confirm
            if (!_bkp.TryGetUndoInfo(out var info))
            {
                MessageBox.Show("No undo snapshot is available.\n" +
                                "Undo is only created right before a restore.",
                                "Undo Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var age = DateTime.UtcNow - info.CreatedUtc;
            var expiresIn = UNDO_TTL - age;
            var expired = age > UNDO_TTL;

            var details =
                $"Undo snapshot:\n" +
                $"- File: {info.UndoPath}\n" +
                $"- Created (UTC): {info.CreatedUtc:yyyy-MM-dd HH:mm:ss}\n" +
                $"- From restore: {info.SourceLabel}\n" +
                $"- Age: {age:g}\n" +
                (expired
                    ? "- Status: EXPIRED (older than 24 hours)\n"
                    : $"- Expires in: {expiresIn:g}\n");

            if (expired)
            {
                var ans = MessageBox.Show(details + "\nThis undo has expired. Clear it now?",
                                          "Undo Restore (Expired)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (ans == DialogResult.Yes)
                {
                    _bkp.ClearUndo(deleteUndoFile: false);
                    MessageBox.Show("Expired undo marker cleared.", "Undo Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            // 1st confirm
            if (MessageBox.Show(details + "\nUndo the most recent restore? This will overwrite current data.",
                                "Confirm Undo Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // 2nd confirm
            if (MessageBox.Show("Final confirmation: Proceed with UNDO?\nThe app will restart afterwards.",
                                "Confirm Undo Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using var db = new AppDbContext();
            if (_bkp.UndoLastRestore(db))
            {
                MessageBox.Show("Undo complete. The app will restart.", "Undo Restore",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Undo failed.", "Undo Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

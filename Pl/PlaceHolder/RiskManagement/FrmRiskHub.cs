using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Pl.PlaceHolder.RiskManagement
{
    /// <summary>
    /// Container for the Risk Management area. A MenuStrip swaps risk-model sub-pages into a
    /// content panel, so we don't add a left-nav entry per model. Each sub-page is its own UserControl.
    /// Implements IResponsiveChildForm: FrmHome pushes the window state here, and it is forwarded
    /// to whichever sub-page is showing.
    /// </summary>
    public partial class FrmRiskHub : UserControl, IResponsiveChildForm
    {
        private ToolStripMenuItem _activeItem;
        private FormWindowStateExtended _currentState = FormWindowStateExtended.Normal;

        public FrmRiskHub()
        {
            InitializeComponent();
            ShowSection(new FrmSlTpFromLeverage(), mnuSlTp);

            WireMenu();
            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => ThemeManager.ThemeChanged -= OnThemeChanged; // avoid static-event leak
            ApplyTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        private void FrmRiskHub_Load(object sender, EventArgs e)
        {
            _currentState = ResponsiveState.GetFor(this);
            ShowSection(new FrmSlTpFromLeverage(), mnuSlTp); // default sub-page
        }

        // ----------------------------------------------------------------- Responsive
        public void SetWindowState(FormWindowStateExtended newState)
        {
            _currentState = newState;
            var active = pnlContent.Controls.OfType<IResponsiveChildForm>().FirstOrDefault();
            active?.SetWindowState(newState);
        }

        // ----------------------------------------------------------------- Navigation
        private void WireMenu()
        {
            mnuSlTp.Click += (s, e) => ShowSection(new FrmSlTpFromLeverage(), mnuSlTp);
            mnuPositionSize.Click += (s, e) => ShowSection(new FrmRiskManagement(), mnuPositionSize);

            // When you build the Partial TP calculator, add its item + line:
            // mnuPartialTp.Click += (s, e) => ShowSection(new FrmPartialTp(), mnuPartialTp);
        }

        private void ShowSection(UserControl section, ToolStripMenuItem sourceItem)
        {
            pnlContent.SuspendLayout();

            // Dispose the outgoing section so its theme handler unsubscribes and it can be GC'd.
            // (Controls.Clear() alone removes but does NOT dispose.)
            var outgoing = pnlContent.Controls.Cast<Control>().ToList();
            pnlContent.Controls.Clear();
            foreach (var c in outgoing) c.Dispose();

            section.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(section);
            section.BringToFront();

            if (section is IResponsiveChildForm responsive)
                responsive.SetWindowState(_currentState);

            pnlContent.ResumeLayout();
            SetActiveItem(sourceItem);
        }

        private void SetActiveItem(ToolStripMenuItem item)
        {
            if (_activeItem != null)
                _activeItem.Font = new Font(_activeItem.Font, FontStyle.Regular);

            _activeItem = item;
            _activeItem.Font = new Font(_activeItem.Font, FontStyle.Bold);
        }

        // ----------------------------------------------------------------- Theme
        private void ApplyTheme()
        {
            BackColor = ThemeManager.BackgroundColor;
            pnlContent.BackColor = ThemeManager.BackgroundColor;

            menuStripNav.BackColor = ThemeManager.BackgroundColor;
            menuStripNav.Renderer = new ThemedMenuRenderer();

            foreach (var item in MenuItems())
            {
                item.ForeColor = ThemeManager.TextColor;
                item.AutoSize = true;
                item.Padding = new Padding(16, 8, 16, 8);
                item.Margin = new Padding(4, 4, 4, 4);    
            }

        }

        private IEnumerable<ToolStripMenuItem> MenuItems()
        {
            yield return mnuSlTp;
            yield return mnuPositionSize;
        }
    }
}
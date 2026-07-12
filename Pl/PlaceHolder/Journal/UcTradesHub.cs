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
using TradingJournal.Pl.PlaceHolder.RiskManagement;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    public partial class UcTradesHub : UserControl, IResponsiveChildForm
    {
        private ToolStripMenuItem _activeItem;
        private FormWindowStateExtended _currentState = FormWindowStateExtended.Normal;

        public UcTradesHub()
        {
            InitializeComponent();
            ShowSection(new FrmTrading(), mnuTrading);

            WireMenu();
            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => ThemeManager.ThemeChanged -= OnThemeChanged; // avoid static-event leak
            ApplyTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        private void FrmRiskHub_Load(object sender, EventArgs e)
        {
            _currentState = ResponsiveState.GetFor(this);
            ShowSection(new FrmTrading(), mnuTrading); // default sub-page
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
            mnuTrading.Click += (s, e) => ShowSection(new FrmTrading(), mnuTrading);
            mnuJournal.Click += (s, e) => ShowSection(new FrmJournal(), mnuJournal);
            mnuPlatforms.Click += (s, e) => ShowSection(new FrmPlatforms(), mnuPlatforms);
        }

        private void ShowSection(UserControl section, ToolStripMenuItem sourceItem)
        {
            pnlContent.SuspendLayout();
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
            yield return mnuTrading;
            yield return mnuJournal;
            yield return mnuPlatforms;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TradingJournal.Core.Logic.Helpers
{
    public static class AppNotifier
    {
        private static NotifyIcon? _icon;

        public static void Initialize(Form owner, Icon? icon = null, string tooltip = "TradingJournal")
        {
            if (_icon != null) return;
            _icon = new NotifyIcon
            {
                Visible = true,
                Icon = icon ?? SystemIcons.Information,
                Text = tooltip
            };
            owner.FormClosed += (_, __) => { try { _icon.Visible = false; _icon.Dispose(); } catch { } };
        }

        public static void ShowInfo(string text, string title = "TradingJournal")
            => _icon?.ShowBalloonTip(5000, title, text, ToolTipIcon.Info);

        public static void ShowWarning(string text, string title = "TradingJournal")
            => _icon?.ShowBalloonTip(5000, title, text, ToolTipIcon.Warning);

        public static void ShowError(string text, string title = "TradingJournal")
            => _icon?.ShowBalloonTip(5000, title, text, ToolTipIcon.Error);
    }
}

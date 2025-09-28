using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Core.Logic.Helpers
{
    public enum AppTheme
    {
        Dark,
        Light
    }

    public static class ThemeManager
    {
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Dark;

        public static Color BackgroundColor =>
    CurrentTheme == AppTheme.Dark ? Color.FromArgb(13, 27, 42) : Color.FromArgb(216, 211, 204);

        public static Color PanelColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(27, 38, 59) : Color.FromArgb(200, 194, 186);

        public static Color CalcPanelColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(200, 194, 186);

        public static Color ButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(27, 38, 59) : Color.FromArgb(184, 176, 166);
        public static Color OthButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(13, 27, 42) : Color.FromArgb(184, 176, 166);

        public static Color DarkButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(184, 176, 166);

        public static Color DataGrid =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(27, 38, 59) : Color.FromArgb(216, 211, 204);

        public static Color TextBoxColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(224, 219, 213);
        public static Color CalcTextBoxColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(13, 27, 42) : Color.FromArgb(224, 219, 213);
        public static Color TextColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(156, 163, 175) : Color.FromArgb(42, 42, 42);

        public static Color ButtonHoverColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(45, 55, 72) : Color.FromArgb(169, 159, 148);

        public static Color ActiveButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(169, 159, 148);

        public static Color CryptoCurrentColor =>
            CurrentTheme == AppTheme.Dark ? Color.White : Color.Black;

        public static Color UpdateColumnColor =>
    CurrentTheme == AppTheme.Dark ? Color.FromArgb(24, 30, 54) : Color.FromArgb(76, 175, 80); // greenish

        public static Color UpdateColumnSelectionColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(45, 51, 73) : Color.FromArgb(67, 160, 71);

        // --- Delete Button Column ---
        public static Color DeleteColumnColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(24, 30, 54) : Color.FromArgb(229, 115, 115); // muted red

        public static Color DeleteColumnSelectionColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(45, 51, 73) : Color.FromArgb(211, 47, 47);

        public static Color AddTradeButtonColor =>
    CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(90, 158, 159);

        // --- Update ---
        public static Color UpdateTradeButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.DarkGreen : Color.FromArgb(122, 158, 108);

        // --- Cancel ---
        public static Color CancelUpdateButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.Crimson : Color.FromArgb(204, 111, 114);

        // --- Clear Data ---
        public static Color ClearDataButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.IndianRed : Color.FromArgb(181, 101, 93);

        // --- Upload Screenshot ---
        public static Color UploadScreenshotButtonColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(30, 58, 95) : Color.FromArgb(112, 136, 169);

        // --- Common text color ---
        public static Color ActionButtonTextColor =>
            Color.White;

        public static Color ChartFigureBackground =>
    CurrentTheme == AppTheme.Dark ? Color.FromArgb(27, 38, 59) : Color.FromArgb(216, 211, 204);

        public static Color ChartDataBackground =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(27, 38, 59) : Color.FromArgb(224, 219, 213);

        public static Color ChartGridColor =>
            CurrentTheme == AppTheme.Dark ? Color.FromArgb(45, 51, 73) : Color.FromArgb(169, 159, 148);

        public static Color ChartAxisLabelColor =>
            CurrentTheme == AppTheme.Dark ? Color.White : Color.FromArgb(42, 42, 42);

        public static Color ChartTickLabelColor =>
            CurrentTheme == AppTheme.Dark ? Color.LightGray : Color.FromArgb(64, 64, 64);


        public static void SetTheme(AppTheme theme)
        {
            CurrentTheme = theme;
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        public static event EventHandler ThemeChanged;
    }
}

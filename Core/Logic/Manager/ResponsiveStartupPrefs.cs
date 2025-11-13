using Microsoft.Win32;
using System;

namespace TradingJournal.Core.Logic.Manager
{
    /// <summary>
    /// Centralized persistence for the *layout* startup state (Normal/Maximized)
    /// used by Home and UCs that implement IResponsiveChildForm.
    /// </summary>
    public static class ResponsiveStartupPrefs
    {
        private const string UXRoot = @"Software\TradingJournal\UX";
        private const string UXLayoutName = "StartupLayoutState"; // "Normal" | "Maximized"

        public static void SetDesired(FormWindowStateExtended state)
        {
            try
            {
                using var k = Registry.CurrentUser.CreateSubKey(UXRoot);
                var v = state == FormWindowStateExtended.Maximized ? "Maximized" : "Normal";
                k?.SetValue(UXLayoutName, v, RegistryValueKind.String);
            }
            catch { /* ignore */ }
        }

        public static FormWindowStateExtended GetDesired()
        {
            try
            {
                using var k = Registry.CurrentUser.CreateSubKey(UXRoot);
                var v = k?.GetValue(UXLayoutName, "Normal")?.ToString() ?? "Normal";
                return string.Equals(v, "Maximized", StringComparison.OrdinalIgnoreCase)
                    ? FormWindowStateExtended.Maximized
                    : FormWindowStateExtended.Normal;
            }
            catch
            {
                return FormWindowStateExtended.Normal;
            }
        }
    }
}

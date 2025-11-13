using System.Windows.Forms;

namespace TradingJournal.Core.Logic.Manager
{
    public static class ResponsiveState
    {
        public static FormWindowStateExtended GetFor(Control c)
        {
            var f = c?.FindForm();
            if (f != null && f.WindowState == FormWindowState.Maximized)
                return FormWindowStateExtended.Maximized;

            return FormWindowStateExtended.Normal;
        }
    }
}

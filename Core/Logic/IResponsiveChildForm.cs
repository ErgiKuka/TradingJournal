using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Core.Logic
{
    public enum FormWindowStateExtended
    {
        Normal,
        Maximized
    }

    public interface IResponsiveChildForm
    {
        void SetWindowState(TradingJournal.Core.Logic.FormWindowStateExtended newState);
    }
}


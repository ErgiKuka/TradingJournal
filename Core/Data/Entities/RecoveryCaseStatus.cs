using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Core.Data.Entities
{
    public enum RecoveryCaseStatus { Active = 0, Paused = 1, Closed = 2, WrittenOff = 3 }
    public enum RecoveryCaseType { HeldBag = 0, RealizedLoss = 1 }
}
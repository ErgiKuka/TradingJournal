using System;

namespace TradingJournal.Core.Data.Entities
{
    public class RecycleBinItem
    {
        public int Id { get; set; }

        // e.g. "Trade", "RecoveryCase", "RecoveryAllocation", "AccountTransaction"
        public string EntityType { get; set; } = "";
        public string? EntityKey { get; set; } // human-friendly key (e.g., "BTCUSDT 2025-01-01")
        public int? OriginalId { get; set; }   // original PK when it existed
        public string PayloadJson { get; set; } = ""; // snapshot of the data to restore
        public string? Note { get; set; }

        public DateTime DeletedUtc { get; set; }
        public DateTime? ExpiresUtc { get; set; }
    }
}

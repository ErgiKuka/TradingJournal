using System;
using System.Collections.Generic;
using System.Linq;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Logic.Services
{
    /// <summary>The facts about a position at the moment it closed, used to build a journal entry.</summary>
    public sealed class ClosedTrade
    {
        public string Symbol { get; set; } = string.Empty;
        public bool IsLong { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal ExitPrice { get; set; }
        public decimal Margin { get; set; }
        public decimal RealizedPnl { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }

        /// <summary>Exchange trade/order id. Used to de-duplicate auto-imports. Null = manual entry.</summary>
        public string? ExternalId { get; set; }

        public DateTime ClosedAtUtc { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Writes closed exchange trades into the journal (the Trades table) so the user doesn't re-enter them.
    ///
    /// De-duplication is by <see cref="ClosedTrade.ExternalId"/>: a trade already present (same id) is
    /// never journaled twice, which makes "reconcile on connect" safe to run repeatedly. Manual journal
    /// entries have a null ExternalId and are never touched by import.
    /// </summary>
    public class TradeJournalService
    {
        /// <summary>Journals one closed trade. Returns the new Trade, or null if it was already journaled.</summary>
        public Trade? JournalClosedTrade(ClosedTrade t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            using var db = new AppDbContext();

            if (!string.IsNullOrEmpty(t.ExternalId) && db.Trades.Any(x => x.ExternalId == t.ExternalId))
                return null; // already journaled

            var trade = Map(t);
            db.Trades.Add(trade);
            db.SaveChanges();
            return trade;
        }

        /// <summary>
        /// Reconcile: import a batch of closed trades pulled from the exchange, skipping any already
        /// journaled (by ExternalId) and any duplicates within the batch. Returns how many were added.
        /// </summary>
        public int ImportClosedTrades(IEnumerable<ClosedTrade> trades)
        {
            if (trades == null) return 0;

            using var db = new AppDbContext();

            // Preload existing ids once, then track batch ids in the same set.
            var seen = new HashSet<string>(
                db.Trades.Where(x => x.ExternalId != null).Select(x => x.ExternalId!));

            int added = 0;
            foreach (var t in trades)
            {
                if (!string.IsNullOrEmpty(t.ExternalId) && !seen.Add(t.ExternalId))
                    continue; // already in DB or duplicated earlier in this batch

                db.Trades.Add(Map(t));
                added++;
            }

            if (added > 0) db.SaveChanges();
            return added;
        }

        private static Trade Map(ClosedTrade t) => new Trade
        {
            Symbol = t.Symbol,
            TradeType = t.IsLong ? "Long" : "Short",
            EntryPrice = t.EntryPrice,
            ExitPrice = t.ExitPrice,
            StopLoss = t.StopLoss ?? 0m,
            TakeProfit = t.TakeProfit ?? 0m,
            Margin = t.Margin,
            ProfitLoss = t.RealizedPnl,
            ExternalId = t.ExternalId,
            Date = t.ClosedAtUtc == default ? DateTime.Now : t.ClosedAtUtc.ToLocalTime()
        };
    }
}
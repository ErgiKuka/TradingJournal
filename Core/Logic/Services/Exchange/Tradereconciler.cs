using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services.Exchange;

namespace TradingJournal.Core.Logic.Services
{
    public sealed class ReconcileResult
    {
        public int Added { get; init; }
        public string? Error { get; init; }
        public bool Ok => Error == null;

        public static ReconcileResult Fail(string error) => new() { Error = error };
        public static ReconcileResult Success(int added) => new() { Added = added };
    }

    /// <summary>
    /// Pulls closed trades from an exchange and journals the new ones (auto-journal).
    ///
    /// Safe to call repeatedly: de-duplication by ExternalId lives in <see cref="TradeJournalService"/>,
    /// so this only ever imports trades that aren't already in the journal. A per-platform watermark
    /// limits how far back each pull scans, but because dedup is the real guard, losing the watermark
    /// only causes a redundant pull — never a duplicate row.
    ///
    /// First run for a platform sets the watermark to "now" and imports nothing: auto-journal captures
    /// trades closed AFTER you switch it on, not your entire account history.
    /// </summary>
    public sealed class TradeReconciler
    {
        private readonly TradeJournalService _journal;

        public TradeReconciler(TradeJournalService? journal = null) => _journal = journal ?? new TradeJournalService();

        /// <param name="platformKey">Stable per-platform key, e.g. "Binance:3".</param>
        public async Task<ReconcileResult> ReconcileAsync(IExchangeClient client, string platformKey)
        {
            if (client == null) return ReconcileResult.Fail("Not connected.");
            if (string.IsNullOrWhiteSpace(platformKey)) return ReconcileResult.Fail("No platform selected.");

            var settings = SettingsManager.Load();
            Dictionary<string, string> marks = settings.AutoJournal.WatermarksUtc;

            // First run for this platform: start the clock now, import nothing retroactively.
            if (!marks.TryGetValue(platformKey, out var raw) ||
                !DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var since))
            {
                marks[platformKey] = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                settings.Save();
                return ReconcileResult.Success(0);
            }

            IReadOnlyList<ClosedTrade> closed;
            try
            {
                closed = await client.GetRecentClosedTradesAsync(since).ConfigureAwait(false);
            }
            catch (NotImplementedException)
            {
                return ReconcileResult.Fail("Auto-journal isn't available for this exchange yet.");
            }
            catch (Exception ex)
            {
                return ReconcileResult.Fail(ex.Message);
            }

            int added = _journal.ImportClosedTrades(closed);

            // Advance the watermark only if we actually saw newer closes (prevents idle backward drift).
            DateTime newest = since;
            foreach (var t in closed)
                if (t.ClosedAtUtc > newest) newest = t.ClosedAtUtc;

            if (newest > since)
            {
                // Store the newest close time as-is. A trade landing exactly on the boundary is re-pulled
                // next run (GetRecentClosedTradesAsync is inclusive) and dropped by dedup — no gap.
                marks[platformKey] = newest.ToString("o", CultureInfo.InvariantCulture);
                settings.Save();
            }

            return ReconcileResult.Success(added);
        }
    }
}
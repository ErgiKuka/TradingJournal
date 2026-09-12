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
    /// so this only ever imports trades that aren't already in the journal.
    ///
    /// First run for a platform sets the watermark to "now" and imports nothing: auto-journal captures
    /// trades closed AFTER you switch it on, not your entire account history.
    ///
    /// WATERMARK ADVANCE — the original version only moved the mark forward when the pull returned at
    /// least one trade, which deadlocks: exchanges cap how wide a single history query may be (Binance
    /// USDT-M: 7 days on /fapi/v1/userTrades), so once the gap exceeds the cap the pull returns nothing,
    /// the mark never moves, and the gap only grows. The mark now advances on every successful pull.
    ///
    /// That trade-off has its own sharp edge: an empty-but-successful pull caused by a BUG (rather than
    /// by genuinely having no trades) silently skips the whole window. <see cref="MaxSilentAdvance"/>
    /// bounds the damage, and every advance is traced so a skip is visible and reversible by editing
    /// the settings file.
    /// </summary>
    public sealed class TradeReconciler
    {
        /// <summary>
        /// Exchanges publish closed-position data with a short delay (Binance's own UI warns of 1–2
        /// minutes). Holding the mark this far behind "now" keeps a trade that closes during the pull
        /// inside the next window. Overlap is free — ExternalId dedup drops anything already journaled.
        /// </summary>
        private static readonly TimeSpan ReportingLag = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Largest gap the mark will close in one run when the pull found nothing. Catching up from a
        /// long gap therefore takes several runs, each re-examining a bounded window — so a bug that
        /// makes pulls come back empty costs you one window per run instead of every trade since the
        /// last successful import. When the pull DID find trades, the mark jumps straight to now:
        /// finding trades is evidence the query worked.
        /// </summary>
        private static readonly TimeSpan MaxSilentAdvance = TimeSpan.FromDays(3);

        private readonly TradeJournalService _journal;

        public TradeReconciler(TradeJournalService? journal = null) => _journal = journal ?? new TradeJournalService();

        /// <param name="platformKey">Stable per-platform key, e.g. "Binance:3".</param>
        public async Task<ReconcileResult> ReconcileAsync(IExchangeClient client, string platformKey)
        {
            if (client == null) return ReconcileResult.Fail("Not connected.");
            if (string.IsNullOrWhiteSpace(platformKey)) return ReconcileResult.Fail("No platform selected.");

            var settings = SettingsManager.Load();
            Dictionary<string, string> marks = settings.AutoJournal.WatermarksUtc;
            var now = DateTime.UtcNow;

            AutoJournalTrace.Write(
                $"--- reconcile key='{platformKey}' exchange='{client.Exchange}' " +
                $"knownKeys=[{string.Join(", ", marks.Keys)}]");

            // First run for this platform: start the clock now, import nothing retroactively.
            if (!marks.TryGetValue(platformKey, out var raw) ||
                !DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var since))
            {
                var seed = now - ReportingLag;
                marks[platformKey] = seed.ToString("o", CultureInfo.InvariantCulture);
                settings.Save();

                AutoJournalTrace.Write(
                    $"    FIRST RUN for '{platformKey}' (stored value was '{raw ?? "<missing>"}'). " +
                    $"Seeded mark={seed:o}. Nothing imported — this is expected only once per platform. " +
                    $"If you expected a backfill, this key does not match the one your trades were " +
                    $"recorded under; edit user_settings.json.");

                return ReconcileResult.Success(0);
            }

            since = since.ToUniversalTime();
            if (since > now) since = now - ReportingLag;   // clock moved back, or file hand-edited

            AutoJournalTrace.Write($"    mark={since:o}  gap={(now - since).TotalDays:F2}d");

            IReadOnlyList<ClosedTrade> closed;
            try
            {
                closed = await client.GetRecentClosedTradesAsync(since).ConfigureAwait(false);
            }
            catch (NotImplementedException)
            {
                AutoJournalTrace.Write("    ABORT: client does not implement GetRecentClosedTradesAsync. Mark unchanged.");
                return ReconcileResult.Fail("Auto-journal isn't available for this exchange yet.");
            }
            catch (Exception ex)
            {
                // Mark deliberately untouched: a failed pull covered nothing.
                AutoJournalTrace.Write($"    ABORT: {ex.GetType().Name}: {ex.Message}. Mark unchanged.");
                return ReconcileResult.Fail(ex.Message);
            }

            AutoJournalTrace.Write($"    client returned {closed.Count} closed trade(s)");
            foreach (var t in closed)
                AutoJournalTrace.Write(
                    $"      {t.ClosedAtUtc:yyyy-MM-dd HH:mm}  {t.Symbol,-12} {(t.IsLong ? "Long " : "Short")} " +
                    $"entry={t.EntryPrice} exit={t.ExitPrice} pnl={t.RealizedPnl} id={t.ExternalId}");

            int added = _journal.ImportClosedTrades(closed);
            AutoJournalTrace.Write($"    journaled {added} new, {closed.Count - added} were already present");

            DateTime advanced;
            if (closed.Count > 0)
            {
                // The query demonstrably worked — jump to now.
                advanced = now - ReportingLag;
                foreach (var t in closed)
                    if (t.ClosedAtUtc > advanced) advanced = t.ClosedAtUtc;   // tolerate clock skew
            }
            else
            {
                // Nothing found. Could be a genuinely quiet period, could be a bug. Step forward by a
                // bounded amount so a bug cannot swallow months in a single run.
                advanced = since + MaxSilentAdvance;
                var ceiling = now - ReportingLag;
                if (advanced > ceiling) advanced = ceiling;
            }

            if (advanced > since)
            {
                marks[platformKey] = advanced.ToString("o", CultureInfo.InvariantCulture);
                settings.Save();
                AutoJournalTrace.Write($"    mark advanced {since:o} -> {advanced:o}");
            }
            else
            {
                AutoJournalTrace.Write("    mark unchanged");
            }

            return ReconcileResult.Success(added);
        }
    }
}
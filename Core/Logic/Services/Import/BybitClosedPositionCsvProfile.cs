using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TradingJournal.Core.Logic.Services
{
    /// <summary>
    /// Bybit UTA "Closed Position" export for perpetuals.
    ///
    /// Header row:
    ///   Market, Quantity, Direction, avgEntryPrice, avgExitPrice, Realized P&amp;L,
    ///   Opening Fee, Closing Fee, Funding Fee, cumOpenTradeFeeInfo, cumClosedTradeFeeInfo,
    ///   Open Time, Open Duration, Close Time
    ///
    /// Semantics verified against a real 28-row export:
    ///  * "Direction" is the POSITION side (Long/Short), not the closing order side. This is the
    ///    opposite convention to /v5/position/closed-pnl, where `side` is the closing order and
    ///    BybitFuturesClient therefore maps Sell -> Long. Do NOT copy that inversion here.
    ///  * "Realized P&amp;L" = gross PnL - opening fee - closing fee - funding fee. It is already
    ///    all-in net, so it maps straight onto ProfitLoss; do not subtract fees again.
    ///  * Timestamps are "HH:mm yyyy-MM-dd" with MINUTE resolution and carry no UTC offset.
    ///    Bybit renders them in the account's display timezone, so the offset must be supplied.
    /// </summary>
    public sealed class BybitClosedPositionCsvProfile : IClosedTradeCsvProfile
    {
        public const string ProfileName = "Bybit closed positions (perpetuals)";

        private static readonly string[] RequiredHeaders =
        {
            "Market", "Quantity", "Direction", "avgEntryPrice", "avgExitPrice", "Realized P&L", "Close Time"
        };

        private static readonly string[] TimeFormats =
        {
            "HH:mm yyyy-MM-dd",
            "H:mm yyyy-MM-dd",
            "HH:mm:ss yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm"
        };

        private readonly TimeSpan _sourceUtcOffset;

        /// <param name="sourceUtcOffset">
        /// UTC offset the export was rendered in, taken from Bybit's timezone setting.
        /// Default <see cref="TimeSpan.Zero"/> treats the file as UTC. Getting this wrong shifts
        /// every journal entry by a fixed amount and silently moves trades between daily PnL
        /// buckets, so it is an explicit parameter rather than a hidden assumption.
        /// </param>
        public BybitClosedPositionCsvProfile(TimeSpan sourceUtcOffset = default)
        {
            _sourceUtcOffset = sourceUtcOffset;
        }

        public string Name => ProfileName;

        public bool Matches(IReadOnlyList<string> headers)
        {
            if (headers == null || headers.Count == 0) return false;
            return RequiredHeaders.All(h => CsvText.IndexOfHeader(headers, h) >= 0);
        }

        public ClosedTradeParseResult Parse(IReadOnlyList<string> headers, IReadOnlyList<string[]> dataRows)
        {
            int iSymbol = CsvText.IndexOfHeader(headers, "Market");
            int iQty = CsvText.IndexOfHeader(headers, "Quantity");
            int iSide = CsvText.IndexOfHeader(headers, "Direction");
            int iEntry = CsvText.IndexOfHeader(headers, "avgEntryPrice");
            int iExit = CsvText.IndexOfHeader(headers, "avgExitPrice");
            int iPnl = CsvText.IndexOfHeader(headers, "Realized P&L");
            int iClose = CsvText.IndexOfHeader(headers, "Close Time");
            int iOpen = CsvText.IndexOfHeader(headers, "Open Time"); // optional; sharpens the dedup key

            var trades = new List<ClosedTrade>(dataRows.Count);
            var errors = new List<CsvRowError>();

            for (int r = 0; r < dataRows.Count; r++)
            {
                int line = r + 2; // 1-based; header is line 1
                var row = dataRows[r];

                // Padding rows from a worksheet read are not user errors.
                if (row.Length == 0 || row.All(string.IsNullOrWhiteSpace)) continue;

                var symbol = CsvText.Field(row, iSymbol);
                if (symbol.Length == 0) { errors.Add(new CsvRowError(line, "Market is empty.")); continue; }

                var qty = DecimalText.ParseOrNull(CsvText.Field(row, iQty));
                if (qty is not > 0m) { errors.Add(new CsvRowError(line, "Quantity is missing or not positive.")); continue; }

                var entry = DecimalText.ParseOrNull(CsvText.Field(row, iEntry));
                if (entry is not > 0m) { errors.Add(new CsvRowError(line, "avgEntryPrice is missing or not positive.")); continue; }

                var exit = DecimalText.ParseOrNull(CsvText.Field(row, iExit));
                if (exit is not > 0m) { errors.Add(new CsvRowError(line, "avgExitPrice is missing or not positive.")); continue; }

                var pnl = DecimalText.ParseOrNull(CsvText.Field(row, iPnl));
                if (pnl is null) { errors.Add(new CsvRowError(line, "Realized P&L is not a number.")); continue; }

                var direction = CsvText.Field(row, iSide);
                bool isLong;
                if (direction.Equals("Long", StringComparison.OrdinalIgnoreCase)) isLong = true;
                else if (direction.Equals("Short", StringComparison.OrdinalIgnoreCase)) isLong = false;
                else { errors.Add(new CsvRowError(line, $"Direction '{direction}' is not Long or Short.")); continue; }

                var closeRaw = CsvText.Field(row, iClose);
                if (!TryParseTimestamp(closeRaw, out var closeUtc))
                { errors.Add(new CsvRowError(line, $"Close Time '{closeRaw}' is not a recognised timestamp.")); continue; }

                DateTime? openUtc = TryParseTimestamp(CsvText.Field(row, iOpen), out var o) ? o : null;

                trades.Add(new ClosedTrade
                {
                    Symbol = symbol,
                    IsLong = isLong,
                    EntryPrice = entry.Value,
                    ExitPrice = exit.Value,

                    // The export has no leverage column, so true initial margin cannot be recovered.
                    // This stores entry NOTIONAL, matching ClosedTradeBuilder.Emit. Note that the
                    // API path (BybitFuturesClient) stores real margin, so any metric dividing
                    // ProfitLoss by Margin is not comparable across the two sources.
                    Margin = entry.Value * qty.Value,

                    RealizedPnl = pnl.Value,
                    ExternalId = BuildExternalId(symbol, openUtc, closeUtc, qty.Value, entry.Value, exit.Value),
                    ClosedAtUtc = closeUtc
                });
            }

            trades.Sort((a, b) => a.ClosedAtUtc.CompareTo(b.ClosedAtUtc));
            return new ClosedTradeParseResult { Trades = trades, Errors = errors };
        }

        private bool TryParseTimestamp(string raw, out DateTime utc)
        {
            utc = default;
            if (string.IsNullOrWhiteSpace(raw)) return false;

            if (!DateTime.TryParseExact(raw, TimeFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var local) &&
                !DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out local))
                return false;

            utc = DateTime.SpecifyKind(local - _sourceUtcOffset, DateTimeKind.Utc);
            return true;
        }

        /// <summary>
        /// Deterministic stand-in for an exchange order id, which this export does not contain, so
        /// re-importing the same file is a no-op. Two caveats:
        ///  * Timestamps have minute resolution, so two identical positions on the same symbol
        ///    opened and closed within the same minute collide and the second is dropped.
        ///  * The prefix differs from the API path's "BYBIT:{symbol}:{orderId}:{ms}", so importing
        ///    a CSV covering a period already auto-imported over the API WILL duplicate those trades.
        /// </summary>
        private static string BuildExternalId(
            string symbol, DateTime? openUtc, DateTime closeUtc, decimal qty, decimal entry, decimal exit)
        {
            string open = openUtc?.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture) ?? "-";
            string close = closeUtc.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture);

            return $"BYBIT-CSV:{symbol}:{open}:{close}:{DecimalText.Normalize(qty)}:" +
                   $"{DecimalText.Normalize(entry)}:{DecimalText.Normalize(exit)}";
        }
    }
}
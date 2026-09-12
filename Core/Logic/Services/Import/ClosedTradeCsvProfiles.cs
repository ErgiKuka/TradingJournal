using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TradingJournal.Core.Logic.Services
{
    /// <summary>
    /// RFC 4180-style CSV reader. Unlike a per-line split, this survives fields that contain
    /// embedded newlines inside quotes (exchange exports do this in JSON-ish fee columns).
    /// </summary>
    public static class CsvText
    {
        /// <summary>Parses the whole document into records. Blank lines are dropped.</summary>
        public static List<string[]> ReadRecords(string text)
        {
            var records = new List<string[]>();
            if (string.IsNullOrEmpty(text)) return records;

            var fields = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            bool rowHasContent = false;

            void EndRecord()
            {
                fields.Add(sb.ToString());
                sb.Clear();
                if (rowHasContent || fields.Count > 1) records.Add(fields.ToArray());
                fields.Clear();
                rowHasContent = false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];

                if (inQuotes)
                {
                    if (ch == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"') { sb.Append('"'); i++; }
                        else inQuotes = false;
                    }
                    else sb.Append(ch);
                    continue;
                }

                switch (ch)
                {
                    case '"': inQuotes = true; rowHasContent = true; break;
                    case ',': fields.Add(sb.ToString()); sb.Clear(); rowHasContent = true; break;
                    case '\r': break;
                    case '\n': EndRecord(); break;
                    default: sb.Append(ch); rowHasContent = true; break;
                }
            }

            if (sb.Length > 0 || fields.Count > 0) EndRecord();
            return records;
        }

        /// <summary>Trims whitespace and a leading UTF-8 BOM that survived encoding detection.</summary>
        public static string CleanHeader(string raw) => raw.Trim().TrimStart('\uFEFF');

        /// <summary>Index of <paramref name="name"/> in <paramref name="headers"/>, or -1.</summary>
        public static int IndexOfHeader(IReadOnlyList<string> headers, string name)
        {
            for (int i = 0; i < headers.Count; i++)
                if (CleanHeader(headers[i]).Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            return -1;
        }

        /// <summary>Safe field access: out-of-range columns read as empty instead of throwing.</summary>
        public static string Field(string[] row, int index) =>
            index >= 0 && index < row.Length ? row[index].Trim() : string.Empty;
    }

    /// <summary>A row that could not be turned into a <see cref="ClosedTrade"/>.</summary>
    public sealed record CsvRowError(int LineNumber, string Message);

    public sealed class ClosedTradeParseResult
    {
        public IReadOnlyList<ClosedTrade> Trades { get; init; } = Array.Empty<ClosedTrade>();
        public IReadOnlyList<CsvRowError> Errors { get; init; } = Array.Empty<CsvRowError>();
    }

    /// <summary>
    /// Maps one exchange's closed-position CSV export onto <see cref="ClosedTrade"/>.
    ///
    /// Profiles are deliberately not responsible for persistence: they produce ClosedTrade
    /// instances and hand them to <see cref="TradeJournalService.ImportClosedTrades"/>, which
    /// already owns de-duplication by ExternalId. Adding an exchange means adding one class here,
    /// not touching ImportService.
    /// </summary>
    public interface IClosedTradeCsvProfile
    {
        /// <summary>Human-readable name shown in the import dialog.</summary>
        string Name { get; }

        /// <summary>True when this profile recognises the header row.</summary>
        bool Matches(IReadOnlyList<string> headers);

        /// <summary>Parses data rows (header row excluded). Bad rows are reported, never thrown.</summary>
        ClosedTradeParseResult Parse(IReadOnlyList<string> headers, IReadOnlyList<string[]> dataRows);
    }

    public static class ClosedTradeCsvProfiles
    {
        // Order matters only if two profiles could match the same headers; keep signatures distinct.
        private static readonly IClosedTradeCsvProfile[] All =
        {
            new BybitClosedPositionCsvProfile()
        };

        /// <summary>Returns the profile that recognises these headers, or null for the native schema.</summary>
        public static IClosedTradeCsvProfile? Resolve(IReadOnlyList<string> headers) =>
            All.FirstOrDefault(p => p.Matches(headers));
    }

    internal static class DecimalText
    {
        /// <summary>Invariant-first decimal parse. Returns null rather than throwing.</summary>
        public static decimal? ParseOrNull(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            s = s.Trim();
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return d;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out d)) return d;
            return null;
        }

        /// <summary>
        /// Scale-independent rendering, so "30" and "30.000000000" produce the same text.
        /// Used for synthetic de-duplication keys, which must be stable across re-exports.
        /// </summary>
        public static string Normalize(decimal d) =>
            d.ToString("0.############################", CultureInfo.InvariantCulture);
    }
}
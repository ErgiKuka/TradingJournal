using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Logic.Services
{
    public enum ImportFormat { Excel, Csv }
    public enum ImportTarget { Journal, Recovery }

    public sealed class ImportRequest
    {
        public ImportTarget Target { get; set; }
        public ImportFormat Format { get; set; }
        public string FilePath { get; set; } = "";
    }

    public sealed class ImportValidationResult
    {
        public bool IsValid { get; init; }
        public string? Message { get; init; }
        public DataTable? Preview { get; init; }
    }

    public interface IImportService
    {
        Task<ImportValidationResult> ValidateAsync(ImportRequest req, CancellationToken ct = default);
        Task<(bool Success, string? Message, int RowsAffected)> ImportAsync(ImportRequest req, CancellationToken ct = default);
    }

    /// <summary>
    /// Reads an import file into a uniform "records" shape (row 0 = headers, every row the same
    /// width) and then applies one set of validation/import rules regardless of whether the bytes
    /// came from CSV or XLSX. Only the reader differs per format; the rules do not.
    ///
    /// Journal imports are resolved against <see cref="ClosedTradeCsvProfiles"/> first, so a raw
    /// exchange export (e.g. a Bybit closed-position file) is recognised by its own headers and
    /// mapped to <see cref="ClosedTrade"/>. Persistence and de-duplication are then delegated to
    /// <see cref="TradeJournalService"/>, which already owns the ExternalId dedup rules — this
    /// service deliberately does not open its own insert path for that case.
    /// </summary>
    public sealed class ImportService : IImportService
    {
        private const int PreviewRowLimit = 500;

        private static readonly string[] ExcelExtensions = { ".xlsx", ".xlsm", ".xltx", ".xltm" };
        private static readonly string[] CsvExtensions = { ".csv", ".txt" };

        // ---------------------------------------------------------------- entry points

        public async Task<ImportValidationResult> ValidateAsync(ImportRequest req, CancellationToken ct = default)
        {
            if (!File.Exists(req.FilePath))
                return new ImportValidationResult { IsValid = false, Message = "File not found." };

            var mismatch = DescribeFormatMismatch(req);
            if (mismatch != null)
                return new ImportValidationResult { IsValid = false, Message = mismatch };

            try
            {
                var records = await ReadRecordsAsync(req, ct).ConfigureAwait(false);
                return ValidateRecords(req.Target, records);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                return new ImportValidationResult { IsValid = false, Message = ex.Message };
            }
        }

        public async Task<(bool Success, string? Message, int RowsAffected)> ImportAsync(ImportRequest req, CancellationToken ct = default)
        {
            if (!File.Exists(req.FilePath))
                return (false, "File not found.", 0);

            var mismatch = DescribeFormatMismatch(req);
            if (mismatch != null)
                return (false, mismatch, 0);

            try
            {
                var records = await ReadRecordsAsync(req, ct).ConfigureAwait(false);
                return await ImportRecordsAsync(req.Target, records, ct).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                return (false, ex.Message, 0);
            }
        }

        /// <summary>
        /// Guards against the UI and the file disagreeing. Without this, an Excel-flagged .csv
        /// reaches ClosedXML and the user sees a raw library exception instead of a useful message.
        /// </summary>
        private static string? DescribeFormatMismatch(ImportRequest req)
        {
            var ext = Path.GetExtension(req.FilePath).ToLowerInvariant();

            if (req.Format == ImportFormat.Excel && CsvExtensions.Contains(ext))
                return $"'{ext}' is a text file — choose the CSV format instead of Excel.";

            if (req.Format == ImportFormat.Csv && ExcelExtensions.Contains(ext))
                return $"'{ext}' is a workbook — choose the Excel format instead of CSV.";

            return null;
        }

        // ---------------------------------------------------------------- readers

        private static Task<List<string[]>> ReadRecordsAsync(ImportRequest req, CancellationToken ct) => req.Format switch
        {
            ImportFormat.Csv => ReadCsvRecordsAsync(req.FilePath, ct),
            ImportFormat.Excel => Task.Run(() => ReadWorkbookRecords(req.FilePath), ct),
            _ => throw new NotSupportedException("Unsupported format.")
        };

        private static async Task<List<string[]>> ReadCsvRecordsAsync(string path, CancellationToken ct)
        {
            var text = await File.ReadAllTextAsync(path, ct).ConfigureAwait(false);
            return CsvText.ReadRecords(text);
        }

        /// <summary>
        /// Reads the first worksheet as a rectangular block. Column count comes from
        /// LastColumnUsed rather than from Row(1).CellsUsed(): CellsUsed() skips empty cells, so a
        /// blank header in the middle of the row would silently shift every column after it.
        /// </summary>
        private static List<string[]> ReadWorkbookRecords(string path)
        {
            using var wb = new XLWorkbook(path);

            var ws = wb.Worksheets.FirstOrDefault()
                     ?? throw new InvalidOperationException("Workbook contains no worksheets.");

            var lastRow = ws.LastRowUsed();
            var lastCol = ws.LastColumnUsed();
            if (lastRow is null || lastCol is null) return new List<string[]>();

            int rowCount = lastRow.RowNumber();
            int colCount = lastCol.ColumnNumber();

            var records = new List<string[]>(rowCount);
            for (int r = 1; r <= rowCount; r++)
            {
                var row = new string[colCount];
                for (int c = 1; c <= colCount; c++)
                    row[c - 1] = ws.Cell(r, c).GetValue<string>().Trim();
                records.Add(row);
            }
            return records;
        }

        // ---------------------------------------------------------------- validation

        private static ImportValidationResult ValidateRecords(ImportTarget target, List<string[]> records)
        {
            if (records.Count == 0)
                return new ImportValidationResult { IsValid = false, Message = "Empty file." };

            var headers = records[0].Select(CsvText.CleanHeader).ToArray();
            if (headers.Length == 0 || headers.All(string.IsNullOrWhiteSpace))
                return new ImportValidationResult { IsValid = false, Message = "Missing header row." };

            var preview = BuildPreview(headers, records, PreviewRowLimit);

            return target == ImportTarget.Journal
                ? ValidateJournal(headers, records, preview)
                : ValidateRecovery(headers, preview);
        }

        private static ImportValidationResult ValidateJournal(string[] headers, List<string[]> records, DataTable preview)
        {
            var profile = ClosedTradeCsvProfiles.Resolve(headers);
            if (profile != null)
            {
                var parsed = profile.Parse(headers, records.Skip(1).ToList());

                var message = $"Detected: {profile.Name}. {parsed.Trades.Count} closed position(s) ready to import.";
                if (parsed.Errors.Count > 0)
                {
                    message += $"{Environment.NewLine}{parsed.Errors.Count} row(s) will be skipped:{Environment.NewLine}" +
                               string.Join(Environment.NewLine,
                                   parsed.Errors.Take(5).Select(e => $"  line {e.LineNumber}: {e.Message}"));
                    if (parsed.Errors.Count > 5)
                        message += $"{Environment.NewLine}  … and {parsed.Errors.Count - 5} more.";
                }

                return new ImportValidationResult
                {
                    IsValid = parsed.Trades.Count > 0,
                    Message = message,
                    Preview = preview
                };
            }

            var cols = ToLowerSet(headers);
            string[] required = { "date", "symbol", "side", "entryprice", "margin" };
            bool ok = required.All(cols.Contains);

            string hint =
                "Unrecognised file. Expected either a supported exchange export, or the native journal " +
                "schema: Date, Symbol, Side (Long|Short), EntryPrice, Margin " +
                "(+ optional ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink).";

            return new ImportValidationResult
            {
                IsValid = ok,
                Message = ok ? "Validation OK." : hint,
                Preview = preview
            };
        }

        private static ImportValidationResult ValidateRecovery(string[] headers, DataTable preview)
        {
            var cols = ToLowerSet(headers);

            bool looksCases = cols.Contains("symbol") && cols.Contains("entrydate") && cols.Contains("entryprice") &&
                              (cols.Contains("investedusdt") || cols.Contains("quantity"));

            bool looksAlloc = (cols.Contains("caseref") || (cols.Contains("symbol") && cols.Contains("entrydate"))) &&
                              cols.Contains("tradedate") && cols.Contains("entryprice") &&
                              (cols.Contains("investedusdt") || cols.Contains("quantity"));

            if (looksCases)
                return new ImportValidationResult { IsValid = true, Message = "Detected Recovery CASES file. Validation OK.", Preview = preview };

            if (looksAlloc)
                return new ImportValidationResult { IsValid = true, Message = "Detected Recovery ALLOCATIONS file. Validation OK.", Preview = preview };

            return new ImportValidationResult
            {
                IsValid = false,
                Message = "Recovery file must be either:\n" +
                          "• CASES: Symbol, EntryDate, EntryPrice, (InvestedUSDT or Quantity), [CaseRef, Status]\n" +
                          "• ALLOCATIONS: (CaseRef or Symbol+EntryDate), TradeDate, EntryPrice, (InvestedUSDT or Quantity)",
                Preview = preview
            };
        }

        private static HashSet<string> ToLowerSet(IEnumerable<string> headers) =>
            headers.Select(h => h.Trim().ToLowerInvariant()).ToHashSet();

        private static DataTable BuildPreview(string[] headers, List<string[]> records, int maxRows)
        {
            var dt = new DataTable("Preview");

            // DataTable rejects duplicate or empty column names; exports occasionally contain both.
            var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int c = 0; c < headers.Length; c++)
            {
                var name = string.IsNullOrWhiteSpace(headers[c]) ? $"Column{c + 1}" : headers[c];
                var candidate = name;
                int suffix = 2;
                while (!used.Add(candidate)) candidate = $"{name} ({suffix++})";
                dt.Columns.Add(candidate);
            }

            int last = Math.Min(records.Count, maxRows + 1);
            for (int r = 1; r < last; r++)
            {
                var row = dt.NewRow();
                for (int c = 0; c < headers.Length; c++)
                    row[c] = CsvText.Field(records[r], c);
                dt.Rows.Add(row);
            }

            return dt;
        }

        // ---------------------------------------------------------------- import

        private static async Task<(bool Success, string? Message, int RowsAffected)> ImportRecordsAsync(
            ImportTarget target, List<string[]> records, CancellationToken ct)
        {
            if (records.Count == 0) return (false, "Empty file.", 0);

            var headers = records[0].Select(CsvText.CleanHeader).ToArray();
            var dataRows = records.Skip(1).ToList();

            return target == ImportTarget.Journal
                ? ImportJournal(headers, dataRows, ct)
                : await ImportRecoveryAsync(headers, dataRows, ct).ConfigureAwait(false);
        }

        private static (bool Success, string? Message, int RowsAffected) ImportJournal(
            string[] headers, List<string[]> dataRows, CancellationToken ct)
        {
            var profile = ClosedTradeCsvProfiles.Resolve(headers);
            if (profile != null)
            {
                var parsed = profile.Parse(headers, dataRows);
                if (parsed.Trades.Count == 0)
                    return (false, $"{profile.Name}: nothing importable ({parsed.Errors.Count} invalid row(s)).", 0);

                // TradeJournalService owns ExternalId de-duplication for both live reconciliation
                // and file import, so re-importing the same export is a no-op.
                int imported = new TradeJournalService().ImportClosedTrades(parsed.Trades);
                int duplicates = parsed.Trades.Count - imported;

                return (true,
                    $"{profile.Name}: imported {imported}, skipped {duplicates} already journaled, " +
                    $"{parsed.Errors.Count} invalid row(s).",
                    imported);
            }

            int H(string name) => Array.FindIndex(headers, h => h.Equals(name, StringComparison.OrdinalIgnoreCase));

            int iDate = H("Date"), iSym = H("Symbol"), iSide = H("Side"),
                iEntry = H("EntryPrice"), iExit = H("ExitPrice"), iSL = H("StopLoss"),
                iTP = H("TakeProfit"), iMargin = H("Margin"), iPnl = H("ProfitLoss"), iLink = H("ScreenshotLink");

            if (iDate < 0 || iSym < 0 || iSide < 0 || iEntry < 0 || iMargin < 0)
                return (false,
                    "Expected: Date, Symbol, Side (Long|Short), EntryPrice, Margin " +
                    "(+ optional ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink).", 0);

            using var db = new AppDbContext();

            int added = 0, skipped = 0;
            foreach (var row in dataRows)
            {
                ct.ThrowIfCancellationRequested();

                if (!DateTime.TryParse(CsvText.Field(row, iDate), CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                    !DateTime.TryParse(CsvText.Field(row, iDate), out date))
                {
                    skipped++;
                    continue;
                }

                var symbol = CsvText.Field(row, iSym);
                if (symbol.Length == 0) { skipped++; continue; }

                db.Trades.Add(new Trade
                {
                    Date = date,
                    Symbol = symbol,
                    TradeType = CsvText.Field(row, iSide),
                    EntryPrice = DecimalText.ParseOrNull(CsvText.Field(row, iEntry)) ?? 0m,
                    ExitPrice = DecimalText.ParseOrNull(CsvText.Field(row, iExit)) ?? 0m,
                    StopLoss = DecimalText.ParseOrNull(CsvText.Field(row, iSL)) ?? 0m,
                    TakeProfit = DecimalText.ParseOrNull(CsvText.Field(row, iTP)) ?? 0m,
                    Margin = DecimalText.ParseOrNull(CsvText.Field(row, iMargin)) ?? 0m,
                    ProfitLoss = DecimalText.ParseOrNull(CsvText.Field(row, iPnl)) ?? 0m,
                    ScreenshotLink = iLink >= 0 && CsvText.Field(row, iLink).Length > 0 ? CsvText.Field(row, iLink) : null
                });
                added++;
            }

            db.SaveChanges();

            var message = skipped == 0
                ? $"Imported {added} row(s)."
                : $"Imported {added} row(s), skipped {skipped} unparseable row(s).";

            return (true, message, added);
        }

        private static async Task<(bool Success, string? Message, int RowsAffected)> ImportRecoveryAsync(
            string[] headers, List<string[]> dataRows, CancellationToken ct)
        {
            int H(string name) => Array.FindIndex(headers, h => h.Equals(name, StringComparison.OrdinalIgnoreCase));

            bool cases = H("Symbol") >= 0 && H("EntryDate") >= 0 && H("EntryPrice") >= 0 &&
                         (H("InvestedUSDT") >= 0 || H("Quantity") >= 0);

            bool alloc = (H("CaseRef") >= 0 || (H("Symbol") >= 0 && H("EntryDate") >= 0)) &&
                         H("TradeDate") >= 0 && H("EntryPrice") >= 0 &&
                         (H("InvestedUSDT") >= 0 || H("Quantity") >= 0);

            if (!cases && !alloc)
                return (false, "Cannot detect Recovery CASES or ALLOCATIONS columns.", 0);

            using var db = new AppDbContext();
            int affected = 0, skipped = 0;

            if (cases)
            {
                int iSym = H("Symbol"), iDate = H("EntryDate"), iEntry = H("EntryPrice"),
                    iInv = H("InvestedUSDT"), iQty = H("Quantity"), iRef = H("CaseRef"), iStatus = H("Status");

                foreach (var row in dataRows)
                {
                    ct.ThrowIfCancellationRequested();

                    var sym = CsvText.Field(row, iSym).Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase);
                    if (sym.Length == 0) { skipped++; continue; }

                    if (!TryParseDateLoose(CsvText.Field(row, iDate), out var entryDate)) { skipped++; continue; }

                    var entry = DecimalText.ParseOrNull(CsvText.Field(row, iEntry)) ?? 0m;
                    var invested = DecimalText.ParseOrNull(CsvText.Field(row, iInv));
                    var qty = DecimalText.ParseOrNull(CsvText.Field(row, iQty));
                    var caseRef = iRef >= 0 ? CsvText.Field(row, iRef) : null;
                    var status = iStatus >= 0 ? CsvText.Field(row, iStatus) : null;

                    var q = ComputeQty(entry, invested, qty);
                    var existing = FindCase(db, caseRef, sym, entryDate);

                    if (existing == null)
                    {
                        db.RecoveryCases.Add(new RecoveryCase
                        {
                            CaseRef = string.IsNullOrWhiteSpace(caseRef) ? null : caseRef,
                            Symbol = sym,
                            EntryDate = entryDate,
                            EntryPrice = entry,
                            InvestedUSDT = invested,
                            Quantity = q == 0m ? (decimal?)null : q,
                            Status = Enum.TryParse<RecoveryCaseStatus>(status ?? "", true, out var st) ? st : RecoveryCaseStatus.Active
                        });
                    }
                    else
                    {
                        existing.EntryPrice = entry;
                        existing.InvestedUSDT = invested;
                        existing.Quantity = q == 0m ? existing.Quantity : q;
                        if (Enum.TryParse<RecoveryCaseStatus>(status ?? "", true, out var st))
                            existing.Status = st;
                    }

                    affected++;
                }
            }
            else
            {
                int iRef = H("CaseRef"), iSym = H("Symbol"), iDate = H("EntryDate"),
                    iTradeDate = H("TradeDate"), iEntry = H("EntryPrice"),
                    iInv = H("InvestedUSDT"), iQty = H("Quantity");

                foreach (var row in dataRows)
                {
                    ct.ThrowIfCancellationRequested();

                    string? caseRef = iRef >= 0 ? CsvText.Field(row, iRef) : null;
                    string? sym = iSym >= 0
                        ? CsvText.Field(row, iSym).Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase)
                        : null;

                    DateTime? caseEntryDate = iDate >= 0 && TryParseDateLoose(CsvText.Field(row, iDate), out var ced)
                        ? ced
                        : null;

                    var caseEnt = FindCase(db, caseRef, sym, caseEntryDate);
                    if (caseEnt == null) { skipped++; continue; }

                    if (!TryParseDateLoose(CsvText.Field(row, iTradeDate), out var tradeDate)) { skipped++; continue; }

                    var entry = DecimalText.ParseOrNull(CsvText.Field(row, iEntry)) ?? 0m;
                    var invested = DecimalText.ParseOrNull(CsvText.Field(row, iInv));
                    var qty = DecimalText.ParseOrNull(CsvText.Field(row, iQty));
                    var q = ComputeQty(entry, invested, qty);

                    db.RecoveryAllocations.Add(new RecoveryAllocation
                    {
                        RecoveryCaseId = caseEnt.Id,
                        TradeDate = tradeDate,
                        EntryPrice = entry,
                        InvestedUSDT = invested,
                        Quantity = q == 0m ? (decimal?)null : q
                    });

                    affected++;
                }
            }

            await db.SaveChangesAsync(ct).ConfigureAwait(false);

            var message = skipped == 0
                ? $"Imported {affected} row(s)."
                : $"Imported {affected} row(s), skipped {skipped} row(s) that could not be parsed or matched.";

            return (true, message, affected);
        }

        // ---------------------------------------------------------------- recovery helpers

        /// <summary>
        /// Non-throwing date parse. The previous version fell back to <c>DateTime.Parse</c> with the
        /// current culture, which threw on a single bad cell and aborted the whole import.
        /// </summary>
        private static bool TryParseDateLoose(string s, out DateTime date)
        {
            date = default;
            if (string.IsNullOrWhiteSpace(s)) return false;

            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d) ||
                DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.None, out d))
            {
                date = d.Date;
                return true;
            }
            return false;
        }

        private static decimal ComputeQty(decimal entryPrice, decimal? invested, decimal? qty)
        {
            if (qty.HasValue) return qty.Value;
            if (invested.HasValue && entryPrice > 0m) return invested.Value / entryPrice;
            return 0m;
        }

        private static RecoveryCase? FindCase(AppDbContext db, string? caseRef, string? symbol, DateTime? entryDate)
        {
            if (!string.IsNullOrWhiteSpace(caseRef))
                return db.RecoveryCases.FirstOrDefault(c => c.CaseRef == caseRef);

            if (!string.IsNullOrWhiteSpace(symbol) && entryDate.HasValue)
                return db.RecoveryCases.FirstOrDefault(c =>
                    c.Symbol == symbol && c.EntryDate == entryDate.Value.Date);

            return null;
        }
    }
}
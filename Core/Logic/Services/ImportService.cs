using ClosedXML.Excel;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

    public sealed class ImportService : IImportService
    {

        public async Task<ImportValidationResult> ValidateAsync(ImportRequest req, CancellationToken ct = default)
        {
            if (!File.Exists(req.FilePath))
                return new ImportValidationResult { IsValid = false, Message = "File not found." };

            return req.Format switch
            {
                ImportFormat.Csv => await ValidateCsvAsync(req, ct),
                ImportFormat.Excel => await ValidateExcelAsync(req, ct),
                _ => new ImportValidationResult { IsValid = false, Message = "Unsupported format." }
            };
        }

        public async Task<(bool Success, string? Message, int RowsAffected)> ImportAsync(ImportRequest req, CancellationToken ct = default)
        {
            if (!File.Exists(req.FilePath))
                return (false, "File not found.", 0);

            return req.Format switch
            {
                ImportFormat.Csv => await ImportCsvAsync(req, ct),
                ImportFormat.Excel => await ImportExcelAsync(req, ct),
                _ => (false, "Unsupported format.", 0)
            };
        }

        // ---------------- CSV ----------------

        private async Task<ImportValidationResult> ValidateCsvAsync(ImportRequest req, CancellationToken ct)
        {
            var (table, msg) = await LoadCsvPreviewAsync(req.FilePath, 500, ct);
            if (table == null) return new ImportValidationResult { IsValid = false, Message = msg };

            var cols = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName.Trim().ToLowerInvariant()).ToHashSet();

            if (req.Target == ImportTarget.Journal)
            {
                string[] required = { "date", "symbol", "side", "entryprice", "margin" };
                bool ok = required.All(cols.Contains);
                string hint = "Expected: Date, Symbol, Side (Long|Short), EntryPrice, Margin (+ optional ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink).";
                return new ImportValidationResult { IsValid = ok, Message = ok ? "Validation OK." : hint, Preview = table };
            }

            // Recovery: detect Cases vs Allocations and check minimal headers
            bool looksCases = cols.Contains("symbol") && cols.Contains("entrydate") && cols.Contains("entryprice") &&
                              (cols.Contains("investedusdt") || cols.Contains("quantity"));
            bool looksAlloc = (cols.Contains("caseref") || (cols.Contains("symbol") && cols.Contains("entrydate"))) &&
                              cols.Contains("tradedate") && cols.Contains("entryprice") &&
                              (cols.Contains("investedusdt") || cols.Contains("quantity"));

            if (looksCases)
                return new ImportValidationResult { IsValid = true, Message = "Detected Recovery CASES file. Validation OK.", Preview = table };
            if (looksAlloc)
                return new ImportValidationResult { IsValid = true, Message = "Detected Recovery ALLOCATIONS file. Validation OK.", Preview = table };

            return new ImportValidationResult
            {
                IsValid = false,
                Message = "Recovery CSV must be either:\n" +
                          "• CASES: Symbol, EntryDate, EntryPrice, (InvestedUSDT or Quantity), [CaseRef, Status]\n" +
                          "• ALLOCATIONS: (CaseRef or Symbol+EntryDate), TradeDate, EntryPrice, (InvestedUSDT or Quantity)",
                Preview = table
            };
        }

        private async Task<(bool Success, string? Message, int RowsAffected)> ImportCsvAsync(ImportRequest req, CancellationToken ct)
        {
            var lines = (await File.ReadAllLinesAsync(req.FilePath, ct)).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            if (lines.Length == 0) return (false, "Empty file.", 0);

            var headers = SplitCsvLine(lines[0]).Select(h => h.Trim()).ToArray();
            int H(string name) => Array.FindIndex(headers, h => h.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (req.Target == ImportTarget.Journal)
            {
                // same logic as before but Side = Long/Short expected now
                int iDate = H("Date"), iSym = H("Symbol"), iSide = H("Side"),
                    iEntry = H("EntryPrice"), iExit = H("ExitPrice"), iSL = H("StopLoss"),
                    iTP = H("TakeProfit"), iMargin = H("Margin"), iPnl = H("ProfitLoss"), iLink = H("ScreenshotLink");

                if (iDate < 0 || iSym < 0 || iSide < 0 || iEntry < 0 || iMargin < 0)
                    return (false, "Expected: Date, Symbol, Side (Long|Short), EntryPrice, Margin (+ optional ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink).", 0);

                int added = 0;
                using var db = new AppDbContext();
                for (int r = 1; r < lines.Length; r++)
                {
                    var row = SplitCsvLine(lines[r]);
                    if (!DateTime.TryParse(row[iDate], out var date)) continue;
                    var sym = row[iSym];
                    var side = row[iSide];
                    var entry = ParseDecOrNull(row[iEntry]) ?? 0m;
                    var exit = iExit >= 0 ? ParseDecOrNull(row[iExit]) ?? 0m : 0m;
                    var sl = iSL >= 0 ? ParseDecOrNull(row[iSL]) ?? 0m : 0m;
                    var tp = iTP >= 0 ? ParseDecOrNull(row[iTP]) ?? 0m : 0m;
                    var margin = ParseDecOrNull(row[iMargin]) ?? 0m;
                    var pnl = iPnl >= 0 ? ParseDecOrNull(row[iPnl]) ?? 0m : 0m;
                    var link = iLink >= 0 ? row[iLink] : null;

                    db.Trades.Add(new TradingJournal.Core.Data.Entities.Trade
                    {
                        Date = date,
                        Symbol = sym,
                        TradeType = side,
                        EntryPrice = entry,
                        ExitPrice = exit,
                        StopLoss = sl,
                        TakeProfit = tp,
                        Margin = margin,
                        ProfitLoss = pnl,
                        ScreenshotLink = string.IsNullOrWhiteSpace(link) ? null : link
                    });
                    added++;
                }
                await db.SaveChangesAsync(ct);
                return (true, null, added);
            }
            else // Recovery
            {
                // Detect CASES vs ALLOCATIONS by headers
                bool cases = H("Symbol") >= 0 && H("EntryDate") >= 0 && H("EntryPrice") >= 0 &&
                             (H("InvestedUSDT") >= 0 || H("Quantity") >= 0);

                bool alloc = (H("CaseRef") >= 0 || (H("Symbol") >= 0 && H("EntryDate") >= 0)) &&
                             H("TradeDate") >= 0 && H("EntryPrice") >= 0 &&
                             (H("InvestedUSDT") >= 0 || H("Quantity") >= 0);

                if (!cases && !alloc)
                    return (false, "Cannot detect Recovery CASES or ALLOCATIONS columns.", 0);

                using var db = new AppDbContext();
                int affected = 0;

                if (cases)
                {
                    int iSym = H("Symbol"), iDate = H("EntryDate"), iEntry = H("EntryPrice"),
                        iInv = H("InvestedUSDT"), iQty = H("Quantity"), iRef = H("CaseRef"), iStatus = H("Status");

                    for (int r = 1; r < lines.Length; r++)
                    {
                        var row = SplitCsvLine(lines[r]);

                        var sym = row[iSym].Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase);
                        var entryDate = ParseDateLoose(row[iDate]);
                        var entry = ParseDecOrNull(row[iEntry]) ?? 0m;
                        var invested = iInv >= 0 ? ParseDecOrNull(row[iInv]) : null;
                        var qty = iQty >= 0 ? ParseDecOrNull(row[iQty]) : null;
                        var caseRef = iRef >= 0 ? row[iRef] : null;
                        var status = iStatus >= 0 ? row[iStatus] : null;

                        var q = ComputeQty(entry, invested, qty);
                        var existing = FindCase(db, caseRef, sym, entryDate);

                        if (existing == null)
                        {
                            var c = new TradingJournal.Core.Data.Entities.RecoveryCase
                            {
                                CaseRef = string.IsNullOrWhiteSpace(caseRef) ? null : caseRef,
                                Symbol = sym,
                                EntryDate = entryDate,
                                EntryPrice = entry,
                                InvestedUSDT = invested,    // nullable
                                Quantity = q == 0m ? (decimal?)null : q, // store qty if we have something
                                Status = Enum.TryParse<RecoveryCaseStatus>(status ?? "", true, out var st) ? st : RecoveryCaseStatus.Active
                            };
                            db.RecoveryCases.Add(c);
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
                else // allocations
                {
                    int iRef = H("CaseRef"), iSym = H("Symbol"), iDate = H("EntryDate"),
                        iTradeDate = H("TradeDate"), iEntry = H("EntryPrice"),
                        iInv = H("InvestedUSDT"), iQty = H("Quantity");

                    for (int r = 1; r < lines.Length; r++)
                    {
                        var row = SplitCsvLine(lines[r]);

                        string? caseRef = iRef >= 0 ? row[iRef] : null;
                        string? sym = iSym >= 0 ? row[iSym]?.Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase) : null;
                        DateTime? caseEntryDate = iDate >= 0 ? ParseDateLoose(row[iDate]) : null;

                        var caseEnt = FindCase(db, caseRef, sym, caseEntryDate);
                        if (caseEnt == null) continue; // skip if not matchable

                        var tradeDate = ParseDateLoose(row[iTradeDate]);
                        var entry = ParseDecOrNull(row[iEntry]) ?? 0m;
                        var invested = iInv >= 0 ? ParseDecOrNull(row[iInv]) : null;
                        var qty = iQty >= 0 ? ParseDecOrNull(row[iQty]) : null;
                        var q = ComputeQty(entry, invested, qty);

                        var a = new TradingJournal.Core.Data.Entities.RecoveryAllocation
                        {
                            RecoveryCaseId = caseEnt.Id,
                            TradeDate = tradeDate,
                            EntryPrice = entry,
                            InvestedUSDT = invested,
                            Quantity = q == 0m ? (decimal?)null : q
                        };
                        db.RecoveryAllocations.Add(a);
                        affected++;
                    }
                }

                await db.SaveChangesAsync(ct);
                return (true, null, affected);
            }
        }

        private static async Task<(DataTable? Table, string? Message)> LoadCsvPreviewAsync(string path, int maxRows, CancellationToken ct)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(path, ct);
                if (lines.Length == 0) return (null, "Empty file.");
                var headers = SplitCsvLine(lines[0]);

                var dt = new DataTable("Preview");
                foreach (var h in headers) dt.Columns.Add(h.Trim());
                for (int i = 1; i < Math.Min(lines.Length, maxRows); i++)
                    dt.Rows.Add(SplitCsvLine(lines[i]));
                return (dt, "Validation OK.");
            }
            catch (Exception ex) { return (null, ex.Message); }
        }

        private static string[] SplitCsvLine(string line)
        {
            var list = new System.Collections.Generic.List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                var ch = line[i];
                if (ch == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"') { sb.Append('"'); i++; }
                    else inQuotes = !inQuotes;
                }
                else if (ch == ',' && !inQuotes) { list.Add(sb.ToString()); sb.Clear(); }
                else sb.Append(ch);
            }
            list.Add(sb.ToString());
            return list.ToArray();
        }

        private static bool Try(string[] arr, int idx, out string val)
        { if (idx >= 0 && idx < arr.Length) { val = arr[idx].Trim(); return true; } val = ""; return false; }

        // ---------------- Excel (ClosedXML) ----------------

        private async Task<ImportValidationResult> ValidateExcelAsync(ImportRequest req, CancellationToken ct)
        {
            try
            {
                using var wb = new XLWorkbook(req.FilePath);
                var ws = wb.Worksheets.FirstOrDefault();
                if (ws == null) return new ImportValidationResult { IsValid = false, Message = "Workbook is empty." };

                var dt = new DataTable("Preview");
                var headers = ws.Row(1).CellsUsed().Select(c => c.GetString().Trim()).ToArray();
                if (headers.Length == 0) return new ImportValidationResult { IsValid = false, Message = "Missing header row." };
                foreach (var h in headers) dt.Columns.Add(h);

                int max = Math.Min(ws.LastRowUsed().RowNumber(), 500);
                for (int r = 2; r <= max; r++)
                {
                    var row = new object?[headers.Length];
                    for (int c = 0; c < headers.Length; c++)
                        row[c] = ws.Cell(r, c + 1).GetValue<string>();
                    dt.Rows.Add(row);
                }

                var cols = headers.Select(h => h.Trim().ToLowerInvariant()).ToHashSet();

                if (req.Target == ImportTarget.Journal)
                {
                    string[] required = { "date", "symbol", "side", "entryprice", "margin" };
                    bool ok = required.All(cols.Contains);
                    string hint = "Expected: Date, Symbol, Side (Long|Short), EntryPrice, Margin (+ optional ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink).";
                    return new ImportValidationResult { IsValid = ok, Message = ok ? "Validation OK." : hint, Preview = dt };
                }

                bool looksCases = cols.Contains("symbol") && cols.Contains("entrydate") && cols.Contains("entryprice") &&
                                  (cols.Contains("investedusdt") || cols.Contains("quantity"));
                bool looksAlloc = (cols.Contains("caseref") || (cols.Contains("symbol") && cols.Contains("entrydate"))) &&
                                  cols.Contains("tradedate") && cols.Contains("entryprice") &&
                                  (cols.Contains("investedusdt") || cols.Contains("quantity"));

                if (looksCases)
                    return new ImportValidationResult { IsValid = true, Message = "Detected Recovery CASES sheet. Validation OK.", Preview = dt };
                if (looksAlloc)
                    return new ImportValidationResult { IsValid = true, Message = "Detected Recovery ALLOCATIONS sheet. Validation OK.", Preview = dt };

                return new ImportValidationResult
                {
                    IsValid = false,
                    Message = "Recovery Excel must be either:\n" +
                              "• CASES: Symbol, EntryDate, EntryPrice, (InvestedUSDT or Quantity), [CaseRef, Status]\n" +
                              "• ALLOCATIONS: (CaseRef or Symbol+EntryDate), TradeDate, EntryPrice, (InvestedUSDT or Quantity)",
                    Preview = dt
                };
            }
            catch (Exception ex) { return new ImportValidationResult { IsValid = false, Message = ex.Message }; }
        }

        private async Task<(bool Success, string? Message, int RowsAffected)> ImportExcelAsync(ImportRequest req, CancellationToken ct)
        {
            // Reuse the same logic by saving to a temp CSV-like in memory table and reusing the CSV path.
            // Or just read worksheet directly.
            try
            {
                using var wb = new XLWorkbook(req.FilePath);
                var ws = wb.Worksheets.FirstOrDefault();
                if (ws == null) return (false, "Workbook is empty.", 0);

                // Build a DataTable to iterate uniformly
                var headers = ws.Row(1).CellsUsed().Select(c => c.GetString().Trim()).ToArray();
                int last = ws.LastRowUsed().RowNumber();

                // Create a temporary CSV-like array
                var lines = new List<string>(last);
                lines.Add(string.Join(",", headers));
                for (int r = 2; r <= last; r++)
                {
                    var row = headers.Select((_, c) =>
                    {
                        var val = ws.Cell(r, c + 1).GetValue<string>();
                        // CSV-escape
                        return val.Contains('"') || val.Contains(',') || val.Contains('\n')
                            ? $"\"{val.Replace("\"", "\"\"")}\""
                            : val;
                    });
                    lines.Add(string.Join(",", row));
                }

                // Write to temp file and reuse CSV importer (keeps behavior consistent)
                var tmp = Path.GetTempFileName();
                await File.WriteAllLinesAsync(tmp, lines, ct);
                try { return await ImportCsvAsync(new ImportRequest { Target = req.Target, Format = ImportFormat.Csv, FilePath = tmp }, ct); }
                finally { try { File.Delete(tmp); } catch { } }
            }
            catch (Exception ex) { return (false, ex.Message, 0); }
        }

        // ---------- Recovery helpers ----------
        private static DateTime ParseDateLoose(string s)
        {
            // accept yyyy-MM-dd, M/d/yyyy, etc.
            return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
                ? d.Date
                : DateTime.Parse(s, CultureInfo.CurrentCulture).Date;
        }

        private static decimal? ParseDecOrNull(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (decimal.TryParse(s.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return d;
            if (decimal.TryParse(s.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out d)) return d;
            return null;
        }

        // compute quantity if not provided but invested+entry exist
        private static decimal ComputeQty(decimal entryPrice, decimal? invested, decimal? qty)
        {
            if (qty.HasValue) return qty.Value;
            if (invested.HasValue && entryPrice > 0m) return invested.Value / entryPrice;
            return 0m;
        }

        // Try resolve case: by CaseRef (string key) or by pair (Symbol, EntryDate)
        private static TradingJournal.Core.Data.Entities.RecoveryCase? FindCase(
            AppDbContext db, string? caseRef, string? symbol, DateTime? entryDate)
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

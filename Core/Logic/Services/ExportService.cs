using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Core.Logic.Services
{
    public enum ExportFormat { Excel, Csv, Pdf }

    public sealed class ExportRequest
    {
        public bool IncludeJournal { get; set; }
        public bool IncludeRecovery { get; set; }
        public bool IncludeStatistics { get; set; }
        public bool IncludeTransactions { get; set; }

        public bool UseDateRange { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        /// <summary>Add computed columns (Risk/Reward/RR if present on Trades)</summary>
        public bool IncludeComputed { get; set; }

        public ExportFormat Format { get; set; }

        /// <summary>
        /// CSV single-source → Save path; CSV multi-source → Folder; Excel/PDF → Save path
        /// </summary>
        public string OutputPath { get; set; } = "";
    }

    public sealed class ExportResult
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public List<string> CreatedPaths { get; init; } = new();
    }

    public interface IExportService
    {
        Task<ExportResult> ExportAsync(ExportRequest req, IProgress<int>? progress = null, CancellationToken ct = default);
    }

    public sealed class ExportService : IExportService
    {
        public async Task<ExportResult> ExportAsync(ExportRequest req, IProgress<int>? progress = null, CancellationToken ct = default)
        {
            if (!req.IncludeJournal && !req.IncludeRecovery && !req.IncludeStatistics && !req.IncludeTransactions)
                return new ExportResult { Success = false, Message = "No sources selected." };

            return req.Format switch
            {
                ExportFormat.Csv => await ExportCsvAsync(req, progress, ct),
                ExportFormat.Excel => await ExportExcelAsync(req, progress, ct),
                ExportFormat.Pdf => await ExportPdfAsync(req, progress, ct),
                _ => new ExportResult { Success = false, Message = "Unsupported format." }
            };
        }

        // ----------------- Helpers -----------------
        private static IQueryable<TradingJournal.Core.Data.Entities.Trade> ApplyDateFilter(
            IQueryable<TradingJournal.Core.Data.Entities.Trade> q,
            ExportRequest req)
        {
            if (req.UseDateRange)
            {
                if (req.From.HasValue) q = q.Where(t => t.Date >= req.From.Value.Date);
                if (req.To.HasValue) q = q.Where(t => t.Date < req.To.Value.Date.AddDays(1));
            }
            return q;
        }

        private static string CsvEscape(string s) =>
            string.IsNullOrEmpty(s) ? "" :
            (s.Contains(',') || s.Contains('"') || s.Contains('\n'))
                ? "\"" + s.Replace("\"", "\"\"") + "\""
                : s;

        // ---------------- CSV ----------------
        private async Task<ExportResult> ExportCsvAsync(ExportRequest req, IProgress<int>? progress, CancellationToken ct)
        {
            var created = new List<string>();
            var count =
                (req.IncludeJournal ? 1 : 0) +
                (req.IncludeRecovery ? 2 : 0) +   // cases + allocations
                (req.IncludeStatistics ? 1 : 0) +
                (req.IncludeTransactions ? 1 : 0);

            var multi = count > 1;

            string JournalPath() => multi ? Path.Combine(req.OutputPath, "journal.csv") : req.OutputPath;
            string RecoveryCasesPath() => multi ? Path.Combine(req.OutputPath, "recovery_cases.csv") : req.OutputPath;
            string RecoveryAllocPath() => multi ? Path.Combine(req.OutputPath, "recovery_allocations.csv") : req.OutputPath;
            string StatisticsPath() => multi ? Path.Combine(req.OutputPath, "statistics.csv") : req.OutputPath;
            string TransactionsPath() => multi ? Path.Combine(req.OutputPath, "transactions.csv") : req.OutputPath;

            if (multi) Directory.CreateDirectory(req.OutputPath);
            else Directory.CreateDirectory(Path.GetDirectoryName(req.OutputPath) ?? ".");

            int done = 0; void Bump() => progress?.Report((int)((++done * 100.0) / Math.Max(1, count)));

            using var db = new AppDbContext();

            // --- Journal ---
            List<dynamic> tradeRows = new();
            if (req.IncludeJournal || req.IncludeStatistics)
            {
                tradeRows = ApplyDateFilter(db.Trades.AsQueryable(), req)
                    .OrderByDescending(t => t.Date)
                    .Select(t => new
                    {
                        t.Date,
                        Symbol = t.Symbol ?? "",
                        Side = t.TradeType ?? "",
                        Entry = t.EntryPrice,
                        Exit = t.ExitPrice,
                        SL = t.StopLoss,
                        TP = t.TakeProfit,
                        Margin = t.Margin,
                        PnL = t.ProfitLoss,
                        Risk = (decimal?)t.Risk,
                        Reward = (decimal?)t.Reward,
                        RR = (decimal?)t.RR
                    }).ToList<dynamic>();
            }

            if (req.IncludeJournal)
            {
                var rows = new List<string>
                {
                    req.IncludeComputed
                        ? "Date,Symbol,Side,EntryPrice,ExitPrice,StopLoss,TakeProfit,Margin,ProfitLoss,Risk,Reward,RR"
                        : "Date,Symbol,Side,EntryPrice,ExitPrice,StopLoss,TakeProfit,Margin,ProfitLoss"
                };

                foreach (var t in tradeRows)
                {
                    var baseCols = new[]
                    {
                        ((DateTime)t.Date).ToString("yyyy-MM-dd"),
                        CsvEscape((string)t.Symbol),
                        CsvEscape((string)t.Side),
                        ((decimal)t.Entry).ToString(CultureInfo.InvariantCulture),
                        ((decimal)t.Exit).ToString(CultureInfo.InvariantCulture),
                        ((decimal)t.SL).ToString(CultureInfo.InvariantCulture),
                        ((decimal)t.TP).ToString(CultureInfo.InvariantCulture),
                        ((decimal)t.Margin).ToString(CultureInfo.InvariantCulture),
                        ((decimal)t.PnL).ToString(CultureInfo.InvariantCulture)
                    };

                    if (req.IncludeComputed)
                    {
                        rows.Add(string.Join(",", baseCols.Concat(new[]
                        {
                            ((t.Risk ?? 0m)).ToString(CultureInfo.InvariantCulture),
                            ((t.Reward ?? 0m)).ToString(CultureInfo.InvariantCulture),
                            ((t.RR ?? 0m)).ToString(CultureInfo.InvariantCulture)
                        })));
                    }
                    else
                    {
                        rows.Add(string.Join(",", baseCols));
                    }
                }

                await File.WriteAllLinesAsync(JournalPath(), rows, ct);
                created.Add(JournalPath()); Bump();
            }

            // --- Recovery ---
            if (req.IncludeRecovery)
            {
                // ---- Cases
                var cases = db.RecoveryCases
                    .Select(c => new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        c.Symbol,
                        c.EntryDate,
                        c.EntryPrice,
                        c.Quantity,
                        c.InvestedUSDT,
                        Status = c.Status.ToString(),
                        c.Notes
                    })
                    .OrderBy(c => c.Case)
                    .ToList();

                var rowsCases = new List<string> {
        "Case,Symbol,EntryDate,EntryPrice,Quantity,InvestedUSDT,Status,Notes"
    };
                rowsCases.AddRange(cases.Select(c =>
                    string.Join(",", new[]
                    {
            CsvEscape(c.Case),
            CsvEscape(c.Symbol ?? ""),
            c.EntryDate.ToString("yyyy-MM-dd"),
            c.EntryPrice.ToString(CultureInfo.InvariantCulture),
            (c.Quantity ?? 0m).ToString(CultureInfo.InvariantCulture),
            (c.InvestedUSDT ?? 0m).ToString(CultureInfo.InvariantCulture),
            CsvEscape(c.Status ?? ""),
            CsvEscape(c.Notes ?? "")
                    })));
                await File.WriteAllLinesAsync(RecoveryCasesPath(), rowsCases, ct);
                created.Add(RecoveryCasesPath()); Bump();

                // ---- Allocations
                var allocs = (
                    from a in db.RecoveryAllocations
                    join c in db.RecoveryCases on a.RecoveryCaseId equals c.Id
                    select new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        a.TradeDate,
                        a.Quantity,
                        a.EntryPrice,
                        a.InvestedUSDT,
                        a.Notes,
                        a.AllocatedAt
                    }
                )
                .OrderBy(x => x.Case).ThenBy(x => x.TradeDate)
                .ToList();

                var rowsAlloc = new List<string> { "Case,TradeDate,Quantity,EntryPrice,InvestedUSDT,Notes,AllocatedAt" };
                rowsAlloc.AddRange(allocs.Select(a =>
                    string.Join(",", new[]
                    {
            CsvEscape(a.Case),
            a.TradeDate.ToString("yyyy-MM-dd"),
            (a.Quantity ?? 0m).ToString(CultureInfo.InvariantCulture),
            a.EntryPrice.ToString(CultureInfo.InvariantCulture),
            (a.InvestedUSDT ?? 0m).ToString(CultureInfo.InvariantCulture),
            CsvEscape(a.Notes ?? ""),
            a.AllocatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                    })));
                await File.WriteAllLinesAsync(RecoveryAllocPath(), rowsAlloc, ct);
                created.Add(RecoveryAllocPath()); Bump();
            }

            // --- Statistics ---
            if (req.IncludeStatistics)
            {
                var cnt = tradeRows.Count;
                var wins = tradeRows.Count(t => ((decimal)t.PnL) > 0);
                var losses = tradeRows.Count(t => ((decimal)t.PnL) < 0);
                var totalPnL = tradeRows.Sum(t => (decimal)t.PnL);
                var avgPnL = cnt == 0 ? 0m : totalPnL / cnt;
                var winRate = cnt == 0 ? 0.0 : wins * 100.0 / cnt;

                var statsRows = new List<string> { "Metric,Value" };
                statsRows.Add($"TotalTrades,{cnt}");
                statsRows.Add($"Wins,{wins}");
                statsRows.Add($"Losses,{losses}");
                statsRows.Add($"WinRate,{winRate:0.0}%");
                statsRows.Add($"TotalPnL,{totalPnL.ToString(CultureInfo.InvariantCulture)}");
                statsRows.Add($"AvgPnL,{avgPnL.ToString(CultureInfo.InvariantCulture)}");

                await File.WriteAllLinesAsync(StatisticsPath(), statsRows, ct);
                created.Add(StatisticsPath()); Bump();
            }

            // --- Transactions ---
            if (req.IncludeTransactions)
            {
                var entries = AccountTransactionsService
                    .QuerySince(db, req.UseDateRange && req.From.HasValue ? req.From.Value : DateTime.MinValue);
                if (req.UseDateRange && req.To.HasValue)
                    entries = entries.Where(x => x.Date < req.To.Value.Date.AddDays(1));

                var trxRows = new List<string> { "Date,Type,Amount,Note" };
                foreach (var x in entries.OrderByDescending(x => x.Date))
                    trxRows.Add($"{x.Date:yyyy-MM-dd},{CsvEscape(x.Type.ToString())},{x.Amount.ToString(CultureInfo.InvariantCulture)},{CsvEscape(x.Note ?? "")}");

                await File.WriteAllLinesAsync(TransactionsPath(), trxRows, ct);
                created.Add(TransactionsPath()); Bump();
            }

            return new ExportResult { Success = true, CreatedPaths = created };
        }

        // ---------------- Excel ----------------
        private async Task<ExportResult> ExportExcelAsync(ExportRequest req, IProgress<int>? progress, CancellationToken ct)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(req.OutputPath) ?? ".");

            var created = new List<string>();
            using var db = new AppDbContext();
            using var wb = new XLWorkbook();

            var tasks =
                (req.IncludeJournal ? 1 : 0) +
                (req.IncludeRecovery ? 2 : 0) +
                (req.IncludeStatistics ? 1 : 0) +
                (req.IncludeTransactions ? 1 : 0);
            int done = 0; void Bump() => progress?.Report((int)((++done * 100.0) / Math.Max(1, tasks)));

            // Journal
            List<dynamic> tradeRows = new();
            if (req.IncludeJournal || req.IncludeStatistics)
            {
                tradeRows = ApplyDateFilter(db.Trades.AsQueryable(), req)
                    .OrderByDescending(t => t.Date)
                    .Select(t => new
                    {
                        t.Date,
                        Symbol = t.Symbol ?? "",
                        Side = t.TradeType ?? "",
                        Entry = t.EntryPrice,
                        Exit = t.ExitPrice,
                        SL = t.StopLoss,
                        TP = t.TakeProfit,
                        Margin = t.Margin,
                        PnL = t.ProfitLoss,
                        Risk = (decimal?)t.Risk,
                        Reward = (decimal?)t.Reward,
                        RR = (decimal?)t.RR
                    }).ToList<dynamic>();
            }

            if (req.IncludeJournal)
            {
                var ws = wb.Worksheets.Add("Journal");
                int c = 1;
                ws.Cell(1, c++).Value = "Date";
                ws.Cell(1, c++).Value = "Symbol";
                ws.Cell(1, c++).Value = "Side";
                ws.Cell(1, c++).Value = "EntryPrice";
                ws.Cell(1, c++).Value = "ExitPrice";
                ws.Cell(1, c++).Value = "StopLoss";
                ws.Cell(1, c++).Value = "TakeProfit";
                ws.Cell(1, c++).Value = "Margin";
                ws.Cell(1, c++).Value = "ProfitLoss";
                if (req.IncludeComputed)
                {
                    ws.Cell(1, c++).Value = "Risk";
                    ws.Cell(1, c++).Value = "Reward";
                    ws.Cell(1, c++).Value = "RR";
                }

                int r = 2;
                foreach (var t in tradeRows)
                {
                    int cc = 1;
                    ws.Cell(r, cc++).SetValue((DateTime)t.Date).Style.DateFormat.Format = "yyyy-mm-dd";
                    ws.Cell(r, cc++).Value = (string)t.Symbol;
                    ws.Cell(r, cc++).Value = (string)t.Side;
                    ws.Cell(r, cc++).Value = (decimal)t.Entry;
                    ws.Cell(r, cc++).Value = (decimal)t.Exit;
                    ws.Cell(r, cc++).Value = (decimal)t.SL;
                    ws.Cell(r, cc++).Value = (decimal)t.TP;
                    ws.Cell(r, cc++).Value = (decimal)t.Margin;
                    ws.Cell(r, cc++).Value = (decimal)t.PnL;
                    if (req.IncludeComputed)
                    {
                        ws.Cell(r, cc++).Value = (decimal)(t.Risk ?? 0m);
                        ws.Cell(r, cc++).Value = (decimal)(t.Reward ?? 0m);
                        ws.Cell(r, cc++).Value = (decimal)(t.RR ?? 0m);
                    }
                    r++;
                }
                ws.Columns().AdjustToContents();
                Bump();
            }

            // Recovery
            if (req.IncludeRecovery)
            {
                var cases = db.RecoveryCases
                    .Select(c => new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        c.Symbol,
                        c.EntryDate,
                        c.EntryPrice,
                        c.Quantity,
                        c.InvestedUSDT,
                        Status = c.Status.ToString(),
                        c.Notes
                    })
                    .OrderBy(c => c.Case)
                    .ToList();

                var ws1 = wb.Worksheets.Add("RecoveryCases");
                ws1.Cell(1, 1).Value = "Case";
                ws1.Cell(1, 2).Value = "Symbol";
                ws1.Cell(1, 3).Value = "EntryDate";
                ws1.Cell(1, 4).Value = "EntryPrice";
                ws1.Cell(1, 5).Value = "Quantity";
                ws1.Cell(1, 6).Value = "InvestedUSDT";
                ws1.Cell(1, 7).Value = "Status";
                ws1.Cell(1, 8).Value = "Notes";

                int r1 = 2;
                foreach (var c in cases)
                {
                    ws1.Cell(r1, 1).Value = c.Case;
                    ws1.Cell(r1, 2).Value = c.Symbol ?? "";
                    ws1.Cell(r1, 3).SetValue(c.EntryDate).Style.DateFormat.Format = "yyyy-mm-dd";
                    ws1.Cell(r1, 4).Value = c.EntryPrice;
                    ws1.Cell(r1, 5).Value = c.Quantity;
                    ws1.Cell(r1, 6).Value = c.InvestedUSDT;
                    ws1.Cell(r1, 7).Value = c.Status ?? "";
                    ws1.Cell(r1, 8).Value = c.Notes ?? "";
                    r1++;
                }
                ws1.Columns().AdjustToContents();
                Bump();

                var allocs = (
                    from a in db.RecoveryAllocations
                    join c in db.RecoveryCases on a.RecoveryCaseId equals c.Id
                    select new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        a.TradeDate,
                        a.Quantity,
                        a.EntryPrice,
                        a.InvestedUSDT,
                        a.Notes,
                        a.AllocatedAt
                    }
                )
                .OrderBy(x => x.Case).ThenBy(x => x.TradeDate)
                .ToList();

                var ws2 = wb.Worksheets.Add("RecoveryAllocations");
                ws2.Cell(1, 1).Value = "Case";
                ws2.Cell(1, 2).Value = "TradeDate";
                ws2.Cell(1, 3).Value = "Quantity";
                ws2.Cell(1, 4).Value = "EntryPrice";
                ws2.Cell(1, 5).Value = "InvestedUSDT";
                ws2.Cell(1, 6).Value = "Notes";
                ws2.Cell(1, 7).Value = "AllocatedAt";

                int r2 = 2;
                foreach (var a in allocs)
                {
                    ws2.Cell(r2, 1).Value = a.Case;
                    ws2.Cell(r2, 2).SetValue(a.TradeDate).Style.DateFormat.Format = "yyyy-mm-dd";
                    ws2.Cell(r2, 3).Value = a.Quantity;
                    ws2.Cell(r2, 4).Value = a.EntryPrice;
                    ws2.Cell(r2, 5).Value = a.InvestedUSDT;
                    ws2.Cell(r2, 6).Value = a.Notes ?? "";
                    ws2.Cell(r2, 7).SetValue(a.AllocatedAt).Style.DateFormat.Format = "yyyy-mm-dd hh:mm:ss";
                    r2++;
                }
                ws2.Columns().AdjustToContents();
                Bump();
            }


            // Statistics
            if (req.IncludeStatistics)
            {
                var cnt = tradeRows.Count;
                var wins = tradeRows.Count(t => ((decimal)t.PnL) > 0);
                var losses = tradeRows.Count(t => ((decimal)t.PnL) < 0);
                var totalPnL = tradeRows.Sum(t => (decimal)t.PnL);
                var avgPnL = cnt == 0 ? 0m : totalPnL / cnt;
                var winRate = cnt == 0 ? 0.0 : wins * 100.0 / cnt;

                var ws = wb.Worksheets.Add("Statistics");
                ws.Cell(1, 1).Value = "Metric";
                ws.Cell(1, 2).Value = "Value";
                ws.Cell(2, 1).Value = "TotalTrades"; ws.Cell(2, 2).Value = cnt;
                ws.Cell(3, 1).Value = "Wins"; ws.Cell(3, 2).Value = wins;
                ws.Cell(4, 1).Value = "Losses"; ws.Cell(4, 2).Value = losses;
                ws.Cell(5, 1).Value = "WinRate"; ws.Cell(5, 2).Value = $"{winRate:0.0}%";
                ws.Cell(6, 1).Value = "TotalPnL"; ws.Cell(6, 2).Value = totalPnL;
                ws.Cell(7, 1).Value = "AvgPnL"; ws.Cell(7, 2).Value = avgPnL;
                ws.Columns().AdjustToContents();
                Bump();
            }

            // Transactions
            if (req.IncludeTransactions)
            {
                var ws = wb.Worksheets.Add("Transactions");
                ws.Cell(1, 1).Value = "Date";
                ws.Cell(1, 2).Value = "Type";
                ws.Cell(1, 3).Value = "Amount";
                ws.Cell(1, 4).Value = "Note";

                var entries = AccountTransactionsService
                    .QuerySince(db, req.UseDateRange && req.From.HasValue ? req.From.Value : DateTime.MinValue);
                if (req.UseDateRange && req.To.HasValue)
                    entries = entries.Where(x => x.Date < req.To.Value.Date.AddDays(1));

                int r = 2;
                foreach (var x in entries.OrderByDescending(x => x.Date))
                {
                    ws.Cell(r, 1).SetValue(x.Date).Style.DateFormat.Format = "yyyy-mm-dd";
                    ws.Cell(r, 2).Value = x.Type.ToString();
                    ws.Cell(r, 3).Value = x.Amount;
                    ws.Cell(r, 4).Value = x.Note ?? "";
                    r++;
                }
                ws.Columns().AdjustToContents();
                Bump();
            }

            var sheetNames = wb.Worksheets.Select(ws => ws.Name).ToList();
            wb.SaveAs(req.OutputPath);
            created.Add(req.OutputPath);
            await Task.CompletedTask;
            return new ExportResult
            {
                Success = true,
                CreatedPaths = new List<string> { req.OutputPath },
                Message = "Sheets: " + string.Join(", ", sheetNames)
            };
        }

        // ---------------- PDF ----------------
        private async Task<ExportResult> ExportPdfAsync(ExportRequest req, IProgress<int>? progress, CancellationToken ct)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(req.OutputPath) ?? ".");

            using var db = new AppDbContext();
            var settings = SettingsManager.Load();
            var now = DateTime.Now;
            var serial = $"{now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8]}";

            var trades = req.IncludeJournal
                ? ApplyDateFilter(db.Trades.AsQueryable(), req).OrderByDescending(t => t.Date).ToList()
                : new List<TradingJournal.Core.Data.Entities.Trade>();

            var txns = req.IncludeTransactions
                ? AccountTransactionsService.QuerySince(db, req.UseDateRange && req.From.HasValue ? req.From.Value : DateTime.MinValue)
                    .Where(x => !req.To.HasValue || x.Date < req.To.Value.Date.AddDays(1))
                    .OrderByDescending(x => x.Date).ToList()
                : new List<AccountTransactionRecord>();

            // Recovery data for PDF (if requested)
            List<dynamic> recCases = new();
            if (req.IncludeRecovery)
            {
                recCases = db.RecoveryCases
                    .Select(c => new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        c.Symbol,
                        c.EntryDate,
                        c.EntryPrice,
                        c.Quantity,
                        c.InvestedUSDT,
                        Status = c.Status.ToString(),
                        c.Notes
                    })
                    .OrderBy(c => c.Case)
                    .AsEnumerable()
                    .Select(x => (dynamic)x)
                    .ToList();
            }

            List<dynamic> recAllocs = new();
            if (req.IncludeRecovery)
            {
                recAllocs = (
                    from a in db.RecoveryAllocations
                    join c in db.RecoveryCases on a.RecoveryCaseId equals c.Id
                    select new
                    {
                        Case = c.CaseRef ?? c.Id.ToString(),
                        a.TradeDate,
                        a.Quantity,
                        a.EntryPrice,
                        a.InvestedUSDT,
                        a.Notes,
                        a.AllocatedAt
                    }
                )
                .OrderBy(x => x.Case).ThenBy(x => x.TradeDate)
                .AsEnumerable()
                .Select(x => (dynamic)x)
                .ToList();
            }
            int countTrades = trades.Count;
            int wins = trades.Count(t => t.ProfitLoss > 0);
            int losses = trades.Count(t => t.ProfitLoss < 0);
            decimal totalPnL = trades.Sum(t => t.ProfitLoss);
            double winRate = countTrades == 0 ? 0 : wins * 100.0 / countTrades;

            static string FDate(DateTime d) => d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string Money(decimal v) => $"{settings.CurrencySymbol}{v:N2}";
            string Dec<T>(T v) where T : struct, IFormattable => v.ToString("N2", CultureInfo.InvariantCulture);

            var P = new
            {
                Primary = Colors.Blue.Darken2,
                PrimaryText = Colors.White,
                TableHeaderBg = Colors.Grey.Darken1,
                TableHeaderFg = Colors.White,
                RowAlt = Colors.Grey.Lighten5,
                RowNorm = Colors.White,
                Border = Colors.Grey.Lighten2,
                Text = Colors.Grey.Darken3,
                Good = Colors.Green.Darken2,
                Bad = Colors.Red.Darken2
            };

            static void Chip(IContainer c, string label, string value, string? sub = null, string bg = null, string fg = null)
            {
                c.Padding(10).Background(bg ?? Colors.Grey.Lighten4)
                 .Border(0.75f).BorderColor(Colors.Grey.Lighten3)
                 .CornerRadius(6)
                 .Column(col =>
                 {
                     col.Spacing(2);
                     col.Item().Text(label).FontSize(9).SemiBold().FontColor(Colors.Grey.Darken2);
                     col.Item().Text(value).FontSize(14).Bold();
                     if (!string.IsNullOrWhiteSpace(sub))
                         col.Item().Text(sub).FontSize(9).FontColor(Colors.Grey.Darken1);
                 });
            }

            static void HeaderCell(IContainer c, string text, string bg, string fg)
            {
                c.Background(bg).PaddingVertical(6).PaddingHorizontal(8)
                 .BorderBottom(1).BorderColor(Colors.White)
                 .Text(t => t.Span(text).SemiBold().FontColor(fg));
            }

            static void BodyCell(IContainer c, bool shade, string text, string? align = null, string? color = null)
            {
                var background = shade ? Colors.Grey.Lighten5 : Colors.White;
                c.Background(background)
                 .PaddingVertical(4).PaddingHorizontal(8)
                 .BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten3)
                 .Text(t =>
                 {
                     t.DefaultTextStyle(s => s.FontSize(9));
                     var span = t.Span(text);
                     if (color != null) span.FontColor(color);
                     if (align == "right") t.AlignRight();
                     else if (align == "center") t.AlignCenter();
                 });
            }

            QuestPDF.Settings.License = LicenseType.Community;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(36);
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor(P.Text));

                    // Header
                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text("TradingJournal Export")
                            .FontSize(20).SemiBold().FontColor(P.Primary);
                        col.Item().AlignCenter().Text(txt =>
                        {
                            txt.Span("Generated: ").SemiBold();
                            txt.Span($"{now:G}");
                            txt.Span("   •   Export # ").SemiBold();
                            txt.Span(serial);
                        });
                        col.Item().AlignCenter().Text(txt =>
                        {
                            txt.Span("Account Balance: ").SemiBold();
                            txt.Span($"{settings.CurrencySymbol}{settings.AccountBalance:N2}");
                            txt.Span("   •   Range: ").SemiBold();
                            if (req.UseDateRange) txt.Span($"{req.From:yyyy-MM-dd} → {req.To:yyyy-MM-dd}");
                            else txt.Span("All data");
                        });
                        col.Item().PaddingTop(8).Height(3).Background(P.Primary);
                    });

                    page.Content().Column(col =>
                    {
                        if (req.IncludeStatistics)
                        {
                            col.Item().PaddingTop(10).Text("Summary").FontSize(14).Bold();
                            col.Item().PaddingTop(4).Row(row =>
                            {
                                row.Spacing(8);
                                row.RelativeItem().Element(e => Chip(e, "Total Trades", countTrades.ToString(), null));
                                row.RelativeItem().Element(e => Chip(e, "Wins", wins.ToString(), null));
                                row.RelativeItem().Element(e => Chip(e, "Losses", losses.ToString(), null));
                                row.RelativeItem().Element(e => Chip(e, "Win Rate", $"{winRate:0.0}%", null));
                                row.RelativeItem().Element(e => Chip(e, "Total PnL", Money(totalPnL), null,
                                    bg: totalPnL >= 0 ? Colors.Green.Lighten4 : Colors.Red.Lighten4));
                            });
                        }

                        // Journal
                        if (req.IncludeJournal)
                        {
                            col.Item().PaddingTop(14).Text("Journal").FontSize(14).Bold();
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.ConstantColumn(78);
                                    c.RelativeColumn(1.2f);
                                    c.ConstantColumn(50);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    if (req.IncludeComputed)
                                    {
                                        c.RelativeColumn(0.7f);
                                        c.RelativeColumn(0.7f);
                                        c.RelativeColumn(0.7f);
                                    }
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(e => HeaderCell(e, "Date", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Symbol", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Side", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Entry", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Exit", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "SL", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "TP", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Margin", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "P/L", P.TableHeaderBg, P.TableHeaderFg));
                                    if (req.IncludeComputed)
                                    {
                                        h.Cell().Element(e => HeaderCell(e, "R", P.TableHeaderBg, P.TableHeaderFg));
                                        h.Cell().Element(e => HeaderCell(e, "Rw", P.TableHeaderBg, P.TableHeaderFg));
                                        h.Cell().Element(e => HeaderCell(e, "RR", P.TableHeaderBg, P.TableHeaderFg));
                                    }
                                });

                                var i = 0;
                                foreach (var t in trades)
                                {
                                    bool shade = (i++ % 2) == 1;
                                    var pnlColor = t.ProfitLoss > 0 ? P.Good : (t.ProfitLoss < 0 ? P.Bad : null);

                                    table.Cell().Element(e => BodyCell(e, shade, FDate(t.Date)));
                                    table.Cell().Element(e => BodyCell(e, shade, t.Symbol ?? ""));
                                    table.Cell().Element(e => BodyCell(e, shade, t.TradeType ?? "", "center"));
                                    table.Cell().Element(e => BodyCell(e, shade, Dec(t.EntryPrice), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, Dec(t.ExitPrice), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, Dec(t.StopLoss), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, Dec(t.TakeProfit), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, Dec(t.Margin), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, Money(t.ProfitLoss), "right", pnlColor));
                                    if (req.IncludeComputed)
                                    {
                                        table.Cell().Element(e => BodyCell(e, shade, Dec(t.Risk), "right"));
                                        table.Cell().Element(e => BodyCell(e, shade, Dec(t.Reward), "right"));
                                        table.Cell().Element(e => BodyCell(e, shade, Dec(t.RR), "right"));
                                    }
                                }
                            });
                        }

                        // Transactions
                        if (req.IncludeTransactions)
                        {
                            col.Item().PaddingTop(14).Text("Account Transactions").FontSize(14).Bold();

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.ConstantColumn(78);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(2);
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(e => HeaderCell(e, "Date", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Type", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Amount", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Note", P.TableHeaderBg, P.TableHeaderFg));
                                });

                                var i = 0;
                                foreach (var x in txns)
                                {
                                    bool shade = (i++ % 2) == 1;
                                    table.Cell().Element(e => BodyCell(e, shade, FDate(x.Date)));
                                    table.Cell().Element(e => BodyCell(e, shade, x.Type.ToString(), "center"));
                                    table.Cell().Element(e => BodyCell(e, shade, Money(x.Amount), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, x.Note ?? ""));
                                }
                            });
                        }

                        // Recovery
                        if (req.IncludeRecovery)
                        {
                            col.Item().PaddingTop(14).Text("Recovery").FontSize(14).Bold();

                            // Cases table
                            col.Item().PaddingTop(6).Text("Cases").SemiBold();
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);   // Case
                                    c.RelativeColumn(1);   // Symbol
                                    c.ConstantColumn(78);  // EntryDate
                                    c.RelativeColumn(1);   // EntryPrice
                                    c.RelativeColumn(1);   // Quantity
                                    c.RelativeColumn(1.2f);// InvestedUSDT
                                    c.RelativeColumn(1);   // Status
                                    c.RelativeColumn(2);   // Notes
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(e => HeaderCell(e, "Case", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Symbol", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "EntryDate", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "EntryPrice", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Quantity", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "InvestedUSDT", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Status", P.TableHeaderBg, P.TableHeaderFg));
                                    h.Cell().Element(e => HeaderCell(e, "Notes", P.TableHeaderBg, P.TableHeaderFg));
                                });

                                var i = 0;
                                foreach (var cse in recCases)
                                {
                                    bool shade = (i++ % 2) == 1;
                                    table.Cell().Element(e => BodyCell(e, shade, cse.Case ?? ""));
                                    table.Cell().Element(e => BodyCell(e, shade, cse.Symbol ?? ""));
                                    table.Cell().Element(e => BodyCell(e, shade, ((DateTime)cse.EntryDate).ToString("yyyy-MM-dd")));
                                    table.Cell().Element(e => BodyCell(e, shade, ((decimal)cse.EntryPrice).ToString("N2", CultureInfo.InvariantCulture), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, ((decimal?)(cse.Quantity ?? 0m)).Value.ToString("N6", CultureInfo.InvariantCulture), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, ((decimal?)(cse.InvestedUSDT ?? 0m)).Value.ToString("N2", CultureInfo.InvariantCulture), "right"));
                                    table.Cell().Element(e => BodyCell(e, shade, cse.Status ?? "", "center"));
                                    table.Cell().Element(e => BodyCell(e, shade, cse.Notes ?? ""));
                                }
                            });

                            // Allocations grouped by Case
                            if (recAllocs.Any())
                            {
                                col.Item().PaddingTop(10).Text("Allocations").SemiBold();

                                foreach (var grp in recAllocs.GroupBy(a => a.Case).OrderBy(g => g.Key))
                                {
                                    col.Item().PaddingTop(6).Text($"Case: {grp.Key}").Italic();

                                    col.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(c =>
                                        {
                                            c.ConstantColumn(78); // TradeDate
                                            c.RelativeColumn(1);  // Quantity
                                            c.RelativeColumn(1);  // EntryPrice
                                            c.RelativeColumn(1.2f); // InvestedUSDT
                                            c.RelativeColumn(2);  // Notes
                                            c.ConstantColumn(110); // AllocatedAt
                                        });

                                        table.Header(h =>
                                        {
                                            h.Cell().Element(e => HeaderCell(e, "TradeDate", P.TableHeaderBg, P.TableHeaderFg));
                                            h.Cell().Element(e => HeaderCell(e, "Quantity", P.TableHeaderBg, P.TableHeaderFg));
                                            h.Cell().Element(e => HeaderCell(e, "EntryPrice", P.TableHeaderBg, P.TableHeaderFg));
                                            h.Cell().Element(e => HeaderCell(e, "InvestedUSDT", P.TableHeaderBg, P.TableHeaderFg));
                                            h.Cell().Element(e => HeaderCell(e, "Notes", P.TableHeaderBg, P.TableHeaderFg));
                                            h.Cell().Element(e => HeaderCell(e, "AllocatedAt", P.TableHeaderBg, P.TableHeaderFg));
                                        });

                                        var j = 0;
                                        foreach (var a in grp.OrderBy(x => x.TradeDate))
                                        {
                                            bool shade = (j++ % 2) == 1;
                                            table.Cell().Element(e => BodyCell(e, shade, ((DateTime)a.TradeDate).ToString("yyyy-MM-dd")));
                                            table.Cell().Element(e => BodyCell(e, shade, ((decimal?)(a.Quantity ?? 0m)).Value.ToString("N6", CultureInfo.InvariantCulture), "right"));
                                            table.Cell().Element(e => BodyCell(e, shade, ((decimal)a.EntryPrice).ToString("N2", CultureInfo.InvariantCulture), "right"));
                                            table.Cell().Element(e => BodyCell(e, shade, ((decimal?)(a.InvestedUSDT ?? 0m)).Value.ToString("N2", CultureInfo.InvariantCulture), "right"));
                                            table.Cell().Element(e => BodyCell(e, shade, a.Notes ?? ""));
                                            table.Cell().Element(e => BodyCell(e, shade, ((DateTime)a.AllocatedAt).ToString("yyyy-MM-dd HH:mm:ss"), "center"));
                                        }
                                    });
                                }
                            }
                        }

                    });

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().AlignLeft().Text(t =>
                        {
                            t.DefaultTextStyle(s => s.FontSize(9));
                            t.Span($"Generated {now:G}");
                        });
                        row.RelativeItem().AlignRight().Text(t =>
                        {
                            t.DefaultTextStyle(s => s.FontSize(9));
                            t.Span("Page "); t.CurrentPageNumber();
                            t.Span(" of "); t.TotalPages();
                        });
                    });
                });
            });

            var pdfBytes = doc.GeneratePdf();
            await File.WriteAllBytesAsync(req.OutputPath, pdfBytes, ct);
            return new ExportResult { Success = true, CreatedPaths = new List<string> { req.OutputPath } };
        }
    }
}

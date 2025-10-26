using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Managers
{
    public class RecoveryCaseRow
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public DateTime EntryDate { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal? InitialInvestedUSDT { get; set; }
        public decimal? InitialQuantity { get; set; }

        public RecoveryCaseStatus Status { get; set; }   // ✅ bound to grid

        public decimal CurrentPrice { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalInvestedUSDT { get; set; }
        public decimal AverageEntryPrice { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal UnrealizedPnL { get; set; }
        public decimal PriceToBreakEven { get; set; }
        public int ProgressPct { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecoveryCaseManager
    {
        public int AddCase(string symbol, DateTime entryDate, decimal entryPrice,
                           decimal? investedUSDT, decimal? quantity, string? notes)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException("Symbol is required", nameof(symbol));
            if (entryPrice <= 0m) throw new ArgumentException("EntryPrice must be > 0", nameof(entryPrice));
            if (!investedUSDT.HasValue && !quantity.HasValue)
                throw new ArgumentException("Provide InvestedUSDT or Quantity for the initial buy.");

            using var db = new AppDbContext();
            var rc = new RecoveryCase
            {
                Symbol = symbol,
                EntryDate = entryDate,
                EntryPrice = entryPrice,
                InvestedUSDT = investedUSDT,
                Quantity = quantity,
                Notes = notes,
                Status = RecoveryCaseStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            db.RecoveryCases.Add(rc);
            db.SaveChanges();
            return rc.Id;
        }

        public void UpdateCase(int id, string symbol, DateTime entryDate, decimal entryPrice,
                               decimal? investedUSDT, decimal? quantity, string? notes)
        {
            if (entryPrice <= 0m) throw new ArgumentException("EntryPrice must be > 0", nameof(entryPrice));
            if (!investedUSDT.HasValue && !quantity.HasValue)
                throw new ArgumentException("Provide InvestedUSDT or Quantity for the initial buy.");

            using var db = new AppDbContext();
            var rc = db.RecoveryCases.Find(id);
            if (rc == null) return;

            rc.Symbol = symbol;
            rc.EntryDate = entryDate;
            rc.EntryPrice = entryPrice;
            rc.InvestedUSDT = investedUSDT;
            rc.Quantity = quantity;
            rc.Notes = notes;
            rc.UpdatedAt = DateTime.UtcNow;

            db.SaveChanges();
        }

        public void DeleteCase(int id)
        {
            using var db = new AppDbContext();
            var rc = db.RecoveryCases.Find(id);
            if (rc == null) return;
            db.RecoveryCases.Remove(rc);
            db.SaveChanges();
        }

        public void ChangeStatus(int id, RecoveryCaseStatus status)
        {
            using var db = new AppDbContext();
            var rc = db.RecoveryCases.Find(id);
            if (rc == null) return;
            rc.Status = status;
            rc.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();
        }

        private static (decimal totalQty, decimal totalInv, decimal avg) ComputeTotals(
            RecoveryCase rc, IEnumerable<RecoveryAllocation> allocs)
        {
            decimal initQty = rc.Quantity ?? ((rc.InvestedUSDT.HasValue && rc.EntryPrice > 0m) ? rc.InvestedUSDT.Value / rc.EntryPrice : 0m);
            decimal initInv = rc.InvestedUSDT ?? (initQty * rc.EntryPrice);

            decimal addQty = 0m, addInv = 0m;
            foreach (var a in allocs)
            {
                var q = a.Quantity ?? ((a.InvestedUSDT.HasValue && a.EntryPrice > 0m) ? a.InvestedUSDT.Value / a.EntryPrice : 0m);
                var inv = a.InvestedUSDT ?? (q * a.EntryPrice);
                addQty += q; addInv += inv;
            }

            var totalQty = initQty + addQty;
            var totalInv = initInv + addInv;
            var avg = totalQty > 0m ? totalInv / totalQty : 0m;
            return (totalQty, totalInv, avg);
        }

        public async Task<List<RecoveryCaseRow>> GetRowsAsync(RecoveryCaseStatus? statusFilter = null)
        {
            using var db = new AppDbContext();

            var q = db.RecoveryCases.AsQueryable();
            if (statusFilter.HasValue)
                q = q.Where(rc => rc.Status == statusFilter.Value);   // ✅ nullable fix

            var cases = await q.OrderByDescending(rc => rc.CreatedAt).ToListAsync();

            var symbols = cases.Select(c => c.Symbol).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var priceMap = await new BinanceApiService().GetLastPricesAsync(symbols);

            var caseIds = cases.Select(c => c.Id).ToList();
            var allocs = await db.RecoveryAllocations
                                 .Where(a => caseIds.Contains(a.RecoveryCaseId))
                                 .ToListAsync();
            var allocLookup = allocs.GroupBy(a => a.RecoveryCaseId)
                                    .ToDictionary(g => g.Key, g => (IEnumerable<RecoveryAllocation>)g);

            var rows = new List<RecoveryCaseRow>(cases.Count);
            foreach (var rc in cases)
            {
                priceMap.TryGetValue(rc.Symbol, out var curPx);
                var myAllocs = allocLookup.TryGetValue(rc.Id, out var list) ? list : Array.Empty<RecoveryAllocation>();

                var (totalQty, totalInv, avgEntry) = ComputeTotals(rc, myAllocs);
                var currentVal = totalQty * curPx;
                var pnl = currentVal - totalInv;

                var toBe = Math.Max(0m, avgEntry - curPx);
                int progress = 0;
                if (avgEntry > 0m)
                {
                    var pct = (curPx / avgEntry) * 100m; // all decimal
                    if (pct < 0m) pct = 0m;
                    if (pct > 100m) pct = 100m;
                    progress = (int)decimal.Round(pct, 0, MidpointRounding.AwayFromZero);
                }

                rows.Add(new RecoveryCaseRow
                {
                    Id = rc.Id,
                    Symbol = rc.Symbol,
                    EntryDate = rc.EntryDate,
                    EntryPrice = rc.EntryPrice,
                    InitialInvestedUSDT = rc.InvestedUSDT,
                    InitialQuantity = rc.Quantity,

                    Status = rc.Status,               // ✅ now populated

                    CurrentPrice = curPx,
                    TotalQuantity = totalQty,
                    TotalInvestedUSDT = totalInv,
                    AverageEntryPrice = avgEntry,
                    CurrentValue = currentVal,
                    UnrealizedPnL = pnl,
                    PriceToBreakEven = toBe,
                    ProgressPct = progress,

                    Notes = rc.Notes,
                    CreatedAt = rc.CreatedAt
                });
            }
            return rows;
        }
    }
}

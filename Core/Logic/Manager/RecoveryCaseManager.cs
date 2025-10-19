using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;                 // <-- needed
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Managers
{
    public class RecoveryCaseRow
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public RecoveryCaseType CaseType { get; set; }
        public RecoveryCaseStatus Status { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal? InvestedUSDT { get; set; }
        public decimal? Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? CurrentValue { get; set; }   // HeldBag only
        public decimal RecoveredSoFar { get; set; }
        public decimal NeededToBreakEven { get; set; }  // clamped to >= 0
        public int ProgressPct { get; set; }            // 0..100
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecoveryCaseManager
    {
        public int AddCase(string symbol, RecoveryCaseType caseType, DateTime entryDate, decimal entryPrice,
                           decimal? investedUSDT, decimal? quantity, string? notes)
        {
            using var db = new AppDbContext();
            var rc = new RecoveryCase
            {
                Symbol = symbol,
                CaseType = caseType,
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

        public void UpdateCase(int id, string symbol, RecoveryCaseType caseType, DateTime entryDate, decimal entryPrice,
                               decimal? investedUSDT, decimal? quantity, string? notes)
        {
            using var db = new AppDbContext();
            var rc = db.RecoveryCases.Find(id);
            if (rc == null) return;

            rc.Symbol = symbol;
            rc.CaseType = caseType;
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

        // -------- FIX: translateable aggregate (no DefaultIfEmpty chain) --------
        private static async Task<decimal> GetRecoveredSoFarAsync(AppDbContext db, int caseId)
        {
            return await db.RecoveryAllocations
                           .Where(a => a.RecoveryCaseId == caseId)
                           .SumAsync(a => (decimal?)a.AllocatedUSDT) ?? 0m;
        }

        private static (decimal? currentValue, decimal unrealized, decimal needed, int progressPct)
            Compute(RecoveryCase rc, decimal currentPrice, decimal recovered)
        {
            if (rc.CaseType == RecoveryCaseType.HeldBag)
            {
                decimal qty = rc.Quantity ?? (rc.InvestedUSDT.HasValue && rc.EntryPrice > 0M
                    ? rc.InvestedUSDT.Value / rc.EntryPrice
                    : 0M);

                decimal currentValue = qty * currentPrice;
                decimal invested = rc.InvestedUSDT ?? (qty * rc.EntryPrice);
                decimal unrealized = currentValue - invested;

                decimal lossNow = Math.Max(0M, invested - currentValue);
                decimal needed = Math.Max(0M, lossNow - recovered);

                int progress = 0;
                var denom = recovered + needed;
                if (denom > 0m)
                    progress = (int)Math.Min(100, Math.Max(0, decimal.ToInt32(decimal.Round((recovered / denom) * 100m, 0))));

                return (currentValue, unrealized, needed, progress);
            }

            decimal realizedLoss = rc.InvestedUSDT ?? 0M;
            decimal neededRL = Math.Max(0M, realizedLoss - recovered);
            int progressRL = realizedLoss <= 0
                ? 0
                : (int)Math.Min(100, Math.Max(0, decimal.ToInt32(decimal.Round((recovered / realizedLoss) * 100m, 0))));

            return (null, 0M, neededRL, progressRL);
        }

        public async Task<List<RecoveryCaseRow>> GetRowsAsync(RecoveryCaseStatus? statusFilter = null)
        {
            using var db = new AppDbContext();

            var q = db.RecoveryCases.AsQueryable();
            if (statusFilter.HasValue) q = q.Where(rc => rc.Status == statusFilter);

            var cases = await q.OrderByDescending(rc => rc.CreatedAt).ToListAsync();

            // Batch prices
            var symbols = cases.Select(c => c.Symbol).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var priceSvc = new BinanceApiService();
            var priceMap = await priceSvc.GetLastPricesAsync(symbols);

            // Build rows
            var rows = new List<RecoveryCaseRow>(cases.Count);
            foreach (var rc in cases)
            {
                priceMap.TryGetValue(rc.Symbol, out var currentPrice);
                var recovered = await GetRecoveredSoFarAsync(db, rc.Id);

                var (currentValue, unrealized, needed, progress) = Compute(rc, currentPrice, recovered);

                rows.Add(new RecoveryCaseRow
                {
                    Id = rc.Id,
                    Symbol = rc.Symbol,
                    CaseType = rc.CaseType,
                    Status = rc.Status,
                    EntryDate = rc.EntryDate,
                    EntryPrice = rc.EntryPrice,
                    InvestedUSDT = rc.InvestedUSDT,
                    Quantity = rc.Quantity,
                    CurrentPrice = currentPrice,
                    CurrentValue = currentValue,
                    RecoveredSoFar = recovered,
                    NeededToBreakEven = needed,
                    ProgressPct = progress,
                    Notes = rc.Notes,
                    CreatedAt = rc.CreatedAt
                });
            }
            return rows;
        }
    }
}

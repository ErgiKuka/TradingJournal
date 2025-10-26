using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Managers
{
    public class AllocationRow
    {
        public int Id { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal? InvestedUSDT { get; set; }  // user enters either this...
        public decimal? Quantity { get; set; }      // ...or this

        // Convenience for grid (running stats)
        public decimal ComputedQuantity { get; set; }   // Quantity or Invested/Entry
        public decimal RunningTotalQty { get; set; }
        public decimal RunningTotalInvested { get; set; }
        public decimal RunningAvgEntry { get; set; }
    }

    public class RecoveryAllocationManager
    {
        // List allocations for a case (sorted desc), with running stats
        public async Task<List<AllocationRow>> GetRowsAsync(int caseId)
        {
            using var db = new AppDbContext();

            var allocs = await db.RecoveryAllocations
                .Where(a => a.RecoveryCaseId == caseId)
                .OrderByDescending(a => a.TradeDate)
                .Select(a => new
                {
                    a.Id,
                    a.TradeDate,
                    a.EntryPrice,
                    a.InvestedUSDT,
                    a.Quantity
                })
                .ToListAsync();

            // Running stats need ascending chronological order then flip back
            var asc = allocs.OrderBy(a => a.TradeDate).ToList();

            decimal runningQty = 0m;
            decimal runningInvested = 0m;
            var withStatsAsc = new List<AllocationRow>(asc.Count);

            foreach (var a in asc)
            {
                var qty = a.Quantity ?? ((a.InvestedUSDT.HasValue && a.EntryPrice > 0m) ? a.InvestedUSDT.Value / a.EntryPrice : 0m);
                var inv = a.InvestedUSDT ?? (qty * a.EntryPrice);

                runningQty += qty;
                runningInvested += inv;

                var avg = runningQty > 0m ? runningInvested / runningQty : 0m;

                withStatsAsc.Add(new AllocationRow
                {
                    Id = a.Id,
                    TradeDate = a.TradeDate,
                    EntryPrice = a.EntryPrice,
                    InvestedUSDT = a.InvestedUSDT,
                    Quantity = a.Quantity,
                    ComputedQuantity = qty,
                    RunningTotalQty = runningQty,
                    RunningTotalInvested = runningInvested,
                    RunningAvgEntry = avg
                });
            }

            return withStatsAsc.OrderByDescending(x => x.TradeDate).ToList();
        }

        public int Add(int caseId, DateTime tradeDate, decimal entryPrice,
                       decimal? investedUsdt, decimal? quantity)
        {
            if (entryPrice <= 0m) throw new ArgumentException("EntryPrice must be > 0", nameof(entryPrice));
            if (!investedUsdt.HasValue && !quantity.HasValue)
                throw new ArgumentException("Provide InvestedUSDT or Quantity.");

            using var db = new AppDbContext();
            var a = new RecoveryAllocation
            {
                RecoveryCaseId = caseId,
                TradeDate = tradeDate,
                EntryPrice = entryPrice,
                InvestedUSDT = investedUsdt,
                Quantity = quantity
            };
            db.RecoveryAllocations.Add(a);
            db.SaveChanges();
            return a.Id;
        }

        public void Update(int id, DateTime tradeDate, decimal entryPrice,
                           decimal? investedUsdt, decimal? quantity)
        {
            if (entryPrice <= 0m) throw new ArgumentException("EntryPrice must be > 0", nameof(entryPrice));
            if (!investedUsdt.HasValue && !quantity.HasValue)
                throw new ArgumentException("Provide InvestedUSDT or Quantity.");

            using var db = new AppDbContext();
            var a = db.RecoveryAllocations.Find(id);
            if (a == null) return;

            a.TradeDate = tradeDate;
            a.EntryPrice = entryPrice;
            a.InvestedUSDT = investedUsdt;
            a.Quantity = quantity;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new AppDbContext();
            var a = db.RecoveryAllocations.Find(id);
            if (a == null) return;

            db.RecoveryAllocations.Remove(a);
            db.SaveChanges();
        }

        // Helper to load a case
        public async Task<RecoveryCase?> GetCaseAsync(int caseId)
        {
            using var db = new AppDbContext();
            return await db.RecoveryCases.FirstOrDefaultAsync(x => x.Id == caseId);
        }
    }
}

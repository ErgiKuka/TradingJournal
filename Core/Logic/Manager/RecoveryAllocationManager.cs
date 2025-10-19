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
        public decimal? ExitPrice { get; set; }
        public decimal? MarginUSDT { get; set; }   // entered as margin
        public decimal? Quantity { get; set; }     // entered as quantity
        public decimal AllocatedUSDT { get; set; } // always non-null in the UI layer
    }

    public class RecoveryAllocationManager
    {
        // List allocations for a case
        public async Task<List<AllocationRow>> GetRowsAsync(int caseId)
        {
            using var db = new AppDbContext();

            return await db.RecoveryAllocations
                .Where(a => a.RecoveryCaseId == caseId)
                .OrderByDescending(a => a.TradeDate)
                .Select(a => new AllocationRow
                {
                    Id = a.Id,
                    TradeDate = a.TradeDate,
                    EntryPrice = (decimal)a.EntryPrice,
                    ExitPrice = a.ExitPrice,
                    MarginUSDT = a.MarginUSDT,
                    Quantity = a.Quantity,
                    // Works whether a.AllocatedUSDT is decimal or decimal?
                    AllocatedUSDT = EF.Property<decimal?>(a, nameof(a.AllocatedUSDT)) ?? 0m
                })
                .ToListAsync();
        }

        // Sum allocated (for KPIs) – safe for nullable or non-nullable columns
        public static async Task<decimal> GetRecoveredSoFarAsync(AppDbContext db, int caseId)
        {
            var sum = await db.RecoveryAllocations
                  .Where(a => a.RecoveryCaseId == caseId)
                  // ✅ Same trick here to keep translation clean
                  .SumAsync(a => EF.Property<decimal?>(a, nameof(a.AllocatedUSDT)));

            return sum ?? 0m;
        }

        public int Add(int caseId, DateTime tradeDate, decimal entryPrice,
                       decimal? exitPrice, decimal? marginUsdt, decimal? quantity,
                       decimal allocatedUsdt)
        {
            using var db = new AppDbContext();
            var a = new RecoveryAllocation
            {
                RecoveryCaseId = caseId,
                TradeDate = tradeDate,
                EntryPrice = entryPrice,
                ExitPrice = exitPrice,
                MarginUSDT = marginUsdt,
                Quantity = quantity,
                // If the entity property is decimal? or decimal, both are fine here
                AllocatedUSDT = allocatedUsdt
            };
            db.RecoveryAllocations.Add(a);
            db.SaveChanges();
            return a.Id;
        }

        public void Update(int id, DateTime tradeDate, decimal entryPrice,
                           decimal? exitPrice, decimal? marginUsdt, decimal? quantity,
                           decimal allocatedUsdt)
        {
            using var db = new AppDbContext();
            var a = db.RecoveryAllocations.Find(id);
            if (a == null) return;

            a.TradeDate = tradeDate;
            a.EntryPrice = entryPrice;
            a.ExitPrice = exitPrice;
            a.MarginUSDT = marginUsdt;
            a.Quantity = quantity;
            a.AllocatedUSDT = allocatedUsdt;

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

        // Helper to load a case (for KPIs)
        public async Task<RecoveryCase?> GetCaseAsync(int caseId)
        {
            using var db = new AppDbContext();
            return await db.RecoveryCases.FirstOrDefaultAsync(x => x.Id == caseId);
        }
    }
}

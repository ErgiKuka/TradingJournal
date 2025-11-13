using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.RecycleBin;

namespace TradingJournal.Core.Logic.Services
{
    public static class RecycleBinService
    {
        private static readonly JsonSerializerOptions J = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false
        };

        public static int RetentionDaysDefault = 30;

        #region Capture
        public static async Task CaptureTradeAsync(AppDbContext db, Trade t, int retentionDays, string? note = null)
        {
            var payload = new TradePayload(
                t.Id, t.Symbol, t.TradeType, t.EntryPrice, t.ExitPrice, t.StopLoss, t.TakeProfit,
                t.Margin, t.ProfitLoss, t.Date, t.ScreenshotLink, t.ScreenshotImage);

            await AddItemAsync(db, new RecycleBinItem
            {
                EntityType = "Trade",
                EntityKey = $"{t.Symbol} {t.Date:yyyy-MM-dd}",
                OriginalId = t.Id,
                PayloadJson = JsonSerializer.Serialize(payload, J),
                Note = note
            }, retentionDays);
        }

        public static async Task CaptureAllocationAsync(AppDbContext db, RecoveryAllocation a, int retentionDays, string? note = null)
        {
            var p = new AllocationPayload(a.Id, a.RecoveryCaseId, a.TradeDate, a.EntryPrice, a.InvestedUSDT, a.Quantity);
            await AddItemAsync(db, new RecycleBinItem
            {
                EntityType = "RecoveryAllocation",
                EntityKey = $"Case#{a.RecoveryCaseId} • {a.TradeDate:yyyy-MM-dd}",
                OriginalId = a.Id,
                PayloadJson = JsonSerializer.Serialize(p, J),
                Note = note
            }, retentionDays);
        }

        public static async Task CaptureCaseWithAllocationsAsync(AppDbContext db, RecoveryCase c, IEnumerable<RecoveryAllocation> alcs, int retentionDays, string? note = null)
        {
            var p = new CaseWithAllocationsPayload(
                c,
                alcs.Select(a => new AllocationPayload(a.Id, a.RecoveryCaseId, a.TradeDate, a.EntryPrice, a.InvestedUSDT, a.Quantity)).ToList()
            );
            await AddItemAsync(db, new RecycleBinItem
            {
                EntityType = "RecoveryCase",
                EntityKey = $"{c.Symbol} {c.EntryDate:yyyy-MM-dd}",
                OriginalId = c.Id,
                PayloadJson = JsonSerializer.Serialize(p, J),
                Note = note
            }, retentionDays);
        }

        public static async Task CaptureAccountTransactionAsync(AppDbContext db, DateTime date, int type, decimal amount, string? note, int retentionDays)
        {
            var p = new AccountTransactionPayload(date, type, amount, note);
            await AddItemAsync(db, new RecycleBinItem
            {
                EntityType = "AccountTransaction",
                EntityKey = $"{date:yyyy-MM-dd} • {amount}",
                OriginalId = null,
                PayloadJson = JsonSerializer.Serialize(p, J),
                Note = note
            }, retentionDays);
        }

        private static async Task AddItemAsync(AppDbContext db, RecycleBinItem item, int retentionDays)
        {
            item.DeletedUtc = DateTime.UtcNow;
            item.ExpiresUtc = item.DeletedUtc.AddDays(Math.Max(1, retentionDays));
            db.RecycleBinItems.Add(item);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Listing / Purge / Empty
        public static Task<List<RecycleBinItem>> ListAsync(AppDbContext db)
            => db.RecycleBinItems.AsNoTracking().OrderByDescending(x => x.DeletedUtc).ToListAsync();

        public static async Task<int> PurgeExpiredAsync(AppDbContext db)
        {
            var now = DateTime.UtcNow;
            var expired = await db.RecycleBinItems.Where(x => x.ExpiresUtc != null && x.ExpiresUtc <= now).ToListAsync();
            db.RecycleBinItems.RemoveRange(expired);
            await db.SaveChangesAsync();
            return expired.Count;
        }

        public static async Task EmptyAllAsync(AppDbContext db)
        {
            db.RecycleBinItems.RemoveRange(db.RecycleBinItems);
            await db.SaveChangesAsync();
        }

        public static async Task DeleteItemAsync(AppDbContext db, int id)
        {
            var it = await db.RecycleBinItems.FindAsync(id);
            if (it == null) return;
            db.RecycleBinItems.Remove(it);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Restore
        public static async Task<bool> RestoreAsync(AppDbContext db, int recycleId)
        {
            var it = await db.RecycleBinItems.FindAsync(recycleId);
            if (it == null) return false;

            try
            {
                switch (it.EntityType)
                {
                    case "Trade":
                        {
                            var p = JsonSerializer.Deserialize<TradePayload>(it.PayloadJson, J)!;
                            // If id already exists, place as new
                            var entity = new Trade
                            {
                                Symbol = p.Symbol,
                                TradeType = p.TradeType,
                                EntryPrice = p.EntryPrice,
                                ExitPrice = p.ExitPrice,
                                StopLoss = p.StopLoss,
                                TakeProfit = p.TakeProfit,
                                Margin = p.Margin,
                                ProfitLoss = p.ProfitLoss,
                                Date = p.Date,
                                ScreenshotLink = p.ScreenshotLink,
                                ScreenshotImage = p.ScreenshotImage
                            };
                            db.Trades.Add(entity);
                            break;
                        }
                    case "RecoveryAllocation":
                        {
                            var p = JsonSerializer.Deserialize<AllocationPayload>(it.PayloadJson, J)!;
                            var okCase = await db.RecoveryCases.AnyAsync(x => x.Id == p.RecoveryCaseId);
                            if (!okCase) return false; // case gone; we purposefully avoid resurrecting orphaned allocs
                            db.RecoveryAllocations.Add(new RecoveryAllocation
                            {
                                RecoveryCaseId = p.RecoveryCaseId,
                                TradeDate = p.TradeDate,
                                EntryPrice = p.EntryPrice,
                                InvestedUSDT = p.InvestedUSDT,
                                Quantity = p.Quantity
                            });
                            break;
                        }
                    case "RecoveryCase":
                        {
                            var p = JsonSerializer.Deserialize<CaseWithAllocationsPayload>(it.PayloadJson, J)!;

                            // restore case first
                            var rc = p.Case;
                            rc.Id = 0; // force new PK
                            db.RecoveryCases.Add(rc);
                            await db.SaveChangesAsync();

                            // restore allocations to the new case id
                            foreach (var a in p.Allocations)
                            {
                                db.RecoveryAllocations.Add(new RecoveryAllocation
                                {
                                    RecoveryCaseId = rc.Id,
                                    TradeDate = a.TradeDate,
                                    EntryPrice = a.EntryPrice,
                                    InvestedUSDT = a.InvestedUSDT,
                                    Quantity = a.Quantity
                                });
                            }
                            break;
                        }
                    case "AccountTransaction":
                        {
                            var p = JsonSerializer.Deserialize<AccountTransactionPayload>(it.PayloadJson, J)!;
                            AccountTransactionsService.Append(db, new AccountTransactionRecord
                            {
                                Date = p.Date,
                                Type = (AccountTransactionType)p.Type,
                                Amount = p.Amount,
                                Note = p.Note
                            });
                            break;
                        }
                    default:
                        return false;
                }

                await db.SaveChangesAsync();
                db.RecycleBinItems.Remove(it);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

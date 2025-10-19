// RecoveryAllocation.cs  (prepared for the next form; used now only to sum recovered)
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace TradingJournal.Core.Data.Entities
{
    public class RecoveryAllocation
    {
        public int Id { get; set; }
        public int RecoveryCaseId { get; set; }
        public RecoveryCase RecoveryCase { get; set; } = default!;

        public DateTime TradeDate { get; set; } = DateTime.UtcNow;

        // Optional trade fields (handy later):
        public decimal? EntryPrice { get; set; }
        public decimal? ExitPrice { get; set; }
        public decimal? MarginUSDT { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TradePnL { get; set; }

        // The actual amount we applied to this case’s recovery
        public decimal AllocatedUSDT { get; set; }

        public string? Notes { get; set; }
        public DateTime AllocatedAt { get; set; } = DateTime.UtcNow;

        internal static void ConfigureForDb(EntityTypeBuilder<RecoveryAllocation> e)
        {
            e.ToTable("RecoveryAllocations");
            e.HasKey(x => x.Id);
            e.Property(x => x.EntryPrice).HasColumnType("decimal(18,8)");
            e.Property(x => x.ExitPrice).HasColumnType("decimal(18,8)");
            e.Property(x => x.MarginUSDT).HasColumnType("decimal(18,2)");
            e.Property(x => x.Quantity).HasColumnType("decimal(18,8)");
            e.Property(x => x.TradePnL).HasColumnType("decimal(18,2)");
            e.Property(x => x.AllocatedUSDT).HasColumnType("decimal(18,2)").IsRequired();

            e.HasOne(x => x.RecoveryCase)
             .WithMany(c => c.Allocations)
             .HasForeignKey(x => x.RecoveryCaseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

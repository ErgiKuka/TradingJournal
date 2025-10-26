using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingJournal.Core.Data.Entities
{
    public class RecoveryAllocation
    {
        public int Id { get; set; }
        public int RecoveryCaseId { get; set; }
        public RecoveryCase RecoveryCase { get; set; } = default!;

        public DateTime TradeDate { get; set; } = DateTime.UtcNow;

        // DCA BUY ONLY: Entry price + either InvestedUSDT or Quantity
        public decimal EntryPrice { get; set; }              // required per UI
        public decimal? InvestedUSDT { get; set; }           // mapped to old DB column MarginUSDT
        public decimal? Quantity { get; set; }               // single Quantity column (no duplicates)

        public string? Notes { get; set; }
        public DateTime AllocatedAt { get; set; } = DateTime.UtcNow;

        internal static void ConfigureForDb(EntityTypeBuilder<RecoveryAllocation> e)
        {
            e.ToTable("RecoveryAllocations");
            e.HasKey(x => x.Id);

            e.Property(x => x.EntryPrice).HasColumnType("decimal(18,8)").IsRequired();

            // Keep the existing DB column name (NO new column created)
            e.Property(x => x.InvestedUSDT)
             .HasColumnName("MarginUSDT")
             .HasColumnType("decimal(18,2)");

            e.Property(x => x.Quantity).HasColumnType("decimal(18,8)");
            e.Property(x => x.Notes).HasMaxLength(1000);

            e.HasOne(x => x.RecoveryCase)
             .WithMany(c => c.Allocations)
             .HasForeignKey(x => x.RecoveryCaseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

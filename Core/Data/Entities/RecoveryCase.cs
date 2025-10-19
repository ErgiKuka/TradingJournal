// RecoveryCase.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace TradingJournal.Core.Data.Entities
{
    public class RecoveryCase
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public RecoveryCaseType CaseType { get; set; }

        public DateTime EntryDate { get; set; }
        public decimal EntryPrice { get; set; }

        // One of these will be provided (based on UI radio):
        public decimal? InvestedUSDT { get; set; }   // how much was spent OR realized loss amount (positive)
        public decimal? Quantity { get; set; }       // base units bought (HeldBag)

        public RecoveryCaseStatus Status { get; set; } = RecoveryCaseStatus.Active;
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RecoveryAllocation> Allocations { get; set; } = new List<RecoveryAllocation>();

        internal static void ConfigureForDb(EntityTypeBuilder<RecoveryCase> e)
        {
            e.ToTable("RecoveryCases");
            e.HasKey(x => x.Id);

            e.Property(x => x.Symbol).HasMaxLength(20).IsRequired();
            e.Property(x => x.CaseType).HasConversion<int>().IsRequired();
            e.Property(x => x.EntryDate).IsRequired();
            e.Property(x => x.EntryPrice).HasColumnType("decimal(18,8)").IsRequired();
            e.Property(x => x.InvestedUSDT).HasColumnType("decimal(18,2)");
            e.Property(x => x.Quantity).HasColumnType("decimal(18,8)");
            e.Property(x => x.Status).HasConversion<int>().IsRequired();
            e.Property(x => x.Notes).HasMaxLength(1000);
            e.Property(x => x.CreatedAt).IsRequired();
            e.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}

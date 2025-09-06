using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingJournal.Core.Data.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string TradeType { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal ExitPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Margin { get; set; }
        public decimal ProfitLoss { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        // Screenshot
        public string? ScreenshotLink { get; set; }
        public byte[]? ScreenshotImage { get; set; }

        // Not mapped – auto-calculated
        [NotMapped]
        public decimal Risk
        {
            get
            {
                if (StopLoss == 0 || EntryPrice == 0) return 0;

                if (TradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                {
                    return EntryPrice - StopLoss;
                }
                else if (TradeType.Equals("Short", StringComparison.OrdinalIgnoreCase))
                {
                    return StopLoss - EntryPrice;
                }
                return StopLoss - EntryPrice;
            }
        }

        [NotMapped]
        public decimal Reward
        {
            get
            {
                if (TakeProfit == 0 || EntryPrice == 0) return 0;

                if (TradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                {
                    return TakeProfit - EntryPrice;
                }
                else if (TradeType.Equals("Short", StringComparison.OrdinalIgnoreCase))
                {
                    return EntryPrice - TakeProfit;
                }
                return EntryPrice - TakeProfit;
            }
        }

        [NotMapped]
        public decimal RR
        {
            get
            {
                decimal riskValue = this.Risk;
                decimal rewardValue = this.Reward;

                if (Risk > 0 && Reward > 0)
                {
                    return Math.Round(Reward / Risk, 2);
                }
                return 0;
            }
        }

        internal static void ConfigureForDb(EntityTypeBuilder<Trade> entity)
        {
            entity.ToTable("Trades");
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Symbol).HasMaxLength(20).IsRequired();
            entity.Property(t => t.TradeType).HasMaxLength(10).IsRequired();

            entity.Property(t => t.EntryPrice).HasColumnType("decimal(18,4)").IsRequired();
            entity.Property(t => t.ExitPrice).HasColumnType("decimal(18,4)");
            entity.Property(t => t.StopLoss).HasColumnType("decimal(18,4)");
            entity.Property(t => t.TakeProfit).HasColumnType("decimal(18,4)");
            entity.Property(t => t.Margin).HasColumnType("decimal(18,4)");
            entity.Property(t => t.ProfitLoss).HasColumnType("decimal(18,4)");
            entity.Property(t => t.Date).IsRequired();

            entity.Property(t => t.ScreenshotLink).HasMaxLength(500).IsRequired(false);
            entity.Property(t => t.ScreenshotImage).HasColumnType("BLOB").IsRequired(false);
        }
    }
}

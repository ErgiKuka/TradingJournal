using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace TradingJournal.Core.Data.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string Symbol { get; set; }        // BTC/USDT
        public string TradeType { get; set; }     // Long / Short
        public decimal EntryPrice { get; set; }
        public decimal ExitPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Margin { get; set; }
        public decimal ProfitLoss { get; set; }   // Profit or loss result
        public DateTime Date { get; set; }

        // Screenshot options
        public string ScreenshotLink { get; set; }    // For Lightshot or imgur link
        public byte[] ScreenshotImage { get; set; }   // For direct image storage

        internal static void ConfigureForDb(EntityTypeBuilder<Trade> entity)
        {
            entity.ToTable("Trades");
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Symbol)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(t => t.TradeType)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(t => t.EntryPrice)
                  .HasColumnType("decimal(18,4)")
                  .IsRequired();

            entity.Property(t => t.ExitPrice)
                  .HasColumnType("decimal(18,4)");

            entity.Property(t => t.StopLoss)
                  .HasColumnType("decimal(18,4)");

            entity.Property(t => t.TakeProfit)
                  .HasColumnType("decimal(18,4)");

            entity.Property(t => t.Margin)
                  .HasColumnType("decimal(18,4)");

            entity.Property(t => t.ProfitLoss)
                  .HasColumnType("decimal(18,4)");

            entity.Property(t => t.Date)
                  .IsRequired();

            entity.Property(t => t.ScreenshotLink)
                  .HasMaxLength(500);

            entity.Property(t => t.ScreenshotImage)
                  .HasColumnType("BLOB"); // SQLite BLOB for raw images
        }
    }
}

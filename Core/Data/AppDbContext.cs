using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace TradingJournal.Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Trade> Trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=tradingjournal.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Trade.ConfigureForDb(modelBuilder.Entity<Trade>());
        }
    }
}

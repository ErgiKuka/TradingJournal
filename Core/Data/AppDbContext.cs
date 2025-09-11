// In TradingJournal.Core.Data, file AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _databasePath;

        public DbSet<Trade> Trades { get; set; }

        // Default constructor for design-time tools (like migrations)
        public AppDbContext()
        {
            // This will use the safe path by default if no path is provided.
            _databasePath = DataAccess.GetDatabasePath();
        }

        // Constructor that accepts the database path
        public AppDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use the database path field to build the connection string
                optionsBuilder.UseSqlite($"Data Source={_databasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Trade.ConfigureForDb(modelBuilder.Entity<Trade>());
        }
    }
}

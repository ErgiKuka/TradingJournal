using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TradingJournal.Core.Data.Entities
{
    public class ExchangePlatform
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

        public bool UseTestnet { get; set; } = true;

        public byte[] CredentialsEncrypted { get; set; } = Array.Empty<byte>();

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public static void ConfigureForDb(EntityTypeBuilder<ExchangePlatform> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Exchange).IsRequired().HasMaxLength(40);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    public sealed class ExchangeCredentials
    {
        private readonly IReadOnlyDictionary<string, string> _values;

        public string Exchange { get; }
        public bool UseTestnet { get; }

        public ExchangeCredentials(IReadOnlyDictionary<string, string> values, string exchange, bool useTestnet)
        {
            _values = values ?? new Dictionary<string, string>();
            Exchange = exchange;
            UseTestnet = useTestnet;
        }

        public string Get(string key) => _values.TryGetValue(key, out var v) ? v : string.Empty;

        // Convenience accessors used by the exchange clients.
        public string ApiKey => Get("ApiKey");
        public string ApiSecret => Get("ApiSecret");
        public string WalletAddress => Get("WalletAddress");
        public string PrivateKey => Get("PrivateKey");
    }

    public class PlatformManager
    {
        public IReadOnlyList<ExchangePlatform> GetAll()
        {
            using var db = new AppDbContext();
            return db.ExchangePlatforms.OrderBy(p => p.Name).ToList();
        }

        public ExchangePlatform Add(string name, string exchange, IDictionary<string, string> credentials, bool useTestnet)
        {
            name = (name ?? string.Empty).Trim();
            exchange = (exchange ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Platform name is required.");
            if (string.IsNullOrWhiteSpace(exchange))
                throw new ArgumentException("Select an exchange.");
            if (credentials == null || credentials.Count == 0 || credentials.Values.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Fill in all credential fields.");

            var json = JsonSerializer.Serialize(credentials);

            var platform = new ExchangePlatform
            {
                Name = name,
                Exchange = exchange,
                UseTestnet = useTestnet,
                CredentialsEncrypted = ApiCredentialProtector.Protect(json),
                CreatedUtc = DateTime.UtcNow
            };

            using var db = new AppDbContext();
            db.ExchangePlatforms.Add(platform);
            db.SaveChanges();
            return platform;
        }

        public void Delete(int id)
        {
            using var db = new AppDbContext();
            var platform = db.ExchangePlatforms.FirstOrDefault(p => p.Id == id);
            if (platform == null) return;
            db.ExchangePlatforms.Remove(platform);
            db.SaveChanges();
        }

        /// <summary>Decrypts a platform's credentials for immediate use. Do not persist the result.</summary>
        public ExchangeCredentials GetCredentials(int id)
        {
            using var db = new AppDbContext();
            var p = db.ExchangePlatforms.FirstOrDefault(x => x.Id == id)
                    ?? throw new InvalidOperationException("Platform not found.");

            var json = ApiCredentialProtector.Unprotect(p.CredentialsEncrypted);
            var dict = string.IsNullOrEmpty(json)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();

            return new ExchangeCredentials(dict, p.Exchange, p.UseTestnet);
        }
    }
}
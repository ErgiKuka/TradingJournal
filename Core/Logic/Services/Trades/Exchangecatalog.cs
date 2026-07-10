using System.Collections.Generic;

namespace TradingJournal.Core.Logic.Services.Trades
{
    public sealed class CredentialField
    {
        public string Key { get; }
        public string Label { get; }
        public bool Secret { get; }  

        public CredentialField(string key, string label, bool secret)
        {
            Key = key;
            Label = label;
            Secret = secret;
        }
    }

    /// <summary>
    /// Declares which credential fields each exchange needs, so the Platforms form can relabel /
    /// show / hide its generic input rows. Adding a new exchange = one case here (plus a client later).
    /// </summary>
    public static class ExchangeCatalog
    {
        public static readonly string[] Exchanges = { "Binance", "Bybit", "Kraken", "Hyperliquid" };

        public const int MaxFields = 2;

        public static IReadOnlyList<CredentialField> FieldsFor(string exchange)
        {
            switch ((exchange ?? string.Empty).Trim())
            {
                case "Hyperliquid":
                    return new[]
                    {
                        new CredentialField("WalletAddress", "Wallet address (0x…)", false),
                        new CredentialField("PrivateKey", "API / agent private key", true),
                    };

                default:
                    return new[]
                    {
                        new CredentialField("ApiKey", "API key", true),
                        new CredentialField("ApiSecret", "API secret", true),
                    };
            }
        }
    }
}
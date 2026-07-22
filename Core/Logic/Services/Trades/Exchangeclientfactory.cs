using System;
using System.Collections.Generic;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services.Trades;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Builds an <see cref="IExchangeClient"/> from decrypted credentials.
    ///
    /// Adding an exchange is now a single registration in <see cref="Builders"/> — no growing switch.
    /// <see cref="ExchangeCatalog"/> stays the source of truth for which exchanges exist and which
    /// credential fields they need; this factory only says which of them have a working client yet.
    /// </summary>
    public static class ExchangeClientFactory
    {
        // Exchange name -> how to build its client. Keys MUST match ExchangeCatalog.Exchanges.
        private static readonly Dictionary<string, Func<ExchangeCredentials, IExchangeClient>> Builders =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["Binance"] = c => new BinanceFuturesClient(c.ApiKey, c.ApiSecret, c.UseTestnet),
                ["Bybit"] = c => new BybitFuturesClient(c.ApiKey, c.ApiSecret, c.UseTestnet),

                // Not wired yet — see notes:
                ["Kraken"] = c => new KrakenFuturesClient(c.ApiKey, c.ApiSecret, c.UseTestnet),
                //     Kraken Futures uses a different scheme (base64 HMAC-SHA512 + nonce) and symbol
                //     naming (PF_XBTUSD…). Feasible, but a separate focused client.
                ["Hyperliquid"] = c => new HyperliquidClient(c.WalletAddress, c.PrivateKey, c.UseTestnet),
                //     Hyperliquid is an on-chain DEX: orders are signed with an Ethereum key via EIP-712
                //     action hashing, not an HMAC secret. Needs an Ethereum signing dependency
                //     (e.g. Nethereum) and exact replication of its action-signing scheme.
            };

        /// <summary>True if the given exchange has a working client. Use it to enable/disable Connect.</summary>
        public static bool IsSupported(string exchange) =>
            !string.IsNullOrWhiteSpace(exchange) && Builders.ContainsKey(exchange.Trim());

        /// <summary>The exchanges that can actually trade right now (for messages/UI).</summary>
        public static IReadOnlyCollection<string> SupportedExchanges => Builders.Keys;

        public static IExchangeClient Create(ExchangeCredentials creds)
        {
            if (creds == null) throw new ArgumentNullException(nameof(creds));

            var exchange = (creds.Exchange ?? string.Empty).Trim();
            if (exchange.Length == 0)
                throw new InvalidOperationException("This platform has no exchange set. Re-add it on the Platforms tab.");

            if (!Builders.TryGetValue(exchange, out var build))
            {
                var known = string.Join(", ", Builders.Keys);
                throw new NotSupportedException(
                    $"Trading for \"{exchange}\" isn't wired up yet. Working exchanges: {known}. " +
                    "You can still store its keys on the Platforms tab.");
            }

            RequireCredentials(exchange, creds);
            return build(creds);
        }

        // Fail early with a clear message if a required field is blank, instead of a cryptic API error later.
        private static void RequireCredentials(string exchange, ExchangeCredentials creds)
        {
            foreach (var field in ExchangeCatalog.FieldsFor(exchange))
                if (string.IsNullOrWhiteSpace(creds.Get(field.Key)))
                    throw new InvalidOperationException(
                        $"{exchange}: \"{field.Label}\" is missing. Re-add the platform on the Platforms tab.");
        }
    }
}
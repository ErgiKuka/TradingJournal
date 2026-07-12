using System;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Builds an <see cref="IExchangeClient"/> from decrypted credentials. Binance first;
    /// the others throw a clear "not supported yet" until their clients are added.
    /// </summary>
    public static class ExchangeClientFactory
    {
        public static IExchangeClient Create(ExchangeCredentials creds)
        {
            if (creds == null) throw new ArgumentNullException(nameof(creds));

            switch ((creds.Exchange ?? string.Empty).Trim())
            {
                case "Binance":
                    return new BinanceFuturesClient(creds.ApiKey, creds.ApiSecret, creds.UseTestnet);

                default:
                    throw new NotSupportedException(
                        $"{creds.Exchange} isn't wired up yet — Binance is supported first.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace TradingJournal.Core.Logic.Services
{
    public class CryptoData
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal Volume { get; set; }
        public decimal MarketCap { get; set; }
        public decimal High24h { get; set; }
        public decimal Low24h { get; set; }
        public string IconUrl { get; set; }
        public string FormattedPrice => FormatPrice(Price);
        public string FormattedVolume => FormatVolume(Volume);
        public string FormattedMarketCap => FormatVolume(MarketCap);

        private string FormatPrice(decimal price)
        {
            if (price >= 1000M) // Added M
                return $"${price:N0}";
            else if (price >= 1M) // Added M
                return $"${price:F2}";
            else if (price >= 0.01M) // Added M
                return $"${price:F4}";
            else if (price >= 0.0001M) // Added M
                return $"${price:F6}";
            else
                return $"${price:F8}";
        }

        private string FormatVolume(decimal volume)
        {
            if (volume >= 1000000000M) // Added M
                return $"${volume / 1000000000M:F1}B"; // Added M
            else if (volume >= 1000000M) // Added M
                return $"${volume / 1000000M:F0}M"; // Added M
            else if (volume >= 1000M) // Added M
                return $"${volume / 1000M:F0}K"; // Added M
            else
                return $"${volume:F2}";
        }
    }

    public class BinanceApiService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string BINANCE_API_URL = "https://api.binance.com/api/v3/ticker/24hr";

        // Top cryptocurrencies by market cap (more comprehensive list )
        private static readonly Dictionary<string, string> CryptoNames = new Dictionary<string, string>
        {
            {"BTCUSDT", "Bitcoin"},
            {"ETHUSDT", "Ethereum"},
            {"BNBUSDT", "BNB"},
            {"SOLUSDT", "Solana"},
            {"XRPUSDT", "XRP"},
            {"ADAUSDT", "Cardano"},
            {"DOGEUSDT", "Dogecoin"},
            {"AVAXUSDT", "Avalanche"},
            {"SHIBUSDT", "Shiba Inu"},
            {"DOTUSDT", "Polkadot"},
            {"MATICUSDT", "Polygon"},
            {"LTCUSDT", "Litecoin"},
            {"UNIUSDT", "Uniswap"},
            {"LINKUSDT", "Chainlink"},
            {"ATOMUSDT", "Cosmos"},
            {"VETUSDT", "VeChain"},
            {"FILUSDT", "Filecoin"},
            {"TRXUSDT", "TRON"},
            {"ETCUSDT", "Ethereum Classic"},
            {"XLMUSDT", "Stellar"},
            {"NEARUSDT", "NEAR Protocol"},
            {"ALGOUSDT", "Algorand"},
            {"HBARUSDT", "Hedera"},
            {"ICPUSDT", "Internet Computer"},
            {"APTUSDT", "Aptos"},
            {"SUIUSDT", "Sui"},
            {"ARBUSDT", "Arbitrum"},
            {"OPUSDT", "Optimism"},
            {"INJUSDT", "Injective"},
            {"PEPEUSDT", "Pepe"},
            {"WIFUSDT", "dogwifhat"},
            {"BONKUSDT", "Bonk"},
            {"FLOKIUSDT", "Floki"},
            {"RNDRUSDT", "Render Token"},
            {"FETUSDT", "Fetch.ai"},
            {"GRTUSDT", "The Graph"},
            {"SANDUSDT", "The Sandbox"},
            {"MANAUSDT", "Decentraland"},
            {"CHZUSDT", "Chiliz"},
            {"ENJUSDT", "Enjin Coin"}
        };

        public async Task<List<CryptoData>> GetTopCryptosAsync()
        {
            try
            {
                var response = await httpClient.GetStringAsync(BINANCE_API_URL);
                var binanceData = JsonConvert.DeserializeObject<List<BinanceTicker>>(response);

                var cryptoList = new List<CryptoData>();

                foreach (var ticker in binanceData)
                {
                    if (CryptoNames.ContainsKey(ticker.symbol))
                    {
                        var price = decimal.Parse(ticker.lastPrice);
                        var volume = decimal.Parse(ticker.volume);
                        var quoteVolume = decimal.Parse(ticker.quoteVolume);

                        cryptoList.Add(new CryptoData
                        {
                            Symbol = ticker.symbol,
                            Name = CryptoNames[ticker.symbol],
                            Price = price,
                            PriceChangePercent = decimal.Parse(ticker.priceChangePercent),
                            Volume = volume,
                            MarketCap = quoteVolume, // Using quote volume as market cap proxy
                            High24h = decimal.Parse(ticker.highPrice),
                            Low24h = decimal.Parse(ticker.lowPrice),
                            IconUrl = GetCryptoIconUrl(ticker.symbol)
                        });
                    }
                }

                // Sort by quote volume (market cap proxy) descending
                return cryptoList.OrderByDescending(c => c.MarketCap).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching crypto data: {ex.Message}");
            }
        }

        private string GetCryptoIconUrl(string symbol)
        {
            var baseSymbol = symbol.Replace("USDT", "").ToLower();
            return $"https://cryptoicons.org/api/icon/{baseSymbol}/200";
        }

        public List<string> GetAllSymbols()
        {
            return new List<string>(CryptoNames.Keys);
        }
    }

    public class BinanceTicker
    {
        public string symbol { get; set; }
        public string lastPrice { get; set; }
        public string priceChangePercent { get; set; }
        public string volume { get; set; }
        public string quoteVolume { get; set; }
        public string highPrice { get; set; }
        public string lowPrice { get; set; }
    }
}

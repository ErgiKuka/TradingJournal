using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            if (price >= 1000M) return $"${price:N0}";
            else if (price >= 1M) return $"${price:F2}";
            else if (price >= 0.01M) return $"${price:F4}";
            else if (price >= 0.0001M) return $"${price:F6}";
            else return $"${price:F8}";
        }

        private string FormatVolume(decimal volume)
        {
            if (volume >= 1_000_000_000M) return $"${volume / 1_000_000_000M:F1}B";
            else if (volume >= 1_000_000M) return $"${volume / 1_000_000M:F0}M";
            else if (volume >= 1_000M) return $"${volume / 1_000M:F0}K";
            else return $"${volume:F2}";
        }
    }

    public class BinanceApiService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string TICKER_24H = "https://api.binance.com/api/v3/ticker/24hr";
        private const string TICKER_PRICE = "https://api.binance.com/api/v3/ticker/price";

        // (kept) names map used by your dashboard
        private static readonly Dictionary<string, string> CryptoNames = new()
        {
            {"BTCUSDT","Bitcoin"},{"ETHUSDT","Ethereum"},{"BNBUSDT","BNB"},{"SOLUSDT","Solana"},
            {"XRPUSDT","XRP"},{"ADAUSDT","Cardano"},{"DOGEUSDT","Dogecoin"},{"AVAXUSDT","Avalanche"},
            {"SHIBUSDT","Shiba Inu"},{"DOTUSDT","Polkadot"},{"MATICUSDT","Polygon"},{"LTCUSDT","Litecoin"},
            {"UNIUSDT","Uniswap"},{"LINKUSDT","Chainlink"},{"ATOMUSDT","Cosmos"},{"VETUSDT","VeChain"},
            {"FILUSDT","Filecoin"},{"TRXUSDT","TRON"},{"ETCUSDT","Ethereum Classic"},{"XLMUSDT","Stellar"},
            {"NEARUSDT","NEAR Protocol"},{"ALGOUSDT","Algorand"},{"HBARUSDT","Hedera"},
            {"ICPUSDT","Internet Computer"},{"APTUSDT","Aptos"},{"SUIUSDT","Sui"},{"ARBUSDT","Arbitrum"},
            {"OPUSDT","Optimism"},{"INJUSDT","Injective"},{"PEPEUSDT","Pepe"},{"WIFUSDT","dogwifhat"},
            {"BONKUSDT","Bonk"},{"FLOKIUSDT","Floki"},{"RNDRUSDT","Render Token"},{"FETUSDT","Fetch.ai"},
            {"GRTUSDT","The Graph"},{"SANDUSDT","The Sandbox"},{"MANAUSDT","Decentraland"},
            {"CHZUSDT","Chiliz"},{"ENJUSDT","Enjin Coin"}
        };

        // ---------- NEW: simple last price for one symbol ----------
        public async Task<decimal> GetLastPriceAsync(string symbol)
        {
            // /api/v3/ticker/price?symbol=BTCUSDT
            var url = $"{TICKER_PRICE}?symbol={symbol}";
            var json = await httpClient.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<PriceDto>(json);
            if (obj?.price == null) return 0m;
            return decimal.TryParse(obj.price, out var d) ? d : 0m;
        }

        // ---------- NEW: last prices for many symbols ----------
        public async Task<Dictionary<string, decimal>> GetLastPricesAsync(IEnumerable<string> symbols)
        {
            // /api/v3/ticker/price returns ALL; we filter in-memory
            var json = await httpClient.GetStringAsync(TICKER_PRICE);
            var arr = JsonConvert.DeserializeObject<List<PriceDto>>(json) ?? new();
            var set = new HashSet<string>(symbols ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            return arr
                .Where(x => x.symbol != null && set.Contains(x.symbol))
                .Select(x => new { x.symbol, Price = decimal.TryParse(x.price, out var d) ? d : 0m })
                .ToDictionary(x => x.symbol, x => x.Price, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<List<CryptoData>> GetTopCryptosAsync()
        {
            try
            {
                var response = await httpClient.GetStringAsync(TICKER_24H);
                var binanceData = JsonConvert.DeserializeObject<List<BinanceTicker>>(response) ?? new();
                var cryptoList = new List<CryptoData>();

                foreach (var ticker in binanceData)
                {
                    if (ticker?.symbol != null && CryptoNames.ContainsKey(ticker.symbol))
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
                            MarketCap = quoteVolume, // proxy
                            High24h = decimal.Parse(ticker.highPrice),
                            Low24h = decimal.Parse(ticker.lowPrice),
                            IconUrl = GetCryptoIconUrl(ticker.symbol)
                        });
                    }
                }
                return cryptoList.OrderByDescending(c => c.MarketCap).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching crypto data: {ex.Message}");
            }
        }

        public List<string> GetAllSymbols() => new List<string>(CryptoNames.Keys);

        private string GetCryptoIconUrl(string symbol)
        {
            var baseSymbol = symbol.Replace("USDT", "").ToLowerInvariant();
            return $"https://cryptoicons.org/api/icon/{baseSymbol}/200";
        }

        private sealed class PriceDto { public string symbol { get; set; } public string price { get; set; } }
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

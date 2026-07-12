using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    public enum OrderSide { Buy, Sell }           
    public enum OrderKind { Market, Limit, Conditional }
    public enum MarginMode { Cross, Isolated }

    public class OrderRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public OrderSide Side { get; set; }
        public OrderKind Kind { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TriggerPrice { get; set; }
        public bool ReduceOnly { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
    }

    public class OrderResult
    {
        public bool Success { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public static OrderResult Ok(string id) => new OrderResult { Success = true, OrderId = id };
        public static OrderResult Fail(string message) => new OrderResult { Success = false, Message = message };
    }

    public class AccountBalance
    {
        public decimal AvailableUsdt { get; set; }
        public decimal WalletUsdt { get; set; }
    }

    public class TickerPrice
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class SymbolRules
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal StepSize { get; set; }
        public decimal TickSize { get; set; }
        public decimal MinNotional { get; set; }
    }

    public class LeverageTier
    {
        public int Bracket { get; set; }
        public int MaxLeverage { get; set; }        // Binance "initialLeverage"
        public decimal NotionalFloor { get; set; }
        public decimal NotionalCap { get; set; }
    }

    public class LeverageBracket
    {
        public string Symbol { get; set; } = string.Empty;

        /// <summary>Tiers ascending by notional floor (tier 1 = smallest size, highest leverage).</summary>
        public IReadOnlyList<LeverageTier> Tiers { get; set; } = new List<LeverageTier>();

        /// <summary>Highest leverage the symbol allows (tier 1).</summary>
        public int MaxLeverage => Tiers.Count > 0 ? Tiers[0].MaxLeverage : 1;

        /// <summary>Largest position notional (USDT) allowed while keeping the given leverage.</summary>
        public decimal MaxNotionalAt(int leverage)
        {
            decimal cap = 0m;
            foreach (var t in Tiers)
                if (t.MaxLeverage >= leverage && t.NotionalCap > cap) cap = t.NotionalCap;
            return cap;
        }

        /// <summary>Highest leverage still permitted for a position of the given notional.</summary>
        public int MaxLeverageForNotional(decimal notional)
        {
            foreach (var t in Tiers)
                if (notional > t.NotionalFloor && notional <= t.NotionalCap) return t.MaxLeverage;
            return Tiers.Count > 0 ? Tiers[Tiers.Count - 1].MaxLeverage : 1;
        }
    }
    public class PositionInfo
    {
        public string Symbol { get; set; } = string.Empty;
        public OrderSide Side { get; set; }
        public decimal Quantity { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal UnrealizedPnl { get; set; }
        public decimal LiquidationPrice { get; set; }   // <-- added
        public decimal Margin { get; set; }             // <-- added (margin allocated, USDT)
        public decimal Leverage { get; set; }
        public MarginMode MarginMode { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
    }

    public interface IExchangeClient
    {
        string Exchange { get; }

        Task<AccountBalance> GetBalanceAsync();
        Task<TickerPrice> GetPriceAsync(string symbol);
        Task<IReadOnlyList<string>> GetSymbolsAsync();
        Task<SymbolRules> GetSymbolRulesAsync(string symbol);

        Task SetLeverageAsync(string symbol, int leverage, MarginMode mode);
        Task<OrderResult> PlaceOrderAsync(OrderRequest request);

        Task<IReadOnlyList<PositionInfo>> GetPositionsAsync();
        Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity);
        Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit);

        Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc);

        Task<LeverageBracket> GetLeverageBracketAsync(string symbol);
    }
}
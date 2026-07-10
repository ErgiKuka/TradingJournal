using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services; // ClosedTrade

namespace TradingJournal.Core.Logic.Services.Exchange
{
    public enum OrderSide { Buy, Sell }              // Buy = Long, Sell = Short (for opening)
    public enum OrderKind { Market, Limit }
    public enum MarginMode { Cross, Isolated }

    /// <summary>An order the user wants to place. Validated by the manager before it reaches a client.</summary>
    public class OrderRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public OrderSide Side { get; set; }
        public OrderKind Kind { get; set; }
        public decimal Quantity { get; set; }       // in coin/contract units
        public decimal? Price { get; set; }          // required for Limit, ignored for Market
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
        public decimal AvailableUsdt { get; set; }   // usable margin for new orders
        public decimal WalletUsdt { get; set; }      // total wallet balance
    }

    public class TickerPrice
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    /// <summary>A live position, for the positions grid.</summary>
    public class PositionInfo
    {
        public string Symbol { get; set; } = string.Empty;
        public OrderSide Side { get; set; }          // net direction (Buy = Long, Sell = Short)
        public decimal Quantity { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal UnrealizedPnl { get; set; }
        public decimal Leverage { get; set; }
        public MarginMode MarginMode { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
    }

    /// <summary>
    /// Everything the Trade UC needs from an exchange. One implementation per exchange
    /// (BinanceFuturesClient first). All methods are async — network I/O must never block the UI thread.
    /// </summary>
    public interface IExchangeClient
    {
        string Exchange { get; }

        Task<AccountBalance> GetBalanceAsync();
        Task<TickerPrice> GetPriceAsync(string symbol);
        Task<IReadOnlyList<string>> GetSymbolsAsync();

        Task SetLeverageAsync(string symbol, int leverage, MarginMode mode);
        Task<OrderResult> PlaceOrderAsync(OrderRequest request);

        // Live view — shows ALL positions on the account, however they were opened.
        Task<IReadOnlyList<PositionInfo>> GetPositionsAsync();
        Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity); // null = close all
        Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit);

        // Reconcile — recent closed trades for auto-journaling (deduped by ExternalId downstream).
        Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc);
    }
}
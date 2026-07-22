using System;
using System.Collections.Generic;
using TradingJournal.Core.Logic.Services;   // ClosedTrade

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>One executed fill, normalized across exchanges. Fed to <see cref="ClosedTradeBuilder"/>.</summary>
    public sealed class ExchangeFill
    {
        public string Symbol { get; set; } = string.Empty;
        public bool IsBuy { get; set; }
        public decimal Quantity { get; set; }        // > 0, in base asset
        public decimal Price { get; set; }
        public decimal RealizedPnl { get; set; }     // exchange-reported realized PnL for this fill (0 on opens)
        public string TradeId { get; set; } = string.Empty;
        public DateTime TimeUtc { get; set; }
    }

    /// <summary>
    /// Folds a time-ordered stream of fills for ONE symbol into completed round-trip trades.
    ///
    /// Model: walk fills oldest → newest keeping a signed net position. Fills in the position's
    /// direction add to it (and to a cost basis); opposite fills reduce it. When net returns to zero
    /// the position is closed and one <see cref="ClosedTrade"/> is emitted. A fill that overshoots
    /// (flips the sign) closes the old lot and opens a new one with the remainder.
    ///
    /// Assumes one-way mode (no simultaneous long+short on the same symbol) — the same assumption the
    /// rest of the client makes by reading a single signed position size. Partial scale-in / scale-out
    /// inside a position are aggregated into that single round-trip entry (VWAP in / VWAP out).
    ///
    /// Recoverable from fills: symbol, side, VWAP entry/exit, realized PnL, close time, a stable id.
    /// NOT recoverable: the leverage/margin used — so <see cref="ClosedTrade.Margin"/> is set to the
    /// entry *notional* (VWAP × size). Treat imported ROI as return-on-notional, not return-on-margin.
    /// </summary>
    public static class ClosedTradeBuilder
    {
        public static IReadOnlyList<ClosedTrade> Fold(IEnumerable<ExchangeFill> fillsOldestFirst)
        {
            var result = new List<ClosedTrade>();
            if (fillsOldestFirst == null) return result;

            decimal netQty = 0m;        // signed net position: + long, - short
            bool longSide = false;      // direction of the currently-open lot
            decimal entryQtyAbs = 0m;   // total base units opened into the current lot
            decimal entryCost = 0m;     // Σ price*qty of the opening fills
            decimal exitQtyAbs = 0m;    // total base units closed out of the current lot
            decimal exitProceeds = 0m;  // Σ price*qty of the closing fills
            decimal pnlAccum = 0m;      // Σ exchange realized PnL of the closing fills
            string lastCloseId = string.Empty;
            DateTime closeTime = default;

            void ResetLot()
            {
                entryQtyAbs = entryCost = exitQtyAbs = exitProceeds = pnlAccum = 0m;
                lastCloseId = string.Empty;
                closeTime = default;
            }

            foreach (var f in fillsOldestFirst)
            {
                if (f == null || f.Quantity <= 0m) continue;
                decimal d = f.IsBuy ? f.Quantity : -f.Quantity;   // signed delta

                // Fresh lot.
                if (netQty == 0m)
                {
                    longSide = f.IsBuy;
                    entryQtyAbs = f.Quantity;
                    entryCost = f.Price * f.Quantity;
                    netQty = d;
                    continue;
                }

                // Same direction -> scale in.
                if (Math.Sign(d) == Math.Sign(netQty))
                {
                    entryQtyAbs += f.Quantity;
                    entryCost += f.Price * f.Quantity;
                    netQty += d;
                    continue;
                }

                // Opposite direction -> reduce / close / flip.
                decimal closingQty = Math.Min(f.Quantity, Math.Abs(netQty));
                exitQtyAbs += closingQty;
                exitProceeds += f.Price * closingQty;
                pnlAccum += f.RealizedPnl;   // Binance/Bybit report realized PnL only on the reducing portion
                lastCloseId = f.TradeId;
                closeTime = f.TimeUtc;

                decimal newNet = netQty + d;

                if (newNet == 0m)
                {
                    result.Add(Emit(f.Symbol, longSide, entryQtyAbs, entryCost, exitQtyAbs, exitProceeds, pnlAccum, lastCloseId, closeTime));
                    ResetLot();
                    netQty = 0m;
                }
                else if (Math.Sign(newNet) == Math.Sign(netQty))
                {
                    // Still open in the same direction, just smaller.
                    netQty = newNet;
                }
                else
                {
                    // Sign flip: this fill closed the old lot and opens a new one with the remainder.
                    result.Add(Emit(f.Symbol, longSide, entryQtyAbs, entryCost, exitQtyAbs, exitProceeds, pnlAccum, lastCloseId, closeTime));
                    ResetLot();

                    decimal remainder = f.Quantity - closingQty;
                    longSide = f.IsBuy;
                    entryQtyAbs = remainder;
                    entryCost = f.Price * remainder;
                    netQty = newNet;
                }
            }

            return result;
        }

        private static ClosedTrade Emit(string symbol, bool longSide,
            decimal entryQtyAbs, decimal entryCost, decimal exitQtyAbs, decimal exitProceeds,
            decimal pnl, string closeId, DateTime closeUtc)
        {
            decimal entryVwap = entryQtyAbs > 0 ? entryCost / entryQtyAbs : 0m;
            decimal exitVwap = exitQtyAbs > 0 ? exitProceeds / exitQtyAbs : 0m;

            return new ClosedTrade
            {
                Symbol = symbol,
                IsLong = longSide,
                EntryPrice = entryVwap,
                ExitPrice = exitVwap,
                Margin = entryVwap * exitQtyAbs,   // notional at entry; leverage/margin isn't in fills
                RealizedPnl = pnl,
                ExternalId = string.IsNullOrEmpty(closeId) ? null : $"{symbol}:{closeId}",
                ClosedAtUtc = closeUtc == default ? DateTime.UtcNow : closeUtc
            };
        }
    }
}
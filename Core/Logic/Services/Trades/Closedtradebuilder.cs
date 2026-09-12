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

        /// <summary>
        /// Exchange-reported realized PnL for this fill (0 on opens). On Binance USDT-M this is the
        /// GROSS figure — it excludes commission, which arrives in <see cref="Commission"/>.
        /// </summary>
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Trading fee charged on this fill, as a POSITIVE amount in the quote currency (USDT).
        /// Leave 0 when the exchange charges in another asset and no conversion rate is available —
        /// a wrong fee is worse than a missing one, because it silently poisons every derived metric.
        /// </summary>
        public decimal Commission { get; set; }

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
    /// Assumes one-way position mode (no simultaneous long+short on the same symbol) — the same
    /// assumption the rest of the client makes by reading a single signed position size. Partial
    /// scale-in / scale-out inside a position are aggregated into that single round-trip entry.
    ///
    /// FEES: the emitted RealizedPnl is NET of commission — gross exchange PnL minus the commission
    /// of every fill in the lot, opening fills included. The previous version summed only
    /// f.RealizedPnl, which on Binance is gross, so journal entries overstated profit by roughly
    /// 0.1% of round-trip notional on every trade. Verified against three Binance positions where
    /// the discrepancy matched commission to the cent.
    ///
    /// NOT accounted for: funding. On a perpetual held across funding intervals the exchange's own
    /// "Realized PNL" also nets funding, which lives in a separate income feed keyed by time rather
    /// than by fill. Positions held for minutes are unaffected; multi-hour holds will still differ
    /// from the exchange UI by the funding amount.
    ///
    /// NOT recoverable from fills: the leverage/margin used — so <see cref="ClosedTrade.Margin"/> is
    /// set to the entry *notional* (VWAP × size). Treat imported ROI as return-on-notional.
    /// </summary>
    public static class ClosedTradeBuilder
    {
        /// <summary>
        /// VWAP is a division and produces full decimal scale (28+ significant digits), which is both
        /// meaningless as a price and wider than the decimal(18,4) column it lands in. 8 decimals is
        /// past the tick size of every USDT pair.
        /// </summary>
        private const int PriceDecimals = 8;

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
            decimal pnlAccum = 0m;      // Σ exchange realized PnL of the closing fills (GROSS)
            decimal feeAccum = 0m;      // Σ commission of every fill in the lot, opens included
            string lastCloseId = string.Empty;
            DateTime closeTime = default;

            void ResetLot()
            {
                entryQtyAbs = entryCost = exitQtyAbs = exitProceeds = pnlAccum = feeAccum = 0m;
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
                    feeAccum = f.Commission;
                    netQty = d;
                    continue;
                }

                // Same direction -> scale in.
                if (Math.Sign(d) == Math.Sign(netQty))
                {
                    entryQtyAbs += f.Quantity;
                    entryCost += f.Price * f.Quantity;
                    feeAccum += f.Commission;
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

                // A flipping fill pays fees for both the part that closes the old lot and the part
                // that opens the new one; split the commission by quantity rather than charging it
                // all to whichever lot happens to be convenient.
                decimal closingFee = f.Quantity > 0m ? f.Commission * closingQty / f.Quantity : 0m;
                feeAccum += closingFee;

                if (newNet == 0m)
                {
                    result.Add(Emit(f.Symbol, longSide, entryQtyAbs, entryCost, exitQtyAbs, exitProceeds,
                        pnlAccum, feeAccum, lastCloseId, closeTime));
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
                    result.Add(Emit(f.Symbol, longSide, entryQtyAbs, entryCost, exitQtyAbs, exitProceeds,
                        pnlAccum, feeAccum, lastCloseId, closeTime));
                    ResetLot();

                    decimal remainder = f.Quantity - closingQty;
                    longSide = f.IsBuy;
                    entryQtyAbs = remainder;
                    entryCost = f.Price * remainder;
                    feeAccum = f.Commission - closingFee;   // the opening share of this fill's fee
                    netQty = newNet;
                }
            }

            return result;
        }

        private static ClosedTrade Emit(string symbol, bool longSide,
            decimal entryQtyAbs, decimal entryCost, decimal exitQtyAbs, decimal exitProceeds,
            decimal grossPnl, decimal fees, string closeId, DateTime closeUtc)
        {
            decimal entryVwap = entryQtyAbs > 0 ? Round(entryCost / entryQtyAbs) : 0m;
            decimal exitVwap = exitQtyAbs > 0 ? Round(exitProceeds / exitQtyAbs) : 0m;

            return new ClosedTrade
            {
                Symbol = symbol,
                IsLong = longSide,
                EntryPrice = entryVwap,
                ExitPrice = exitVwap,
                Margin = Round(entryVwap * exitQtyAbs),   // notional at entry; leverage isn't in fills
                RealizedPnl = grossPnl - fees,
                ExternalId = string.IsNullOrEmpty(closeId) ? null : $"{symbol}:{closeId}",
                ClosedAtUtc = closeUtc == default ? DateTime.UtcNow : closeUtc
            };
        }

        private static decimal Round(decimal value) =>
            Math.Round(value, PriceDecimals, MidpointRounding.ToEven);
    }
}
using System;
using System.Collections.Generic;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Logic.RecycleBin
{
    // Flatten payloads so schema changes in EF entities don't break restore
    public record TradePayload(
        int Id, string Symbol, string TradeType, decimal EntryPrice, decimal ExitPrice,
        decimal StopLoss, decimal TakeProfit, decimal Margin, decimal ProfitLoss,
        DateTime Date, string? ScreenshotLink, byte[]? ScreenshotImage);

    public record AllocationPayload(
        int Id, int RecoveryCaseId, DateTime TradeDate, decimal EntryPrice,
        decimal? InvestedUSDT, decimal? Quantity);

    public record CaseWithAllocationsPayload(
        RecoveryCase Case,
        List<AllocationPayload> Allocations
    );

    public record AccountTransactionPayload(
        DateTime Date, int Type, decimal Amount, string? Note);
}

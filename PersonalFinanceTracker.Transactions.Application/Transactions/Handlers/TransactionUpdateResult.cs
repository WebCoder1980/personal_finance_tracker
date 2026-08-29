namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionUpdateResult(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment);
}

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionGetResult(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment);
}

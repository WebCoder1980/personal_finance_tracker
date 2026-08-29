namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionCreateResult(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment);
}

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionUpdateCommand(Guid Id, Guid UserId, DateOnly Date, double Value, string? Comment);
}

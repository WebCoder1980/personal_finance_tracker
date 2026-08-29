namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionDeleteCommand(Guid Id, Guid UserId);
}

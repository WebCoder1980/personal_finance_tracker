namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryDeleteCommand(Guid Id, Guid UserId);
}

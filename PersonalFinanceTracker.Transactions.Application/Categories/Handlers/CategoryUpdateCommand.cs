namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryUpdateCommand(Guid Id, Guid UserId, string Name, double? MonthlyAmount);
}

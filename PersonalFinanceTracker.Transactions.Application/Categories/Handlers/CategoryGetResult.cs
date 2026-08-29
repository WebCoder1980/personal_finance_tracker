using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryGetResult(Guid Id, Guid UserId, string Name, CategoryType Type, double? MonthlyAmount);
}

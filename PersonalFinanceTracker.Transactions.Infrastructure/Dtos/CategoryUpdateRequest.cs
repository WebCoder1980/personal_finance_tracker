using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Dtos
{
    public record CategoryUpdateRequest([Length(1, 200)] string Name, [Range(0, double.MaxValue)] double MonthlyAmount);
}

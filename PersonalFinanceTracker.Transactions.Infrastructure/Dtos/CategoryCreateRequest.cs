using PersonalFinanceTracker.Transactions.Domain;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Dtos
{
    public record CategoryCreateRequest([Length(1, 200)] string Name, CategoryType Type, [Range(0, double.MaxValue)] double MonthlyAmount);
}

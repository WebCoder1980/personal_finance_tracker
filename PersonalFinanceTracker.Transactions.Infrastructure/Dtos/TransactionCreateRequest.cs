using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Dtos
{
    public record TransactionCreateRequest(Guid CategoryId, DateOnly Date, [Range(0, double.MaxValue)] double Value, string? Comment);
}

using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Dtos
{
    public record TransactionUpdateRequest(DateOnly Date, [Range(0, double.MaxValue)] double Value, string? Comment);
}

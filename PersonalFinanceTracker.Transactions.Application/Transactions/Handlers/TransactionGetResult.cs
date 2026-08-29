using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionGetResult(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment);
}

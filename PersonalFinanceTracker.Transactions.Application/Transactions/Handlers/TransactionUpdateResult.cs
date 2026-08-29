using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public record TransactionUpdateResult(Guid Id, Guid CategoryId, DateOnly Date, double Value, string? Comment);
}

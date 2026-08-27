using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryUpdateResult(Guid Id, Guid UserId, string Name, CategoryType Type, double? MonthlyAmount);
}

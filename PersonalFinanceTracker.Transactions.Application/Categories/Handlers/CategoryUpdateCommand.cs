using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryUpdateCommand(Guid Id, Guid UserId, string Name, double? MonthlyAmount);
}

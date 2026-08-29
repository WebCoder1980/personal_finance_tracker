using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryCreateCommand(Guid Id, Guid UserId, string Name, CategoryType Type, double? MonthlyAmount)
    {
        public CategoryCreateCommand(Guid userId, string name, CategoryType type, double? MonthlyAmount) : this(Guid.NewGuid(), userId, name, type, null)
        {

        }
    }
}

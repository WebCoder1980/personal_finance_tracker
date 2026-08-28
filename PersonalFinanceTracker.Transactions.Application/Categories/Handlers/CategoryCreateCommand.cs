using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public record CategoryCreateCommand(Guid UserId, string Name, CategoryType Type, double? MonthlyAmount)
    {
        public CategoryCreateCommand(Guid userId, string name, CategoryType type) : this(userId, name, type, null)
        {

        }
    }
}

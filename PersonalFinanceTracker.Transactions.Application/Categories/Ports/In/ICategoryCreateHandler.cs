using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryCreateHandler
    {
        Task<Category> ExecuteAsync(Guid UserId, string Name, CategoryType Type, double MonthlyAmount, CancellationToken token);
    }
}

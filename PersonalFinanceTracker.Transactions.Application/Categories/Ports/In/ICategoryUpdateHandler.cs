using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryUpdateHandler
    {
        Task<Category> ExecuteAsync(Guid Id, Guid UserId, string Name, double? MonthlyAmount, CancellationToken token);
    }
}

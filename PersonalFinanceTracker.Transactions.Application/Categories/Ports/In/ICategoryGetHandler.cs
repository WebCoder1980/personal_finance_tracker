using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryGetHandler
    {
        Task<IEnumerable<Category>> ExecuteAsync(Guid UserId, CancellationToken token);
    }
}

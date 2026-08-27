using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryGetHandler
    {
        Task<IEnumerable<CategoryGetResult>> ExecuteAsync(CategoryGetCommand command, CancellationToken token);
    }
}

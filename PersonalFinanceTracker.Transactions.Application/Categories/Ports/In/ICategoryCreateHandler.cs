using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryCreateHandler
    {
        Task<CategoryCreateResult> ExecuteAsync(CategoryCreateCommand command, CancellationToken token);
    }
}

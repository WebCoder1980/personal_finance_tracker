using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryDeleteHandler
    {
        Task ExecuteAsync(CategoryDeleteCommand command, CancellationToken token);
    }
}

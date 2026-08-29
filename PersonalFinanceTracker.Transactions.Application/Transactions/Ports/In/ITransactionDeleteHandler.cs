using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionDeleteHandler
    {
        Task ExecuteAsync(TransactionDeleteCommand command, CancellationToken token);
    }
}

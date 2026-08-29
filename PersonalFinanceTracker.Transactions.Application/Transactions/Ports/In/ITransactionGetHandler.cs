using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionGetHandler
    {
        Task<IEnumerable<TransactionGetResult>> ExecuteAsync(TransactionGetCommand command, CancellationToken token);
    }
}

using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionUpdateHandler
    {
        Task<TransactionUpdateResult> ExecuteAsync(TransactionUpdateCommand command, CancellationToken token);
    }
}

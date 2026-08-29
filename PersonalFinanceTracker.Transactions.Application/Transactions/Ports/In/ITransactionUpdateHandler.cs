using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionUpdateHandler
    {
        Task<TransactionUpdateResult> ExecuteAsync(TransactionUpdateCommand command, CancellationToken token);
    }
}

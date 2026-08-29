using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionDeleteHandler
    {
        Task ExecuteAsync(TransactionDeleteCommand command, CancellationToken token);
    }
}

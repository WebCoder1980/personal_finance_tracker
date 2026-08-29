using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionGetHandler
    {
        Task<IEnumerable<TransactionGetResult>> ExecuteAsync(TransactionGetCommand command, CancellationToken token);
    }
}

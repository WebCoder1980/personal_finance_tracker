using PersonalFinanceTracker.Transactions.Application.Transactions.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In
{
    public interface ITransactionCreateHandler
    {
        Task<TransactionCreateResult> ExecuteAsync(TransactionCreateCommand command, CancellationToken token);
    }
}

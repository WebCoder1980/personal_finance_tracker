using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Handlers
{
    public class TransactionGetHandler : ITransactionGetHandler
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionGetHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionGetResult>> ExecuteAsync(TransactionGetCommand command, CancellationToken token)
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetByUserIdAsync(command.UserId, token);

            return transactions.Select(t => new TransactionGetResult(t.Id, t.CategoryId, t.Date, t.Value, t.Comment));
        }
    }
}

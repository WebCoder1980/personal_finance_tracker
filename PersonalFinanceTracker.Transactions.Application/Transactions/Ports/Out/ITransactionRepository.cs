using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId, CancellationToken token);
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token);
        Task<bool> IsEmpty(CancellationToken token);

        Task SaveAsync(Transaction transaction, CancellationToken token);

        void DeleteAsync(Transaction transaction);
    }
}

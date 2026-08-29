using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Transactions.Application.Transactions.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId, CancellationToken token) => await _db.Transactions.Where(transaction => transaction.Category.UserId == userId).ToListAsync(token);
        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token) => await _db.Transactions.Where(transaction => transaction.Id == id).FirstOrDefaultAsync(token);
        public async Task<bool> IsEmpty(CancellationToken token) => !await _db.Transactions.AnyAsync(token);

        public async Task SaveAsync(Transaction transaction, CancellationToken token) => await _db.Transactions.AddAsync(transaction, token);

        public void DeleteAsync(Transaction transaction) => _db.Transactions.Remove(transaction);
    }
}

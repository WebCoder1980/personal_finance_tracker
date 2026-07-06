using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Transactions.Data
{
    public class TransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            return await _db.Transactions
                .AsNoTracking()
                .Include(transaction => transaction.Category)
                .Where(transaction => transaction.UserId == userId)
                .ToListAsync(cancellationToken);
        }
        public async Task<Transaction> GetByIdAndUserIdAsync(long id, long userId)
        {
            return await _db.Transactions
                .Include(transaction => transaction.Category)
                .FirstOrDefaultAsync(transaction => transaction.Id == id && transaction.UserId == userId)
                ?? throw new InvalidOperationException($"Transaction with Id = {id} not found.");
        }

        public async Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await _db.Transactions.AddAsync(transaction, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return transaction;
        }
        public async Task<Transaction> UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync(cancellationToken);
            return transaction;
        }
        public async Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync(cancellationToken);
        }
        
    }
}

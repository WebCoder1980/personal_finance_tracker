using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public Task<int> SaveChangesAsync(CancellationToken token) => _db.SaveChangesAsync(token);
    }
}

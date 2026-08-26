using PersonalFinanceTracker.Users.Application.Ports.Out;

namespace PersonalFinanceTracker.Users.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync(CancellationToken token) => await _db.SaveChangesAsync(token);
    }
}

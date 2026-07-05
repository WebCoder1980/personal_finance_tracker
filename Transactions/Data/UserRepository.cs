using Microsoft.EntityFrameworkCore;
using Transactions.Models;

namespace Transactions.Data
{
    public class UserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> ExistsByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            return await _db.Users
                .AsNoTracking()
                .AnyAsync(user => user.Id == userId, cancellationToken);
        }
        public async Task<UserReference> CreateAsync(UserReference user, CancellationToken cancellationToken)
        {
            await _db.Users.AddAsync(user, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Users.Application.Ports.Out;
using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> UserNameIsBusyAsync(string userName, CancellationToken token) => await _db.Users.AsNoTracking().AnyAsync(user => user.UserName == userName, token);

        public async Task SaveAsync(User user, CancellationToken token) => await _db.Users.AddAsync(user, token);
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}

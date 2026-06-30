using Microsoft.EntityFrameworkCore;
using Transactions.Models;

namespace Transactions.Data
{
    public class CategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyCollection<Category>> GetAsync(long userId, CancellationToken cancellationToken)
        {
            return await _db.Categories
                .AsNoTracking()
                .Include(category => category.Type)
                .Where(category => category.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}

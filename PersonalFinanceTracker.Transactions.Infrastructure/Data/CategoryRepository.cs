using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken token) => await _db.Categories.Where(category => category.UserId == userId).ToListAsync();
        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken token) => await _db.Categories.Where(category => category.Id == id).FirstOrDefaultAsync(token);
        public async Task<bool> IsEmpty(CancellationToken token) => !await _db.Categories.AnyAsync(token);

        public async Task SaveAsync(Category category, CancellationToken token) => await _db.Categories.AddAsync(category, token);

        public async Task DeleteAsync(Category category) => _db.Categories.Remove(category);
    }
}

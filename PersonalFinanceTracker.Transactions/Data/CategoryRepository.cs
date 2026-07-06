using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Transactions.Data
{
    public class CategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            return await _db.Categories
                .AsNoTracking()
                .Include(category => category.Type)
                .Where(category => category.UserId == userId)
                .ToListAsync(cancellationToken);
        }
        public async Task<Category> GetByIdAndUserIdAsync(long id, long userId)
        {
            return await _db.Categories
                .Include(category => category.Type)
                .FirstOrDefaultAsync(category => category.Id == id && category.UserId == userId)
                ?? throw new InvalidOperationException($"Category with Id = {id} not found.");
        }

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            await _db.Categories.AddAsync(category, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return category;
        }
        public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync(cancellationToken);
            return category;
        }
        public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}

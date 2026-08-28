using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken token);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken token);
        Task<bool> IsEmpty(CancellationToken token);

        Task SaveAsync(Category category, CancellationToken token);

        Task DeleteAsync(Category category);
    }
}

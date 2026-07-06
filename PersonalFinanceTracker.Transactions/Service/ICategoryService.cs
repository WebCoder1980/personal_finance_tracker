using Microsoft.AspNetCore.JsonPatch;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Transactions.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken);
        Task<Category> CreateAsync(CategoryUpsertRequest request, CancellationToken cancellationToken);
        Task<Category> UpdateAsync(long categoryId, JsonPatchDocument<CategoryUpsertRequest> patchDoc, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}

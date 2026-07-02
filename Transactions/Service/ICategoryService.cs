using Microsoft.AspNetCore.JsonPatch;
using Transactions.Dtos;
using Transactions.Models;

namespace Transactions.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken);
        Task<Category> CreateAsync(CategoryUpsertRequest request, CancellationToken cancellationToken);
        Task<Category> UpdateAsync(long categoryId, JsonPatchDocument<CategoryUpsertRequest> patchDoc, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}

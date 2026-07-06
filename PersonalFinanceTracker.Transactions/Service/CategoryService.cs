using Microsoft.AspNetCore.JsonPatch;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Domain.Converters;
using PersonalFinanceTracker.Transactions.Service.Auth;
using PersonalFinanceTracker.Transactions.Data;

namespace PersonalFinanceTracker.Transactions.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CategoryService(CategoryRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }
        public async Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken)
        {
            long userId = _currentUser.Id;
            return await _repository.GetByUserIdAsync(userId, cancellationToken);
        }
        public async Task<Category> CreateAsync(CategoryUpsertRequest request, CancellationToken cancellationToken)
        {
            Category model = request.ToModel();
            model.UserId = _currentUser.Id;
            return await _repository.CreateAsync(model, cancellationToken);
        }
        public async Task<Category> UpdateAsync(long id, JsonPatchDocument<CategoryUpsertRequest> patchDoc, CancellationToken cancellationToken)
        {
            Category model = await _repository.GetByIdAndUserIdAsync(id, _currentUser.Id);
            CategoryUpsertRequest dto = model.ToDto();
            patchDoc.ApplyTo(dto);
            model.UpdateFrom(dto);
            
            return await _repository.UpdateAsync(model, cancellationToken);
        }
        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            Category model = await _repository.GetByIdAndUserIdAsync(id, _currentUser.Id);

            await _repository.DeleteAsync(model, cancellationToken);
        }
    }
}

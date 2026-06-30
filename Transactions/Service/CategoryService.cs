using System.Security.Claims;
using Transactions.Data;
using Transactions.Models;
using Transactions.Service.Auth;

namespace Transactions.Service
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

        public async Task<IReadOnlyCollection<Category>> GetAsync(CancellationToken cancellationToken)
        {
            long userId = _currentUser.Id ?? throw new UnauthorizedAccessException("JWT does not contain 'user_id'.");
            return await _repository.GetAsync(userId, cancellationToken);
        }
    }
}

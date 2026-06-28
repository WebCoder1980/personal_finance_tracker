using System.Collections.ObjectModel;
using Transactions.Data;
using Transactions.Models;

namespace Transactions.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<Category>> GetAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(cancellationToken);
        }
    }
}

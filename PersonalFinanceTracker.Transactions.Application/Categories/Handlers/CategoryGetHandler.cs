using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public class CategoryGetHandler : ICategoryGetHandler
    {
        private readonly ICurrentUser _currentUser;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGetHandler(ICurrentUser currentUser, ICategoryRepository categoryRepository)
        {
            _currentUser = currentUser;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryGetResult>> ExecuteAsync(CancellationToken token)
        {
            IEnumerable<Category> category = await _categoryRepository.GetByUserIdAsync(_currentUser.Id, token);

            return category.Select(category => new CategoryGetResult(category.Id, category.UserId, category.Name, category.Type, category.MonthlyAmount));
        }
    }
}

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
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGetHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryGetResult>> ExecuteAsync(CategoryGetCommand command, CancellationToken token)
        {
            IEnumerable<Category> category = await _categoryRepository.GetByUserIdAsync(command.UserId, token);

            return category.Select(category => new CategoryGetResult(category.Id, category.UserId, category.Name, category.Type, category.MonthlyAmount));
        }
    }
}

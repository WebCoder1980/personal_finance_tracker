using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public class CategoryGetHandler : ICategoryGetHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGetHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> ExecuteAsync(Guid userId, CancellationToken token)
        {
            IEnumerable<Category> categories = await _categoryRepository.GetByUserIdAsync(userId, token);

            return categories;
        }
    }
}

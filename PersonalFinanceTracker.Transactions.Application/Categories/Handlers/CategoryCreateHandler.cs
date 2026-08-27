using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public class CategoryCreateHandler : ICategoryCreateHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCreateHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> ExecuteAsync(Guid userId, string name, CategoryType type, double monthlyAmount, CancellationToken token)
        {
            Category category = Category.Create(userId, name, type, monthlyAmount);

            await _categoryRepository.SaveAsync(category, token);
            await _unitOfWork.SaveChangesAsync(token);

            return category;
        }
    }
}

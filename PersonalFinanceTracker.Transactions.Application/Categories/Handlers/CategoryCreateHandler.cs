using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<CategoryCreateResult> ExecuteAsync(CategoryCreateCommand command, CancellationToken token)
        {
            Category category = Category.Create(command.UserId, command.Name, command.Type, command.MonthlyAmount);

            await _categoryRepository.SaveAsync(category, token);
            await _unitOfWork.SaveChangesAsync(token);

            return new CategoryCreateResult(category.Id, category.UserId, category.Name, category.Type, category.MonthlyAmount);
        }
    }
}

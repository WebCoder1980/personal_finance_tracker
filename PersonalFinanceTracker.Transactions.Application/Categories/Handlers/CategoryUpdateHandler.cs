using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public class CategoryUpdateHandler : ICategoryUpdateHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryUpdateHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> ExecuteAsync(Guid id, Guid userId, string name, double? monthlyAmount, CancellationToken token)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, token)
                ?? throw new DomainException("Category was not found");

            if (!category.HasAccess(userId))
            {
                throw new PermissionDeniedException();
            }

            category.ChangeName(name);
            category.ChangeMonthlyAmount(monthlyAmount);

            await _unitOfWork.SaveChangesAsync(token);

            return category;
        }
    }
}

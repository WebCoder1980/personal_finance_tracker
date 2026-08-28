using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Handlers
{
    public class CategoryUpdateHandler : ICategoryUpdateHandler
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryUpdateHandler(ICurrentUser currentUser, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryUpdateResult> ExecuteAsync(CategoryUpdateCommand command, CancellationToken token)
        {
            Category category = await _categoryRepository.GetByIdAsync(command.Id, token)
                ?? throw new DomainException("Category was not found");

            if (!category.HasAccess(_currentUser.Id))
            {
                throw new PermissionDeniedException();
            }

            category.ChangeName(command.Name);
            category.ChangeMonthlyAmount(command.MonthlyAmount);

            await _unitOfWork.SaveChangesAsync(token);

            return new CategoryUpdateResult(category.Id, category.UserId, category.Name, category.Type, category.MonthlyAmount);
        }
    }
}

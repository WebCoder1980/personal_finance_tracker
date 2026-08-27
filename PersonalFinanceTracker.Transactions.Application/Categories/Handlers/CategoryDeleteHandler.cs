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
    public class CategoryDeleteHandler : ICategoryDeleteHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDeleteHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task ExecuteAsync(Guid id, Guid userId, CancellationToken token)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, token)
                ?? throw new DomainException("Category was not found");

            if (!category.HasAccess(userId))
            {
                throw new PermissionDeniedException();
            }

            await _categoryRepository.DeleteAsync(category, token);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}

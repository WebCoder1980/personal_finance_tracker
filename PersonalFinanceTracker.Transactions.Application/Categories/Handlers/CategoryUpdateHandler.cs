using PersonalFinanceTracker.ServiceDefaults.Exceptions;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.Out;
using PersonalFinanceTracker.Transactions.Application.Common.Ports.Out;
using PersonalFinanceTracker.Transactions.Domain;

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

        public async Task<CategoryUpdateResult> ExecuteAsync(CategoryUpdateCommand command, CancellationToken token)
        {
            Category category = await _categoryRepository.GetByIdAsync(command.Id, token)
                ?? throw new NotFoundException("Category was not found");

            if (!category.HasAccess(command.UserId))
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

using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryUpdateHandler
    {
        Task<CategoryUpdateResult> ExecuteAsync(CategoryUpdateCommand command, CancellationToken token);
    }
}

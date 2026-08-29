using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryCreateHandler
    {
        Task<CategoryCreateResult> ExecuteAsync(CategoryCreateCommand command, CancellationToken token);
    }
}

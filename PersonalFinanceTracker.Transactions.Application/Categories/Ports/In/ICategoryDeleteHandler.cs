using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryDeleteHandler
    {
        Task ExecuteAsync(CategoryDeleteCommand command, CancellationToken token);
    }
}

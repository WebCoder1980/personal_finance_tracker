using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;

namespace PersonalFinanceTracker.Transactions.Application.Categories.Ports.In
{
    public interface ICategoryGetHandler
    {
        Task<IEnumerable<CategoryGetResult>> ExecuteAsync(CategoryGetCommand command, CancellationToken token);
    }
}

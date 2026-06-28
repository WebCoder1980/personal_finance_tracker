using Transactions.Models;

namespace Transactions.Service
{
    public interface ICategoryService
    {
        Task<IReadOnlyCollection<Category>> GetAsync(CancellationToken cancellationToken);
    }
}

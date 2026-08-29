namespace PersonalFinanceTracker.Transactions.Application.Common.Ports.Out
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}

namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}

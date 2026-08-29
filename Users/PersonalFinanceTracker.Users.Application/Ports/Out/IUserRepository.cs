using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<User?> GetByUserName(string userName, CancellationToken token);

        Task<bool> UserNameIsBusyAsync(string userName, CancellationToken token);

        Task SaveAsync(User user, CancellationToken token);
    }
}

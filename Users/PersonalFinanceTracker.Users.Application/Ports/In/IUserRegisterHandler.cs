using PersonalFinanceTracker.Users.Application.Handlers;

namespace PersonalFinanceTracker.Users.Application.Ports.In
{
    public interface IUserRegisterHandler
    {
        Task<UserRegisterResult> ExecuteAsync(UserRegisterCommand command, CancellationToken token);
    }
}

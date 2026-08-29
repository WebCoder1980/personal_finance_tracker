using PersonalFinanceTracker.Users.Application.Handlers;

namespace PersonalFinanceTracker.Users.Application.Ports.In
{
    public interface IUserLoginHandler
    {
        Task<UserLoginResult> ExecuteAsync(UserLoginCommand command, CancellationToken token);
    }
}

using PersonalFinanceTracker.Users.Domain.Models;

namespace PersonalFinanceTracker.Users.Application.Ports.Out
{
    public interface IJwtGenerator
    {
        string Execute(User user);
    }
}

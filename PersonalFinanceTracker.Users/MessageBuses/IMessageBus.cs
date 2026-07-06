using PersonalFinanceTracker.Domain.Models;

namespace PersonalFinanceTracker.Users.MessageBuses
{
    public interface IMessageBus
    {
        Task SendUserCreatedAsync(User user);
    }
}

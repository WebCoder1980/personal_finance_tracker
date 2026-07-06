using PersonalFinanceTracker.Domain.Models;

namespace Users.MessageBuses
{
    public interface IMessageBus
    {
        Task SendUserCreatedAsync(User user);
    }
}

using Users.Models;

namespace Users.MessageBuses
{
    public interface IMessageBus
    {
        Task SendUserCreatedAsync(User user);
    }
}

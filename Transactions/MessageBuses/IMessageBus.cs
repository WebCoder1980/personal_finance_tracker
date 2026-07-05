using RabbitMQ.Client.Events;

namespace Transactions.MessageBuses
{
    public interface IMessageBus
    {
        Task ReceiveUserCreatedAsync(object model, BasicDeliverEventArgs eventArguments);
    }
}

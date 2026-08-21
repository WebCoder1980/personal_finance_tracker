using RabbitMQ.Client.Events;

namespace PersonalFinanceTracker.Transactions.MessageBuses
{
    public interface IMessageBus
    {
        Task ReceiveUserCreatedAsync(object model, BasicDeliverEventArgs eventArguments);
    }
}

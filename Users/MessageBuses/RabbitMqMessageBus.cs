using PersonalFinanceTracker.Domain.Contracts;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Domain.Converters;
using RabbitMQ.Client;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Users.MessageBuses
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        public RabbitMqMessageBus(IConnection connection) {
            _connection = connection;
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            Init().GetAwaiter().GetResult();
        }

        private const string EVENTS_EXCHANGE = "events";
        private const string USER_CREATED_ROUTING_KEY = "user.created";

        private async Task Init()
        {
            await _channel.ExchangeDeclareAsync(
                exchange: EVENTS_EXCHANGE,
                type: ExchangeType.Topic,
                durable: true
            );
        }

        public async Task SendUserCreatedAsync(User user)
        {
            await SendAsync(USER_CREATED_ROUTING_KEY, user.ToModel());
        }

        private async Task SendAsync<T>(string routingKey, T payload) {
            MessageBusEvent<T> newEvent = new MessageBusEvent<T>
            {
                Id = Guid.NewGuid(),
                OccuredAt = DateTime.Now,
                Payload = payload
            };
            await _channel.BasicPublishAsync(EVENTS_EXCHANGE, routingKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newEvent)));
        }
    }
}

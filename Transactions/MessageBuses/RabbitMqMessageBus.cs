using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Transactions.Data;
using Transactions.MessageBuses;
using Transactions.MessageBuses.Contracts;
using Transactions.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Users.MessageBuses
{
    public class RabbitMqMessageBus : BackgroundService, IMessageBus
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly AsyncEventingBasicConsumer _userCreatedConsumer;
        private readonly IServiceScopeFactory _scopeFactory;
        public RabbitMqMessageBus(IConnection connection, IServiceScopeFactory scopeFactory) {
            _connection = connection;
            _scopeFactory = scopeFactory;

            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _userCreatedConsumer = new AsyncEventingBasicConsumer(_channel);
        }

        private const string EVENTS_EXCHANGE = "events";
        private const string TRANSACTIONS_USER_CREATED_QUEUE = "transactions.user.created";
        private const string USER_CREATED_ROUTING_KEY = "user.created";
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _channel.ExchangeDeclareAsync(
                exchange: EVENTS_EXCHANGE,
                type: ExchangeType.Topic,
                durable: true
            );
            await _channel.QueueDeclareAsync(TRANSACTIONS_USER_CREATED_QUEUE, true, false, false);
            await _channel.QueueBindAsync(TRANSACTIONS_USER_CREATED_QUEUE, EVENTS_EXCHANGE, USER_CREATED_ROUTING_KEY);
            _userCreatedConsumer.ReceivedAsync += ReceiveUserCreatedAsync;
            await _channel.BasicConsumeAsync(TRANSACTIONS_USER_CREATED_QUEUE, false, _userCreatedConsumer);
        }
        public async Task ReceiveUserCreatedAsync(object model, BasicDeliverEventArgs eventArguments)
        {
            MessageBusEvent<UserReference>? messageBusEvent = JsonSerializer.Deserialize<MessageBusEvent<UserReference>>(Encoding.UTF8.GetString(eventArguments.Body.ToArray()));
            if (messageBusEvent is null)
            {
                throw new JsonException("Invalid MessageBusEvent<UserReference> message format");
            }

            UserRepository userRepository = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<UserRepository>();

            await userRepository.CreateAsync(messageBusEvent.Payload, eventArguments.CancellationToken);

            await _channel.BasicAckAsync(eventArguments.DeliveryTag, false);
        }
    }
}

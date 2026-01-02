using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Infrastructure.Messaging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using ProfileService.Domain.Interfaces;
using ProfileService.Domain;
using System.Text.Json;

namespace ProfileService.Infrastructure.Repositories
{
    public class RabbitMqConsumerRepository : BackgroundService
    {
        private readonly RabbitMqConnection _connection;
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMqConsumerRepository(
            RabbitMqConnection connection,
            IServiceScopeFactory scopeFactory)
        {
            _connection = connection;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync("user.events", ExchangeType.Fanout, durable: true);
            var queue = await channel.QueueDeclareAsync(durable: true);

            await channel.QueueBindAsync(queue.QueueName, "user.events", "");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var evt = JsonSerializer.Deserialize<AddProfileEvent>(json)!;

                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IProfileRepository>();

                await service.AddProfileAsync(evt.UserId, evt.Email, evt.Name,evt.PhoneNo);
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue.QueueName, false, consumer);
        }
    }
}

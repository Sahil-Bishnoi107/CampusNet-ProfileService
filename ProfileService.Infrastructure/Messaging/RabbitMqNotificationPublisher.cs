using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProfileService.Domain.DTOs;
using ProfileService.Domain.Interfaces;
using RabbitMQ.Client;

namespace ProfileService.Infrastructure.Messaging
{
    public class RabbitMqNotificationPublisher : INotificationPublisher
    {
        private readonly RabbitMqConnection _connection;

        private const string SmsQueue = "notification.sms.queue";
        private const string EmailQueue = "notification.email.queue";

        public RabbitMqNotificationPublisher(RabbitMqConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishSmsAsync(NotificationMessage message)
            => await PublishAsync(message, SmsQueue);

        public async Task PublishEmailAsync(NotificationMessage message)
            => await PublishAsync(message, EmailQueue);

        private async Task PublishAsync(NotificationMessage message, string queue)
        {
            await using var channel = await _connection.CreateChannelAsync();

            // Ensure queue exists (safe & idempotent)
            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message)
            );

            var props = new BasicProperties
            {
                Persistent = true,
                CorrelationId = message.CorrelationId
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                basicProperties: props,
                body: body
            );
        }
    }
}

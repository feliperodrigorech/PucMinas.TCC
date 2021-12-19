using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PucMinas.TCC.Domain.Services
{
    public class MessageQueueBaseService
    {
        readonly IConnection Connection;
        public readonly IModel Channel;

        public MessageQueueBaseService(IConfiguration configuration)
        {
            ConnectionFactory factory = new() { HostName = configuration["ConnectionFactoryHostName"] };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public IBasicProperties BasicProperties(bool persistent = true)
        {
            IBasicProperties properties = Channel.CreateBasicProperties();
            properties.Persistent = persistent;

            return properties;
        }

        protected void BasicConsume(string queueName, bool autoAck, EventingBasicConsumer consumer)
        {
            Channel.BasicConsume(
                queue: queueName,
                autoAck: autoAck,
                consumer: consumer);
        }

        protected void BasicQos(uint prefetchSize, ushort prefetchCount, bool global)
        {
            Channel.BasicQos(
                prefetchSize: prefetchSize,
                prefetchCount: prefetchCount,
                global: global);
        }

        protected EventingBasicConsumer CreateEventingBasicConsumer()
        {
            return new EventingBasicConsumer(Channel);
        }
    }
}

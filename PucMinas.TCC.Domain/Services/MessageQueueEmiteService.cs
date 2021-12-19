using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace PucMinas.TCC.Domain.Services
{
    public class MessageQueueEmiteService : MessageQueueBaseService
    {
        public MessageQueueEmiteService(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void Publish(string queueName, string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);
            Channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: BasicProperties(),
                body: body);
        }
    }
}

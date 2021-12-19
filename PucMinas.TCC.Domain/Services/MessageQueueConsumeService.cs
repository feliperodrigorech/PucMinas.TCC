using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Services
{
    public abstract class MessageQueueConsumeService : MessageQueueBaseService
    {
        public MessageQueueConsumeService(IConfiguration configuration, string queueName)
            : base(configuration)
        {
            BasicQos(0, 1, false);
            EventingBasicConsumer consumer = CreateEventingBasicConsumer();
            consumer.Received += Received;
            BasicConsume(queueName, false, consumer);
        }

        protected abstract Task DoWork(string message);

        private void Received(object sender, BasicDeliverEventArgs e)
        {
            IModel channel = ((DefaultBasicConsumer)sender).Model;
            ReadOnlyMemory<byte> body = e.Body;
            string message = Encoding.UTF8.GetString(body.ToArray());

            try
            {
                DoWork(message).Wait();

                channel.BasicAck(
                    deliveryTag: e.DeliveryTag,
                    multiple: false);
            }
            catch
            {
                channel.BasicNack(
                    deliveryTag: e.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        }
    }
}

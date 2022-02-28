using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace EmailSenderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://qwrxqkei:CzmI2f2m5Gr0OcxIaS3U2j91KsuoQKqu@sparrow.rmq.cloudamqp.com/qwrxqkei");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messageQueue", false, false, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("messageQueue", true, consumer);

            consumer.Received += (s, e) =>
            {
                string serializeData = Encoding.UTF8.GetString(e.Body.Span);
                User user = JsonSerializer.Deserialize<User>(serializeData);
                EmailSender.Send(user.Email, user.Message);
            };
        }
    }
}

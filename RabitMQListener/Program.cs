using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabitMQListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://wshfrkdl:mQeHhSM2SYagOsk4ttKUSwjiz3j8AnoV@moose.rmq.cloudamqp.com/wshfrkdl");

            var connection = factory.CreateConnection();
            using var channel=connection.CreateModel();
            channel.QueueDeclare("test", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mesaj: {message}");
            };

            //mesajı okuma
            channel.BasicConsume(queue:"test", autoAck:true, consumer:consumer);
            Console.ReadKey();
        }
        
        
    }
}

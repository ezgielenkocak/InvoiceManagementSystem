using InvoiceManagementSystem.BLL.Abstract.RabitMQ;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Create connection
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://wshfrkdl:mQeHhSM2SYagOsk4ttKUSwjiz3j8AnoV@moose.rmq.cloudamqp.com/wshfrkdl");

            //make active connection
            var connection=factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("test", exclusive: false);

            var json=JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "test", body: body);
        }
    }
}

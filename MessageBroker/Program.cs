using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MessageBroker
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                SendRequest();
            }

        }

        static void SendRequest()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("report-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var message = new ReportOrder { Location = Console.ReadLine() };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "report-queue", null, body);
        }
    }
}

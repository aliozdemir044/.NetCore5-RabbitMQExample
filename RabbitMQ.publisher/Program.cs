using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(@"amqps://ciombhfv:OxogfgHODC_jFlBggHETJ8WAMcNFbVi5@chimpanzee.rmq.cloudamqp.com/ciombhfv");

            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("hello-queue", true, false, false);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message  {x}";

                var messageByte = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, "hello-queue", null, messageByte);
                Console.WriteLine($"mesaj gönderildi. : {message}");

            });

            Console.ReadLine();


        }
    }
}

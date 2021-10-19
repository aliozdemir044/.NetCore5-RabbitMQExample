using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(@"amqps://ciombhfv:OxogfgHODC_jFlBggHETJ8WAMcNFbVi5@chimpanzee.rmq.cloudamqp.com/ciombhfv");

            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue", false, consumer);
            channel.BasicQos(0, 1, false);


            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine($"Gelen Mesaj -> {message}");
                channel.BasicAck(e.DeliveryTag, false);

            };



            Console.ReadLine();
        }
    }
}

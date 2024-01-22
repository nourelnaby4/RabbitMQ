using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerPoisonMessage
    {
        public void TestPoisonMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (obj, args) =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    channel.BasicNack(args.DeliveryTag, false, true);
                    Console.WriteLine($"{args.DeliveryTag} Message is Recived:{message} but will requeue");
                    Console.WriteLine($"Press Enter To Exit");

                    Console.ReadKey();
                };


                channel.BasicConsume(queue: "quorum-q", autoAck: false, consumer: consumer, consumerTag: "TEST_POISON_MESSAGE");
                Console.WriteLine();
                Console.ReadKey();
            }

        }
    }
}

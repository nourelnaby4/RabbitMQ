using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerPrefetch
    {
        public void Create()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection  = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.BasicQos(prefetchSize: 0, prefetchCount: 3, global: false);

                var consumer=new EventingBasicConsumer(channel);

                consumer.Received += (obj, args) =>
                {
                    channel.BasicAck(args.DeliveryTag, false); //rescived and delete from queue
                    var body =args.Body.ToArray();
                    var message= Encoding.UTF8.GetString(body);
                    Console.WriteLine($"recieved message from {args.ConsumerTag}:{message}");


                    Console.WriteLine("Press Enter to exit");
                    Console.ReadLine();
                };

                channel.BasicConsume(queue:"q7",
                                     autoAck:false,
                                     consumer: consumer,
                                     consumerTag:"PrefetchConsumer");
            }
        }
    }
}

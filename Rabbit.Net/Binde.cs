using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Net
{
    public class Binde
    {
        private readonly ConnectionFactory factory;

        public Binde()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void Bind_Fanout_To_Quorum()
        {
            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                 

                    channel.QueueBind(queue: "quorum-q",
                                  exchange: "Myfanout-exchange",
                                  routingKey: ""); // Routing key is empty for fanout exchanges

                    Console.WriteLine("Quorum queue is binded successfully");
                    Console.WriteLine("Press Enter to exit");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");
            }
        }
    }
}

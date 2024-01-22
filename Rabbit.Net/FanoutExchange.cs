using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Net
{
    public class FanoutExchange
    {
        private readonly ConnectionFactory factory;

        public FanoutExchange()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void DeclareFanout()
        {
            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "Myfanout-exchange",
                                            type: ExchangeType.Fanout,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);

                    Console.WriteLine("Fanout exchange is created successfully");
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

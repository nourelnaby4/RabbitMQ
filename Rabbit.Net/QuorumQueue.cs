using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Net
{
    public class QuorumQueue
    {
        private readonly ConnectionFactory factory;

        public QuorumQueue()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void DeclareQuorum()
        {
            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    // Declare a quorum queue
                    channel.QueueDeclare(queue: "quorum-q",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: new Dictionary<string, object>
                                         {
                                         { "x-queue-type", "quorum" }, // Set the queue type to "quorum"
                                    
                                         });
                    Console.WriteLine("Quorum queue is created successfully");



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

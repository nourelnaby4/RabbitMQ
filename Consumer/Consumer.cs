using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    public class Consumer
    {
        public void Acknowlegment()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();
                var consumer=new EventingBasicConsumer(channel);
                consumer.Received += (sender, args) =>
                {
                    ;
                    var body =args.Body.ToArray() ;
                    var message = Encoding.UTF8.GetString(body);
                    channel.BasicAck(args.DeliveryTag, false); //rescived and delete from queue

                    Console.WriteLine($"{args.DeliveryTag}: {message}");

                    Console.WriteLine("Press Enter to exit");
                    Console.ReadLine();

                };
                channel.BasicConsume(queue: "q4",
                                 consumer:consumer,
                                 autoAck:false, // the most importante section to activate confirmation 
                                 consumerTag:"MSGTag"); 

            }
        }

    }
}

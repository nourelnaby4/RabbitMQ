using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
    public class Sender
    {
        // reliable to confirm that message send from publisher to exchange to queue 
        public void Relaible()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection =factory.CreateConnection())
            using(var channel=connection.CreateModel())
            {
                var message = "hello ahmed iam try to get this message to confirm ";
                var body= Encoding.UTF8.GetBytes(message);

                //first step
                channel.BasicAcks += (sender, ea) =>
                {
                    Console.WriteLine($"{ea.DeliveryTag}: first step that confirm message to exchange");
                };


                //second confirm if replay that mean message is not send to queue
                channel.BasicReturn += (sender, ea) =>
                {
                    Console.WriteLine($"{ea.ReplyText}");

                    Console.WriteLine("Press Enter to exit");
                    Console.ReadLine();
                };

                channel.BasicPublish(exchange: "amq.direct",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body,
                                     mandatory:true); // the most importante section to activate confirmation 

               
            }
        }

    }
}

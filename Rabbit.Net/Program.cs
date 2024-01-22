// See https://aka.ms/new-console-template for more information
using Consumer;
using Rabbit.Net;
using Publisher;

//var sender = new Publisher.Sender();
//sender.Relaible();

//var subscriber =new Consumer.Subscriber();
//subscriber.Acknowlegment();


//var prefetch =new ConsumerPrefetch();
//prefetch.Create();

//DeclareQueue queue = new DeclareQueue();
//queue.DeclarDurableQueue();  // Make sure this line is executed



#region PoisonMessage
var quorumQ = new QuorumQueue();
quorumQ.DeclareQuorum();

var exchange = new FanoutExchange();
exchange.DeclareFanout();

var bind = new Binde();
bind.Bind_Fanout_To_Quorum();

var cunsumer = new ConsumerPoisonMessage();
cunsumer.TestPoisonMessage();

var publisher = new Publishers();
publisher.PublishMessageToQuorum();


#endregion
Console.ReadKey();
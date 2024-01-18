// See https://aka.ms/new-console-template for more information
using Consumer;
using Rabbit.Net;

//var sender = new Publisher.Sender();
//sender.Relaible();

//var subscriber =new Consumer.Subscriber();
//subscriber.Acknowlegment();


//var prefetch =new ConsumerPrefetch();
//prefetch.Create();

DeclareQueue queue = new DeclareQueue();
queue.DeclarDurableQueue();  // Make sure this line is executed
Console.ReadKey();
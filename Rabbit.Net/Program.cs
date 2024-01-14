// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var sender = new Publisher.Sender();
sender.Relaible();

var subscriber =new Consumer.Subscriber();
subscriber.Acknowlegment();
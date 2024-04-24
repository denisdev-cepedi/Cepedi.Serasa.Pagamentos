using RabbitMQ.Client;
using System;
using System.Text;

var factory = new ConnectionFactory() { HostName = "srv508250.hstgr.cloud"};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "FilaRabbitMQ",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
    string message = "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "",
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);
    Console.WriteLine("[x] Sent {0}", message);
}

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();


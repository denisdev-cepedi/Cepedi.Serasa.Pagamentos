using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "srv508250.hstgr.cloud", UserName = "aluno", Password = "changeme"};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "SeOAviaoForCairEhSoPularAntesDeChegarNoChao",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
    string message = "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "Obvio",
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);
    Console.WriteLine("[x] Sent {0}", message);
}

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();


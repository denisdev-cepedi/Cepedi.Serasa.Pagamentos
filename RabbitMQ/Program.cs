using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System;

class Program
{
    public static void Main()
    {

//         RabbitMQ": {
//   "Hostname": "srv508250.hstgr.cloud",
//   "QueueName": "Fila.Teste",
//   "User": "aluno",
//   "Password": "changeme"

        var factory = new ConnectionFactory() { HostName = "srv508250.hstgr.cloud", UserName = "aluno", Password = "changeme" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "Fila.Teste.Wilton",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = "Teste Vitor";
            var body = Encoding.UTF8.GetBytes(message);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

    }
}
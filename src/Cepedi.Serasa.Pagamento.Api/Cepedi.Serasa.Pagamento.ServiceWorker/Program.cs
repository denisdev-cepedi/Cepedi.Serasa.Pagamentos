using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.IoC;
using Cepedi.Serasa.Pagamento.RabbitMQ;
using Cepedi.Serasa.Pagamento.ServiceWorker;
using Cepedi.Serasa.Pagamento.ServiceWorker.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureAppDependencies(hostContext.Configuration);
        services.AddSingleton<IConsumerRabbitMQ<CriarUsuarioRequest>, FilaConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();

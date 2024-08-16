using Console.Kafka.Consumer.Console.MessagesKafka.Services;
using ConsoleApp.Kafka.Consumer.Console.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddHostedService<ConsumerService<Pessoa>>();
}).Build();

await host.RunAsync();

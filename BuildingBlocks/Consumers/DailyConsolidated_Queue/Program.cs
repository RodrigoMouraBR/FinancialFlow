using Carrefas.Core.Interfaces;
using Carrefas.Core.IoC;
using Carrefas.Core.Notifications;
using Carrefas.FinancialFlow.API.Configuration;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.IoC;
using Carrefas.FinancialFlow.Application.Services;
using Carrefas.FinancialFlow.Data.Contexts;
using Carrefas.FinancialFlow.Data.Repositories;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json;

namespace ProjetoConsumer;
class Program
{  
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .Build();

        var serviceCollection = new ServiceCollection();

        ResolveDependencies(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "consolidatedQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    var projeto = JsonSerializer.Deserialize<DailyConsolidated>(message);

                    if (projeto != null)
                    {                       
                        var t = serviceProvider.GetService<IFinancialFlowService>().Atualizar(projeto);


                        Console.WriteLine("Processado....");
                        string json = JsonSerializer.Serialize(projeto);
                        Console.WriteLine(json);
                    }

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);

                    Console.WriteLine(e);
                }
            };

            channel.BasicConsume(queue: "consolidatedQueue",
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

    }


    public static IServiceCollection ResolveDependencies(IServiceCollection services)
    {
        InversionOfControl.RegisterServices(services);
        CoreIoC.CoreIoCServices(services);

        return services;
    }
}














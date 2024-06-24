using FinancialFlow.Application.AutoMapper;
using FinancialFlow.Application.Interfaces;
using FinancialFlow.Application.IoC;
using FinancialFlow.Application.Models;
using FinancialFlow.Core.EnvironmentVariable;
using FinancialFlow.Core.IoC;
using FinancialFlow.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var cancellationTokenSource = new CancellationTokenSource();

    var rabbitMqHost = EnvironmentVariableRepository.rabbithost;    
    var factory = new ConnectionFactory() { HostName = rabbitMqHost };


    int retryCount = 0;
    const int maxRetries = 20;
    const int delayBetweenRetries = 10000; // 10 seconds

    while (retryCount < maxRetries)
    {
        try
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the Dead Letter Exchange and Queue
                channel.ExchangeDeclare(exchange: "dead_letter_exchange", type: ExchangeType.Direct);
                channel.QueueDeclare(queue: "dead_letter_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(queue: "dead_letter_queue",
                                  exchange: "dead_letter_exchange",
                                  routingKey: "dead_letter_routing_key");

                // Declare the main queue with DLX settings
                var arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "dead_letter_exchange" },
            { "x-dead-letter-routing-key", "dead_letter_routing_key" }
        };

                channel.QueueDeclare(queue: "ProjetoQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: arguments);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var projeto = JsonSerializer.Deserialize<FinancialTransactionModel>(message);

                        if (projeto != null)
                        {
                            var transactionService = serviceProvider.GetService<IFinancialTransactionAppService>();
                            if (transactionService != null)
                            {
                                Console.WriteLine(" Processando....");
                                var result = transactionService.AddFinancialTransaction(projeto).Result;

                                if (result)
                                {
                                    channel.BasicAck(ea.DeliveryTag, false);
                                }
                                else
                                {
                                    // Handle non-processing cases
                                    channel.BasicNack(ea.DeliveryTag, false, false);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Send the message to DLX
                        channel.BasicNack(ea.DeliveryTag, false, false);
                        Console.WriteLine(e);
                    }
                };

                channel.BasicConsume(queue: "ProjetoQueue",
                                     autoAck: false,
                                     consumer: consumer);



                Console.WriteLine(" Press 'q' to exit.");
                await Task.Run(() =>
                {
                    while (true)
                    {
                        var input = Console.ReadLine();
                        if (input?.ToLower() == "q")
                        {
                            cancellationTokenSource.Cancel();
                            break;
                        }
                    }
                }, cancellationTokenSource.Token);


            }


            Console.WriteLine(" Press 'q' to exit.");
            await Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input?.ToLower() == "q")
                    {
                        cancellationTokenSource.Cancel();
                        break;
                    }
                }
            }, cancellationTokenSource.Token);

            break; // Exit the retry loop if the connection is successful
        }
        catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException)
        {
            retryCount++;
            Console.WriteLine($"Attempt {retryCount} of {maxRetries} failed. Retrying in {delayBetweenRetries / 1000} seconds...");
            Thread.Sleep(delayBetweenRetries);
        }
    }

    if (retryCount == maxRetries)
    {
        Console.WriteLine("Max retry attempts reached. Exiting application.");
        return;
    }

}


static IServiceCollection ResolveDependencies(IServiceCollection services)
{
    services.AddAutoMapper(typeof(FinancialFlowMappingConfig));
    CoreIoC.CoreIoCServices(services);
    InversionOfControl.RegisterServices(services);
    
    var postgresHost = EnvironmentVariableRepository.postgrehost;
    var postgresUser = EnvironmentVariableRepository.postgreuser;
    var postgresPassword = EnvironmentVariableRepository.postgrepassword;
    var postgresDb = EnvironmentVariableRepository.postgredb;

    var connectionString = $"Host={postgresHost};Database={postgresDb};Username={postgresUser};Password={postgresPassword}";

    services.AddDbContext<FinancialFlowContext>(options =>
        options.UseNpgsql(connectionString));
    return services;
}
static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            ResolveDependencies(services); 
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Carrefas.FinancialFlow.Domain.Entities;
using System.Text.Json;
using System.Text;

namespace Consumer
{    
    public class Worker : BackgroundService
    {
        private readonly IFinancialFlowService _financialFlowService;
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger, IFinancialFlowService financialFlowService)
        {
            _logger = logger;
            _financialFlowService = financialFlowService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {

                    _FinancialFlowQueue();

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(3000, stoppingToken);
            }
        }


        public void _FinancialFlowQueue()
        {
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
                            var t = _financialFlowService.Atualizar(projeto);

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







    }
}

using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Carrefas.FinancialFlow.Domain.Queue
{
    public class FinancialFlowQueue
    {
        private readonly IFinancialFlowService _financialFlowService;
        public FinancialFlowQueue(IFinancialFlowService financialFlowService)
        {
            _financialFlowService = financialFlowService;
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

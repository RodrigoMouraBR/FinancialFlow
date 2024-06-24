using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace FinancialFlow.Core.Queue
{
    public static class QueueFlow
    {
        public static async Task EnviacardParaFila<T>(T obj, string nomeFila) where T : class
        {            
            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
            var factory = new ConnectionFactory() { HostName = rabbitMqHost };

            int retryCount = 0;
            const int maxRetries = 5;
            const int delayBetweenRetries = 5000; // 5 seconds


            while (retryCount < maxRetries)
            {
                try
                {
                    await Task.Run(() =>
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

                            channel.QueueDeclare(queue: nomeFila,
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: arguments);

                            string message = JsonSerializer.Serialize(obj);
                            var body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "",
                                                 routingKey: nomeFila,
                                                 basicProperties: null,
                                                 body: body);
                        }
                    });

                    break; // Exit the retry loop if the connection is successful
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException)
                {
                    retryCount++;
                    Console.WriteLine($"Attempt {retryCount} of {maxRetries} failed. Retrying in {delayBetweenRetries / 1000} seconds...");
                    Thread.Sleep(delayBetweenRetries);
                }
            }           
        }
    }
}

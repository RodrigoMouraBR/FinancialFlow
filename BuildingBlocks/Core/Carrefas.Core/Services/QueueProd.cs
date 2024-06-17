using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Carrefas.Core.Services
{
    public static class QueueProd
    {
        public static void EnviacardParaFila<T>(T obj, string nomeFila) where T : class
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: nomeFila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(obj);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: nomeFila,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}

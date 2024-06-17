using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Domain.Queue;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Carrefas.FinancialFlow.Application.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMQSettings _settings;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqService(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
            InitializeRabbitMQ();
        }
       
        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = _settings.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: " + message);
            };

            _channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);
        }
    }
}

using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Carrefas.FinancialFlow.Application.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMQSettings _settings;
        private readonly IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqService(IOptions<RabbitMQSettings> options, IServiceScopeFactory scopeFactory)
        {
            _settings = options.Value;
            _scopeFactory = scopeFactory;
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
                var messageBody = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<DailyConsolidated>(messageBody);

                using (var scope = _scopeFactory.CreateScope())
                {
                   
                        var financialFlowService = scope.ServiceProvider.GetRequiredService<IFinancialFlowService>();
                        financialFlowService.Atualizar(message);
                                       
                }
            };

            _channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);
        }
    }
}

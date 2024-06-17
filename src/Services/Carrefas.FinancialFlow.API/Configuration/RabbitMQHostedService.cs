using Carrefas.FinancialFlow.Application.Interfaces;

namespace Carrefas.FinancialFlow.API.Configuration
{
    public class RabbitMQHostedService : IHostedService
    {
        private readonly IRabbitMqService _consumer;

        public RabbitMQHostedService(IRabbitMqService consumer)
        {
            _consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.StartConsuming();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

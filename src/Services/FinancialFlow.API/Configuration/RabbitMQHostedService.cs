namespace FinancialFlow.API.Configuration
{
    public class RabbitMQHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                //var consumer = scope.ServiceProvider.GetRequiredService<IRabbitMqService>();
                //consumer.StartConsuming();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

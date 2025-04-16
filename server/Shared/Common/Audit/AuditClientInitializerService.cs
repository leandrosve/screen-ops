using Microsoft.Extensions.Hosting;

namespace Common.Audit
{
    class AuditClientInitializerService: IHostedService
    {
        private readonly IAuditClient _rabbitMqClient;

        public AuditClientInitializerService(IAuditClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _rabbitMqClient.InitializeAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

using BackgroundService_Demo.Entities;
using BackgroundService_Demo.Queues;

namespace BackgroundService_Demo.BackgroundService
{
    public class QueueHostedService : Microsoft.Extensions.Hosting.BackgroundService, IQueueHostedService
    {
        private readonly ILogger<QueueHostedService> _logger;
        private readonly IBackgroundTaskQueue<Entity> _queue;

        public QueueHostedService(ILogger<QueueHostedService> logger, IBackgroundTaskQueue<Entity> queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var entity = await _queue.DeQueueAsync(stoppingToken);

                await Task.Delay(2000);
                _logger.LogInformation($"Executed {entity.Name}");
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken = default)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken = default)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}

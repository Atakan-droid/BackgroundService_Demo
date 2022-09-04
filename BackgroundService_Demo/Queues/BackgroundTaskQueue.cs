using BackgroundService_Demo.Entities;
using System.Threading.Channels;

namespace BackgroundService_Demo.Queues
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue<Entity>
    {
        private readonly Channel<Entity> _queue;

        public BackgroundTaskQueue(IConfiguration configuration)
        {
            int.TryParse(configuration["QueueCapacity"], out int capacity);

            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<Entity>(options);
        }

        public async Task AddQueueAsync(Entity item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            await _queue.Writer.WriteAsync(item);
        }

        public async Task<Entity> DeQueueAsync(CancellationToken cancellationToken)
        {
            var workItem = _queue.Reader.ReadAsync(cancellationToken);

            return workItem.Result;
        }
    }
}

using BackgroundService_Demo.Entities;

namespace BackgroundService_Demo.Queues
{
    public interface IBackgroundTaskQueue<T> where T : class, IEntity
    {
        Task AddQueueAsync(T item);
        Task<T> DeQueueAsync(CancellationToken cancellationToken);
    }
}

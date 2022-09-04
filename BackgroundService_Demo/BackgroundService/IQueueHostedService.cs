namespace BackgroundService_Demo.BackgroundService
{
    public interface IQueueHostedService
    {
        Task StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}

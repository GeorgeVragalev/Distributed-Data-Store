using PartitionLeader.Services;
using PartitionLeader.Services.SyncService;

namespace PartitionLeader.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Timer _timer;
    private int number;

    public BackgroundTask(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000);
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scoped = scope.ServiceProvider.GetRequiredService<ISyncService>();
            await scoped.SyncData(stoppingToken);
        }
    }
}
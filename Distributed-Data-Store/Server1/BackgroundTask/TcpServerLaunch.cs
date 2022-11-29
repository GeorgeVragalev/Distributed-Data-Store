using Server1.Services.SyncService;
using Server1.Services.Tcp;

namespace Server1.BackgroundTask;

public class TcpServerLaunch : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Timer _timer;
    private int number;

    public TcpServerLaunch(IServiceScopeFactory serviceScopeFactory)
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
        using var scope = _serviceScopeFactory.CreateScope();
        var scoped = scope.ServiceProvider.GetRequiredService<ITcpService>();
        await scoped.Run();
    }
}
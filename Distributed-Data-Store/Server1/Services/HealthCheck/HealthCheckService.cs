using Newtonsoft.Json;
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.Http;
using Server1.Services.SyncService;

namespace Server1.Services.HealthCheck;

public class HealthCheckService : IHealthCheckService
{
    private readonly ISyncService _syncService;

    public HealthCheckService(ISyncService syncService)
    {
        _syncService = syncService;
    }

    public async Task CheckHealth()
    {
        await Task.Delay(10000);
        
        //check if the partition leader is healthy
        try
        {
            while (await IsServerHealthy(Settings.PartitionLeader))
            {
                await Task.Delay(60000);
            }
            PrintConsole.Write($"Server 1 is leader now", ConsoleColor.Green);
            StorageHelper._partitionLeaderStatus.IsRunning = false;
            Settings.Leader = true;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Partition leader check failed. Server 1 is leader now", ConsoleColor.DarkRed);
            StorageHelper._partitionLeaderStatus.IsRunning = false;
            Settings.Leader = true;
        }
    }

    public async Task<bool> IsServerHealthy(string url)
    {
        try
        {
            using var client = new HttpClient();
            
            var response = await client.GetAsync($"{url}/check");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<bool>(dataAsJson);
            
            PrintConsole.Write($"Partition leader is healthy", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Partition leader check failed. Reassigning leader...", ConsoleColor.DarkRed);
            return false;
        }
    }
}
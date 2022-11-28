using Newtonsoft.Json;
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.Http;

namespace Server1.Services.HealthCheck;

public class HealthCheckService : IHealthCheckService
{
    private readonly IHttpService _httpService;

    public HealthCheckService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task CheckHealth()
    {
        await Task.Delay(10000);
        
        //check if the partition leader is healthy
        try
        {
            while (await IsPartitionLeaderHealthy())
            {
                await Task.Delay(10000);
            }
            PrintConsole.Write($"Server 1 is leader now", ConsoleColor.Green);
            Settings.Leader = true;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Partition leader check failed. Server 1 is leader now", ConsoleColor.DarkRed);
            Settings.Leader = true;
        }
    }

    private async Task<bool> IsPartitionLeaderHealthy()
    {
        try
        {
            var url = Settings.PartitionLeader;

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

        return false;
    }
}
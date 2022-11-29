using Newtonsoft.Json;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
using PartitionLeader.Services.Http;

namespace PartitionLeader.Services.HealthCheck;

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
            while (true)
            {
                await Task.Delay(10000);
                var server1Health = await IsServerHealthy(Settings.Server1);
                var server2Health = await IsServerHealthy(Settings.Server2);

                if (!server1Health)
                {
                    StorageHelper._server1Status.SetServerStatus(server1Health);
                    PrintConsole.Write($"Server 1 is down", ConsoleColor.Red);
                    break;
                }
                if (!server2Health)
                {
                    StorageHelper._server2Status.SetServerStatus(server2Health);
                    PrintConsole.Write($"Server 2 is down", ConsoleColor.Red);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Health check failed", ConsoleColor.DarkRed);
        }
    }

    private async Task<bool> IsServerHealthy(string url)
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
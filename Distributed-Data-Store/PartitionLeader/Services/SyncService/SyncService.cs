using Newtonsoft.Json;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.Http;

namespace PartitionLeader.Services.SyncService;

public class SyncService : ISyncService
{
    private readonly IHttpService _httpService;
    private readonly IDataService _dataService;

    public SyncService(IHttpService httpService, IDataService dataService)
    {
        _httpService = httpService;
        _dataService = dataService;
    }

    public async Task SyncData(CancellationToken cancellationToken)
    {
        //sync all data between clusters
        while (true)
        {
            await Task.Delay(10000);
            var serverData = await _dataService.GetAll();
            if (serverData != null)
            {
                foreach (var data in serverData)
                {
                    await CheckDataBackup(data);
                }
            }

            PrintConsole.Write($"All data has a reserve copy", ConsoleColor.Green);
        }
    }

    private async Task CheckDataBackup(KeyValuePair<int, Data> data)
    {
        KeyValuePair<int, Data>? backupServer1 = null;
        KeyValuePair<int, Data>? backupServer2 = null;
        if (StorageHelper._server2Status.IsRunning)
        {
            try
            {
                backupServer1 = await _httpService.GetById(data.Key, Settings.Server2);
            }
            catch (Exception e)
            {
                StorageHelper._server2Status.IsRunning = false;
            }
        }

        if (StorageHelper._server1Status.IsRunning)
        {
            try
            {
                backupServer2 = await _httpService.GetById(data.Key, Settings.Server1);
            }
            catch (Exception e)
            {
                StorageHelper._server1Status.IsRunning = false;
            }
        }

        if (backupServer1?.Value == null && backupServer2?.Value == null && (StorageHelper._server1Status.IsRunning || StorageHelper._server2Status.IsRunning))
        {
            var optimalServerUrl = StorageHelper.GetOptimalServerUrl();

            if (!await IsServerHealthy(optimalServerUrl))
            {
                optimalServerUrl = Settings.PartitionLeader;
            }

            if (!await IsServerHealthy(optimalServerUrl))
            {
                optimalServerUrl = Settings.Server2;
            }

            if (!await IsServerHealthy(optimalServerUrl))
            {
                return;
            }

            PrintConsole.Write(
                $"No data backup found for id: {data.Key}. Creating a backup on server: {optimalServerUrl}",
                ConsoleColor.Red);

            var result = await _httpService.Save(data.Value, optimalServerUrl);

            result?.UpdateServerStatus();
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
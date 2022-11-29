﻿using Server2.Configurations;
using Server2.Helpers;
using Server2.Models;
using Server2.Services.DataService;
using Server2.Services.Http;

namespace Server2.Services.SyncService;

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
        var backupServer1 = await _httpService.GetById(data.Key, Settings.Server1);
        var backupServer2 = await _httpService.GetById(data.Key, Settings.PartitionLeader);

        if (backupServer1?.Value == null && backupServer2?.Value == null)
        {
            var optimalServerUrl = StorageHelper.GetOptimalServerUrl();
            PrintConsole.Write($"No data backup found for id: {data.Key}. Creating a backup on server: {optimalServerUrl}", ConsoleColor.Red);

            var result = await _httpService.Save(data.Value, optimalServerUrl);
            
            result?.UpdateServerStatus();
        }
    }
}
﻿using Server1.Configurations;
using Server1.Models;

namespace Server1.Helpers;

public static class StorageHelper
{
    private static ResultSummary _partitionLeaderStatus = new ResultSummary(){
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };

    private static ResultSummary _server1Status = new ResultSummary(){
        StorageCount = 0,
        LastProcessedId = 0
    };

    private static ResultSummary _server2Status = new ResultSummary()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };
    
    public static string GetOptimalServerUrl()
    {
        var optimalServer = _partitionLeaderStatus;
        if (optimalServer.StorageCount > _server1Status.StorageCount)
        {
            optimalServer = _server1Status;
        }
        
        if (optimalServer.StorageCount > _server2Status.StorageCount)
        {
            optimalServer = _server2Status;
        }

        return $"{Settings.BaseUrl}{optimalServer.Port}";
    }
    
    public static List<ServerName> GetOptimalServers()
    {
        var servers = new List<ServerName>();

        var optimalServer1 = _partitionLeaderStatus;
        var optimalServer2 = _server1Status;

        if (_server2Status.StorageCount < optimalServer1.StorageCount)
        {
            optimalServer1 = _server2Status;
        }
        else if (_server2Status.StorageCount < optimalServer2.StorageCount)
        {
            optimalServer2 = _server2Status;
        }
        
        servers.Add(optimalServer1.ServerName);
        servers.Add(optimalServer2.ServerName);

        return servers;
    }

    public static void UpdateServerStatus(this ResultSummary? summary)
    {
        if (summary != null)
        {
            switch (summary.ServerName)
            {
                case ServerName.PartitionLeader:
                    _partitionLeaderStatus = summary;
                    break;
                case ServerName.Server1:
                    _server1Status = summary;
                    break;
                case ServerName.Server2:
                    _server2Status = summary;
                    break;
            }
        }
    }

    public static ResultSummary? GetStatus()
    {
        return _server1Status;
    }
}
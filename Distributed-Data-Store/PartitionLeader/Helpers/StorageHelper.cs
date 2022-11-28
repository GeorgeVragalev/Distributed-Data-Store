using PartitionLeader.Configurations;
using PartitionLeader.Models;

namespace PartitionLeader.Helpers;

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
        LastProcessedId = 0,
        Port = Settings.Server1Port,
        ServerName = ServerName.Server1
    };

    private static ResultSummary _server2Status = new ResultSummary()
    {
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.Server2Port,
        ServerName = ServerName.Server2
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

        /*
        if (_server2Status.StorageCount < optimalServer1.StorageCount)
        {
            optimalServer1 = _server2Status;
        }
        else if (_server2Status.StorageCount < optimalServer2.StorageCount)
        {
            optimalServer2 = _server2Status;
        }
        */
        
        servers.Add(optimalServer1.ServerName);
        servers.Add(optimalServer2.ServerName);

        return servers;
    }

    public static void UpdateServerStatus(this ResultSummary? summary)
    {
        if (summary != null)
            _server1Status = summary;
    }

    public static ResultSummary? GetStatus()
    {
        return _partitionLeaderStatus;
    }
}
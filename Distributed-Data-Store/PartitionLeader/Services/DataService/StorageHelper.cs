using PartitionLeader.Configurations;
using PartitionLeader.Models;

namespace PartitionLeader.Services.DataService;

public static class StorageHelper
{
    public static ResultSummary? PartitionLeaderStatus = new ResultSummary(){
        StorageCount = 0,
        LastProcessedId = 0,
        Port = Settings.ThisPort,
        ServerName = Settings.ServerName
    };
    public static ResultSummary? Server_1_Status = new ResultSummary(){
        StorageCount = 0,
        LastProcessedId = 0
    };
    public static ResultSummary? Server_2_Status = new ResultSummary()
    {
        StorageCount = 0,
        LastProcessedId = 0
    };
    
    public static string GetOptimalServerUrl()
    {
        var optimalServer = PartitionLeaderStatus;
        if (optimalServer.StorageCount > Server_1_Status.StorageCount)
        {
            optimalServer = Server_1_Status;
        }
        
        if (optimalServer.StorageCount > Server_2_Status.StorageCount)
        {
            optimalServer = Server_2_Status;
        }

        return $"{Settings.BaseUrl}{optimalServer.Port}";
    }

    public static void UpdateServerStatus(this ResultSummary? summary)
    {
        switch (summary.ServerName)
        {
            case ServerName.PartitionLeader:
                PartitionLeaderStatus = summary;
                break;
            case ServerName.Server1:
                Server_1_Status = summary;
                break;
            case ServerName.Server2:
                Server_2_Status = summary;
                break;
            default:
                break;
        }
    }

    public static ResultSummary? GetStatus()
    {
        return PartitionLeaderStatus;
    }
}
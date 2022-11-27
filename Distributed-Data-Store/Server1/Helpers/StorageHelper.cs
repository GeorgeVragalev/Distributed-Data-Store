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

    public static void UpdateServerStatus(this ResultSummary? summary)
    {
        if (summary != null)
            _server1Status = summary;
    }

    public static ResultSummary? GetStatus()
    {
        return _server1Status;
    }
}
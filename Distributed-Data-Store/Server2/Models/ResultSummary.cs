using Server2.Configurations;

namespace Server2.Models;

public class ResultSummary
{
    public int StorageCount { get; set; }

    public int LastProcessedId { get; set; }

    public int Port { get; set; }

    public ServerName ServerName { get; set; }

    public ResultSummary()
    {
        Port = Settings.Port;
        ServerName = Settings.ServerName;
    }
}
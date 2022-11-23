using Server1.Configurations;

namespace Server1.Models;

public class ResultSummary
{
    public int StorageCount { get; set; }

    public int LastProcessedId { get; set; }

    public int Port { get; set; }

    public ServerName ServerName { get; set; }

    public ResultSummary()
    {
        Port = Settings.ThisPort;
        ServerName = Settings.ServerName;
    }
}
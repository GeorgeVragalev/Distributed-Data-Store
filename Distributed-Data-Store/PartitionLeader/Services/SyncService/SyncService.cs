namespace PartitionLeader.Services.SyncService;

public class SyncService : ISyncService
{
    public async Task SyncData(CancellationToken cancellationToken)
    {
        //sync all data between clusters
    }
}
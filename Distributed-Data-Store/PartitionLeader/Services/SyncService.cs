namespace PartitionLeader.Services;

public class SyncService : ISyncService
{
    public void SyncData(CancellationToken cancellationToken)
    {
        //sync all data between clusters
    }
}
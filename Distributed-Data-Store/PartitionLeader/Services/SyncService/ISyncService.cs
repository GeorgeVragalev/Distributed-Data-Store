namespace PartitionLeader.Services.SyncService;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}
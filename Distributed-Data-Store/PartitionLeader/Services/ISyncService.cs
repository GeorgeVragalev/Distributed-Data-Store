namespace PartitionLeader.Services;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}
namespace PartitionLeader.Services.SyncService;

public interface ISyncService
{
    public Task SyncData(CancellationToken cancellationToken);
}
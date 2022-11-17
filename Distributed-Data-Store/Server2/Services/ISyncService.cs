namespace Server2.Services;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}
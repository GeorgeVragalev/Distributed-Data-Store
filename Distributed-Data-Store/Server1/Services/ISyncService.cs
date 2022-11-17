namespace Server1.Services;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}
﻿namespace Server2.Services.SyncService;

public interface ISyncService
{
    public Task SyncData(CancellationToken cancellationToken);
    public Task<bool> IsServerHealthy(string url);
}
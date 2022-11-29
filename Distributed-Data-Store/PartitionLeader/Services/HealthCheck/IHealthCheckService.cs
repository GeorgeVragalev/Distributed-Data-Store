namespace PartitionLeader.Services.HealthCheck;

public interface IHealthCheckService
{
    public Task CheckHealth();
}
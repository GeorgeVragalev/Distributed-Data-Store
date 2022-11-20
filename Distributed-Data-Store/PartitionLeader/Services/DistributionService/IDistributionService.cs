using PartitionLeader.Models;

namespace PartitionLeader.Services.DistributionService;

public interface IDistributionService
{
    public void Client();
    public void Listener();
}
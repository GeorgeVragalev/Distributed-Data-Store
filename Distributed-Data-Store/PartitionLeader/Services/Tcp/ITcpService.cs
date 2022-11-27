using PartitionLeader.Models;

namespace PartitionLeader.Services.Tcp;

public interface ITcpService
{
    public string TcpSave(Data data);
}
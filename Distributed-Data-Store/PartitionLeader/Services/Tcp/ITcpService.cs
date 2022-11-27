using PartitionLeader.Models;

namespace PartitionLeader.Services.Tcp;

public interface ITcpService
{
    public ResultSummary? TcpSave(Data data, int serverPort);
}
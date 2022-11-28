using Server2.Models;

namespace Server2.Services.Tcp;

public interface ITcpService
{
    public Task Run();
    public ResultSummary? TcpSave(Data data, int serverPort);
}
using Server1.Models;

namespace Server1.Services.Tcp;

public interface ITcpService
{
    public Task Run();
}
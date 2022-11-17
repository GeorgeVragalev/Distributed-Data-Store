using Microsoft.AspNetCore.Mvc;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("/server")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public async Task Index()
    {
        
    }
}
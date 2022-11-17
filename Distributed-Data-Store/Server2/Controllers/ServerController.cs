using Microsoft.AspNetCore.Mvc;

namespace Server2.Controllers;

[ApiController]
[Route("/server")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public async Task Index()
    {
        
    }
}
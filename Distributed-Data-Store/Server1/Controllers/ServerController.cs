using Microsoft.AspNetCore.Mvc;

namespace Server1.Controllers;

[ApiController]
[Route("/server")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public async Task Index()
    {
        
    }
}
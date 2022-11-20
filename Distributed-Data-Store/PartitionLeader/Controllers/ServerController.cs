using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("/server")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;

    public ServerController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpPost]
    public async Task Save([FromBody] Data data)
    {
        await _dataService.Save(data);
    }
}
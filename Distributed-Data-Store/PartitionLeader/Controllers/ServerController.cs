using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.Http;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IHttpService _httpService;

    public ServerController(IDataService dataService, IHttpService httpService)
    {
        _dataService = dataService;
        _httpService = httpService;
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>> GetById([FromRoute] int id)
    {
        return await Task.FromResult(_dataService.GetById(id));
    }
    
    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>> GetAll()
    {
        return await Task.FromResult(_dataService.GetAll());
    }
    
    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] Data data)
    {
        return await _dataService.Update(id, data);
    }

    [HttpPost]
    public async Task<ResultSummary> Save([FromForm] Data data)
    {
        var url = StorageStatus.GetOptimalServerUrl();
        var result =  await _dataService.Save(data);
        
        return result;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<ResultSummary> Delete([FromRoute] int id)
    {
        return await _dataService.Delete(id);
    }
}
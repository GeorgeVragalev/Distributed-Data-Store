using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
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
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _dataService.GetById(id);
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataService.GetAll();
    }
    
    [HttpGet("/summary")]
    public async Task<IDictionary<int, Data>?> GetSummary()
    {
        return await _dataService.GetAll();
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.Map();
        return await _dataService.Update(id, data);
    }

    [HttpPost]
    public async Task<ResultSummary> Save([FromForm] DataModel dataModel)
    {
        var data = dataModel.Map();
        var result = await _dataService.Save(data);
        try
        {
            var server1Result = await _httpService.Save(data, Settings.Server1);
            var server2Result = await _httpService.Save(data, Settings.Server2);
            
            server1Result.UpdateServerStatus();
            server2Result.UpdateServerStatus();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        result.UpdateServerStatus();
        return result;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<ResultSummary> Delete([FromRoute] int id)
    {
        var result = await _dataService.Delete(id);
        result.UpdateServerStatus();
        return result;
    }
}
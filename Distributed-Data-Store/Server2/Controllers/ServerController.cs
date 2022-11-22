using Microsoft.AspNetCore.Mvc;
using Server2.Models;
using Server2.Services.DataService;
using Server2.Services.Http;

namespace Server2.Controllers;

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
    
    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] Data data)
    {
        return await _dataService.Update(id, data);
    }

    [HttpPost]
    public async Task<ResultSummary> Save([FromForm] Data data)
    {
        var result =  await _dataService.Save(data);
        
        result.UpdateServerStatus();
        return result;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<ResultSummary> Delete([FromRoute] int id)
    {
        var result =  await _dataService.Delete(id);
        result.UpdateServerStatus();
        return result;
    }
}
using Microsoft.AspNetCore.Mvc;
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;
using Server1.Services.Http;

namespace Server1.Controllers;

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
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _dataService.Update(id, data);
    }

    //convert to  dto object
    //map object
    //add memorystream to dto and save dto not Data
    [HttpPost]
    public async Task<ResultSummary> Save([FromBody] Data data)
    {
        var result = await _dataService.Save(data);
        
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
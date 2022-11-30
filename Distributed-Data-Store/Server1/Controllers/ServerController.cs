using Microsoft.AspNetCore.Mvc;
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;
using Server1.Services.DistributionService;
using Server1.Services.Http;

namespace Server1.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;
    private readonly IDataService _dataService;

    public ServerController(IDistributionService distributionService, IDataService dataService)
    {
        _distributionService = distributionService;
        _dataService = dataService;
    }
    
    [HttpGet("/check")]
    public Task<bool> CheckStatus()
    {
        return Task.FromResult(true);
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>?> GetById([FromRoute] int id)
    {
        return await _distributionService.GetById(id);
    }

    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _distributionService.GetAll();
    }

    [HttpGet("/summary")]
    public async Task<ResultSummary?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatus());
    }
    
    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromBody] Data data)
    {
        return await _distributionService.Update(id, data);
    }
    
    [HttpPost]
    public async Task<ResultSummary> Save([FromBody] Data data)
    {
        ResultSummary resultSummary = new ResultSummary();
        try
        {
            resultSummary = await _dataService.Save(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return resultSummary;
    }
  

    [HttpDelete("/delete/{id}")]
    public async Task<ResultSummary> Delete([FromRoute] int id)
    {
        return await _distributionService.Delete(id);
    }

    #region FormBody Endpoints

    [HttpPut("/form/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
        var data = dataModel.Map();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    [HttpPost("/form/save")]
    public async Task<IList<ResultSummary>> Save([FromForm] DataModel dataModel)
    {
        if (!Settings.Leader)
        {
            return null;
        }
        
        var data = dataModel.Map();

        IList<ResultSummary> resultSummaries = new List<ResultSummary>();
        try
        {
            resultSummaries = await _distributionService.Save(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return resultSummaries;
    }

    #endregion
}
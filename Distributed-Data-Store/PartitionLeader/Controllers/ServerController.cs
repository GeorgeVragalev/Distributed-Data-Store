using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.DistributionService;
using PartitionLeader.Services.Http;
using PartitionLeader.Services.Tcp;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDistributionService _distributionService;
    private readonly IHttpService _httpService;

    public ServerController(IDistributionService distributionService, IHttpService httpService)
    {
        _distributionService = distributionService;
        _httpService = httpService;
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
    public async Task<IList<ResultSummary>?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatusFromServers());
    }

    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.Map();

        var updateResult = await _distributionService.Update(id, data);

        return updateResult;
    }

    [HttpPost]
    public async Task<IList<ResultSummary>> Save([FromForm] DataModel dataModel)
    {
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

    [HttpDelete("/delete/{id}")]
    public async Task<IList<ResultSummary>> Delete([FromRoute] int id)
    {
        return await _distributionService.Delete(id);
    }
}
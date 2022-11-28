﻿using Microsoft.AspNetCore.Mvc;
using Server2.Configurations;
using Server2.Helpers;
using Server2.Models;
using Server2.Services.DistributionService;
using Server2.Services.Http;

namespace Server2.Controllers;

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
    public async Task<IList<ResultSummary>> Save([FromBody] Data data)
    {
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
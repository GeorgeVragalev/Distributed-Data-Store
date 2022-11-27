using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.Http;
using PartitionLeader.Services.Tcp;

namespace PartitionLeader.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IHttpService _httpService;
    private readonly ITcpService _tcpService;

    public ServerController(IDataService dataService, IHttpService httpService, ITcpService tcpService)
    {
        _dataService = dataService;
        _httpService = httpService;
        _tcpService = tcpService;
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
    public async Task<ResultSummary?> GetSummary()
    {
        return await Task.FromResult(StorageHelper.GetStatus());
    }

    [HttpPut("/update/{id}")]
    public async Task<List<Data>> Update([FromRoute] int id, [FromForm] DataModel dataModel)
    {
        var data = dataModel.Map();
        var updatedData = await _dataService.Update(id, data);
        var list = new List<Data>();
        
        list.Add(updatedData);
        
        try
        {
            var server1Result = await _httpService.Update(id, data, Settings.Server1);
            Console.WriteLine(server1Result);
            if (server1Result != null )
            {
                list.Add(server1Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            var server2Result = await _httpService.Update(id, data, Settings.Server2);
            Console.WriteLine(server2Result);
            if (server2Result != null )
            {
                list.Add(server2Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return list;
    }

    [HttpPost]
    public async Task<IList<ResultSummary>> Save([FromForm] DataModel dataModel)
    {
        var list = new List<ResultSummary>();
        var data = dataModel.Map();
        var result = await _dataService.Save(data);
        result.UpdateServerStatus();
        list.Add(result);
        
        var response = _tcpService.TcpSave(data);

        try
        {
            var server1Result = await _httpService.Save(data, Settings.Server1);
            Console.WriteLine(server1Result);
            server1Result.UpdateServerStatus();
            if (server1Result != null )
            {
                list.Add(server1Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            var server2Result = await _httpService.Save(data, Settings.Server2);
            Console.WriteLine(server2Result);
            server2Result.UpdateServerStatus();
            if (server2Result != null )
            {
                list.Add(server2Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return list;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<List<ResultSummary>> Delete([FromRoute] int id)
    {
        
        var list = new List<ResultSummary>();
        var result = await _dataService.Delete(id);
        result.UpdateServerStatus();

        list.Add(result);
        
        
        try
        {
            var server1Result = await _httpService.Delete(id, Settings.Server1);
            Console.WriteLine(server1Result);
            server1Result.UpdateServerStatus();
            if (server1Result != null )
            {
                list.Add(server1Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            var server2Result = await _httpService.Delete(id, Settings.Server2);
            Console.WriteLine(server2Result);
            server2Result.UpdateServerStatus();
            if (server2Result != null )
            {
                list.Add(server2Result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return list;
    }
}
using System.Net;
using System.Net.Sockets;
using System.Text;
using PartitionLeader.Configurations;
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.Http;
using PartitionLeader.Services.Tcp;

namespace PartitionLeader.Services.DistributionService;

public class DistributionService : IDistributionService
{
    private readonly IDataService _dataService;
    private readonly IHttpService _httpService;
    private readonly ITcpService _tcpService;

    public DistributionService(IDataService dataService, IHttpService httpService, ITcpService tcpService)
    {
        _dataService = dataService;
        _httpService = httpService;
        _tcpService = tcpService;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id)
    {
        //try get from local storage if not get from clusters
        var res = await _dataService.GetById(id);
        if (res.Value.Value == null)
        {
            res = await _httpService.GetById(id, Settings.Server1);
        }

        if (res.Value.Value == null)
        {
            res = await _httpService.GetById(id, Settings.Server2);
        }

        return res;
    }

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        //call ge all from all servers and remove duplicates

        var resultDictionary = await _dataService.GetAll();

        //try to get from server 1 if not get from server 2
        var server1Data = await _httpService.GetAll(Settings.Server1);

        if (resultDictionary != null && server1Data != null)
        {
            foreach (var data in server1Data)
            {
                if (!resultDictionary.ContainsKey(data.Key))
                {
                    resultDictionary.Add(data);
                }
            }
        }
        else if(resultDictionary != null)
        {
            var server2Data = await _httpService.GetAll(Settings.Server2);

            if (server2Data != null)
            {
                foreach (var data in server2Data)
                {
                    if (!resultDictionary.ContainsKey(data.Key))
                    {
                        resultDictionary.Add(data);
                    }
                }
            }
        }

        return resultDictionary;
    }

    public async Task<Data> Update(int id, Data data)
    {
        //update try update all servers
        var server1Data = await _httpService.Update(id, data, Settings.Server1);
        var server2Data = await _httpService.Update(id, data, Settings.Server2);

        return await _dataService.Update(id, data);
    }

    public async Task<IList<ResultSummary>> Save(Data data)
    {
        var results = new List<ResultSummary>();

        var optimalServerNames = StorageHelper.GetOptimalServers();

        if (optimalServerNames.Contains(Settings.ServerName))
        {
            var result = await _dataService.Save(data);
            result.UpdateServerStatus();

            results.Add(result);
        }

        //use tcp to save data to other servers
        if (optimalServerNames.Contains(ServerName.Server1))
        {
            var server1ResponseHttp = await _httpService.Save(data, Settings.Server1);
            var server1Response = _tcpService.TcpSave(data, Settings.Server1TcpSavePort);

            if (server1Response != null)
            {
                server1Response.UpdateServerStatus();
                results.Add(server1Response);
            }
        }

        if (optimalServerNames.Contains(ServerName.Server2))
        {
            var server2Response = _tcpService.TcpSave(data, Settings.Server2TcpSavePort);
            if (server2Response != null)
            {
                server2Response.UpdateServerStatus();
                results.Add(server2Response);
            }
        }

        return results;
    }

    public async Task<IList<ResultSummary>> Delete(int id)
    {
        var results = new List<ResultSummary>();
        var result = await _dataService.Delete(id);
        result.UpdateServerStatus();

        var server1Result = await _httpService.Delete(id, Settings.Server1);
        var server2Result = await _httpService.Delete(id, Settings.Server2);
        
        if (server1Result != null)
        {
            server1Result.UpdateServerStatus();
            results.Add(server1Result);
        }
        
        if (server2Result != null)
        {
            server2Result.UpdateServerStatus();
            results.Add(server2Result);
        }

        return results;
    }
}
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;
using Server1.Services.Http;
using Server1.Services.Tcp;

namespace Server1.Services.DistributionService;

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

        if (Settings.Leader)
        {
            if (res.Value.Value == null)
            {
                res = await _httpService.GetById(id, Settings.Server2);
            }
        }

        return res;
    }

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        //call get all from all servers and remove duplicates

        var resultDictionary = await _dataService.GetAll();

        if (Settings.Leader)
        {
            //try to get from server 1 if not get from server 2
            var server2Data = await _httpService.GetAll(Settings.Server2);

            if (resultDictionary != null && server2Data != null)
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
        if (Settings.Leader)
        {
            var server2Data = await _httpService.Update(id, data, Settings.Server2);
        }

        return await _dataService.Update(id, data);
    }

    public async Task<IList<ResultSummary>> Save(Data data)
    {
        var results = new List<ResultSummary>();

        var result = await _dataService.Save(data);
        result.UpdateServerStatus();

        results.Add(result);

        //use tcp to save data to other servers
        if (Settings.Leader)
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

        if (Settings.Leader)
        {
            var server2Result = await _httpService.Delete(id, Settings.Server2);

            if (server2Result != null)
            {
                server2Result.UpdateServerStatus();
                results.Add(server2Result);
            }
        }

        return results;
    }
}
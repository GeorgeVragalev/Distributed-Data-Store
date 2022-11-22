using System.Net;
using System.Net.Sockets;
using System.Text;
using Server2.Configurations;
using Server2.Models;
using Server2.Services.DataService;
using Server2.Services.Http;

namespace Server2.Services.DistributionService;

public class DistributionService : IDistributionService
{
    private readonly IDataService _dataService;
    private readonly IHttpService _httpService;

    public DistributionService(IDataService dataService, IHttpService httpService)
    {
        _dataService = dataService;
        _httpService = httpService;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id)
    {
        //try get from local storage if not get from clusters
        var res =  await _dataService.GetById(id);
        if (res.Value.Value == null)
        {
            res = await _httpService.GetById(id, Settings.Server1);
        }

        return res;
    }
    
    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataService.GetAll();
    }
    
    public async Task<Data> Update(int id, Data data)
    {
        return await _dataService.Update(id, data);
    }

    public async Task<ResultSummary> Save(Data data)
    {
        var url = StorageHelper.GetOptimalServerUrl();
        
        var result =  await _dataService.Save(data);
        result.UpdateServerStatus();
        return result;
    }

    public async Task<ResultSummary> Delete(int id)
    {
        var result =  await _dataService.Delete(id);
        result.UpdateServerStatus();
        return result;
    }

    
    
    public void Client()
    {
        TcpClient client = new TcpClient(Settings.ServerIP, Settings.Port);

        int byteCount = Encoding.ASCII.GetByteCount("George");
        byte[] sendData = new byte[byteCount];

        NetworkStream stream = client.GetStream();
        stream.Write(sendData, 0, sendData.Length);
        stream.Close();
        client.Close();
    }

    public void Listener()
    {
        IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
        TcpListener server = new TcpListener(ip, Settings.Port);
        TcpClient client = default(TcpClient);
        try
        {
            server.Start();
            Console.WriteLine("Démarrage du Serveur");
            Console.Read();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.Read();
        }

        while (true)
        {
            client = server.AcceptTcpClient();
            byte[] receivedBuffer = new byte[1024];
            NetworkStream stream = client.GetStream();
            stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            string msg = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);
            Console.Write(msg);
            Console.Read();
        }
    }
}
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server1.Configurations;
using Server1.Models;
using Server1.Services.DataService;

namespace Server1.Controllers;

[ApiController]
[Route("")]
public class ServerController : ControllerBase
{
    private readonly IDataService _dataService;

    public ServerController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("/get/{id}")]
    public async Task<KeyValuePair<int, Data>> GetById([FromRoute] int id)
    {
        return await Task.FromResult(_dataService.GetById(id));
    }
    
    [HttpGet("/all")]
    public async Task<IDictionary<int, Data>> GetAll()
    {
        return await Task.FromResult(_dataService.GetAll());
    }
    
    [HttpPut("/update/{id}")]
    public async Task<Data> Update([FromRoute] int id, [FromForm] Data data)
    {
        return await _dataService.Update(id, data);
    }

    [HttpPost]
    public async Task Save([FromForm] Data data)
    {
        await _dataService.Save(data);
        
        TcpClient client = new TcpClient(Settings.ServerIP, Settings.Port);

        int byteCount = Encoding.ASCII.GetByteCount("George");
        byte[] sendData = new byte[byteCount];

        NetworkStream stream = client.GetStream();
        stream.Write(sendData, 0, sendData.Length);
        stream.Close();
        client.Close();
    }

    [HttpDelete("/delete/{id}")]
    public async Task Delete([FromRoute] int id)
    {
        await _dataService.Delete(id);
    }
}
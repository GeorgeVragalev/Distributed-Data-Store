using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Server1.Configurations;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;

namespace Server1.Services.Tcp;
public class TcpService : ITcpService
{
    private readonly IDataService _dataService;

    public TcpService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task Run()
    {
        Console.WriteLine("Server starting !");
 
        // IP Address to listen on. Loopback in this case
        var ipAddr = IPAddress.Loopback;
        // Port to listen on
        var port = Settings.Server1TcpSavePort;
        // Create a network endpoint
        var ep = new IPEndPoint(ipAddr, port);
        // Create and start a TCP listener
        var listener = new TcpListener(ep);
        listener.Start();
 
        Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);
 
        // keep running
        while(true)
        {
            var sender = await listener.AcceptTcpClientAsync();
            // streamToMessage - discussed later
            var request = StreamConverter.StreamToMessage(sender.GetStream());
            if (request != null)
            {
                var responseMessage = await MessageHandler(request);
                SendMessage(responseMessage, sender);
            }
        }
    }

    public async Task<string> MessageHandler(string message)
    {
        Console.WriteLine("Received message: " + message);
        var deserialized = JsonConvert.DeserializeObject<Data>(message);

        var resultSummary = await _dataService.Save(deserialized);
        
        var requestMessage = JsonConvert.SerializeObject(resultSummary);

        Console.WriteLine(deserialized?.FileName);
        Console.WriteLine(resultSummary.StorageCount);
        return requestMessage;
    }
    
    private void SendMessage(string message, TcpClient client)
    {
        // messageToByteArray- discussed later
        var bytes = StreamConverter.MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }
}
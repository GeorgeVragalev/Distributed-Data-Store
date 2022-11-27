using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Server2.Configurations;
using Server2.Models;

namespace Server2.Services.Tcp;
public class TcpService : ITcpService
{
    private readonly Encoding _encoding = Encoding.UTF8;

    public void Run()
    {
        Console.WriteLine("Server starting !");
 
        // IP Address to listen on. Loopback in this case
        var ipAddr = IPAddress.Loopback;
        // Create a network endpoint
        var ep = new IPEndPoint(ipAddr, Settings.Server2TcpSavePort);
        // Create and start a TCP listener
        var listener = new TcpListener(ep);
        listener.Start();
 
        Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);
 
        // keep running
        while(true)
        {
            var sender = listener.AcceptTcpClient();
            // streamToMessage - discussed later
            var request = StreamToMessage(sender.GetStream());
            if (request != null)
            {
                // var responseMessage = MessageHandler(request);
                // SendMessage(responseMessage, sender);
            }
        }
    }
 
    private void SendMessage(string message, TcpClient client)
    {
        // messageToByteArray- discussed later
        var bytes = MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }

    private byte[] MessageToByteArray(string message)
    {
        // get the size of original message
        var messageBytes = _encoding.GetBytes(message);
        var messageSize = messageBytes.Length;
        // add content length bytes to the original size
        var completeSize = messageSize + 4;
        // create a buffer of the size of the complete message size
        var completeMessage = new byte[completeSize];

        // convert message size to bytes
        var sizeBytes = BitConverter.GetBytes(messageSize);
        // copy the size bytes and the message bytes to our overall message to be sent 
        sizeBytes.CopyTo(completeMessage, 0);
        messageBytes.CopyTo(completeMessage, 4);
        return completeMessage;
    }

    private string StreamToMessage(Stream stream)
    {
        // size bytes have been fixed to 4
        var sizeBytes = new byte[4];
        // read the content length
        stream.ReadAsync(sizeBytes, 0, 4);
        var messageSize = BitConverter.ToInt32(sizeBytes, 0);
        // create a buffer of the content length size and read from the stream
        var messageBytes = new byte[messageSize];
        stream.ReadAsync(messageBytes, 0, messageSize);
        // convert message byte array to the message string using the encoding
        var message = _encoding.GetString(messageBytes);
        string result = null!;

        foreach (var c in message)
            if (c != '\0')
            {
                result += c;
            }

        return result;
    }
}
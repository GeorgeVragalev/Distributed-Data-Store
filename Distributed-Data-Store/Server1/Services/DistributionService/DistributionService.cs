using System.Net;
using System.Net.Sockets;
using System.Text;
using Server1.Configurations;

namespace Server1.Services.DistributionService;

public class DistributionService : IDistributionService
{
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
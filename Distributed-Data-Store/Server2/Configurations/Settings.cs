using Server2.Models;

namespace Server2.Configurations;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.Server2;
    
    public static readonly bool Leader = true;
    
    public static readonly bool InDocker = true; // set to false when running on localhost
    
    public static readonly string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public static readonly int LeaderPort = InDocker ? 5112 : 7112;
    public static readonly int Server1Port = InDocker ? 5173 : 7173;
    public static readonly int Server2Port = InDocker ? 5156 : 7156;
    
    public static readonly int ThisPort = Server2Port;
    
    public static readonly int Server1TcpSavePort = 8081;
    public static readonly int Server2TcpSavePort = 8082;
    
    public static readonly string BaseUrl = $"https://{ServerIp}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIp}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"https://{ServerIp}:{LeaderPort}"; //local
    public static readonly string Server1 = $"https://{ServerIp}:{Server1Port}"; //local
    public static readonly string Server2 = $"https://{ServerIp}:{Server2Port}"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
using Server1.Models;

namespace Server1.Configurations;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    
    public static readonly bool InDocker = true; // set to false when running on localhost
    // public static readonly string ServerIP = "localhost";
    public static readonly string ServerIP = "host.docker.internal";

    public static readonly int LeaderPort = InDocker ? 5112 : 7112;
    public static readonly int Server1Port = InDocker ? 5173 : 7173;
    public static readonly int Server2Port = InDocker ? 5156 : 7156;
    
    public static readonly int ThisPort = Server1Port;
    
    public static readonly string BaseUrl = $"https://{ServerIP}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIP}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"https://{ServerIP}:{LeaderPort}"; //local
    public static readonly string Server1 = $"https://{ServerIP}:{Server1Port}"; //local
    public static readonly string Server2 = $"https://{ServerIP}:{Server2Port}"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
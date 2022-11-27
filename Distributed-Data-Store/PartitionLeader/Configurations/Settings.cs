using PartitionLeader.Models;

namespace PartitionLeader.Configurations;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    
    public static readonly bool InDocker = false; // set to false when running on localhost
    
    public static readonly string ServerIp = InDocker ? "host.docker.internal" : "localhost";

    public static readonly int LeaderPort = 7112;
    public static readonly int Server1Port = 7173;
    public static readonly int Server2Port = 7156;
    
    public static readonly int ThisPort = LeaderPort;
    
    public static readonly string BaseUrl = $"https://{ServerIp}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIp}:{ThisPort}"; //docker

    public static readonly string PartitionLeader = $"http://{ServerIp}:{LeaderPort}"; //local
    public static readonly string Server1 = $"http://{ServerIp}:{Server1Port}"; //local
    public static readonly string Server2 = $"http://{ServerIp}:{Server2Port}"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
using PartitionLeader.Models;

namespace PartitionLeader.Configurations;

public static class Settings
{
    public static readonly ServerName ServerName = ServerName.PartitionLeader;
    
    public static readonly bool Leader = true;
    // public static readonly string ServerIP = "localhost";
    public static readonly string ServerIP = "host.docker.internal";  
    public static readonly int Port = 5112;
    public static readonly string BaseUrl = $"https://{ServerIP}:"; //local

    public static readonly string ThisServerUrl = $"https://{ServerIP}:{Port}"; //docker

    public static readonly string PartitionLeader = $"https://{ServerIP}:{7112}"; //local
    public static readonly string Server1 = $"https://{ServerIP}:{7173}"; //local
    public static readonly string Server2 = $"https://{ServerIP}:{7156}"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
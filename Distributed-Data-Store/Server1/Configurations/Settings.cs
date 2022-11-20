namespace Server1.Configurations;

public static class Settings
{
    public static readonly bool Leader = true;
    public static readonly string ServerIP = "localhost";  
    public static readonly int Port = 5173;  
    
    // public static readonly string ThisServerUrl = "http://host.docker.internal:5112"; //docker
    public static readonly string PartitionLeader = $"https://localhost:{5112}"; //local
    public static readonly string Server1 = $"https://localhost:{5173}"; //local
    public static readonly string Server2 = $"https://localhost:{5156}"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
namespace PartitionLeader.Settings;

public static class Settings
{
    public static readonly int Tables = 10;
    public static readonly int Waiters = 5;
    
    public static readonly string ClientUrl = "http://host.docker.internal:7139"; //docker
    // public static readonly string ClientUrl = "https://localhost:7139"; //local
    
    public static readonly int TimeUnit = 1; //seconds = 1000  ms = 1 minutes = 60000 
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
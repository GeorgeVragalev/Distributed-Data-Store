namespace PartitionLeader.Settings;

public static class Settings
{
    public static readonly bool Leader = true;
    
    // public static readonly string ThisServerUrl = "http://host.docker.internal:5112"; //docker
    public static readonly string ThisServerUrl = "https://localhost:5112"; //local
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/
using Microsoft.Extensions.Primitives;

namespace PartitionLeader.Models;

public class Data : Entity
{ 
    public string StreamData { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}
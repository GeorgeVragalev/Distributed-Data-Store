using PartitionLeader.Models;

namespace PartitionLeader.Services.DataService;

public interface IDataService : IStorageService<Data>
{
    public bool DoesKeyExist(int id);
}
using PartitionLeader.Models;

namespace PartitionLeader.Services;

public interface IDataService : IStorageService<Data>
{
    public bool DoesKeyExist(int id);
}
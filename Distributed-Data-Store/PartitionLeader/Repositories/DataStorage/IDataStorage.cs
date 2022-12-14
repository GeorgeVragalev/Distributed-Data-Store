using PartitionLeader.Models;
using PartitionLeader.Repositories.SharedStorage;

namespace PartitionLeader.Repositories.DataStorage;

public interface IDataStorage : IStorageRepository<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
using PartitionLeader.Models;

namespace PartitionLeader.Repositories;

public interface IDataStorage : IStorageRepository<Data>
{
    public bool DoesKeyExist(int id);
}
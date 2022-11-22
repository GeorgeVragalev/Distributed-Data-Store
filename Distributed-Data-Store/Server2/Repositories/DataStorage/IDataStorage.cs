using Server2.Models;
using Server2.Repositories.SharedStorage;

namespace Server2.Repositories.DataStorage;

public interface IDataStorage : IStorageRepository<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
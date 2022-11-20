using Server1.Models;
using Server1.Repositories.SharedStorage;

namespace Server1.Repositories.DataStorage;

public interface IDataStorage : IStorageRepository<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
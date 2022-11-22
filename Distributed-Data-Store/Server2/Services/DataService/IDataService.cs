using Server2.Models;

namespace Server2.Services.DataService;

public interface IDataService : IStorageService<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
using Server1.Models;

namespace Server1.Services.DataService;

public interface IDataService : IStorageService<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
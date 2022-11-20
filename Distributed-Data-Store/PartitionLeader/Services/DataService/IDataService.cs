using PartitionLeader.Models;

namespace PartitionLeader.Services.DataService;

public interface IDataService : IStorageService<Data>
{
    public Task<bool> DoesKeyExist(int id);
}
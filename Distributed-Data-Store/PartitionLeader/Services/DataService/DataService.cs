using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Repositories.DataStorage;

namespace PartitionLeader.Services.DataService;

public class DataService : IDataService
{
    private readonly IDataStorage _dataStorage;

    public DataService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _dataStorage.GetById(id);
    }

    public IDictionary<int, Data> GetAll()
    {
        return _dataStorage.GetAll();
    }

    public async Task Save(Data data)
    {
        var id = IdGenerator.GenerateId();
        await _dataStorage.Save(id, data);
    }

    public Task<Data> Update(int id, Data data)
    {
        return _dataStorage.Update(id, data);
    }

    public async Task Delete(int id)
    {
        await _dataStorage.Delete(id);
    }

    public Task<bool> DoesKeyExist(int id)
    {
        return _dataStorage.DoesKeyExist(id);
    }
}
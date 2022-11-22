using Server2.Helpers;
using Server2.Models;
using Server2.Repositories.DataStorage;

namespace Server2.Services.DataService;

public class DataService : IDataService
{
    private readonly IDataStorage _dataStorage;

    public DataService(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public async Task<KeyValuePair<int, Data>?> GetById(int id)
    {
        return await _dataStorage.GetById(id);
    }

    public async Task<IDictionary<int, Data>?> GetAll()
    {
        return await _dataStorage.GetAll();
    }

    public async Task<ResultSummary> Save(Data data)
    {
        var id = IdGenerator.GenerateId();
        return await _dataStorage.Save(id, data);
    }

    public Task<Data> Update(int id, Data data)
    {
        return _dataStorage.Update(id, data);
    }

    public async Task<ResultSummary> Delete(int id)
    {
        return await _dataStorage.Delete(id);
    }

    public Task<bool> DoesKeyExist(int id)
    {
        return _dataStorage.DoesKeyExist(id);
    }
}
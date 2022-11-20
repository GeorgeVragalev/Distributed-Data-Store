using PartitionLeader.Models;
using PartitionLeader.Repositories.SharedStorage;

namespace PartitionLeader.Repositories.DataStorage;

public class DataStorage : IDataStorage
{
    private readonly IStorageRepository<Data> _dataRepository;

    public DataStorage(IStorageRepository<Data> dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public Task<KeyValuePair<int, Data>> GetById(int id)
    {
        return _dataRepository.GetById(id);
    }

    public Task<IDictionary<int, Data>> GetAll()
    {
        return _dataRepository.GetAll();
    }

    public async Task<ResultSummary> Save(int id, Data data)
    {
        return await _dataRepository.Save(id, data);
    }

    public async Task<Data> Update(int id, Data data)
    {
        return await _dataRepository.Update(id, data);
    }

    public async Task<ResultSummary> Delete(int id)
    {
        return await _dataRepository.Delete(id);
    }

    public async Task<bool> DoesKeyExist(int id)
    {
        var dictionary = _dataRepository.GetAll().Result;
        return dictionary != null && await Task.FromResult(dictionary.ContainsKey(id));
    }
}
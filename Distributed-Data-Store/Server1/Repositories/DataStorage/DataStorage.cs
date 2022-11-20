using Server1.Models;
using Server1.Repositories.SharedStorage;

namespace Server1.Repositories.DataStorage;

public class DataStorage : IDataStorage
{
    private readonly IStorageRepository<Data> _dataRepository;

    public DataStorage(IStorageRepository<Data> dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public KeyValuePair<int, Data> GetById(int id)
    {
        return _dataRepository.GetById(id);
    }

    public IDictionary<int, Data> GetAll()
    {
        return _dataRepository.GetAll();
    }

    public async Task Save(int id, Data data)
    {
        await _dataRepository.Save(id, data);
    }

    public async Task<Data> Update(int id, Data data)
    {
        return await _dataRepository.Update(id, data);
    }

    public async Task Delete(int id)
    {
        await _dataRepository.Delete(id);
    }

    public Task<bool> DoesKeyExist(int id)
    {
        return Task.FromResult(_dataRepository.GetAll().ContainsKey(id));
    }
}
using PartitionLeader.Models;
using PartitionLeader.Repositories.SharedStorage;

namespace PartitionLeader.Repositories.DataStorage;

public class DataStorage : IDataStorage
{
    private readonly StorageRepository<Data> _dataRepository;

    public DataStorage(StorageRepository<Data> dataRepository)
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

    public void Save(int id, Data data)
    {
        _dataRepository.Save(id, data);
    }

    public Data Update(int id, Data data)
    {
        return _dataRepository.Update(id, data);
    }

    public void Delete(int id)
    {
        _dataRepository.Delete(id);
    }

    public bool DoesKeyExist(int id)
    {
        return _dataRepository.GetAll().ContainsKey(id);
    }
}
using PartitionLeader.Helpers;
using PartitionLeader.Models;
using PartitionLeader.Repositories;

namespace PartitionLeader.Services;

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

    public void Save(Data data)
    {
        var id = IdGenerator.GenerateId();
        _dataStorage.Save(id, data);
    }

    public Data Update(int id, Data data)
    {
        return _dataStorage.Update(id, data);
    }

    public void Delete(int id)
    {
        _dataStorage.Delete(id);
    }

    public bool DoesKeyExist(int id)
    {
        return _dataStorage.DoesKeyExist(id);
    }
}
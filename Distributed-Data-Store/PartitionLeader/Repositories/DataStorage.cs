using PartitionLeader.Models;

namespace PartitionLeader.Repositories;

public class DataStorage : IDataStorage
{
    private readonly StorageRepository<Data> _dataRepository;

    public DataStorage(StorageRepository<Data> dataRepository)
    {
        _dataRepository = dataRepository;
    }
    
    public Data? GetById(int id)
    {
        return _dataRepository.GetById(id);
    }

    public IList<Data> GetAll()
    {
        return _dataRepository.GetAll();
    }

    public void Save(Data entity)
    {
        _dataRepository.Save(entity);
    }

    public Data Update(Data entity)
    {
        return _dataRepository.Update(entity);
    }

    public void Delete(Data entity)
    {
        _dataRepository.Delete(entity);
    }

    public bool DoesKeyExist(Data data)
    {
        throw new NotImplementedException();
    }
}
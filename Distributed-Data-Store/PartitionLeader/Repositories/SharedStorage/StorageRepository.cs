using PartitionLeader.Configurations;
using PartitionLeader.Models;

namespace PartitionLeader.Repositories.SharedStorage;

public class StorageRepository <T> : IStorageRepository<T> where T : Entity
{
    private IDictionary<int, T> _storage = new Dictionary<int, T>();

    public KeyValuePair<int, T> GetById(int id)
    {
        return _storage.FirstOrDefault(s=>s.Key==id);
    }

    public IDictionary<int, T> GetAll()
    {
        return _storage;
    }

    public Task<ResultSummary> Save(int id, T entity)
    {
        _storage.Add(id, entity);
        return Task.FromResult(new ResultSummary()
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }

    public Task<T> Update(int id, T entity)
    {
        return Task.FromResult(_storage[id] = entity);;
    }

    public Task<ResultSummary> Delete(int id)
    {
        _storage.Remove(id); 
        return Task.FromResult(new ResultSummary()
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });;
    }
}
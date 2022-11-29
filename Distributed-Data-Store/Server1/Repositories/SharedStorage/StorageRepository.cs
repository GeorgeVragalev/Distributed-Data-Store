using Server1.Models;

namespace Server1.Repositories.SharedStorage;

public class StorageRepository <T> : IStorageRepository<T> where T : Entity
{
    private IDictionary<int, T> _storage = new Dictionary<int, T>();

    public Task<KeyValuePair<int, T>> GetById(int id)
    {
        return Task.FromResult(_storage.FirstOrDefault(s=>s.Key==id));
    }

    public Task<IDictionary<int, T>> GetAll()
    {
        return Task.FromResult(_storage);
    }

    public Task<ResultSummary> Save(int id, T entity)
    {
        entity.Id = id;
        _storage.TryAdd(id, entity);
        return Task.FromResult(new ResultSummary()
        {
            StorageCount = _storage.Count,
            LastProcessedId = id
        });
    }

    public Task<T> Update(int id, T entity)
    {
        try
        {
            entity.Id = id;
            _storage[id] = entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return Task.FromResult(entity);
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
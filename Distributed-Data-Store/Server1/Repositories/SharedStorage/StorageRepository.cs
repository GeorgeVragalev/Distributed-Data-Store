using Server1.Models;

namespace Server1.Repositories.SharedStorage;

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

    public Task Save(int id, T entity)
    {
        _storage.Add(id, entity);
        return Task.CompletedTask;
    }

    public Task<T> Update(int id, T entity)
    {
        return Task.FromResult(_storage[id] = entity);;
    }

    public Task Delete(int id)
    {
        _storage.Remove(id);
        return Task.CompletedTask;
    }
}
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

    public void Save(int id, T entity)
    {
        _storage.Add(id, entity);
    }

    public T Update(int id, T entity)
    {
        return _storage[id] = entity;;
    }

    public void Delete(int id)
    {
        _storage.Remove(id);
    }
}
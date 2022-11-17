using PartitionLeader.Models;

namespace PartitionLeader.Repositories;

public class StorageRepository <T> : IStorageRepository<T> where T : Entity
{
    private IList<T> _storage = new List<T>();

    public T? GetById(int id)
    {
        return _storage.FirstOrDefault(a=>a.Id == id);
    }

    public IList<T> GetAll()
    {
        return _storage;
    }

    public void Save(T entity)
    {
        _storage.Add(entity);
    }

    public T Update(T entity)
    {
        var entityInList = GetById(entity.Id);
        entityInList = entity;
        return entityInList;
    }

    public void Delete(T entity)
    {
        _storage.Remove(entity);
    }
}
using PartitionLeader.Models;

namespace PartitionLeader.Services;

public interface IStorageService <T> where T : Entity
{
    public KeyValuePair<int, T> GetById(int id);
    public IDictionary<int, T> GetAll();
    public void Save(T entity);
    public T Update(int id, T entity);
    public void Delete(int id);
}
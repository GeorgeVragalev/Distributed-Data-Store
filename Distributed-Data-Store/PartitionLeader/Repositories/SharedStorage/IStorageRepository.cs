using PartitionLeader.Models;

namespace PartitionLeader.Repositories.SharedStorage;

public interface IStorageRepository <T> where T : Entity
{
    public KeyValuePair<int, T> GetById(int id);
    public IDictionary<int, T> GetAll();
    public void Save(int id, T entity);
    public T Update(int id, T entity);
    public void Delete(int id);
}
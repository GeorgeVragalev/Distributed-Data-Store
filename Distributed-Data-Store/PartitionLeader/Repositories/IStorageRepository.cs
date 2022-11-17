using PartitionLeader.Models;

namespace PartitionLeader.Repositories;

public interface IStorageRepository <T> where T : Entity
{
    public T? GetById(int id);
    public IList<T> GetAll();
    public void Save(T entity);
    public T Update(T entity);
    public void Delete(T entity);
}
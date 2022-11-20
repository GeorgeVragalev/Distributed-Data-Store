using Server1.Models;

namespace Server1.Repositories.SharedStorage;

public interface IStorageRepository <T> where T : Entity
{
    public KeyValuePair<int, T> GetById(int id);
    public IDictionary<int, T> GetAll();
    public Task Save(int id, T entity);
    public Task<T> Update(int id, T entity);
    public Task Delete(int id);
}
using PartitionLeader.Models;

namespace PartitionLeader.Services;

public interface IStorageService <T> where T : Entity
{
    public KeyValuePair<int, T> GetById(int id);
    public IDictionary<int, T> GetAll();
    public Task<ResultSummary> Save(T entity);
    public Task<T> Update(int id, T entity);
    public Task<ResultSummary> Delete(int id);
}
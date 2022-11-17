using PartitionLeader.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PartitionLeader.Repositories;

public interface IStorageRepository <T> where T : Entity
{
    public KeyValuePair<int, T> GetById(int id);
    public IDictionary<int, T> GetAll();
    public void Save(int id, T entity);
    public T Update(int id, T entity);
    public void Delete(int id);
}
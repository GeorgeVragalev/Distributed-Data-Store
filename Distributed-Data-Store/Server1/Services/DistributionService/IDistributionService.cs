using Server1.Models;

namespace Server1.Services.DistributionService;

public interface IDistributionService
{
    
    public Task<KeyValuePair<int, Data>?> GetById(int id);
    public Task<IDictionary<int, Data>?> GetAll();
    public Task<Data> Update(int id, Data data);
    public Task<IList<ResultSummary>> Save(Data data);
    public Task<IList<ResultSummary>> Delete(int id);
}
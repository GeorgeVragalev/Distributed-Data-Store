using Server2.Models;

namespace Server2.Services.DistributionService;

public interface IDistributionService
{
    
    public Task<KeyValuePair<int, Data>?> GetById(int id);
    public Task<IDictionary<int, Data>?> GetAll();
    public Task<Data> Update(int id, Data data);
    public Task<ResultSummary> Save(Data data);
    public Task<ResultSummary> Delete(int id);
    public void Client();
    public void Listener();
}
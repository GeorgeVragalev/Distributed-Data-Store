using Microsoft.AspNetCore.Mvc;
using PartitionLeader.Models;

namespace PartitionLeader.Services.Http;

public interface IHttpService
{
    public Task<KeyValuePair<int, Data>?> GetById(int id, string url);
    public Task<IDictionary<int, Data>?> GetAll(string url);
    public Task<Data> Update(int id, [FromForm] Data data, string url);
    public Task<ResultSummary?> Save([FromForm] Data data, string url);
    public Task<ResultSummary> Delete([FromRoute] int id, string url);
}
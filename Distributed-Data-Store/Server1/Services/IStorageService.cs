﻿using Server1.Models;

namespace Server1.Services;

public interface IStorageService <T> where T : Entity
{
    public Task<KeyValuePair<int, Data>?> GetById(int id);
    public Task<IDictionary<int, Data>?> GetAll();
    public Task<ResultSummary> Save(T entity);
    public Task<T> Update(int id, T entity);
    public Task<ResultSummary> Delete(int id);
}
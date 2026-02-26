using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Repository.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetDataAsync<T>(string connectionName, string storedProcName, DynamicParameters? parameters = null);
        Task<bool> SaveDataAsync(string connectionName, string storedProcName, DynamicParameters? parameters = null);
    }
}

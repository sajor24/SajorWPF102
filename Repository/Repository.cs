using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;

namespace Repository;

public class Repository : IRepository
{
    private readonly IConfiguration _configuration;

    public Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SaveDataAsync(string connectionName, string storedProcedureName, DynamicParameters? parameters = null)
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        using IDbConnection connection = new SqlConnection(connectionString);
        var rows = await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        return rows > 0;
    }

    public async Task<IEnumerable<T>> GetDataAsync<T>(string connectionName, string storedProcedureName, DynamicParameters? parameters = null) where T : class
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        using IDbConnection connection = new SqlConnection(connectionString);
        if (parameters != null) return await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        else return await connection.QueryAsync<T>(storedProcedureName, new { }, commandType: CommandType.StoredProcedure);
    }
}



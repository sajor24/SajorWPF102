using System.Data;
using Dapper;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Framework;

public class Repository
{
    private readonly IConfiguration _configuration;

    public Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SaveDataAsync(string connectionName, string storedProcedureName, DynamicParameters parameters)
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{connectionName}' not found in configuration.");
        }

        using var connection = new SqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Database error while executing '{storedProcedureName}': {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<T>?> GetDataAsync<T>(string connectionName, string storedProcedureName, DynamicParameters? parameters) where T : class
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{connectionName}' not found in configuration.");
        }

        using var connection = new SqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            if (parameters != null) return await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            else return await connection.QueryAsync<T>(storedProcedureName, new { }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Database error while executing '{storedProcedureName}': {ex.Message}", ex);
        }
    }
}

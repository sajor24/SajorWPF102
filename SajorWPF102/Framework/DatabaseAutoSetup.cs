using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Framework
{
    public class DatabaseAutoSetup
    {
        private readonly IConfiguration _configuration;

        public DatabaseAutoSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnsureDatabaseSetupAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            // Extract database name
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            var masterConnectionString = connectionString.Replace($"Initial Catalog={databaseName}", "Initial Catalog=master");

            try
            {
                // Step 1: Create database if not exists
                await CreateDatabaseIfNotExistsAsync(masterConnectionString, databaseName);

                // Step 2: Create table if not exists
                await CreateTableIfNotExistsAsync(connectionString);

                // Step 3: Create stored procedures
                await CreateStoredProceduresAsync(connectionString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database auto-setup failed: {ex.Message}", ex);
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync(string masterConnectionString, string databaseName)
        {
            using var connection = new SqlConnection(masterConnectionString);
            await connection.OpenAsync();

            var checkDbSql = $"SELECT database_id FROM sys.databases WHERE name = '{databaseName}'";
            using var checkCmd = new SqlCommand(checkDbSql, connection);
            var result = await checkCmd.ExecuteScalarAsync();

            if (result == null)
            {
                var createDbSql = $"CREATE DATABASE [{databaseName}]";
                using var createCmd = new SqlCommand(createDbSql, connection);
                await createCmd.ExecuteNonQueryAsync();
            }
        }

        private async Task CreateTableIfNotExistsAsync(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employee')
                BEGIN
                    CREATE TABLE [dbo].[Employee]
                    (
                        [EmployeeId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        [FirstName] NVARCHAR(100) NOT NULL,
                        [LastName] NVARCHAR(100) NOT NULL,
                        [Age] INT NOT NULL,
                        [Position] NVARCHAR(100) NOT NULL
                    );
                END";

            using var command = new SqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }

        private async Task CreateStoredProceduresAsync(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var procedures = new[]
            {
                // CreateEmployee
                @"IF OBJECT_ID('[dbo].[CreateEmployee]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[CreateEmployee];",
                @"CREATE PROCEDURE [dbo].[CreateEmployee]
                    @FirstName NVARCHAR(100),
                    @LastName NVARCHAR(100),
                    @Age INT,
                    @Position NVARCHAR(100)
                AS
                BEGIN
                    INSERT INTO [dbo].[Employee] (FirstName, LastName, Age, Position)
                    VALUES (@FirstName, @LastName, @Age, @Position);
                END",

                // UpdateEmployee
                @"IF OBJECT_ID('[dbo].[UpdateEmployee]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[UpdateEmployee];",
                @"CREATE PROCEDURE [dbo].[UpdateEmployee]
                    @EmployeeId INT,
                    @FirstName NVARCHAR(100),
                    @LastName NVARCHAR(100),
                    @Age INT,
                    @Position NVARCHAR(100)
                AS
                BEGIN
                    UPDATE [dbo].[Employee]
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        Age = @Age,
                        Position = @Position
                    WHERE EmployeeId = @EmployeeId;
                END",

                // DeleteEmployee
                @"IF OBJECT_ID('[dbo].[DeleteEmployee]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[DeleteEmployee];",
                @"CREATE PROCEDURE [dbo].[DeleteEmployee]
                    @EmployeeId INT
                AS
                BEGIN
                    DELETE FROM [dbo].[Employee]
                    WHERE EmployeeId = @EmployeeId;
                END",

                // GetAllEmployee
                @"IF OBJECT_ID('[dbo].[GetAllEmployee]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[GetAllEmployee];",
                @"CREATE PROCEDURE [dbo].[GetAllEmployee]
                AS
                BEGIN
                    SELECT * FROM Employee ORDER BY EmployeeId;
                END",

                // ReadEmployeeById
                @"IF OBJECT_ID('[dbo].[ReadEmployeeById]', 'P') IS NOT NULL DROP PROCEDURE [dbo].[ReadEmployeeById];",
                @"CREATE PROCEDURE [dbo].[ReadEmployeeById]
                    @EmployeeId INT
                AS
                BEGIN
                    SELECT * FROM [dbo].[Employee]
                    WHERE EmployeeId = @EmployeeId;
                END"
            };

            foreach (var sql in procedures)
            {
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    using var command = new SqlCommand(sql, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

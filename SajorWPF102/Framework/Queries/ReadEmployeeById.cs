using Dapper;
using Domain.Models;
using Domain.Queries;
using System.Linq;
using System.Threading.Tasks;
namespace Framework.Queries
{
    public class ReadEmployeeById : IReadEmployeeById
    {
        private readonly Repository _repository;

        public ReadEmployeeById(Repository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeModel?> ExecuteAsync(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var data = await _repository.GetDataAsync<EmployeeModel>(
                "DefaultConnection",
                "[dbo].[GetEmployeeById]",
                parameters
            );

            return data?.FirstOrDefault();
        }
    }
}
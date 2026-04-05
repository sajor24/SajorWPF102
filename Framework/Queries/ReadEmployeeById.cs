using Dapper;
using Domain.Models;
using Domain.Queries;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interfaces;
namespace Framework.Queries
{
    public class ReadEmployeeById : IReadEmployeeById
    {
        private readonly IRepository _repository;

        public ReadEmployeeById(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeModel?> ExecuteAsync(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var data = await _repository.GetDataAsync<EmployeeModel>(
                "DefaultConnection",
                "[dbo].[ReadEmployeeById]",
                parameters
            );

            return data?.FirstOrDefault();
        }
    }
}
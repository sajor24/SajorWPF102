using Domain.Commands;
using Domain.Models;
using Framework.Extensions;

namespace Framework.Commands
{
    public class CreateEmployee : ICreateEmployee
    {
        private readonly Repository _repository;

        public CreateEmployee(Repository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(EmployeeModel employee)
        {
            var parameters = employee.ToCreateEmployeeDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[CreateEmployee]", parameters);
        }
    }
}

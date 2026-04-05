using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands
{
    public class CreateEmployee : ICreateEmployee
    {
        private readonly IRepository _repository;

        public CreateEmployee(IRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(EmployeeModel model)
        {
            var parameters = model.ToCreateEmployeeDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[CreateEmployee]", parameters);
        }
    }
}

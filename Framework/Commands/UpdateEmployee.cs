using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands
{
    public class UpdateEmployee : IUpdateEmployee
    {
        private readonly IRepository _repository;

        public UpdateEmployee(IRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(EmployeeModel model)
        {
            var parameters = model.ToEmployeeDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[UpdateEmployee]", parameters);
        }
    }
}

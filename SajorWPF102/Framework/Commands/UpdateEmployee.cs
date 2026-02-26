using Domain.Commands;
using Domain.Models;
using Framework.Extensions;

namespace Framework.Commands
{
    public class UpdateEmployee : IUpdateEmployee
    {
        private readonly Repository _repository;

        public UpdateEmployee(Repository repository)
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

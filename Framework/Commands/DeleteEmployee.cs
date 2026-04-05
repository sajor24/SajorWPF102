using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands
{

    public class DeleteEmployee : IDeleteEmployee
    {
        private readonly IRepository _repository;

        public DeleteEmployee(IRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(EmployeeModel model)
        {
            var parameters = model.ToDeleteEmployeeDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[DeleteEmployee]", parameters);
        }
    }
}

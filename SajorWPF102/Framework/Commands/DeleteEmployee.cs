using System;
using System.Collections.Generic;
using Domain.Commands;
using Domain.Models;
using Framework.Extensions;

namespace Framework.Commands
{

    public class DeleteEmployee : IDeleteEmployee
    {
        private readonly Repository _repository;

        public DeleteEmployee(Repository repository)
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

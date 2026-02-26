using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Domain.Queries;
namespace Framework.Queries
{
    public class GetAllEmployee :IGetAllEmployees
    {
        private readonly Repository _repository;

        public GetAllEmployee(Repository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeModel>?> ExecuteAsync()
        {
            return await _repository.GetDataAsync<EmployeeModel>("DefaultConnection", "[dbo].[GetAllEmployee]", null);
        }
    }
}

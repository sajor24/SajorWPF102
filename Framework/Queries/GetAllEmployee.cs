using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Domain.Queries;
using Repository.Interfaces;
namespace Framework.Queries
{
    public class GetAllEmployee : IGetAllEmployees
    {
        private readonly IRepository _repository;

        public GetAllEmployee(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeModel>?> ExecuteAsync()
        {
            return await _repository.GetDataAsync<EmployeeModel>("DefaultConnection", "[dbo].[GetAllEmployees]", null);
        }
    }
}

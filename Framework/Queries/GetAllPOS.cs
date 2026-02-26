using Framework.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Queries
{
    public class GetAllPOS
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


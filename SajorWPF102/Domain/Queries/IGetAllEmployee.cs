using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
    public interface IGetAllEmployees
    {
        Task<IEnumerable<EmployeeModel>?> ExecuteAsync();
    }
}

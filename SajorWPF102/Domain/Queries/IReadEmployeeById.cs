using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
    public interface IReadEmployeeById
    {
        Task<EmployeeModel?> ExecuteAsync(int employeeId);
    }
}

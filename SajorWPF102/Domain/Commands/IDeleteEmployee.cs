using System;
using System.Collections.Generic;
using System.Text;
using Domain.Commands;
using Domain.Models;

namespace Domain.Commands
{
    public interface IDeleteEmployee
    {
        Task ExecuteAsync(EmployeeModel model);

    }
}

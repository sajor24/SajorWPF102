using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public interface IUpdateEmployee
    {
        Task ExecuteAsync(EmployeeModel model);

    }
}

using System;
using Domain.Models;
using SajorWPF.ViewModels;

namespace SajorWPF.Commands
{
    public class DeleteEmployeeCommand : BaseCommand
    {
        private readonly AddEmployeeViewModel _viewModel;

        public DeleteEmployeeCommand(AddEmployeeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is EmployeeModel employee)
            {
                _viewModel.DeleteEmployee(employee);
            }
        }
    }
}

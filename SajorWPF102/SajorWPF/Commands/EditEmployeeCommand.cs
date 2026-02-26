using System;
using Domain.Models;
using SajorWPF.ViewModels;

namespace SajorWPF.Commands
{
    public class EditEmployeeCommand : BaseCommand
    {
        private readonly AddEmployeeViewModel _viewModel;

        public EditEmployeeCommand(AddEmployeeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is EmployeeModel employee)
            {
                _viewModel.LoadEmployeeForEdit(employee);
            }
        }
    }
}

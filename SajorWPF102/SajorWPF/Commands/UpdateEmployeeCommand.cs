using System;
using SajorWPF.ViewModels;

namespace SajorWPF.Commands
{
    public class UpdateEmployeeCommand : BaseCommand
    {
        private readonly AddEmployeeViewModel _viewModel;

        public UpdateEmployeeCommand(AddEmployeeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.UpdateEmployee();
        }
    }
}

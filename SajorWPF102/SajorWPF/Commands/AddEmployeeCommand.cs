using System;
using SajorWPF.ViewModels;

namespace SajorWPF.Commands
{
    public class AddEmployeeCommand : BaseCommand
    {
        private readonly AddEmployeeViewModel _viewModel;

        public AddEmployeeCommand(AddEmployeeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.SaveEmployee();
        }
    }
}

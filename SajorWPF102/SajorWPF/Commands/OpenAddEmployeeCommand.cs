using System;
using SajorWPF.Services;

namespace SajorWPF.Commands
{
    public class OpenAddEmployeeCommand : BaseCommand
    {
        private readonly INavigationService _navigationService;

        public OpenAddEmployeeCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}

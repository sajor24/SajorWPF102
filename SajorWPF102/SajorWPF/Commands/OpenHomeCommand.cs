using System;
using SajorWPF.Services;

namespace SajorWPF.Commands
{
    public class OpenHomeCommand : BaseCommand
    {
        private readonly INavigationService _navigationService;

        public OpenHomeCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}

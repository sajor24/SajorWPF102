using System.Windows.Input;
using SajorWPF.Commands;
using SajorWPF.Services;

namespace SajorWPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigateAddEmployeeCommand { get; }

        public HomeViewModel(INavigationService addEmployeeNavigationService)
        {
            NavigateAddEmployeeCommand = new OpenAddEmployeeCommand(addEmployeeNavigationService);
        }
    }
}

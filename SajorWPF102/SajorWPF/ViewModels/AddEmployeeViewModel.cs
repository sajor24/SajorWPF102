using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using SajorWPF.Commands;
using SajorWPF.Services;

namespace SajorWPF.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        private readonly ICreateEmployee _createEmployee;
        private readonly IGetAllEmployees _getAllEmployees;
        private readonly IUpdateEmployee _updateEmployee;
        private readonly IDeleteEmployee _deleteEmployee;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _age;
        private string _position = string.Empty;
        private string _searchText = string.Empty;
        private bool _isEditMode = false;
        private int _currentEmployeeId;
        private ObservableCollection<EmployeeModel> _allEmployees = new ObservableCollection<EmployeeModel>();

        public AddEmployeeViewModel(
            ICreateEmployee createEmployee,
            IGetAllEmployees getAllEmployees,
            IUpdateEmployee updateEmployee,
            IDeleteEmployee deleteEmployee,
            INavigationService homeNavigationService)
        {
            _createEmployee = createEmployee;
            _getAllEmployees = getAllEmployees;
            _updateEmployee = updateEmployee;
            _deleteEmployee = deleteEmployee;

            Employees = new ObservableCollection<EmployeeModel>();

            SaveCommand = new AddEmployeeCommand(this);
            UpdateCommand = new UpdateEmployeeCommand(this);
            DeleteCommand = new DeleteEmployeeCommand(this);
            EditCommand = new EditEmployeeCommand(this);
            CancelCommand = new OpenHomeCommand(homeNavigationService);

            LoadEmployeesAsync();
        }

        public ObservableCollection<EmployeeModel> Employees { get; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterEmployees();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(IsAddMode));
                OnPropertyChanged(nameof(SaveButtonText));
            }
        }

        public bool IsAddMode => !IsEditMode;
        public string SaveButtonText => IsEditMode ? "Update" : "Save";

        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CancelCommand { get; }

        public async void SaveEmployee()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("SaveEmployee called");
                
                if (IsEditMode)
                {
                    await UpdateEmployee();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Creating employee: {FirstName} {LastName}");
                    
                    var employee = new EmployeeModel
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Age = Age,
                        Position = Position
                    };

                    await _createEmployee.ExecuteAsync(employee);
                    
                    System.Diagnostics.Debug.WriteLine("Employee created successfully");

                    ClearForm();
                    await LoadEmployeesAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SaveEmployee: {ex.Message}");
                System.Windows.MessageBox.Show($"Error saving employee: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public async Task UpdateEmployee()
        {
            var employee = new EmployeeModel
            {
                EmployeeId = _currentEmployeeId,
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Position = Position
            };

            await _updateEmployee.ExecuteAsync(employee);

            ClearForm();
            IsEditMode = false;
            await LoadEmployeesAsync();
        }

        public async void DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                await _deleteEmployee.ExecuteAsync(employee);
                await LoadEmployeesAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void LoadEmployeeForEdit(EmployeeModel employee)
        {
            _currentEmployeeId = employee.EmployeeId;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Age = employee.Age;
            Position = employee.Position;
            IsEditMode = true;
        }

        private void ClearForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Position = string.Empty;
            IsEditMode = false;
            _currentEmployeeId = 0;
        }

        private async Task LoadEmployeesAsync()
        {
            var employees = await _getAllEmployees.ExecuteAsync();

            _allEmployees.Clear();
            Employees.Clear();

            if (employees != null)
            {
                foreach (var emp in employees)
                {
                    _allEmployees.Add(emp);
                    Employees.Add(emp);
                }
            }
        }

        private void FilterEmployees()
        {
            Employees.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var emp in _allEmployees)
                {
                    Employees.Add(emp);
                }
            }
            else
            {
                var filtered = _allEmployees.Where(e =>
                    e.FirstName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                    e.LastName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                    e.Position.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

                foreach (var emp in filtered)
                {
                    Employees.Add(emp);
                }
            }
        }
    }
}

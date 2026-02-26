using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;

namespace SajorWPF.VIewModel
{
    public class AddViewEmployee : INotifyPropertyChanged
    {
        private readonly ICreateEmployee _createEmployee;
        private readonly IGetAllEmployees _getAllEmployees;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _age;
        private string _position = string.Empty;

        public AddViewEmployee(ICreateEmployee createEmployee, IGetAllEmployees getAllEmployees)
        {
            _createEmployee = createEmployee;
            _getAllEmployees = getAllEmployees;

            Employees = new ObservableCollection<EmployeeModel>();
            SaveCommand = new RelayCommand(async () => await SaveEmployeeAsync());
            CancelCommand = new RelayCommand(ClearForm);

            LoadEmployeesAsync();
        }

        public ObservableCollection<EmployeeModel> Employees { get; set; }

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

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task SaveEmployeeAsync()
        {
            var employee = new EmployeeModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Position = Position
            };

            await _createEmployee.ExecuteAsync(employee);

            ClearForm();
            await LoadEmployeesAsync();
        }

        private void ClearForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Position = string.Empty;
        }

        private async Task LoadEmployeesAsync()
        {
            var employees = await _getAllEmployees.ExecuteAsync();

            Employees.Clear();
            if (employees != null)
            {
                foreach (var emp in employees)
                {
                    Employees.Add(emp);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}

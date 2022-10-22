using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Todo.Models;
using Xamarin.Forms;


namespace Todo
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public MainPageViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
        }

        public async void GetEmployees()
        {
            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "https://jsonplaceholder.typicode.com/users";
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var EmployeeList = JsonConvert.DeserializeObject<List<TodoItem>>(result);

                Employees = new ObservableCollection<TodoItem>(EmployeeList);
                IsRefreshing = false;

              
            }
        }
        /*public Command AddEmployee
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new AddEmployee(null));
                });
            }
        }
        public Command EditEmployee
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new AddEmployee(null));
                });
            }
        }
        public Command RefreshData
        {
            get
            {
                return new Command(() =>
                {
                    GetEmployees();
                });
            }
        }*/


        bool _isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        /*TodoItem _selectedEmployee;
        public TodoItem SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                if (value != null)
                {
                    Navigation.PushAsync(new AddEmployee(SelectedEmployee));
                }
                OnPropertyChanged();
            }
        }*/

        ObservableCollection<TodoItem> _employees;
        public ObservableCollection<TodoItem> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
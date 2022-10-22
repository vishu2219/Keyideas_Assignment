using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            (this.BindingContext as MainPageViewModel).GetEmployees();
        }
    }
}

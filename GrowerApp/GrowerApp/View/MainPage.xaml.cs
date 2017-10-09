using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowerApp.ViewModel;
using Xamarin.Forms;

namespace GrowerApp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            var vm = (this.BindingContext as MainPageViewModel);
            if (vm.ViewManager.BackStack.Count > 0)
            {
                vm.BackButtonCommand.Execute(null);
                return true;
            }
            return false;
        }

        private async Task<bool> GetResult()
        {
            return await DisplayAlert("Confirmation", "Do you want to close the app", "Close", "Cancel");
        }
    }
}

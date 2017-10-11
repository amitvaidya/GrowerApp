using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Component.Helper;
using Components.Component.Weather.Model;
using Components.Component.Weather.View;
using HarmanPOC.Helper;
using Xamarin.Forms;

namespace Components.Component.Weather.ViewModel
{
    public class WeatherViewModel : ViewModelBase
    {

        private ComponentsManager manager;
        private int _counter = 0;
        private int indexer = 0;

        //TODO: Need to understand the Weather Data - To Proceed with the Business Logic Implementation
        public WeatherViewModel(ComponentsManager cManager)
        {
            manager = cManager;
            HandleMessage();
        }

        private WeatherModel weatherData;

        public WeatherModel WeatherData
        {
            get { return weatherData; }
            set
            {
                weatherData = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> weatherDaysCollection = new ObservableCollection<int>();

        public ObservableCollection<int> WeatherDataCollection { get; set; }

        private void HandleMessage()
        {
            MessagingCenter.Subscribe<string>(this, AppConstants.NextCommand, async (sender) =>
            {
                IsBusy = true;
                await Task.Delay(100);
                //if (_counter < 0 || _counter > _itemsSource.Count - 1)
                //{
                //    IsBusy = false;
                //    return;
                //}
                if (sender.Equals(AppConstants.NextCommand))
                {
                    _counter++;
                    var template = new CustomDataTemplate(new WeatherDetailViewModel(), 
                        new DataTemplate(typeof(WeatherPagedView)));
                    (manager as WeatherManager).UpdateView(template);
                    MessagingCenter.Unsubscribe<string>(this, AppConstants.NextCommand);
                }
                else if (sender.Equals(AppConstants.PreviousCommand))
                {
                    _counter--;
                }
                IsBusy = false;
            });
        }

        protected override void Dispose(bool canDispose)
        {
            
        }
    }
}

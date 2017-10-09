using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Component.Helper;
using Components.Component.Weather.Model;

namespace Components.Component.Weather.ViewModel
{
    public class WeatherViewModel : ViewModelBase
    {
        //TODO: Need to understand the Weather Data - To Proceed with the Business Logic Implementation
        public WeatherViewModel()
        {
            
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

        protected override void Dispose(bool canDispose)
        {
            
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Components.Component.Helper;
using Components.Component.Weather.Model;
using HarmanPOC.Helper;
using Xamarin.Forms;

namespace Components.Component.Weather.ViewModel
{
    public class WeatherDetailViewModel : ViewModelBase
    {
        private int _indexer = 0;
        private int incrementer = 0;

        //TODO: Need to understand the Weather Data - To Proceed with the Business Logic Implementation
        public WeatherDetailViewModel()
        {
            PopulateCollection();
            HandleMessage();
        }

        private void PopulateCollection()
        {
            WeatherDataCollection = new ObservableCollection<int>();
            for (int i = 0; i < 20; i++)
            {
                _weatherDaysCollection.Add(i);
            }
            WeatherDataCollection =
                new ObservableCollection<int>(_weatherDaysCollection.Take(_indexer = _indexer + 8).ToList());
            WeatherData = new WeatherModel
            {
                WeatherWeek = GetWeatherWeek(),
                CurrentWeather = "cloudy.png",
                MaxTemp = "14.3",
                MinTemp = "14.3",
                Pressure = "EK",
                Humidity = "59",
                TempValue = "3.3",
                WeatherValue = "0",
                ChanceOfRain = "0"
            };
            incrementer = _indexer;
        }

        private WeatherModel _weatherData;

        public WeatherModel WeatherData
        {
            get { return _weatherData; }
            set
            {
                _weatherData = value;
                RaisePropertyChanged();
            }
        }

        private readonly ObservableCollection<int> _weatherDaysCollection = new ObservableCollection<int>();

        private ObservableCollection<int> weatherDataCollection;

        public ObservableCollection<int> WeatherDataCollection
        {
            get { return weatherDataCollection; }
            set
            {
                weatherDataCollection = value;
                RaisePropertyChanged();
            }
        }

        private void HandleMessage()
        {
            MessagingCenter.Subscribe<string>(this, AppConstants.NextCommand, async (sender) =>
            {
                IsBusy = true;
                await Task.Delay(1000);
                // Update the Model and the underlying business collection from 
                // the local collection or the database as per the architecture docx.
                if (_indexer > _weatherDaysCollection.Count - 1)
                    return;
                WeatherDataCollection =
                    new ObservableCollection<int>(_weatherDaysCollection.Skip(_indexer).Take(_indexer = _indexer + 8));
                WeatherData = new WeatherModel
                {
                    WeatherWeek = GetWeatherWeek(),
                    CurrentWeather = "next.png",
                    MaxTemp = "14.3",
                    MinTemp = "9.3",
                    Pressure = "EK",
                    Humidity = "59",
                    TempValue = "3.3",
                    WeatherValue = "0",
                    ChanceOfRain = "0"
                };
                incrementer = _indexer;
                IsBusy = false;
            });
        }

        private string GetWeatherWeek()
        {
            return
                $"{(incrementer > 9 ? incrementer.ToString() : "0" + incrementer)}-{(_indexer > 9 ? _indexer.ToString() : "0" + _indexer)}";
        }


        protected override void Dispose(bool canDispose)
        {
            
        }
    }
}

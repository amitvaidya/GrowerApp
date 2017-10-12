using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Components.Component.Weather.Model
{
    public class WeatherModel : INotifyPropertyChanged
    {
        private string _tempValue;
        private string _maxTemp;
        private string _minTemp;
        private string _pressure;
        private string _humidity;
        private string _weatherValue;
        private string _chanceOfRain;
        private string _currentWeather;
        private string _weatherWeek;


        public string WeatherWeek
        {
            get { return _weatherWeek; }
            set
            {
                _weatherWeek = value;
                OnPropertyChanged();
            }
        }

        //TODO: Change it to stream in future to support StreamImageSource
        public string CurrentWeather
        {
            get { return _currentWeather; }
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        public string TempValue
        {
            get { return _tempValue; }
            set
            {
                _tempValue = value;
                OnPropertyChanged();
            }
        }

        public string MaxTemp
        {
            get { return _maxTemp; }
            set
            {
                _maxTemp = value;
                OnPropertyChanged();
            }
        }

        public string MinTemp
        {
            get { return _minTemp; }
            set
            {
                _minTemp = value;
                OnPropertyChanged();
            }
        }

        public string Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                OnPropertyChanged();
            }
        }

        public string Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        public string WeatherValue
        {
            get { return _weatherValue; }
            set
            {
                _weatherValue = value;
                OnPropertyChanged();
            }
        }

        public string ChanceOfRain
        {
            get { return _chanceOfRain; }
            set
            {
                _chanceOfRain = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> DaysInstruction { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

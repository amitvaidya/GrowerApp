using Components.Component.Weather.View;
using Components.Component.Weather.ViewModel;

namespace Components.Component.Weather
{
    public class WeatherManager : ComponentsManager
    {
        public WeatherManager()
        {
            viewModel = new WeatherViewModel();
            CurrentView = new WeatherView() {BindingContext = viewModel};
        }
    }
}

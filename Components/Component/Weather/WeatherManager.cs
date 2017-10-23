using Components.Component.Helper;
using Components.Component.Weather.View;
using Components.Component.Weather.ViewModel;

namespace Components.Component.Weather
{
    public class WeatherManager : ComponentsManager
    {
        public WeatherManager()
        {
            viewModel = new WeatherViewModel(this);
            CurrentView = new WeatherView() {BindingContext = viewModel};
        }

        internal void UpdateView(CustomDataTemplate template)
        {
            BackStack.Add(CurrentView);
            var content = template.CreateContent();
            CurrentView = content;
        }
    }
}

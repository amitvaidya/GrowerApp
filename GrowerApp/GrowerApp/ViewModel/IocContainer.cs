/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:GrowerApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Components.Component.News.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GrowerApp.Interfaces;
using GrowerApp.Services;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace GrowerApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class IocContainer
    {
        /// <summary>
        /// Initializes a new instance of the IocContainer class.
        /// </summary>
        public IocContainer()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

        }

        public static SimpleIoc Container => SimpleIoc.Default;

        public static void CreateContainer()
        {
            SimpleIoc.Default.Register(() => DependencyService.Get<ILogger>());

            //Register your dependencies here...
            SimpleIoc.Default.Register(() => DependencyService.Get<IDbOperations>());
            SimpleIoc.Default.Register<IDbSchemaService, DbSchemaService>();

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<IndividualNewsViewModel>();
            SimpleIoc.Default.Register<NewsViewModel>();

        }

        //Properties for the view to bind with ViewModel
        //TODO: needs refactoration
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
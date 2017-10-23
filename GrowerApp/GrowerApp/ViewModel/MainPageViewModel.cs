using System.Collections.ObjectModel;
using System.Windows.Input;
using Components.Component;
using Components.Component.Helper;
using Components.Component.News;
using Components.Component.News.Model;
using Components.Component.News.ViewModel;
using Components.Component.Weather;
using Xamarin.Forms;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace GrowerApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            PopulateDummyData();
            VisibilityCommandInit();
        }

        private ComponentsManager _newsManager;

        public ComponentsManager ViewManager
        {
            get { return _newsManager; }
            set
            {
                _newsManager = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<NewsModel> _listofNews;

        public ObservableCollection<NewsModel> ListOfNews
        {
            get { return _listofNews; }
            set
            {
                _listofNews = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedView = string.Empty;

        public string SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                RaisePropertyChanged();
            }
        }

        private bool _previousItemVisibility;

        public bool PreviousItemVisibility
        {
            get { return _previousItemVisibility; }
            set
            {
                _previousItemVisibility = value;
                RaisePropertyChanged();
            }
        }

        private bool _nextItemVisibility;

        public bool NextItemVisibility
        {
            get { return _nextItemVisibility; }
            set
            {
                _nextItemVisibility = value;
                RaisePropertyChanged();
            }
        }

        public ICommand NavigationCommand => new Command(o => NavigateToPage(o));

        public ICommand NextItemCommand => new Command(NavigateToNextView);

        public ICommand PreviousItemCommand => new Command(NavigateToPreviousView);

        public ICommand BackButtonCommand => new Command(() =>
        {
            ViewManager.BackCommand();
        });

        private void NavigateToPage(object commandParameter)
        {
            switch (commandParameter.ToString())
            {
                case Constants.News:
                    if(ViewManager is NewsManager)
                        break;
                    ViewManager.Dispose();
                    ViewManager = new NewsManager(IocContainer.Container.GetInstance<NewsViewModel>()) {ItemsSource = ListOfNews};
                    VisibilityCommandInit();
                    break;
                case Constants.Weather:
                    if (ViewManager is WeatherManager)
                        break;
                    ViewManager.Dispose();
                    ViewManager = new WeatherManager() { };
                    VisibilityCommandInit(true);
                    break;
            }
            SelectedView = commandParameter.ToString();
        }

        private void NavigateToNextView()
        {
            MessagingCenter.Send(Constants.NextCommand, Constants.NextCommand);
        }

        private void NavigateToPreviousView()
        {
            MessagingCenter.Send(Constants.PreviousCommand, Constants.NextCommand);
        }

        private void VisibilityCommandInit(bool visibility = false)
        {
            NextItemVisibility = visibility;
            PreviousItemVisibility = visibility;
            MessagingCenter.Unsubscribe<string>(this, Constants.ItemVisibility);
            MessagingCenter.Subscribe<string>(this, Constants.ItemVisibility, (sender) =>
            {
                switch (sender)
                {
                    case Constants.NextVisible:
                        NextItemVisibility = true;
                        break;
                    case Constants.PreviousVisible:
                        PreviousItemVisibility = true;
                        break;
                    case Constants.NextInvisible:
                        NextItemVisibility = false;
                        break;
                    case Constants.PreviousInVisible:
                        PreviousItemVisibility = false;
                        break;
                }
            });
        }

        private void PopulateDummyData()
        {
            SelectedView = "News";
            //DI Injection
            ListOfNews = new ObservableCollection<NewsModel>();
            for (var i = 0; i < 20; i++)
            {
                var newsModel = new NewsModel
                (
                    "Title" + i.ToString(),
                    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." +
                    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
//                    "icon.png"
                    i % 2 == 0 ? "news1.png" : "news2.png"
                );
                ListOfNews.Add(newsModel);
            }
            ViewManager = new NewsManager(IocContainer.Container.GetInstance<NewsViewModel>()) {ItemsSource = ListOfNews};
        }

    }
}
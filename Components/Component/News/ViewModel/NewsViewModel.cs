using System.Collections.ObjectModel;
using System.Windows.Input;
using Components.Component.Helper;
using Components.Component.News.Model;
using Components.Component.News.View;
using Xamarin.Forms;

namespace Components.Component.News.ViewModel
{
    public class NewsViewModel : ViewModelBase
    {
        private NewsManager manager;
        private IndividualNewsViewModel individualNewsViewModel;

//        public NewsViewModel(NewsManager manager, IndividualNewsViewModel individualNewsViewModel)
        public NewsViewModel(IndividualNewsViewModel individualNewsViewModel)
        {
            this.manager = manager;
            this.individualNewsViewModel = individualNewsViewModel;
        }

        public ICommand ItemSelectedCommand => new Command(OnItemSelected);

        private ObservableCollection<NewsModel> itemsSource;

        public ObservableCollection<NewsModel> ItemsSource
        {
            get { return itemsSource; }
            set
            {
                itemsSource = value;
                RaisePropertyChanged();
            }
        }

        protected override void Dispose(bool canDispose)
        {
            manager = null;
        }

        private void OnItemSelected(object selectedItem)
        {
            individualNewsViewModel.NewsModelData = selectedItem as NewsModel;
            individualNewsViewModel.ItemsSource = ItemsSource;
            var template = new CustomDataTemplate(individualNewsViewModel,
                new DataTemplate(typeof(IndividualNewsView)));
            //TODO: Simple Viewmodel based navigation/ Content Switching
//            manager.UpdateView(template);
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Components.Component.Helper;
using Components.Component.News.Model;
using Components.Component.News.View;
using HarmanPOC.Helper;
using Xamarin.Forms;

namespace Components.Component.News.ViewModel
{
    public class NewsViewModel : ViewModelBase
    {

        private NewsManager manager;

        public NewsViewModel(NewsManager _manager)
        {
            manager = _manager;
        }

        public ICommand ItemSelectedCommand => new Command(o => OnItemSelected(o));

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
            var template = new CustomDataTemplate(new IndividualNewsViewModel(selectedItem as NewsModel, ItemsSource),
                new DataTemplate(typeof(IndividualNewsView)));
            //TODO: Simple Viewmodel based navigation/ Content Switching
            manager.UpdateView(template);
        }
    }
}

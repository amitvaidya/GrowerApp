using System.Collections.ObjectModel;
using System.Linq;
using Components.Component.Helper;
using Components.Component.News.Model;
using Components.Component.News.View;
using Components.Component.News.ViewModel;

namespace Components.Component.News
{
    public class NewsManager : ComponentsManager
    {
        public NewsManager(NewsViewModel newsViewModel)
        {
            viewModel = newsViewModel;
            CurrentView = new NewsView {BindingContext = viewModel};
        }

        public ObservableCollection<NewsModel> ItemsSource
        {
            get { return (viewModel as NewsViewModel).ItemsSource; }
            set { (viewModel as NewsViewModel).ItemsSource = value; }
        }

        internal void UpdateView(CustomDataTemplate template)
        {
            BackStack.Add(CurrentView);
            var content = template.CreateContent();
            CurrentView = content;
        }

        public override void BackCommand()
        {
            if (BackStack.Count > 0)
            {
                CurrentView = BackStack.Last();
                BackStack.Remove(CurrentView);
            }
        }

        protected override void Dispose(bool canDispose)
        {
            if (CurrentView == null)
                return;
            (CurrentView.BindingContext as ViewModelBase).Dispose();
            CurrentView = null;
        }
    }
}

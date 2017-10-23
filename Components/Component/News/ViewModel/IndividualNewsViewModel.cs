using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Components.Component.Helper;
using Components.Component.News.Model;
using Xamarin.Forms;

namespace Components.Component.News.ViewModel
{
    public class IndividualNewsViewModel : ViewModelBase
    {
        public ObservableCollection<NewsModel> ItemsSource { get; set; }

        private int counter;

        private NewsModel newsModelData;

        public NewsModel NewsModelData
        {
            get { return newsModelData; }
            set
            {
                newsModelData = value;
                RaisePropertyChanged();
            }
        }

        public IndividualNewsViewModel()
        {
            if (NewsModelData == null)
                return;

            counter = ItemsSource.IndexOf(NewsModelData);
            if (counter > 0 && counter < ItemsSource.Count - 1)
            {
                MessagingCenter.Send("PreviousVisible", "ItemVisibility");
                MessagingCenter.Send("NextVisible", "ItemVisibility");
            }
            else if (counter >= ItemsSource.Count - 1)
                MessagingCenter.Send("PreviousVisible", "ItemVisibility");
            else if (counter >= 0)
                MessagingCenter.Send("NextVisible", "ItemVisibility");
            HandleMessage();
        }

        private void HandleMessage()
        {
            MessagingCenter.Subscribe<string>(this, Constants.NextCommand, async (sender) =>
            {
                IsBusy = true;
                await Task.Delay(100);
                if (counter < 0 || counter > ItemsSource.Count - 1)
                {
                    IsBusy = false;
                    return;
                }
                if (sender.Equals(Constants.NextCommand))
                {
                    counter++;
                    NewsModelData = ItemsSource[counter];
                    MessagingCenter.Send(Constants.PreviousVisible, Constants.ItemVisibility);
                    if(counter == ItemsSource.Count -1)
                        MessagingCenter.Send(Constants.NextInvisible, Constants.ItemVisibility);
                }
                else if (sender.Equals(Constants.PreviousCommand))
                {
                    counter--;
                    NewsModelData = ItemsSource[counter];
                    MessagingCenter.Send(Constants.NextVisible, Constants.ItemVisibility);
                    if(counter ==0)
                        MessagingCenter.Send(Constants.PreviousInVisible, Constants.ItemVisibility);
                }
                IsBusy = false;
            });
        }

        protected override void Dispose(bool canDispose)
        {
            MessagingCenter.Unsubscribe<string>(this, Constants.NextCommand);
        }
    }
}
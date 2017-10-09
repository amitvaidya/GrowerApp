using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Components.Component.Helper;
using Components.Component.News.Model;
using HarmanPOC.Helper;
using Xamarin.Forms;

namespace Components.Component.News.ViewModel
{
    public class IndividualNewsViewModel : ViewModelBase
    {

        #region Fields

        private readonly ObservableCollection<NewsModel> _itemsSource;
        private int _counter = 0;

        #endregion


        #region Properties

        private NewsModel _newsModelData;

        public NewsModel NewsModelData
        {
            get { return _newsModelData; }
            set
            {
                _newsModelData = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public IndividualNewsViewModel(NewsModel selectedItem, ObservableCollection<NewsModel> collection)
        {
            _itemsSource = collection;
            NewsModelData = selectedItem;
            _counter = _itemsSource.IndexOf(NewsModelData);
            if (_counter > 0 && _counter < _itemsSource.Count - 1)
            {
                MessagingCenter.Send("PreviousVisible", "ItemVisibility");
                MessagingCenter.Send("NextVisible", "ItemVisibility");
            }
            else if (_counter >= _itemsSource.Count - 1)
                MessagingCenter.Send("PreviousVisible", "ItemVisibility");
            else if (_counter >= 0)
                MessagingCenter.Send("NextVisible", "ItemVisibility");
            HandleMessage();
        }

        #region Private Methods

        private void HandleMessage()
        {
            MessagingCenter.Subscribe<string>(this, AppConstants.NextCommand, async (sender) =>
            {
                IsBusy = true;
                await Task.Delay(100);
                if (_counter < 0 || _counter > _itemsSource.Count - 1)
                {
                    IsBusy = false;
                    return;
                }
                if (sender.Equals(AppConstants.NextCommand))
                {
                    _counter++;
                    NewsModelData = _itemsSource[_counter];
                    MessagingCenter.Send(AppConstants.PreviousVisible, AppConstants.ItemVisibility);
                    if(_counter == _itemsSource.Count -1)
                        MessagingCenter.Send(AppConstants.NextInvisible, AppConstants.ItemVisibility);
                }
                else if (sender.Equals(AppConstants.PreviousCommand))
                {
                    _counter--;
                    NewsModelData = _itemsSource[_counter];
                    MessagingCenter.Send(AppConstants.NextVisible, AppConstants.ItemVisibility);
                    if(_counter ==0)
                        MessagingCenter.Send(AppConstants.PreviousInVisible, AppConstants.ItemVisibility);
                }
                IsBusy = false;
            });
        }

        #endregion

        protected override void Dispose(bool canDispose)
        {
            MessagingCenter.Unsubscribe<string>(this, AppConstants.NextCommand);
        }
    }
}

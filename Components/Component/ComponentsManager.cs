using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Components.Component.Helper;
using Xamarin.Forms;

namespace Components.Component
{
    public abstract class ComponentsManager : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase viewModel;

        private Xamarin.Forms.View currentView;

        public Xamarin.Forms.View CurrentView
        {
            get
            {
                return currentView;
            }
            protected set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        private List<View> backStack = new List<View>();

        public List<View> BackStack
        {
            get { return backStack; }
            set
            {
                backStack = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void BackCommand()
        {
            
        }

        protected virtual void Dispose(bool canDispose)
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

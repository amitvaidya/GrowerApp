using System;
using Xamarin.Forms;

namespace Components.Component.Helper
{
    public class CustomDataTemplate : IDisposable
    {
        private ViewModelBase targetViewModel;
        private DataTemplate internalTemplate;

        //TODO: Add a ViewModel <-> Page/View Dictionary to create based on ViewModel
        public CustomDataTemplate(ViewModelBase viewModel, DataTemplate viewType)
        { 
            targetViewModel = viewModel;
            internalTemplate = viewType;
        }

        public View CreateContent()
        {
            var view = (View)internalTemplate.CreateContent();
            view.BindingContext = targetViewModel;
            return view;
        }

        public void Dispose()
        {
            internalTemplate = null;
            targetViewModel.Dispose();
            targetViewModel = null;
        }
    }
}
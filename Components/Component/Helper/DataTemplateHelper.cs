using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Component.Helper;
using Components.Component.News.ViewModel;
using Xamarin.Forms;

namespace HarmanPOC.Helper
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

        public Xamarin.Forms.View CreateContent()
        {
            var view = (Xamarin.Forms.View)internalTemplate.CreateContent();
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

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Billmanager.Database;
using Billmanager.Interface.ViewModels;
using PropertyChanged;

namespace Billmanager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : BindableBase, IViewModelBase
    {
        protected INavigationService? NavigationService { get; private set; }
        protected INavigationParameters? navigationParameters; 

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService? navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            // away from this
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            // when this page is added to the navigation stack
            this.navigationParameters = parameters;
        }

        public virtual void Destroy()
        {

        }
    }
}

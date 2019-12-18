using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CreateOffertPageViewModel : ViewModelBase
    {
        public CreateOffertPageViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateOffer;
        }
    }
}

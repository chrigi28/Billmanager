using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Translations.Texts;
using Prism.Navigation;

namespace Billmanager.ViewModels
{
    public class CreateCarViewModel : ViewModelBase
    {
        public CreateCarViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateCar;
        }
    }
}

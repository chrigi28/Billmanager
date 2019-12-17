using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Database.Tables;
using Billmanager.Translations.Texts;
using Prism.Navigation;

namespace Billmanager.ViewModels
{
    public class CreateCarViewModel : DataViewModelBase<CarDbt>
    {
        public CreateCarViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateCar;
        }
    }
}

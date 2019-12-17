using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Billmanager.ViewModels
{
    public class CreateCustomerViewModel : DataViewModelBase<CustomerDbt>
    {
        public CreateCustomerViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateCustomer;
        }
    }
}

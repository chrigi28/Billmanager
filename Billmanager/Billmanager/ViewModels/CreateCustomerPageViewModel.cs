using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Billmanager.ViewModels
{
    public class CreateCustomerPageViewModel : DataViewModelBase<CustomerDbt>, ICreateCustomerViewModel<CustomerDbt>
    {
        public CreateCustomerPageViewModel(INavigationService? ns) : base(ns)
        {
            this.Title = Resources.CreateCustomer;
        }

        public ICommand SelectCustomerCommand => new Command(async () => await this.SelectCustomer());

        private async Task SelectCustomer()
        {
            await this.NavigationService?.NavigateAsync("CustomerSelection");
        }
    }


}

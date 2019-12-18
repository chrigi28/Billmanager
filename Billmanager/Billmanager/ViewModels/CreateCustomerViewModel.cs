using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Database.Tables;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Billmanager.ViewModels
{
    public class CreateCustomerViewModel : DataViewModelBase<CustomerDbt>, ICreateCustomerViewModel<CustomerDbt>
    {
        public CreateCustomerViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateCustomer;
        }

        public ICommand SelectCustomerCommand => new Command(async () => await this.SelectCustomer());
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            if (parameters.ContainsKey("selectedCustomer"))
            {
                var cust = parameters.GetValue<CustomerDbt>("selectedCustomer");
            }
        }   
        
        private async Task SelectCustomer()
        {
            var parameter = new NavigationParameters();
            var custs = await DependencyService.Get<ICustomerService>().GetCustomerSelection();
            parameter.Add("SelectionPageProperty", custs);
            await this.NavigationService.NavigateAsync("CustomerSelection", parameter);
        }
    }

    
}

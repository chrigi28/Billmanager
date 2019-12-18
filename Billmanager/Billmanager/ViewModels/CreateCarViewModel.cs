using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CreateCarViewModel : DataViewModelBase<CarDbt>, ICreateCarViewModel<CarDbt>
    {
        private IEnumerable<ICustomerDbt> _customerItems;

        public CreateCarViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateCar;
        }

        public ICustomerDbt SelectedCustomer { get; set; }

        public Command SelectCustomerCommand => new Command(async () => await this.SelectCustomer());

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var customers = await DependencyService.Get<ICustomerService>().GetCustomerSelection();
            this.CustomerItems = new ObservableCollection<ICustomerDbt>(customers);
        }
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            if (parameters.ContainsKey("selectedCustomer"))
            {

                var cust = parameters.GetValue<CustomerDbt>("selectedCustomer");
                this.SelectedCustomer = cust;
            }
        }

        public IEnumerable<ICustomerDbt> CustomerItems
        {
            get => _customerItems;
            set
            {
                if (_customerItems != value)
                {
                    _customerItems = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private async Task SelectCustomer()
        {
            var parameter = new NavigationParameters();
            parameter.Add("SelectionPageProperty", this.CustomerItems);
            await this.NavigationService.NavigateAsync("CustomerSelection", parameter);
        }
    }
}

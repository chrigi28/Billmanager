using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Billmanager.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class OverviewPageViewModel : ViewModelBase
    {
        private Command? createCustomerCommand;
        private Command? createOffertCommand;
        private Command? createCarCommand;
        private Command? createWorkcardCommand;
        private Command? createAddresscardCommand;
        private Command? createBillCommand;
        private CustomerDbt? _selectedCustomer;
        private Command? editCommand;

        public OverviewPageViewModel(INavigationService? ns) : base(ns)
        {
            this.Title = Resources.Overview;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.Customers = new ObservableCollection<ICustomerDbt>(await DependencyService.Get<ICustomerService>().GetCustomerSelection());
        }

        public Command CreateCustomerCommand => this.createCustomerCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateCustomerPage)));

        public Command CreateCarCommand => this.createCarCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateCarPage)));

        public Command CreateBillCommand => this.createBillCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateBillPage)));
        
        public Command EditCommand => this.editCommand  ??= new Command(async o => await this.Edit(o));

        public Command CreateWorkcardCommand => this.createWorkcardCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateWorkcardPage)));

        public Command CreateAddresscardCommand => this.createAddresscardCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateAddresscardPage)));

        public Command CreateOffertCommand => this.createOffertCommand ??= new Command(async () => await this.NavigationService?.NavigateAsync(nameof(CreateOffertPage)));

        public ObservableCollection<ICustomerDbt>? Customers { get; set; }

        public CustomerDbt? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer == value)
                {
                    return;
                }

                _selectedCustomer = value;

                if (value != null)
                {
                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        var cars = await DependencyService.Get<ICarService>()
                            .GetCarSelectionFromCustomerAsync(value.Id);
                        this.Cars = new ObservableCollection<ICarDbt>(cars);

                    });

                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        var bills = await DependencyService.Get<IBillService>().GetBillsOfCustomerAsync(value.Id);
                        this.Bills = new ObservableCollection<IBillDbt>(bills);

                    });
                }
                else
                {
                    this.Bills.Clear();
                    this.Cars.Clear();
                }
            }
        }

        private async Task Edit(object item)
        {
            var param = new NavigationParameters();
            switch (item)
            {
                case CustomerDbt customer:
                    param.Add(nameof(NavigationParameter.Selection), customer);
                    await this.NavigationService?.NavigateAsync(nameof(CreateCustomerPage), param);
                    break;
                case CarDbt car:
                    param.Add(nameof(NavigationParameter.Selection), car);
                    await this.NavigationService?.NavigateAsync(nameof(CreateCarPage), param);
                    break;
                case BillDbt bill:
                    param.Add(nameof(NavigationParameter.Selection), bill);
                    await this.NavigationService?.NavigateAsync(nameof(CreateBillPage), param);
                    break;
            }
        }

        public ObservableCollection<ICarDbt>? Cars { get; set; }

        public CarDbt? SelectedCar { get; set; }

        public ObservableCollection<IBillDbt>? Bills { get; set; }

        public CarDbt? SelectedBill { get; set; }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Billmanager.Views;
using Prism.Navigation;
using SkiaSharpSample.Samples;
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
        private Command testCommand;

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
        public Command TestCommand => this.testCommand ??= new Command(async () => await this.CreateTestPdf());

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


        private async Task CreateTestPdf()
        {
            var testCust = new CustomerDbt
            {
                Title_customer = "Herr",
                FirstName = "Hans",
                LastName = "Muster",
                Address = "Bahnhofstrasse 192",
                Zip = "88888",
                Location = "St.Nimmerwhere",
                Phone = "+41234567890"
            };

            var testCar = new CarDbt {Customer = testCust, CarMake = "Nissan", Typ = "GTR", Cubic = "4.5l"};
            var itempos = new ObservableCollection<ItemPositionDbt>
            {
                new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                ////new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                ////new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                ////new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                ////new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                ////new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                ////new ItemPositionDbt {Amount = 1, Description = "Material 1", Price = 23.50m,},
                ////new ItemPositionDbt {Amount = 2, Description = "Material 2", Price = 123.50m,},
                ////new ItemPositionDbt {Amount = 3, Description = "Material 3", Price = 3.50m,},
                new ItemPositionDbt {Amount = 4, Description = "Arbeit", Price = 80m,},
                new ItemPositionDbt {Amount = 5, Description = "Arbeit 2", Price = 80m,},
                new ItemPositionDbt {Amount = 6, Description = "Arbeit 3", Price = 80m,},
                new ItemPositionDbt {Amount = 7, Description = "Arbeit 4", Price = 80m,},
                ////new ItemPositionDbt {Amount = 8, Description = "Arbeit 5", Price = 80m,},
                ////new ItemPositionDbt {Amount = 9, Description = "Arbeit 6", Price = 80m,},
                ////new ItemPositionDbt {Amount = 10, Description = "Arbeit 7", Price = 80m,},
                ////new ItemPositionDbt {Amount = 11, Description = "Arbeit 8", Price = 80m,},
                ////new ItemPositionDbt {Amount = 12, Description = "Arbeit 9", Price = 80m,},
                ////new ItemPositionDbt {Amount = 13, Description = "Arbeit 10", Price = 80m,},
                ////new ItemPositionDbt {Amount = 14, Description = "Arbeit 11", Price = 80m,},
                ////new ItemPositionDbt {Amount = 4, Description = "Arbeit", Price = 80m,},
                ////new ItemPositionDbt {Amount = 5, Description = "Arbeit 2", Price = 80m,},
                ////new ItemPositionDbt {Amount = 6, Description = "Arbeit 3", Price = 80m,},
                ////new ItemPositionDbt {Amount = 7, Description = "Arbeit 4", Price = 80m,},
                ////new ItemPositionDbt {Amount = 8, Description = "Arbeit 5", Price = 80m,},
                ////new ItemPositionDbt {Amount = 9, Description = "Arbeit 6", Price = 80m,},
                ////new ItemPositionDbt {Amount = 10, Description = "Arbeit 7", Price = 80m,},
                ////new ItemPositionDbt {Amount = 11, Description = "Arbeit 8", Price = 80m,},
                ////new ItemPositionDbt {Amount = 12, Description = "Arbeit 9", Price = 80m,},
                ////new ItemPositionDbt {Amount = 13, Description = "Arbeit 10", Price = 80m,},
                ////new ItemPositionDbt {Amount = 14, Description = "Arbeit 11", Price = 80m,},
            };

            var testBill = new BillDbt
            {
                Customer = testCust,
                Car = testCar,
                BillNumber = "R55555",
                Conclusion = "Das ist eine zusammenfassung und kann über mehrer zeilen gehen" + Environment.NewLine + "Das wäre die 2. Zeile",
                Date = DateTime.Now,
                Kilometers = 123456,
                NettoPrice = 9003.25m,
                Remark = "Das ist eine Bemerkung",
                ItemPositions = itempos
            };

            PdfFactory.CreateBill(Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "PdfFile.pdf"), testBill);
        }        
    }
}

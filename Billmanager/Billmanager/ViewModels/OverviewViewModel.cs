using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Translations.Texts;
using Billmanager.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private Command createCustomerCommand;
        private Command createOffertCommand ;
        private Command createCarCommand ;
        private Command createWorkcardCommand ;
        private Command createAddresscardCommand ;
        private Command createBillCommand ;

        public OverviewViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.Overview;
        }

        public Command CreateCustomerCommand => this.createCustomerCommand ?? (this.createCustomerCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateCustomer))));

        public Command CreateCarCommand => this.createCarCommand ?? (this.createCarCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateCarViewModel))));

        public Command CreateBillCommand => this.createBillCommand ?? (this.createBillCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateBillViewModel))));

        public Command CreateWorkcardCommand => this.createWorkcardCommand ?? (this.createWorkcardCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateWorkcardViewModel))));

        public Command CreateAddresscardCommand => this.createAddresscardCommand ?? (this.createAddresscardCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateAddresscardViewModel))));

        public Command CreateOffertCommand => this.createOffertCommand ?? (this.createOffertCommand = new Command(async () => await this.NavigationService.NavigateAsync(nameof(CreateOffertViewModel))));
    }
}

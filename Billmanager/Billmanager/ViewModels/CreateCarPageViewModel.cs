﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.StaticAppData;
using Billmanager.Translations.Texts;
using Billmanager.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CreateCarPageViewModel : DataViewModelBase<CarDbt>, ICreateCarViewModel<CarDbt>
    {
        public CreateCarPageViewModel(INavigationService? ns) : base(ns)
        {
            this.Title = Resources.CreateCar;
        }

        public ICustomerDbt SelectedCustomer => SelectionData.SelectedCustomer;

        public Command SelectCustomerCommand => new Command(async () => await this.SelectCustomer());

        private async Task SelectCustomer()
        {
            var navparm = new NavigationParameters
            {
                {
                    nameof(NavigationParameter.SelectionItems),
                    await DependencyService.Get<ICustomerService>().GetCustomerSelection()
                }
            };

            await this.NavigationService?.NavigateAsync(nameof(SelectionPage), navparm);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(NavigationParameter.Selection), out object selection))
            {
                if (selection is CarDbt dbo)
                {
                    this.Model = dbo;
                }
            }
        }
    }
}

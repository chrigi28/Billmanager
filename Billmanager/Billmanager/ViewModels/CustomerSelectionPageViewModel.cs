﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Billmanager.Interfaces;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.StaticAppData;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CustomerSelectionPageViewModel : GenericSelectionViewModel<ICustomerDbt>
    {
        public CustomerSelectionPageViewModel(INavigationService ns) : base(ns)
        {
        }

        public override ICustomerDbt SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                SelectionData.SelectedCustomer = value;
                this.NavigationService.GoBackAsync();
            }
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            this.ItemSource = await DependencyService.Get<ICustomerService>().GetCustomerSelection();
        }
    }
}
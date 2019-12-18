using Prism.Commands;
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
    public class CustomerSelectionPageViewModel : ViewModelBase
    {
        private IEnumerable<ICustomerDbt> itemSource;
        private ICustomerDbt selectedItem;

        public CustomerSelectionPageViewModel(INavigationService ns) : base(ns)
        {
        }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        
        public ObservableCollection<ICustomerDbt> FilteredItems;

        public ICustomerDbt SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                SelectionData.SelectedCustomer = value;
                this.NavigationService.GoBackAsync();
            }
        }

        private void FilterItems(string filter)
        {
            var filteredItems = itemSource.Where(i=> i.FilterString.Contains(filter)).ToList();
            foreach (var item in filteredItems)
            {
                if (!FilteredItems.Contains(item))
                {
                    this.FilteredItems.Remove(item);
                }
                else
                {
                    if (!this.FilteredItems.Contains(item))
                    {
                        this.FilteredItems.Add(item);
                    }
                }
            }
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            this.itemSource = await DependencyService.Get<ICustomerService>().GetCustomerSelection();
            this.FilteredItems = new ObservableCollection<ICustomerDbt>(this.itemSource);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            parameters.Add("selectedCustomer", this.SelectedItem);
        }
    }
}

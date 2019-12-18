using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Billmanager.Interfaces;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class SelectionPageViewModel<T> : ViewModelBase where T : IFilterStringProperty
    {
        private IEnumerable<T> itemSource;
        private T selectedItem;

        public SelectionPageViewModel(INavigationService ns) : base(ns)
        {
        }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        
        public ObservableCollection<T> FilteredItems;

        public T SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            if (parameters.ContainsKey("SelectionPageProperty"))
            {
                this.itemSource = parameters.GetValue<IEnumerable<T>>("SelectionPageProperty");
                this.FilteredItems = new ObservableCollection<T>(this.itemSource);
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            parameters.Add("selectedCustomer", this.SelectedItem);
        }
    }
}

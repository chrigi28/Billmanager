using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.StaticAppData;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class GenericSelectionViewModel<T> : ViewModelBase where T : IDatabaseTable
    {
        protected T selectedItem;
        private string _filter;
        private IEnumerable<T> _itemSource;

        public GenericSelectionViewModel(INavigationService ns) : base(ns)
        {
        }

        public IEnumerable<T> ItemSource
        {
            get => _itemSource;
            set
            {
                _itemSource = value;
                this.FilteredItems = new ObservableCollection<T>(value);
                this.RaisePropertyChanged(nameof(this.FilteredItems));
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    this.FilterItems(_filter);
                }
            }
        }

        public ICommand FilterCommand => new Command<string>(FilterItems);

        public ObservableCollection<T> FilteredItems { get; set; }

        public virtual T SelectedItem
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
            var filteredItems = this.ItemSource.Where(i => i.FilterString.Contains(filter)).ToList();
            ////foreach (var item in filteredItems)
            ////{
            ////    if (!FilteredItems.Contains(item))
            ////    {
            ////        this.FilteredItems.Remove(item);
            ////    }
            ////    else
            ////    {
            ////        if (!this.FilteredItems.Contains(item))
            ////        {
            ////            this.FilteredItems.Add(item);
            ////        }
            ////    }
            ////}
            this.FilteredItems = new ObservableCollection<T>(filteredItems);

            this.RaisePropertyChanged(nameof(this.FilteredItems));
        }
    }
}
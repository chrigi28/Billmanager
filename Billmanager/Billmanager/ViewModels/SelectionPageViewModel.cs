using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Helper;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.SharedAppData;
using Billmanager.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels;

public class SelectionPageViewModel : ViewModelBase
{
    private object? _selectedItem;
    private string? _filter;
    private IEnumerable<IDatabaseTable>? _itemSource;
        
    public SelectionPageViewModel(INavigationService? ns) : base(ns)
    {
    }

    public object? SelectedItem
    {
        get => this._selectedItem;
        set
        {
            this._selectedItem = value;
            var param = new NavigationParameters {{nameof(NavigationParameter.Selection), this.SelectedItem}};
            this.NavigationService?.GoBackAsync(param);
        }
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        if (parameters.TryGetValue(nameof(NavigationParameter.SelectionItems), out IEnumerable<IDatabaseTable> source))
        {
            this.ItemSource = source;
        }
    }   
        
    public IEnumerable<IDatabaseTable> ItemSource
    {
        get => _itemSource;
        set
        {
            _itemSource = value;
            this.FilteredItems = new ObservableCollection<IDatabaseTable>(value);
            this.RaisePropertyChanged(nameof(this.FilteredItems));
        }
    }

    public string? Filter
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

    public ObservableCollection<IDatabaseTable> FilteredItems { get; set; }

    private void FilterItems(string? filter)
    {
        if (string.IsNullOrEmpty(filter))
        {
            this.FilteredItems = new ObservableCollection<IDatabaseTable>(this.ItemSource);
            return;
        }

        var filteredItems = this.ItemSource.Where(i => i.FilterString.Contains(filter)).ToList();

        this.FilteredItems = new ObservableCollection<IDatabaseTable>(filteredItems);

        this.RaisePropertyChanged(nameof(this.FilteredItems));
    }
}
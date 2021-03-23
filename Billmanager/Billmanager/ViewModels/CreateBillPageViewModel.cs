using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Billmanager.Views;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CreateBillPageViewModel : DataViewModelBase<BillDbt>, ICreateBillViewModel
    {
        private Command? _selectCarCommand;
        private Command? _selectCustomerCommand;
        private Command? _addPositionCommand;
        private string _description = string.Empty;
        private decimal _netPrice;
        private int _amount;
        private decimal _pricePerPiece;
        private ItemPositionDbt? latestItem;

        public CreateBillPageViewModel(INavigationService? ns) : base(ns)
        {
            this.Title = Resources.CreateBill;
            MessagingCenter.Subscribe<ItemPositionDbt>(new WeakReference(this), BillmanagerMessages.BillTotalChanged, (sender) => this.RaisePropertyChanged(nameof(Model.Total)));
        }

        ~CreateBillPageViewModel()
        {
            MessagingCenter.Unsubscribe<ItemPositionDbt>(new WeakReference(this), BillmanagerMessages.BillTotalChanged);
        }

        public Command SelectCarCommand => this._selectCarCommand ??= new Command(async () => await this.SelectCar());

        public Command SelectCustomerCommand => this._selectCustomerCommand ??= new Command(async () => await this.SelectCustomer());

        ////public Command AddPositionCommand => this._addPositionCommand ??= new Command(this.AddItemPosition);

        ////public string Description
        ////{
        ////    get => this._description;
        ////    set
        ////    {
        ////        if (this._description != value)
        ////        {
        ////            this._description = value;
        ////            this.RaisePropertyChanged();
        ////        }
        ////    }
        ////}

        public ICommand DeletePosition => new Command(this.DeleteItemPosition);
        
        [AlsoNotifyFor(nameof(Model))]
        public string SelectedCustomerText
        {
            get => this.Model.Customer == null
                ? Resources.Customer
                : string.Format(CultureInfo.CurrentCulture,
                    $"{this.Model.Customer.FirstName} {this.Model.Customer.LastName}");
        }

        public override async Task Save(bool goBack = true)
        {
            this.Model.ItemPositions.Remove(this.latestItem);
            this.latestItem = null;
            await base.Save(goBack: false);

            ////await Database.SqliteDatabase.AddRangeAsync(this._items).ConfigureAwait(false);
            await this.NavigationService?.GoBackAsync();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (StaticAppData.SelectionData.SelectedBill != null)
            {
                this.Model = (BillDbt)StaticAppData.SelectionData.SelectedBill;
            }
            else
            {
                this.Model = new BillDbt();
            }

            this.AddExtraItem();

            if (StaticAppData.SelectionData.SelectedCar != null)
            {
                // todo
            }

            if (StaticAppData.SelectionData.SelectedBill != null)
            {
                // todo
            }

            this.RaisePropertyChanged(nameof(this.Model));
        }

        private void AddExtraItem()
        {
            // add empty item
            if (this.latestItem != null)
            {
                this.latestItem.PropertyChanged -= this.LatestItemOnPropertyChanged;
            }

            this.latestItem = new ItemPositionDbt {Bill = this.Model};
            this.latestItem.PropertyChanged += this.LatestItemOnPropertyChanged;
            this.Model.ItemPositions.Add(this.latestItem);
        }

        private void LatestItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.AddExtraItem();
        }

        private async Task SelectCustomer()
        {
            var navparm = new NavigationParameters();
            var data = await DependencyService.Get<ICustomerService>().GetCustomerSelection();
            navparm.Add(nameof(NavigationParameter.SelectionItems), data);

            await this.NavigationService?.NavigateAsync(nameof(SelectionPage), navparm);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(NavigationParameter.Selection), out object selection))
            {
                if (selection is CustomerDbt customer)
                {
                    this.Model.CustomerId = customer.Id;
                    this.Model.Customer = customer;
                }

                if (selection is CarDbt car)
                {
                    this.Model.CarId = car.Id;
                    this.Model.Car = car;
                }

                if (selection is BillDbt bill)
                {
                    this.Model = bill;
                    this.AddExtraItem();
                }
            }
        }

        private async Task SelectCar()
        {
            IEnumerable<ICarDbt> cars;
            var navparm = new NavigationParameters();
            if (this.Model.CustomerId > 0)
            {
                cars = await DependencyService.Get<ICarService>().GetCarSelectionFromCustomerAsync(this.Model.CustomerId);
            }
            else
            {
                cars = await DependencyService.Get<ICarService>().GetCarSelectionAsync();
            }

            navparm.Add(nameof(NavigationParameter.SelectionItems), cars);

            await this.NavigationService?.NavigateAsync("SelectionPage", navparm);
        }

        ////private void AddItemPosition()
        ////{
        ////    if (!this.Description.IsNullOrEmpty())
        ////    {
        ////        var item = new ItemPositionDbt()
        ////        {
        ////            Bill = this.Model,
        ////            Amount = this.Amount,
        ////            Description = this.Description,
        ////            Price = this.PricePerPiece,
        ////        };

        ////        this.Model.ItemPositions.Add(item);
        ////        this.RaisePropertyChanged(nameof(this.Model.ItemPositions));
        ////    }
        ////}

        private void DeleteItemPosition(object item)
        {
            if (item is ItemPositionDbt position)
            {
                if (position == this.latestItem)
                {
                    this.AddExtraItem();
                }

                position.Deleted = true;
                this.Model.ItemPositions.Remove(position);
            }
        }
    }
}

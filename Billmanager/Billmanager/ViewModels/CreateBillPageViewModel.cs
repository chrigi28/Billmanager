﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CreateBillPageViewModel : DataViewModelBase<BillDbt>, ICreateBillViewModel
    {
        private Command _selectCarCommand;
        private Command _selectCustomerCommand;
        private Command _addPositionCommand;
        private string _description;
        private decimal _netPrice;
        private int _amount;
        private decimal _pricePerPiece;
        private decimal _itemTotal;
        private decimal _total;
        private ObservableCollection<IItemPositionDbt> _items;
        private ItemPositionDbt latestItem;

        public CreateBillPageViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateBill;
            this._items = new ObservableCollection<IItemPositionDbt>();
        }

        public Command SelectCarCommand => this._selectCarCommand ??= new Command(async () => await this.SelectCar());

        public Command SelectCustomerCommand => this._selectCustomerCommand ??= new Command(async () => await this.SelectCustomer());

        public Command AddPositionCommand => this._addPositionCommand ??= new Command(this.AddItemPosition);

        public string Description
        {
            get => this._description;
            set
            {
                if (this._description != value)
                {
                    this._description = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public decimal NetPrice
        {
            get => this._netPrice;
            set
            {
                if (this._netPrice != value)
                {
                    this._netPrice = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary> The Amount of the new Item </summary>
        public int Amount
        {
            get => this._amount;
            set
            {
                if (this._amount != value)
                {
                    this._amount = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(this.ItemTotal));
                }
            }
        }

        /// <summary> The price per pice of the new Item.</summary>
        public decimal PricePerPiece
        {
            get => this._pricePerPiece;
            set
            {
                if (this._pricePerPiece != value)
                {
                    this._pricePerPiece = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(this.ItemTotal));
                }
            }
        }

        public ICommand DeletePosition => new Command(this.DeleteItemPosition);

        public decimal ItemTotal => this.Amount * this.PricePerPiece;

        public decimal Total => this.Items.Sum(f => f.Total);

        public ObservableCollection<IItemPositionDbt> Items
        {
            get => this._items;
        }

        public override async Task Save(bool goBack = true)
        {
            await base.Save(goBack: false);

            await Database.SqliteDatabase.GetTable<ItemPositionDbt>().AddRangeAsync(this._items).ConfigureAwait(false);
            await this.NavigationService.GoBackAsync();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (StaticAppData.SelectionData.SelectedBill != null)
            {
                this.Model = (BillDbt)StaticAppData.SelectionData.SelectedBill;
                this._items = new ObservableCollection<IItemPositionDbt>(DependencyService.Get<IBillService>().GetItemPositions(this.Model.Id));
                this.RaisePropertyChanged(nameof(this.Items));
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

        }

        private void AddExtraItem()
        {
            // add empty item
            if (this.latestItem != null)
            {
                this.latestItem.PropertyChanged -= this.LatestItemOnPropertyChanged;
            }

            this.latestItem = new ItemPositionDbt();
            this.latestItem.PropertyChanged += this.LatestItemOnPropertyChanged;
            this._items.Add(this.latestItem);
        }

        private void LatestItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.AddExtraItem();
        }

        private async Task SelectCustomer()
        {
            await this.NavigationService.NavigateAsync("CustomerSelectionPage");
        }

        private async Task SelectCar()
        {
            await this.NavigationService.NavigateAsync("CarSelectionPage");
        }

        private void AddItemPosition()
        {
            if (!this.Description.IsNullOrEmpty())

            {
                var item = new ItemPositionDbt()
                {
                    BillId = this.Model.Id,
                    Amount = this.Amount,
                    Description = this.Description,
                    Price = this.PricePerPiece,
                };

                this._items.Add(item);
                this.RaisePropertyChanged(nameof(this.Items));
            }
        }

        private void DeleteItemPosition(object item)
        {
            if (item is ItemPositionDbt position)
            {
                if (position == this.latestItem)
                {
                    this.AddExtraItem();
                }

                if (position.Id != -1)
                {
                    position.Deleted = true;

                    Device.InvokeOnMainThreadAsync(() => Database.SqliteDatabase.GetTable<ItemPositionDbt>().UpdateItemAsync(position));
                }

                this.Items.Remove(position);
            }
        }
    }
}

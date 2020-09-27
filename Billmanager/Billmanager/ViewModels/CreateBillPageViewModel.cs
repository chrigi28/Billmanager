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
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using PropertyChanged;
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
        private ItemPositionDbt latestItem;

        public CreateBillPageViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateBill;
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

        public decimal Total => this.Model.ItemPositions.Sum(f => f.Total);

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
            await this.NavigationService.GoBackAsync();
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

            this.latestItem = new ItemPositionDbt();
            this.latestItem.Bill = this.Model;
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

            await this.NavigationService.NavigateAsync("SelectionPage", navparm);
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

            await this.NavigationService.NavigateAsync("SelectionPage", navparm);
        }

        private void AddItemPosition()
        {
            if (!this.Description.IsNullOrEmpty())
            {
                var item = new ItemPositionDbt()
                {
                    Bill = this.Model,
                    Amount = this.Amount,
                    Description = this.Description,
                    Price = this.PricePerPiece,
                };

                this.Model.ItemPositions.Add(item);
                this.RaisePropertyChanged(nameof(this.Model.ItemPositions));
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

                position.Deleted = true;
                this.Model.ItemPositions.Remove(position);
            }
        }
    }
}

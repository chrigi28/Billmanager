using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Billmanager.Database.Tables;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Data;
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
        private IList<IItemPosition> _items;

        public CreateBillPageViewModel(INavigationService ns) : base(ns)
        {
            this.Title = Resources.CreateBill;
        }

        public Command SelectCarCommand => _selectCarCommand;

        public Command SelectCustomerCommand => _selectCustomerCommand;

        public Command AddPositionCommand => _addPositionCommand;

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public decimal NetPrice
        {
            get => _netPrice;
            set => _netPrice = value;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public decimal PricePerPiece
        {
            get => _pricePerPiece;
            set => _pricePerPiece = value;
        }

        public decimal ItemTotal => _itemTotal;

        public decimal Total => _total;

        public IList<IItemPosition> Items
        {
            get => _items;
            set => _items = value;
        }
    }
}

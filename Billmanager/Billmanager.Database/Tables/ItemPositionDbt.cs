using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

using static Billmanager.Interfaces.Data.BillmanagerMessages;

namespace Billmanager.Database.Tables
{
    public class ItemPositionDbt : BaseDbt, IItemPositionDbt
    {
        private decimal _price;
        private int _amount;
        private string _description;

        [ForeignKey(nameof(BillDbt.ItemPositions))]
        public int BillId { get; set; }
        public virtual BillDbt Bill { get; set; }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description)
                {
                    return;

                }

                this._description = value;
                this.OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amount;
            set 
            {
                if (_amount == value)
                {
                    return;
                }

                _amount = value;
                this.OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price == value)
                {
                    return;
                }

                _price = value;
                this.OnPropertyChanged();
            }
        }

        public decimal Total => this.Price * this.Amount;
        
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.Amount) || propertyName == nameof(this.Price))
            {
                base.OnPropertyChanged(nameof(Total));
                MessagingCenter.Send<ItemPositionDbt>(this, BillTotalChanged);
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}''
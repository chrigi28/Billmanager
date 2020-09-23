using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class ItemPositionDbt : BaseDbt, IItemPositionDbt, INotifyPropertyChanged
    {
        private decimal _price;
        private int _amount;

        [ForeignKey(nameof(BillDbt.ItemPositions))]
        public int BillId { get; set; }
        public BillDbt Bill { get; set; }
        public string Description { get; set; }

        public event EventHandler ItemChanged;

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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.Amount) || propertyName == nameof(this.Price))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Total)));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.ItemChanged?.Invoke(this, new EventArgs());
        }
    }
}
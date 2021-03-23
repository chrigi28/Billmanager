using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;
using PropertyChanged;

namespace Billmanager.Database.Tables
{
    public class BillDbt : BaseDbt, IBillDbt
    {
        [ForeignKey(nameof(CustomerDbt))]
        public int CustomerId { get; set; }
        public virtual CustomerDbt Customer { get; set; }

        [ForeignKey(nameof(CarDbt))]
        public int CarId { get; set; }
        public virtual CarDbt Car { get; set; }
        
        [ForeignKey(nameof(ItemPositionDbt))]
        public virtual ObservableCollection<ItemPositionDbt> ItemPositions { get; set; } = new ObservableCollection<ItemPositionDbt>();

        public DateTime Date { get; set; } = DateTime.Now;
        public string BillNumber { get; set; }
        public string Conclusion { get; set; }
        public int Kilometers { get; set; }
        public decimal NettoPrice { get; set; }

        [AlsoNotifyFor(nameof(ItemPositions))] 
        public decimal Total => ItemPositions.Sum(f => f.Total);
        public bool Payed { get; set; }

        [AlsoNotifyFor(nameof(Customer), nameof(Car), nameof(Conclusion))]
        public override bool CanSave => this.Customer != null || this.Car != null || !string.IsNullOrEmpty(this.Conclusion);

        public override string FilterString => base.FilterString + Date + Conclusion + Customer.FilterString + Car.FilterString;

    }
}
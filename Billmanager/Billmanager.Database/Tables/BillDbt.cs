using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class BillDbt : BaseDbt, IBillDbt
    {
        [ForeignKey(nameof(CustomerDbt))]
        public int CustomerId { get; set; }
        public CustomerDbt Customer { get; set; }

        [ForeignKey(nameof(CarDbt))]
        public int CarId { get; set; }
        public CarDbt Car { get; set; }
        
        [ForeignKey(nameof(ItemPositionDbt))]
        public ObservableCollection<ItemPositionDbt> ItemPositions { get; set; } = new ObservableCollection<ItemPositionDbt>();

        public DateTime Date { get; set; } = DateTime.Now;
        public string Conclusion { get; set; }
        public int kilometers { get; set; }
        public decimal NettoPrice { get; set; }
        public decimal Total { get; }
        public bool Payed { get; set; }

        public override string FilterString => base.FilterString + Date + Conclusion + Customer.FilterString + Car.FilterString;
    }
}
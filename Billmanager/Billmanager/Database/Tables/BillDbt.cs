using System;
using System.Collections.Generic;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class BillDbt : BaseDbt, IBillDbt
    {
        public string CustomerId { get; set; }
        public CustomerDbt Customer { get; set; }
        public string CarId { get; set; }
        public CarDbt Car { get; set; }
        
        public List<ItemPositionDbt> ItemPositions { get; } = new List<ItemPositionDbt>();

        public DateTime Date { get; set; } = DateTime.Now;
        public string Conclusion { get; set; }
        public int kilometers { get; set; }
        public decimal NettoPrice { get; set; }
        public decimal Total { get; }
        public bool Payed { get; set; }

        public override string FilterString => base.FilterString + Date + Conclusion + Customer.FilterString + Car.FilterString;
    }
}
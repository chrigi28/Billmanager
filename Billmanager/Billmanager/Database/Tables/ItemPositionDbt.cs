using System;
using System.Collections.Generic;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class ItemPositionDbt : BaseDbt, IItemPositionDbt
    {
        public string ItemPositionId { get; set; }
        public string BillId { get; set; }

        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total => this.Price * this.Amount;
    }
}
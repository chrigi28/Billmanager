using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Billmanager.Interfaces.Data;

namespace Billmanager.Interfaces.Database.Datatables
{
    public interface IBillDbt : IDatabaseTable
    {
        int CustomerId { get; set; }
        int CarId { get; set; }

        ////IList<IItemPositionDbt> ItemPositions { get; set; }
        DateTime Date { get; set; }
        string Conclusion { get; set; }
        int Kilometers { get; set; }
        decimal NettoPrice { get; set; }
        decimal Total { get; }
        bool Payed { get; set; }
    }
}
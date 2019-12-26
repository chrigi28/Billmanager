﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Billmanager.Interfaces.Data;

namespace Billmanager.Interfaces.Database.Datatables
{
    public interface IBillDbt : IDatabaseTable
    {
        string BillId { get; set; }
        string CustomerId { get; set; }
        string CarId { get; set; }

        IList<IItemPositionDbt> ItemPositions { get; set; }
        DateTime Date { get; set; }
        string Conclusion { get; set; }
        int kilometers { get; set; }
        decimal NettoPrice { get; set; }
        decimal Total { get; }
        bool Payed { get; set; }
    }
}
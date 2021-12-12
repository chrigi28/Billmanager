using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Billmanager.Interfaces.Data;

namespace Billmanager.Interfaces.Database.Datatables;

public interface IItemPositionDbt : IDatabaseTable
{
    int BillId { get; set; }
    decimal Amount { get; set; }
    decimal Price { get; set; }
    string Description { get; set; }
    decimal Total { get; }
}
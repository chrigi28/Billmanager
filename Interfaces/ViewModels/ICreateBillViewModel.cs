﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.Interface.ViewModels;

public interface ICreateBillViewModel : IViewModelBase
{
    Command SelectCarCommand { get; }
    Command SelectCustomerCommand { get; }
    ////Command AddPositionCommand { get; }
    ////string Description { get; set; }
    ////decimal NetPrice { get; set; }
    ////int Amount { get; set; }
    ////decimal PricePerPiece { get; set; }
    ////decimal ItemTotal { get; }
    ////decimal Total { get; }
}
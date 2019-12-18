using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.Interface.ViewModels
{
    public interface IOverviewViewModel : IViewModelBase
    {
        Command CreateCustomerCommand { get; }
        Command CreateCarCommand { get; }
        Command CreateBillCommand { get; }
        Command CreateWorkcardCommand { get; }
        Command CreateAddresscardCommand { get; }
        Command CreateOffertCommand { get; }
    }
}

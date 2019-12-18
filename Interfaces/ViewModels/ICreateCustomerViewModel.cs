using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Billmanager.Interface.ViewModels
{
    public interface ICreateCustomerViewModel<T> : IDataViewModelBase<T> where T : ICustomerDbt
    {
        ICommand SelectCustomerCommand { get; }
    }
}

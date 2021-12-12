using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Prism.Navigation;

namespace Billmanager.Interface.ViewModels;

public interface ICreateCarViewModel<T> : IDataViewModelBase<T> where T : ICarDbt
{
}
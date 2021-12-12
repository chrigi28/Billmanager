using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Billmanager.Interface.ViewModels;

public interface IViewModelBase : IInitialize, INavigationAware, IDestructible
{
}
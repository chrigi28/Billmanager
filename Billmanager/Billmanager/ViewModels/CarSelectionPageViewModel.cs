using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.StaticAppData;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels
{
    public class CarSelectionPageViewModel  : GenericSelectionViewModel<ICarDbt>
    {
        public CarSelectionPageViewModel(INavigationService ns) : base(ns)
        {
        }

        public override ICarDbt SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                SelectionData.SelectedCar = value;
                this.NavigationService.GoBackAsync();
            }
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            this.ItemSource = await DependencyService.Get<ICarService>().GetCarSelectionAsync();
            
        }
    }
}
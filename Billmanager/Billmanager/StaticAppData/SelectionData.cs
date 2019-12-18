using System;
using System.Collections.Generic;
using System.Text;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.StaticAppData;
using Xamarin.Forms;

[assembly: Dependency(typeof(SelectionData))]
namespace Billmanager.StaticAppData
{
    public static class SelectionData
    {
        public static ICustomerDbt SelectedCustomer { get; set; }
        public static ICarDbt SelectedCar { get; set; }
        public static IBillDbt SelectedBill { get; set; }
    }
}

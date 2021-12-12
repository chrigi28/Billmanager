using System;
using System.Collections.Generic;
using System.Text;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.SharedAppData;
using Xamarin.Forms;

[assembly: Dependency(typeof(SelectionData))]
namespace Billmanager.SharedAppData;

public class SelectionData
{
    private static ICustomerDbt _selectedCustomer;
    private static ICarDbt _selectedCar;
    private static IBillDbt _selectedBill;

    public ICustomerDbt SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (_selectedCustomer == value)
            {
                _selectedCustomer = value;
            }
        }
    }

    public ICarDbt SelectedCar
    {
        get => _selectedCar;
        set => _selectedCar = value;
    }

    public IBillDbt SelectedBill
    {
        get => _selectedBill;
        set => _selectedBill = value;
    }
}
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Database;

public interface ISelectionData
{
        
    ICustomerDbt SelectedCustomer { get; set; }
        
    ICarDbt SelectedCar { get; set; }
        
    IBillDbt SelectedBill { get; set; }
}
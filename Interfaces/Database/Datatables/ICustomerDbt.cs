namespace Billmanager.Interfaces.Database.Datatables;

public interface ICustomerDbt : IDatabaseTable
{
    string Title_customer { get; set; }
    string CustomerNumber { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    string Zip { get; set; }
    string Location { get; set; }
    string Phone { get; set; }
}
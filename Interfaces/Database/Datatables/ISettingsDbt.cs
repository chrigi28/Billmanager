namespace Billmanager.Interfaces.Database.Datatables
{
    public interface ISettingsDbt : IDatabaseTable
    {
        string Title { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string Zip { get; set; }
        string Location { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string IBAN { get; set; }
        byte[] Logo { get; set; }

    }
}
using System.ComponentModel;

namespace Billmanager.Interfaces.Database.Datatables
{
    public interface ICarDbt : IDatabaseTable
    {
        int CustomerId { get; set; }
        string CarMake { get; set; }
        string Typ { get; set; }
        string Typecertificate { get; set; }
        string EnginNo { get; set; }
        string FirstOnMarket { get; set; }
        string Cubic { get; set; }
        string ChassisNo { get; set; }
        string Plate { get; set; }
        string Rootnumber { get; set; }
    }
}
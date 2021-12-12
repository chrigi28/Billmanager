using System.ComponentModel.DataAnnotations.Schema;
using Billmanager.Interfaces.Database.Datatables;
using PropertyChanged;

namespace Billmanager.Database.Tables;

public class CarDbt : BaseDbt, ICarDbt
{
    [ForeignKey(nameof(CustomerDbt))]
    public int CustomerId { get; set; }
    public virtual CustomerDbt Customer { get; set; }
    public string CarNumber { get; set; }
    public string CarMake { get; set; }
    public string Typ { get; set; }
    public string Typecertificate { get; set; }
    public string EnginNo { get; set; }
    public string FirstOnMarket { get; set; }
    public string Cubic { get; set; }
    public string ChassisNo { get; set; }
    public string Plate { get; set; }
    public string Rootnumber { get; set; }

    [AlsoNotifyFor(nameof(Customer), nameof(CarMake), nameof(Typ))]
    public override bool CanSave => this.Customer != null || !string.IsNullOrEmpty(this.CarMake) && !string.IsNullOrEmpty(this.Typ);

    public override string FilterString => base.FilterString + CarNumber + CarMake + Typ + Typecertificate + EnginNo + FirstOnMarket + Cubic +
                                           ChassisNo + Plate + Rootnumber + base.FilterString;
}
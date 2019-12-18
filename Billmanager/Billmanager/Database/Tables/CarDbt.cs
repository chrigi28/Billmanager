using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class CarDbt : BaseDbt, ICarDbt
    {
        public string CarMake { get; set; }
        public string Typ { get; set; }
        public string Typecertificate { get; set; }
        public string EnginNo { get; set; }
        public string FirstOnMarket { get; set; }
        public string Cubic { get; set; }
        public string ChassisNo { get; set; }
        public string Plate { get; set; }
        public string Rootnumber { get; set; }

        public override string FilterString => CarMake + Typ + Typecertificate + EnginNo + FirstOnMarket + Cubic +
                                               ChassisNo + Plate + Rootnumber + base.FilterString;
    }
}
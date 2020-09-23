using System.ComponentModel.DataAnnotations;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class CustomerDbt : BaseDbt, ICustomerDbt
    {
        public string Title_customer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }

        public override string FilterString => base.FilterString + this.Title_customer + LastName + Address + Zip +
                                               Location + Phone + base.FilterString;

    }
}
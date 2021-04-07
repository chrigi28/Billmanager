using System.ComponentModel.DataAnnotations;
using Billmanager.Interfaces.Database.Datatables;
using PropertyChanged;

namespace Billmanager.Database.Tables
{
    public class CustomerDbt : BaseDbt, ICustomerDbt
    {
        public string Title_customer { get; set; }
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        
        [AlsoNotifyFor(nameof(FirstName))]
        public override bool CanSave => !string.IsNullOrEmpty(this.FirstName);

        public override string FilterString => base.FilterString + this.Title_customer + this.CustomerNumber + LastName + Address + Zip +
                                               Location + Phone + base.FilterString;
    }
}
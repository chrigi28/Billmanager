using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service
{
    public interface IBillService
    {
        IEnumerable<IBillDbt> GetBillById(string BillId);
        IEnumerable<IBillDbt> GetBillByCar(string CarId);
        IEnumerable<IBillDbt> GetBillsOfCustomer(string CustomerId);
        IEnumerable<IItemPositionDbt> GetItemPositions(string BillId);
    }
}
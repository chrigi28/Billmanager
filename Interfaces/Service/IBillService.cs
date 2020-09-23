using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service
{
    public interface IBillService
    {
        IEnumerable<IBillDbt> GetBillById(int BillId);
        IEnumerable<IBillDbt> GetBillByCar(int CarId);
        IEnumerable<IBillDbt> GetBillsOfCustomer(int CustomerId);
        IEnumerable<IItemPositionDbt> GetItemPositions(int BillId);
    }
}
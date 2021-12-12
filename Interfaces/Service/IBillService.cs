using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service;

public interface IBillService
{
    Task<IBillDbt> GetBillByIdAsync(int BillId);
    Task<IEnumerable<IBillDbt>> GetBillsByCarAsync(int CarId);
    Task<IEnumerable<IBillDbt>> GetBillsOfCustomerAsync(int CustomerId);
    Task<IEnumerable<IItemPositionDbt>> GetItemPositionsAsync(int BillId);
}
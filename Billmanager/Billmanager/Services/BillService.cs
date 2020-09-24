using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BillService))]
namespace Billmanager.Services
{
    public class BillService : BaseService, IBillService
    {
        public async Task<IBillDbt> GetBillByIdAsync(int BillId)
        {
            return await this.GetByIdAsync<BillDbt>(BillId);
        }

        public async Task<IEnumerable<IBillDbt>> GetBillsByCarAsync(int CarId)
        {
            return await SqliteDatabase.AssureDb().GetItemsAsync<BillDbt>(f => f.CarId == CarId);

        }

        public async Task<IEnumerable<IBillDbt>> GetBillsOfCustomerAsync(int CustomerId)
        {
            return await SqliteDatabase.AssureDb().GetItemsAsync<BillDbt>(f => f.CustomerId == CustomerId);

        }

        public async Task<IEnumerable<IItemPositionDbt>> GetItemPositionsAsync(int BillId)
        {
            return await SqliteDatabase.AssureDb().GetItemsAsync<ItemPositionDbt>(f => f.BillId == BillId);

        }
    }
}
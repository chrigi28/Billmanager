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
    public class BillService : IBillService
    {
        public IEnumerable<IBillDbt> GetBillById(string BillId)
        {
            return SqliteDatabase.GetTable<BillDbt>().Table.Where(f => f.BillId == BillId);
        }

        public IEnumerable<IBillDbt> GetBillByCar(string CarId)
        {
            return SqliteDatabase.GetTable<BillDbt>().Table.Where(f => f.CarId == CarId);
        }
    
        public IEnumerable<IBillDbt> GetBillsOfCustomer(string CustomerId)
        {
            return SqliteDatabase.GetTable<BillDbt>().Table.Where(f => f.CustomerId == CustomerId);
        }

        public IEnumerable<IItemPositionDbt> GetItemPositions(string BillId)
        {
            return SqliteDatabase.GetTable<IItemPositionDbt>().Table.Where(f => f.BillId == BillId);
        }
    }
}
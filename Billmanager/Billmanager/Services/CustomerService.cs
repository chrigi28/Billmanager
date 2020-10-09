using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomerService))]
namespace Billmanager.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public async Task<IEnumerable<ICustomerDbt>> GetCustomerSelection()
        {
           return await SqliteDatabase.AssureDb().GetItemsAsync<CustomerDbt>();
           
        }
    }
}
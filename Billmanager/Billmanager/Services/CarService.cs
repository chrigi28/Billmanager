using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarService))]
namespace Billmanager.Services
{
    public class CarService : ICarService
    {
        public async Task<IEnumerable<ICarDbt>> GetCarSelection()
        {
            return await SqliteDatabase.GetTable<CarDbt>().GetItemsAsync();
        }

        public async Task<IEnumerable<ICarDbt>> GetCarSelectionFromCustomer(string customer)
        {
            return await SqliteDatabase.GetTable<CarDbt>().GetItemsAsync(f=> f.CustomerId == customer);
        }
    }
}
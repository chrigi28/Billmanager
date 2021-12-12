using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarService))]
namespace Billmanager.Services;

public class CarService : BaseService, ICarService
{
    public async Task<IEnumerable<ICarDbt>> GetCarSelectionAsync()
    {
        return await this.GetAllAsync<CarDbt>();
    }

    public async Task<IEnumerable<ICarDbt>> GetCarSelectionFromCustomerAsync(int customer)
    {
        return await SqliteDatabase.AssureDb().GetItemsAsync<CarDbt>(f => f.CustomerId == customer);
           
    }
}
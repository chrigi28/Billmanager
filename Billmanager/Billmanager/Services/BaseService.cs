using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseService))]
namespace Billmanager.Services;

public class BaseService : IBaseService
{
    public async Task<T> GetByIdAsync<T>(int id) where T : class, IDatabaseTable
    {
        return await SqliteDatabase.AssureDb().GetItemAsync<T>(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IDatabaseTable
    {
        return await SqliteDatabase.AssureDb().GetItemsAsync<T>();

    }
}
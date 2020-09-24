using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service
{
    public interface IBaseService
    {
        Task<T> GetByIdAsync<T>(int id) where T : class, IDatabaseTable;
    }
}
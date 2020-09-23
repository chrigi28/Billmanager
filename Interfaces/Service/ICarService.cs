using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service
{
    public interface ICarService
    {
        Task<IEnumerable<ICarDbt>> GetCarSelection();
        Task<IEnumerable<ICarDbt>> GetCarSelectionFromCustomer(int customer);
    }
}
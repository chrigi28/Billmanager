using System.Collections.Generic;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Interfaces.Service;

public interface ICustomerService
{
    Task<IEnumerable<ICustomerDbt>> GetCustomerSelection();
}
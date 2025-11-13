using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IRoleServices
    {
        Task<IEnumerable<Role>> GetAllActiveRolesAsync();
        Task<DataTableResult<Role>> GetAllRoleAsync(Table request);
        Task<Role> GetRoleAsync(int id);
        Task CreateRoleAsync(Role model);
        Task UpdateRoleAsync(Role model);
        Task<bool> DeleteRoleAsync(int id);
        Task<bool> IsExistByNameAsync(string name);
        Task<int> Count(bool? isActive = false);
    }
}

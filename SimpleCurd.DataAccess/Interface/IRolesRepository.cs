using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IRolesRepository : IRepostiory<Role>
    {
        Task<IEnumerable<Role>> GetAllAsync(Expression<Func<Role, bool>> filter = null);
        Task<Role> GetActiveByIdAsync(int id);
        Task<List<Role>> GetByNameAsync(string name);
    }
}

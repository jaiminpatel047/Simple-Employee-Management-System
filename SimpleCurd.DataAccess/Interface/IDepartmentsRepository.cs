using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IDepartmentsRepository : IRepostiory<Department>
    {
        Task<IEnumerable<Department>> GetAllActiveAsync();
        Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null);
        Task<Department> GetActiveByIdAsync(int id);
    }
}

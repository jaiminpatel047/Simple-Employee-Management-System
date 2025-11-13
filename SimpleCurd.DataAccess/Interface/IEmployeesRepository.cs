using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IEmployeesRepository : IRepostiory<Employee>
    {
        Task<IEnumerable<Employee>> GetAllActiveAsync(Expression<Func<Employee, bool>> filter = null);
        Task<Employee> GetActiveByIdAsync(int id);
        bool EmployeeExistsAsync(int id);
    }
}

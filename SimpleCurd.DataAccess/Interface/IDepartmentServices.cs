using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IDepartmentServices 
    {
        Task<IEnumerable<Department>> GetAllDepartmentAsync();
        Task<DataTableResult<Department>> GetAllDepartmentAsync(Table request);
        Task<Department> GetDepartmentAsync(int id);
        Task CreateDepartmentAsync(Department model);
        Task UpdateDepartmentAsync(Department model);
        Task<bool> DeleteDepartmentAsync(int id);
        Task<bool> IsExistByNameAsync(string name);
        Task<int> Count(bool? isActive = false);
    }
}

using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Interface
{
    public interface IEmployeeServices
    {
        Task<DataTableResult<Employee>> GetAllAsync(Table request);
        Task<Employee> GetEmployeeAsync(int id);
        Task CreateEmployeeAsync(Employee model);
        Task UpdateEmployeeAsync(Employee model);
        Task<bool> DeleteEmployeeAsync(int id);
        bool IsEmployeExist(int id);
        Task<bool> IsExistByNameAsync(string firstname, string lastname);
        Task<int> Count(bool? isActive = false);
    }
}

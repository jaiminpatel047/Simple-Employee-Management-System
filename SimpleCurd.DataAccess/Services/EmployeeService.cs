using SimpleCurd.DataAccess.Helper;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.DataAccess.Repository;
using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IRolesRepository _rolesRepository;

        public EmployeeServices(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository, IRolesRepository rolesRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentsRepository = departmentsRepository;
            _rolesRepository = rolesRepository;
        }
        public async Task<DataTableResult<Employee>> GetAllAsync(Table request)
        {
            var list = await _employeesRepository.GetAllAsync(includePropeties : "Department,Role,Address");

            var totalRecords = list.Count();
            // Search
            if (!string.IsNullOrEmpty(request.search?.value))
            {
                var searchValue = request.search.value.ToLower();
                list = list.Where(x => x.FirstName.ToLower().Contains(searchValue));
            }

            if (request.IsActive.HasValue)
            {
                list = list.Where(x => x.IsActive == request.IsActive.Value);
            }

            var filteredRecords = list.Count();

            // Sorting
            if (!string.IsNullOrEmpty(request.SortColumnName))
            {
                switch (request.SortColumnName)
                {
                    case "CreatedAt":
                        list = request.SortDirection == "asc" ? list.OrderBy(s => s.CreatedAt) : list.OrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        list = request.SortDirection == "desc" ? list.OrderBy(s => s.Id) : list.OrderByDescending(s => s.Id);
                        break;
                }
            }

            int start = request.start < 0 ? 0 : request.start;
            int length = (request.length <= 0) ? 10 : request.length;

            // Paging
            list = list.Skip(start).Take(length);

            return new DataTableResult<Employee>
            {
                totalRecords = totalRecords,
                filteredrecords = filteredRecords,
                data = list.ToList()
            };
        }
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var employe = await _employeesRepository.GetAsync(x => x.Id == id, includePropeties : "Department,Address,Role");
            if (employe == null) {
                return null;
            }
            return employe;
        }
        public async Task CreateEmployeeAsync(Employee employee)
        {
            var uniqueId = CommentHelper.CreateUniqueId(employee.FirstName, employee.DateOfBirth, employee.Email, employee.Phone);

            employee.UniqueId = uniqueId;
            employee.CreatedAt = DateTime.UtcNow;
            employee.IsActive = true;

            if (employee.Address != null)
            {
                employee.Address.CreatedAt = DateTime.UtcNow;
                employee.Address.IsActive = true;
            }

            await _employeesRepository.AddAsync(employee);
            await _employeesRepository.SaveAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            employee.ModifiedAt = DateTime.UtcNow;

            _employeesRepository.Update(employee);
            await _employeesRepository.SaveAsync();
        }
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if (id <= 0) { return false; }

            var employee = await _employeesRepository.GetByIdAsync(id);

            if (employee == null) { return false; }

            employee.IsActive = false;
            employee.ModifiedAt = DateTime.UtcNow;

            _employeesRepository.Update(employee);
            await _employeesRepository.SaveAsync();
            return true;
        }
        public bool IsEmployeExist(int id)
        {
            bool isExist = _employeesRepository.EmployeeExistsAsync(id);
            return isExist;
        }
        public async Task<bool> IsExistByNameAsync(string firstname, string lastname)
        {
            firstname = firstname.Trim().ToLower();
            lastname = lastname.Trim().ToLower();
            return await _employeesRepository.IsExistByNameAsync(x => x.FirstName.ToLower() == firstname && x.LastName.ToLower() == lastname);
        }
        public async Task<int> Count(bool? isActive = false)
        {
            int count = 0;
            if (isActive == true)
            {
                count = await _employeesRepository.CountAsync(x => x.IsActive);
            }
            else
            {
                count = await _employeesRepository.CountAsync();
            }
            return count;
        }
    }
}

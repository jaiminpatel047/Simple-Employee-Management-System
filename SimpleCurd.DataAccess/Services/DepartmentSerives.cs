using SimpleCurd.DataAccess.Interface;
using SimpleCurd.DataAccess.Repository;
using SimpleCurd.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.DataAccess.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentsRepository _departmentRepository;
        public DepartmentServices(IDepartmentsRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllActiveAsync();
        }
        public async Task<DataTableResult<Department>> GetAllDepartmentAsync(Table request)
        {
            var list = await _departmentRepository.GetAllAsync();

            var totalRecords = list.Count();
            // Search
            if (!string.IsNullOrEmpty(request.search?.value))
            {
                var searchValue = request.search.value.ToLower();
                list = list.Where(x => x.Name.ToLower().Contains(searchValue));
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
                    case "Name":
                        list = request.SortDirection == "asc" ? list.OrderBy(s => s.Name) : list.OrderByDescending(s => s.Name);
                        break;
                    case "Description":
                        list = request.SortDirection == "asc" ? list.OrderBy(s => s.Description) : list.OrderByDescending(s => s.Description);
                        break;
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

            return new DataTableResult<Department>
            {
                totalRecords = totalRecords,
                filteredrecords = filteredRecords,
                data = list.ToList()
            };
        }
        public async Task<Department> GetDepartmentAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return await _departmentRepository.GetByIdAsync(id);
        }
        public async Task CreateDepartmentAsync(Department model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.IsActive = true;

            await _departmentRepository.AddAsync(model);
            await _departmentRepository.SaveAsync();
        }
        public async Task UpdateDepartmentAsync(Department model)
        {
            model.ModifiedAt = DateTime.UtcNow;

            _departmentRepository.Update(model);
            await _departmentRepository.SaveAsync();
        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            if (id <= 0) { return false; }

            var department = await _departmentRepository.GetActiveByIdAsync(id);

            if (department == null) { return false; }

            department.IsActive = false;
            department.ModifiedAt = DateTime.UtcNow;

            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync();
            return true;
        }
        public async Task<bool> IsExistByNameAsync(string name)
        {
            name = name.Trim().ToLower();
            return await _departmentRepository.IsExistByNameAsync(x => x.Name.ToLower() == name);
        }
        public async Task<int> Count(bool? isActive = false)
        {
            int count = 0;
            if (isActive == true)
            {
                count = await _departmentRepository.CountAsync(x => x.IsActive);
            }
            else
            {
                count = await _departmentRepository.CountAsync();
            }
            return count;
        }
    }
}

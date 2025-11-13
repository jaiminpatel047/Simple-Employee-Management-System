using Microsoft.IdentityModel.Tokens;
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
    public class RoleServices : IRoleServices
    {
        private readonly IRolesRepository _rolesRepository;
        public RoleServices(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<IEnumerable<Role>> GetAllActiveRolesAsync()
        {
            return await _rolesRepository.GetAllAsync(x => x.IsActive == false);
        }
        public async Task<DataTableResult<Role>> GetAllRoleAsync(Table request)
        {
            var roles = await _rolesRepository.GetAllAsync(); 

            var totalRecords = roles.Count();

            // Search
            if (!string.IsNullOrEmpty(request.search?.value))
            {
                var searchValue = request.search.value.ToLower();
                roles = roles.Where(x => x.Name.ToLower().Contains(searchValue));
            }

            if (request.IsActive.HasValue)
            {
                roles = roles.Where(x => x.IsActive == request.IsActive.Value);
            }

            var filteredRecords = roles.Count();

            // Sorting
            if (!string.IsNullOrEmpty(request.SortColumnName))
            {
                switch (request.SortColumnName)
                {
                    case "Name":
                        roles = request.SortDirection == "asc" ? roles.OrderBy(s => s.Name) : roles.OrderByDescending(s => s.Name);
                        break;
                    case "Description":
                        roles = request.SortDirection == "asc" ? roles.OrderBy(s => s.Description) : roles.OrderByDescending(s => s.Description);
                        break;
                    case "CreatedAt":
                        roles = request.SortDirection == "asc" ? roles.OrderBy(s => s.CreatedAt) : roles.OrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        roles = request.SortDirection == "desc" ? roles.OrderBy(s => s.Id) : roles.OrderByDescending(s => s.Id);
                        break;
                }
            }

            // Paging
            roles = roles.Skip(request.start).Take(request.length);

            return new DataTableResult<Role>
            {
                totalRecords = totalRecords,
                filteredrecords = filteredRecords,
                data = roles.ToList()
            };
        }
        public async Task<Role> GetRoleAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Role ID", nameof(id));

            var roleView = await _rolesRepository.GetByIdAsync(id);
            
            if (roleView == null)
                throw new KeyNotFoundException("Role not found");

            return roleView;
        }
        public async Task CreateRoleAsync(Role model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.IsActive = true;

            await _rolesRepository.AddAsync(model);
            await _rolesRepository.SaveAsync();
        }
        public async Task UpdateRoleAsync(Role model)
        {
            model.ModifiedAt = DateTime.UtcNow;

            _rolesRepository.Update(model);
            await _rolesRepository.SaveAsync();
        }
        public async Task<bool> DeleteRoleAsync(int id)
        {
            if (id <= 0) { return false; }

            var role = await _rolesRepository.GetActiveByIdAsync(id);

            if (role == null) { return false; }

            role.IsActive = false;
            role.ModifiedAt = DateTime.UtcNow;

            _rolesRepository.Update(role);
            await _rolesRepository.SaveAsync();
            return true;
        }
        public async Task<bool> IsExistByNameAsync(string name)
        {
            name = name.Trim().ToLower();
            return await _rolesRepository.IsExistByNameAsync(x => x.Name.ToLower() == name);
        }
        public async Task<int> Count(bool? isActive = false)
        {
            int count = 0;
            if (isActive == true)
            {
                count = await _rolesRepository.CountAsync(x => x.IsActive);
            }
            else
            {
                count = await _rolesRepository.CountAsync();
            }
            return count;
        }
    }
}

using SimpleCurd.Model;
using SimpleCurd.Web.Models;
using System.Net;

namespace SimpleCurd.Web.Helper
{
    public static class CustomeMapper
    {
        // Address
        public static AddressDto ToAddressDto(this Address entity)
        {
            if (entity == null) return null;

            return new AddressDto
            {
                Street = entity.Street,
                State = entity.State,
                City = entity.City,
                Country = entity.Country,
                PostalCode = entity.PostalCode,
            };
        }
        public static AddressDto ToModel(this Address entity)
        {
            return new AddressDto
            {
                Street = entity.Street,
                State = entity.State,
                City = entity.City,
                Country = entity.Country,
                PostalCode = entity.PostalCode,
            };
        }
        public static Address ToEntity(this AddressDto entity)
        {
            return new Address
            {
                Street = entity.Street,
                State = entity.State,
                City = entity.City,
                Country = entity.Country,
                PostalCode = entity.PostalCode
            };
        }

        // Employee
        public static EmployeeDto ToModel(this Employee entity)
        {
            return new EmployeeDto
            {
                Id = entity.Id,
                FirstName = entity.LastName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth,
                DateOfBirthStr = entity.DateOfBirth.ToString("dd MMMM yyyy"),
                HireDate = entity.HireDate,
                HireDateStr = entity.HireDate.ToString("dd MMMM yyyy"),
                Salary = entity.Salary,
                DepartmentId = entity.DepartmentId,
                departmentDto = entity.Department.ToModel(),
                RoleId = entity.RoleId,
                roleDto = entity.Role.ToModel(),
                IsActive = entity.IsActive,
                Address = entity.Address.ToModel()
            };
        }
        public static Employee ToEntity(this EmployeeDto entity)
        {
            return new Employee
            {
                Id = entity.Id,
                FirstName = entity.LastName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth,
                HireDate = entity.HireDate ?? DateTime.UtcNow,
                Salary = entity.Salary ?? 00,
                DepartmentId = entity.DepartmentId,
                RoleId = entity.RoleId,
                IsActive = entity.IsActive,
                Address = entity.Address.ToEntity()
            };
        }
        public static List<EmployeeDto> ToModelList(this IEnumerable<Employee> entities)
        {
            return entities.Select(entity => new EmployeeDto
            {
                Id = entity.Id,
                UniqueId = entity.UniqueId,
                FullName = entity.FullName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth,
                DateOfBirthStr = entity.DateOfBirth.ToString("dd MMMM yyyy"),
                HireDate = entity.HireDate,
                HireDateStr = entity.HireDate.ToString("dd MMMM yyyy"),
                Salary = entity.Salary,
                DepartmentId = entity.DepartmentId,
                departmentDto = entity.Department.ToModel(),
                roleDto = entity.Role.ToModel(),
                RoleId = entity.RoleId,
                IsActive = entity.IsActive,
                Address = entity.Address.ToAddressDto()
            }).ToList();
        }

        // Role
        public static RoleDto ToModel(this Role entity)
        {
            return new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt.ToString("dd MMMM yyyy"),
                IsActive = entity.IsActive
            };
        }
        public static Role ToEntity(this RoleDto entity)
        {
            return new Role
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }
        public static List<RoleDto> ToModelList(this IEnumerable<Role> entities)
        {
            return entities.Select(entity => new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt.ToString("dd MMMM yyyy")
            }).ToList();
        }

        // Department
        public static DepartmentDto ToModel(this Department entity)
        {
            return new DepartmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt.ToString("dd MMMM yyyy")
            };
        }
        public static Department ToEntity(this DepartmentDto entity)
        {
            return new Department
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
            };
        }
        public static List<DepartmentDto> ToModelList(this IEnumerable<Department> entities)
        {
            return entities.Select(entity => new DepartmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt.ToString("dd MMMM yyyy")
            }).ToList();
        }
    }
}

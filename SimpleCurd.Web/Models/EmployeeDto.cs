using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleCurd.Model;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SimpleCurd.Web.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string? DateOfBirthStr { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }
        public string? HireDateStr { get; set; }
        public decimal? Salary { get; set; }
        public string? UniqueId { get; set; }
        public List<SelectListItem>? DepartmentList { get; set; }
        public List<SelectListItem>? RoleList { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public AddressDto? Address { get; set; }
        public bool IsActive { get; set; }

        public DepartmentDto? departmentDto { get; set; }
        public RoleDto? roleDto { get; set; }
    }
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedAt { get; set; }
    }
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedAt { get; set; }
    }
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

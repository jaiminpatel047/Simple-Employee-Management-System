using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.Model
{
    public class Employee : BaseClass
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }
        [Phone]
        [MaxLength(20)]
        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string? UniqueId { get; set; }

        // Navigation / foreign keys
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Address Address { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}".Trim();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.Model
{
    public class Address : BaseClass
    {
        [MaxLength(250)]
        public string Street { get; set; }


        [MaxLength(100)]
        public string City { get; set; }


        [MaxLength(100)]
        public string State { get; set; }


        [MaxLength(20)]
        public string PostalCode { get; set; }


        [MaxLength(100)]
        public string Country { get; set; }


        // For one-to-one relation with Employee
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

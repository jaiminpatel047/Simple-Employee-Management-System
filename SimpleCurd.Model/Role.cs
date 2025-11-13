using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurd.Model
{
    public class Role : BaseClass
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }


        [MaxLength(250)]
        public required string Description { get; set; }
    }
}

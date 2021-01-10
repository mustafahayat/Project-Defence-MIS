using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class Person : Base
    {

        [Required] 
        [StringLength(150)]
        public string Name { get; set; }

        [Required] 
        [StringLength(150)]
        public string LastName { get; set; }


        public string Image { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }

    }
}

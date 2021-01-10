using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class Committee: Base
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}

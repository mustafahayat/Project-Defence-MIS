using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class Faculty : Base
    {
        [Required]
        [StringLength(120)]
        [Display(Name = "Faculty Name")]
        public string Name { get; set; }
        
        [Required] 
        public string Description { get; set; }

        [Required]
        [Display(Name = "University Name")]
        public int UniversityId { get; set; }

        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }
    }

}

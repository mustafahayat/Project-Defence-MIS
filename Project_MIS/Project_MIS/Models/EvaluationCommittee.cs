using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{ 
    public class EvaluationCommittee : Base
    {
        [Required]
        [Display(Name = "Committee Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Lecturer Name")]
        public int LecturerId { get; set; }

        [ForeignKey(nameof(LecturerId))]
        public Lecturer Lecturer { get; set; }


        [Required]
        public LecturerTask LecturerTask { get; set; }


    }
}

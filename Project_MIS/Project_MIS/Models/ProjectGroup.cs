using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class ProjectGroup : Base
    {
        [Required]
        [StringLength(120)] 
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [Required]
        [Display(Name = "LeaderLecturer Name")]
        public int LecturerId { get; set; }
        
        [ForeignKey(nameof(LecturerId))]
        public Lecturer Lecturer { get; set; }

        [Required]
        public ProjectState ProjectState { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class ProjectResult :Base
    {

        [Required]
        public DateTime DefenceDateTime { get; set; }

        public string Comment { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        [Required]
        public ProjectState ProjectState { get; set; }

        [Required]
        [Display(Name = "Committee Name")]
        public int CommitteeId { get; set; }

        [ForeignKey(nameof(CommitteeId))]
        public Committee Committee { get; set; }

        [Required]
        [Display(Name = "Lecturer Name")]
        public int LecturerId { get; set; }

        [ForeignKey(nameof(LecturerId))]
        public Lecturer Lecturer { get; set; }
    }
}

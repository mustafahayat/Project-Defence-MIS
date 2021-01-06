using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class DefenceDay :Base
    {

        [Required]
        public DateTime DefenceDateTime { get; set; }

        public string Comment { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public int ProjectGroupId { get; set; }

        [ForeignKey(nameof(ProjectGroupId))]
        public ProjectGroup ProjectGroup { get; set; }

        [Required]
        public ProjectState ProjectState { get; set; }

        [Required]
        [Display(Name = "Committee Name")]
        public int CommitteeId { get; set; }

        [ForeignKey(nameof(CommitteeId))]
        public EvaluationCommittee EvaluationCommittee { get; set; }
    }
}

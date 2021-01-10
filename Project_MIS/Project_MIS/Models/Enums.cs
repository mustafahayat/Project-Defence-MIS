using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Project_MIS.Models
{

    public enum LecturerTask
    {
        ProjectLeader,
        ProjectCommenter,
        ProjectExaminer
    }

    public enum Role
    {
       ProjectLeader,
       ProjectMember
    }
    public enum Gender
    {
        Female,
        Male
    }
    public enum ProjectState
    {
        UnderDevelopment,
        Ready,
        Completed,
        Rejected
    }
   
}

    

    

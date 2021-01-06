using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Models;

namespace Project_MIS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ProjectGroup> ProjectGroups { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<EvaluationCommittee> EvaluationCommittees { get; set; }
        public virtual DbSet<DefenceDay> DefenceDays { get; set; }
        public virtual DbSet<University> Universities { get; set; }
         


    }
}

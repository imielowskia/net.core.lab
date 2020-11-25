using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CW4.Models
{
    public class CW4Context : DbContext
    {
        public CW4Context(DbContextOptions<CW4Context> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseGroup>().HasKey(cg => new { cg.CourseID, cg.GroupID });
            modelBuilder.Entity<Grade>().HasKey(g => new { g.StudentID, g.CourseID, g.GroupID });
        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}

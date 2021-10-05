using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Students.Model.Entities;

namespace Students.Model
{
    public class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Students.Properties.Settings.studentsConn"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // student
            modelBuilder.Entity<Student>()
                .HasIndex(x => x.PersonalId).IsUnique().HasName("UQ_STUDENT_PERSONAL_ID");

            // enrollment
            modelBuilder.Entity<Enrollment>()
                .Property(x => x.EnrollmentDate).HasDefaultValue(DateTime.Now);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
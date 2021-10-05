using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Students.Model.Entities;

namespace Students.Model
{
    public class StudentContext : DbContext
    {
        private readonly IContextInitializer _contextInitializer;
        public StudentContext()
        {
            _contextInitializer = Services.Services.ServiceProvider.GetService<IContextInitializer>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_contextInitializer.GetConnectionString());
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students.Model.Entities;
using Students.Model.Interfaces;

namespace Students.Model.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        public async Task<bool> SaveAsync(Enrollment enrollment)
        {
            await using var context = new StudentContext();
            await using var tx = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Enrollments.AddAsync(enrollment);
                var result = await context.SaveChangesAsync();
                await tx.CommitAsync();

                return result > 0;
            }
            catch (Exception e)
            {
                await tx.RollbackAsync();

                return false;
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesForStudentAsync(long studentId)
        {
            await using var context = new StudentContext();
            return context.Enrollments.Where(x => x.StudentId == studentId).Select(x => x.Course).ToList();
        }

        public async Task<IEnumerable<Student>> GetStudentsForCourseAsync(long courseId)
        {
            await using var context = new StudentContext();
            return context.Enrollments.Where(x => x.CourseId == courseId).Select(x => x.Student).ToList();
        }
    }
}
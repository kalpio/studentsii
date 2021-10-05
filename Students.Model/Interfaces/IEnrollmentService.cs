using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students.Model.Entities;

namespace Students.Model.Interfaces
{
    public interface IEnrollmentService
    {
        Task<bool> SaveAsync(Enrollment enrollment);
        Task<IEnumerable<Course>> GetCoursesForStudentAsync(long studentId);
        Task<IEnumerable<Student>> GetStudentsForCourseAsync(long courseId);
    }
}

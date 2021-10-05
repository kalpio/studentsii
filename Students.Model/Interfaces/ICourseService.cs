using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Model.Entities;

namespace Students.Model.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<bool> SaveAsync(Course course);
        Task<bool> UpdateAsync(Course course);
        Task<bool> ExistsAsync(long courseNumber);
        Task<long> GetLastNumberAsync();
        Task<Course> GetByIdAsync(long id);
    }
}

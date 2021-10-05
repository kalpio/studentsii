using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Model.Entities;

namespace Students.Model.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Entities.Student>> GetAllAsync();
        Task<bool> SaveAsync(Student student);
        Task<Student> GetByPeselAsync(string pesel);
    }
}

using System;
using System.Threading.Tasks;

namespace Students.Model
{
    public delegate void ChangeEventHandler(ChangeEventArgs e);
    public interface IStudentInitializer
    {
        event ChangeEventHandler Change;
        Task CreateDatabaseAsync();
        Task InsertTestDataAsync();
    }
}

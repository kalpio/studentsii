using Microsoft.Extensions.DependencyInjection;
using Students.Model;

namespace Students
{
    internal static class Services
    {

        public static void ConfigureServices()
        {
            Model.Services.Services.ServiceCollection.AddSingleton<IContextInitializer, ContextInitializer>();
            Model.Services.Services.ServiceCollection.AddTransient<IStudentInitializer, StudentsInitializer>();
            Model.Services.Services.ServiceCollection.AddTransient<Model.Interfaces.IStudentService, Model.Services.StudentService>();
            Model.Services.Services.ServiceCollection.AddTransient<Model.Interfaces.ICourseService, Model.Services.CourseService>();
            Model.Services.Services.ServiceCollection.AddTransient<Model.Interfaces.IEnrollmentService, Model.Services.EnrollmentService>();
            Model.Services.Services.ConfigureServices();
        }
    }
}

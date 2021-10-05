using System;
using System.Threading.Tasks;
using NLog;
using Students.Model.Entities;
using Students.Model.Interfaces;

namespace Students.Model
{
    public class ChangeEventArgs : EventArgs
    {
        public ChangeEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class StudentsInitializer : IStudentInitializer
    {
        public event ChangeEventHandler Change;
        private readonly IStudentService _studentService;
        private readonly ILogger _logger;

        public StudentsInitializer(IStudentService studentService)
        {
            _studentService = studentService;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task CreateDatabaseAsync()
        {
            try
            {
                await using var db = new StudentContext();
                OnChange("Checking database...");
                var existing = await db.Database.EnsureCreatedAsync();
                OnChange(existing ? "Database created!" : "Database already existed!");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Create database with tables");
                OnChange("Error during create database. Please check log file for more information.");
            }
        }

        public async Task InsertTestDataAsync()
        {
            var studentTestData = new[]
            {
                new Student
                {
                    DateOfBirth = new DateTime(1999, 10, 22), FirstName = "Juliusz", LastName = "Zalewski",
                    PersonalId = "99102263611"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1997, 1, 24), FirstName = "Alojzy", LastName = "Pawlak",
                    PersonalId = "97012446777"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2002, 12, 13), FirstName = "Dawid", LastName = "Karpiński",
                    PersonalId = "02321322898"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2002, 08, 02), FirstName = "Jerzy", LastName = "Wieczorek",
                    PersonalId = "02280270096"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1998, 01, 12), FirstName = "Rozalia", LastName = "Markiewicz",
                    PersonalId = "98011217988"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1998, 04, 29), FirstName = "Paulina", LastName = "Rogowska",
                    PersonalId = "98042994829"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2002, 7, 18), FirstName = "Aleksandra", LastName = "Dąbrowska",
                    PersonalId = "02271808963"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1999, 3, 23), FirstName = "Włodzimierz", LastName = "Makowski",
                    PersonalId = "99032380192"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2001, 8, 3), FirstName = "Edyta", LastName = "Pająk",
                    PersonalId = "01280386743"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1998, 8, 1), FirstName = "Magdalena", LastName = "Kubiak",
                    PersonalId = "98080183122"
                },
                new Student
                {
                    DateOfBirth = new DateTime(1998, 2, 17), FirstName = "Ihor", LastName = "Ciesielski",
                    PersonalId = "98021707554"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2001, 4, 15), FirstName = "Iga", LastName = "Kaczmarek",
                    PersonalId = "01241501866"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2001, 8, 4), FirstName = "Klemens", LastName = "Ostrowski",
                    PersonalId = "01280413555"
                },
                new Student
                {
                    DateOfBirth = new DateTime(2000, 9, 27), FirstName = "Romana", LastName = "Czerwińska",
                    PersonalId = "00292756621"
                }
            };

            await using var db = new StudentContext();
            foreach (var student in studentTestData)
            {
                await AddStudentAsync(student);
            }

            await db.SaveChangesAsync();
        }

        private async Task AddStudentAsync(Student model)
        {
            try
            {
                await _studentService.SaveAsync(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Database initialize: could not save student: {SubstringPesel(model.PersonalId)}");
            }
        }

        private static string SubstringPesel(string pesel)
        {
            if (string.IsNullOrWhiteSpace(pesel))
                return "";
            return pesel.Length >= 7 ? pesel.Substring(0, 7) : pesel;
        }

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }
    }
}
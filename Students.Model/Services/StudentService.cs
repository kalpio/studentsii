using System;
using Students.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Students.Model.Exceptions;

namespace Students.Model.Services
{
    public class StudentService : Interfaces.IStudentService
    {
        private readonly ILogger _logger;

        public StudentService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            await using var context = new StudentContext();
            return await context.Students.ToListAsync();
        }

        public async Task<bool> SaveAsync(Student student)
        {
            await ValidateStudentSaveAsync(student);
            return await SaveStudentAsync(student);
        }

        private async Task ValidateStudentSaveAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var exists = await GetByPeselAsync(student.PersonalId);
            if (exists != default(Student))
                throw new RecordExistsException("Student z takim nr PESEL już istnieje");
        }

        private async Task<bool> SaveStudentAsync(Student student)
        {
            await using var context = new StudentContext();
            await using var tx = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Students.AddAsync(student);
                var result = await context.SaveChangesAsync();

                await tx.CommitAsync();
                return result > 0;
            }
            catch (Exception e)
            {
                await tx.RollbackAsync();
                _logger.Error(e, "Fail to save Student");
                return false;
            }
        }

        public async Task<Student> GetByPeselAsync(string pesel)
        {
            if (string.IsNullOrWhiteSpace(pesel))
                return default;

            await using var context = new StudentContext();
            return await context.Students.SingleOrDefaultAsync(x => x.PersonalId == pesel);
        }
    }
}
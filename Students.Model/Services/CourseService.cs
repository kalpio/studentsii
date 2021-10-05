using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Students.Model.Entities;
using Students.Model.Exceptions;
using Students.Model.Interfaces;

namespace Students.Model.Services
{
    public class CourseService : ICourseService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            await using var context = new StudentContext();
            return await context.Courses.ToListAsync();
        }

        public async Task<bool> SaveAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            var isExists = await ExistsAsync(course.Number);
            if (isExists)
                throw new RecordExistsException($"course record with ID: {course.Number} already exists");
            
            return await SaveCourseAsync(course);
        }

        private async Task<bool> SaveCourseAsync(Course course)
        {
            await using var context = new StudentContext();
            await using var tx = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Courses.AddAsync(course);
                var result = await context.SaveChangesAsync();

                await tx.CommitAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.Error(ex, "Fail to save Course");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            await using var context = new StudentContext();
            await using var tx = await context.Database.BeginTransactionAsync();
            try
            {
                context.Courses.Update(course);
                var result = await context.SaveChangesAsync();
                await tx.CommitAsync();

                return result > 0;
            }
            catch (Exception e)
            {
                await tx.RollbackAsync();
                _logger.Error(e, "Fail to update course");
                return false;
            }
        }

        public async Task<bool> ExistsAsync(long courseNumber)
        {
            await using var context = new StudentContext();
            var result = await context.Courses.FirstOrDefaultAsync(x => x.Number == courseNumber);

            return result != default;
        }

        public async Task<long> GetLastNumberAsync()
        {
            await using var context = new StudentContext();
            return await context.Courses.MaxAsync(x => x.Number);
        }

        public async Task<Course> GetByIdAsync(long id)
        {
            await using var context = new StudentContext();
            return await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
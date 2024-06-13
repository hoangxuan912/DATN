using asd123.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asd123.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentService Departments { get; }
        IMajor Majors { get; }
        ISubject Subjects { get; }
        IClass Class { get; }
        IStudent Students { get; }
        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
        void Commit();
        ApplicationDbContext GetDbContext();
        public IEnumerable<ValidationResult> GetValidationErrors();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IDepartmentService Departments { get; }
        public IMajor Majors { get; }
        public ISubject Subjects { get; }
        public IClass Class { get; }
        public IStudent Students { get; }
        public UnitOfWork(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            Departments = new DepartmentService(dbContext);
            Majors = new MajorService(dbContext);
            Subjects = new SubjectService(dbContext);
            Class = new ClassService(dbContext);
            Students = new StudentService(dbContext);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return dbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        public void Commit()
        {
            using (var transaction = BeginTransaction())
            {
                try
                {
                    SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<ValidationResult> GetValidationErrors()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(Students); // Assuming _uow is your DbContext
            Validator.TryValidateObject(Students, context, validationResults, validateAllProperties: true);
            return validationResults;
        }

        
        public ApplicationDbContext GetDbContext()
        {
            return dbContext;
        }
    }
}

using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using EntityFrameWorkTp.Models;
using EntityFrameWorkTp.utils;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace EntityFrameWorkTp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Container _container;
    private bool _disposed = false;

    public UnitOfWork(AppDbContext context, Container container)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _container = container ?? throw new ArgumentNullException(nameof(container));
    }

    ///////////////////
    public IRepository<Person> Persons => _container.GetInstance<IRepository<Person>>();

    public IReadOnlyRepository<Person> PersonsReadOnly =>
        _container.GetInstance<IReadOnlyRepository<Person>>();

    /////////////////////
    public IRepository<Student> Students => _container.GetInstance<IRepository<Student>>();

    public IReadOnlyRepository<Student> StudentsReadOnly => _container.GetInstance<IReadOnlyRepository<Student>>();

    ///////////////////
    public IRepository<Teacher> Teachers => _container.GetInstance<IRepository<Teacher>>();

    public IReadOnlyRepository<Teacher> TeachersReadOnly => _container.GetInstance<IReadOnlyRepository<Teacher>>();

    ///////////////////
    public IRepository<Class> Classes => _container.GetInstance<IRepository<Class>>();

    public IReadOnlyRepository<Class> ClassesReadOnly => _container.GetInstance<IReadOnlyRepository<Class>>();

    ///////////////////
    public IRepository<Subject> Subjects => _container.GetInstance<IRepository<Subject>>();

    public IReadOnlyRepository<Subject> SubjectsReadOnly => _container.GetInstance<IReadOnlyRepository<Subject>>();

    ///////////////////
    public IRepository<Enrollment> Enrollments => _container.GetInstance<IRepository<Enrollment>>();

    public IReadOnlyRepository<Enrollment> EnrollmentsReadOnly =>
        _container.GetInstance<IReadOnlyRepository<Enrollment>>();

    /// <inheritdoc/>
    public async Task<bool> CompleteAsync()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            // Log the exception or handle it as needed
            throw new UnitOfWorkException("An error occurred while saving changes", ex);
        }
    }
    /// <summary>
    /// Disposes the unit of work and underlying context
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
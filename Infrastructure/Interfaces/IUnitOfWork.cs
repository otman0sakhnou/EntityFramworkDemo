using EntityFrameWorkTp.Models;

namespace EntityFrameWorkTp.Infrastructure.Interfaces;

public interface IUnitOfWork :IDisposable
{
    IRepository<Person> Persons { get; }
    IReadOnlyRepository<Person> PersonsReadOnly { get; }
    IRepository<Student> Students { get; }
    IReadOnlyRepository<Student> StudentsReadOnly { get; }
    IRepository<Teacher> Teachers { get; }
    IReadOnlyRepository<Teacher> TeachersReadOnly { get; }
    IRepository<Class> Classes { get; }
    IReadOnlyRepository<Class> ClassesReadOnly { get; }
    IRepository<Subject> Subjects { get; }
    IReadOnlyRepository<Subject> SubjectsReadOnly { get; }
    IRepository<Enrollment> Enrollments { get; }
    IReadOnlyRepository<Enrollment> EnrollmentsReadOnly { get; }
    Task<bool> CompleteAsync();
    
}
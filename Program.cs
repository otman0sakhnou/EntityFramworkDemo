using System.Diagnostics;
using EntityFrameWorkTp;
using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using EntityFrameWorkTp.Models;
using EntityFrameWorkTp.Services;
using Microsoft.CSharp.RuntimeBinder;
using SimpleInjector;
using SimpleInjector.Lifestyles;

// Configure the DI container
var container = DependencyConfig.Configure();

// Initialize database with test data
using (AsyncScopedLifestyle.BeginScope(container))
{
    var context = container.GetInstance<AppDbContext>();
    
    // Check if database already has data
    if (context.Persons.Any() || 
        context.Students.Any() || 
        context.Teachers.Any())
    {
        Console.WriteLine("Database already contains data - skipping seeding");
    }
    else
    {
        Console.WriteLine("Starting database seeding...");
        var stopwatch = Stopwatch.StartNew();
        try
        {
            DataSeeder.Initialize(context);
            Console.WriteLine($"Database seeded successfully in {stopwatch.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Initialization error: {ex.Message}");
        }
    }

    // Example usage of UnitOfWork
    var unitOfWork = container.GetInstance<IUnitOfWork>();
    try
    {
        var students = await unitOfWork.StudentsReadOnly.GetAllAsync();
        Console.WriteLine($"Found {students.Count()} students");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Operation failed: {ex.Message}");
    }

    // Example usage of DatabaseService (view and stored procedure)
    var databaseService = container.GetInstance<IDatabaseService>();
    
    // 1. Using the view 
    var teachers = await databaseService.GetTeacherSubjectsDynamicAsync();
    if (teachers.Any())
    {
        Console.WriteLine("Teachers with their subjects:");
        foreach (var teacher in teachers)
        {
            try 
            {
                Console.WriteLine($"{teacher.TeacherFullName} teaches {teacher.SubjectName}");
            }
            catch (RuntimeBinderException ex)
            {
                Console.WriteLine($"Invalid teacher data structure: {ex.Message}");
            }
        }
    }
    else
    {
        Console.WriteLine("No teacher-subject data available");
    }

    // 2. Using the stored procedure 
    try
    {
        var student = await databaseService.GetStudentByNumberAsync("STU001");
        if (student != null)
        {
            try
            {
                Console.WriteLine($"Found student: {student.FullName}");
                Console.WriteLine($"Student number: {student.StudentNumber}");
            }
            catch (RuntimeBinderException ex)
            {
                Console.WriteLine($"Invalid student data structure: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Student not found");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Student lookup failed: {ex.Message}");
    }
}
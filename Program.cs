

// Initialisation de la base de données avec des données de test

using System.Diagnostics;
using EntityFrameWorkTp;
using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using EntityFrameWorkTp.Models;
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
        // Remove the return here to continue execution 
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

// Example usage
    using (AsyncScopedLifestyle.BeginScope(container))
    {
        var unitOfWork = container.GetInstance<IUnitOfWork>();

        try
        {
            //repositories operations
            var students = await unitOfWork.StudentsReadOnly.GetAllAsync();
            Console.WriteLine($"Found {students.Count()} students");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Operation failed: {ex.Message}");
        }
    }
}
using EntityFrameWorkTp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameWorkTp.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Person> Persons { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your connection string here
        optionsBuilder.UseSqlServer(@"Server=RABH7NTPN222\;Database=EFCoreDemo;Integrated Security=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration qui n'est pas possible avec Data Annotations
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.ClassId })
            .IsUnique();
    
        // Configuration de relations complexes
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Classes)
            .WithOne(c => c.Teacher)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
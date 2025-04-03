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
    public DbSet<TeacherSubjectView> TeacherSubjects { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your connection string here
        optionsBuilder.UseSqlServer(@"Server=RABH7NTPN222\;Database=EFCoreDemo;Integrated Security=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.ClassId })
            .IsUnique();
        
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Classes)
            .WithOne(c => c.Teacher)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<TeacherSubjectView>().HasNoKey().ToView("V_Teacher_Subject");
    }
}
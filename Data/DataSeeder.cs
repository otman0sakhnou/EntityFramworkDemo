using System.Diagnostics;
using EntityFrameWorkTp.Models;
using Microsoft.Extensions.Logging;

namespace EntityFrameWorkTp.Data;

public static  class DataSeeder
{
      public static void Initialize(AppDbContext context,ILogger logger = null)
        {
            context.Database.EnsureCreated();
            
            // 1. Add Subjects
            var subjects = new Subject[]
            {
                new Subject { Name = "Mathématiques", Description = "Algèbre et géométrie" },
                new Subject { Name = "Physique", Description = "Mécanique et thermodynamique" },
                new Subject { Name = "Français", Description = "Littérature et grammaire" },
                new Subject { Name = "Histoire", Description = "Histoire mondiale" }
            };
            context.Subjects.AddRange(subjects);
            context.SaveChanges();
            logger?.LogDebug("Added {Count} subjects", subjects.Length);

            //  Add Persons
            var persons = new Person[]
            {
                new Person { FirstName = "Jean", LastName = "Dupont" },
                new Person { FirstName = "Marie", LastName = "Martin" },
                new Person { FirstName = "Pierre", LastName = "Durand" },
                new Person { FirstName = "Sophie", LastName = "Leroy" },
                new Person { FirstName = "Thomas", LastName = "Petit" },
                new Person { FirstName = "Laura", LastName = "Moreau" }
            };
            context.Persons.AddRange(persons);
            context.SaveChanges();
            logger?.LogDebug("Added {Count} Persons", persons.Length);

            //  Add teachers
            var teachers = new Teacher[]
            {
                new Teacher { 
                    Personal = persons[0], 
                    HireDate = new DateTime(2015, 9, 1), 
                    Subject = subjects[0] 
                },
                new Teacher { 
                    Personal = persons[1], 
                    HireDate = new DateTime(2018, 9, 1), 
                    Subject = subjects[1] 
                },
                new Teacher { 
                    Personal = persons[2], 
                    HireDate = new DateTime(2020, 9, 1), 
                    Subject = subjects[2] 
                }
            };
            context.Teachers.AddRange(teachers);
            context.SaveChanges();

            // 4. Add classes
            var classes = new Class[]
            {
                new Class { 
                    Name = "Math101", 
                    Level = "Débutant", 
                    Teacher = teachers[0] 
                },
                new Class { 
                    Name = "Phys202", 
                    Level = "Avancé", 
                    Teacher = teachers[1] 
                },
                new Class { 
                    Name = "Franc301", 
                    Level = "Intermédiaire", 
                    Teacher = teachers[2] 
                }
            };
            context.Classes.AddRange(classes);
            context.SaveChanges();
            logger?.LogDebug("Added {Count} classes", classes.Length);

            // 5. Add students
            var students = new Student[]
            {
                new Student { 
                    StudentNumber = "STU001", 
                    Personal = persons[3] 
                },
                new Student { 
                    StudentNumber = "STU002", 
                    Personal = persons[4] 
                },
                new Student { 
                    StudentNumber = "STU003", 
                    Personal = persons[5] 
                }
            };
            context.Students.AddRange(students);
            context.SaveChanges();
            logger?.LogDebug("Added {Count} students", students.Length);

            // 6. Add Enrollment
            var enrollments = new Enrollment[]
            {
                new Enrollment { 
                    Student = students[0], 
                    Class = classes[0], 
                    EnrollmentDate = new DateTime(2023, 9, 1) 
                },
                new Enrollment { 
                    Student = students[0], 
                    Class = classes[1], 
                    EnrollmentDate = new DateTime(2023, 9, 1) 
                },
                new Enrollment { 
                    Student = students[1], 
                    Class = classes[0], 
                    EnrollmentDate = new DateTime(2023, 9, 1) 
                },
                new Enrollment { 
                    Student = students[2], 
                    Class = classes[2], 
                    EnrollmentDate = new DateTime(2023, 9, 1) 
                }
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
            logger?.LogDebug("Added {Count} enrollements", enrollments.Length);
        }

        public static void ClearAllData(AppDbContext context)
        {
            context.Enrollments.RemoveRange(context.Enrollments);
            context.Students.RemoveRange(context.Students);
            context.Classes.RemoveRange(context.Classes);
            context.Teachers.RemoveRange(context.Teachers);
            context.Subjects.RemoveRange(context.Subjects);
            context.Persons.RemoveRange(context.Persons);
            context.SaveChanges();
        }
}
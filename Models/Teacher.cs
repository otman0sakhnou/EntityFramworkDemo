using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameWorkTp.utils;

namespace EntityFrameWorkTp.Models;

[Table("Teacher")]
public class Teacher
{
    [Required(ErrorMessage = "La date d'embauche est obligatoire")]
    // Add a custom data annotation 
    [FutureDate(ErrorMessage = "La date d'embauche ne peut pas être dans le futur")]
    public DateTime HireDate { get; set; }
    [Key,ForeignKey("Personal")]
    public Guid PersonId { get; set; }
    [Required]
    public Person? Personal { get; set; }
    [ForeignKey("SubjectId")]
    public Guid SubjectId { get; set; }
    [Required]
    public Subject? Subject { get; set; }
    public List<Class>? Classes { get; set; } = new();
}
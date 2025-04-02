using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWorkTp.Models;
[Table("Student")]
public class Student
{
    [Key]
    [StringLength(20)]
    public string? StudentNumber { get; set; }
    [ForeignKey("Personal")]
    public Guid PersonId { get; set; }
    [Required]
    public Person? Personal { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}
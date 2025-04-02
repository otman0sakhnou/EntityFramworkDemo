using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWorkTp.Models;
[Table("Enrollment")]
public class Enrollment
{
    [Key]
    public Guid Id { get; set; }
        
    [ForeignKey("Student")]
    [StringLength(20)]
    public string? StudentId { get; set; }
        
    [Required]
    public Student? Student { get; set; }
        
    [ForeignKey("Class")]
    public Guid ClassId { get; set; }
        
    [Required]
    public Class? Class { get; set; }
        
    [Required]
    public DateTime EnrollmentDate { get; set; }
}

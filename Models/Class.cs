using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWorkTp.Models;

[Table("Class")]
public class Class
{
    [Key]
    public Guid Id { get; set; }
        
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
        
    [Required]
    [StringLength(20)]
    public string? Level { get; set; }
        
    [ForeignKey("Teacher")]
    public Guid TeacherId { get; set; }
        
    [Required]
    public Teacher? Teacher { get; set; }
        
    public List<Enrollment> Enrollments { get; set; } = new();
}
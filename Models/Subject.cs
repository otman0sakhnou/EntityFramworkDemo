using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWorkTp.Models;

[Table("Subject")]
public class Subject
{
    [Key]
    public Guid Id { get; set; }
        
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
        
    [StringLength(500)]
    public string? Description { get; set; }
        
    public List<Teacher> Teachers { get; set; } = new();
}
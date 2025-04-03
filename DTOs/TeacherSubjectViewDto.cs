namespace EntityFrameWorkTp.DTOs;

public class TeacherSubjectViewDto
{
    public Guid TeacherId { get; set; }
    public string TeacherFullName { get; set; }
    public string SubjectName { get; set; }
    public string SubjectDescription { get; set; }
    public DateTime HireDate { get; set; }
}

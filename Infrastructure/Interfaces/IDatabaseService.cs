using EntityFrameWorkTp.DTOs;
using EntityFrameWorkTp.Models;

namespace EntityFrameWorkTp.Infrastructure.Interfaces;

public interface IDatabaseService
{
    Task<List<TeacherSubjectView>> GetTeacherSubjectsDynamicAsync();
    Task<StudentDetailsDto> GetStudentByNumberAsync(string studentNumber);
}
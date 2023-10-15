using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI.Models.DTOs.Professor;

public class GetProfessorDTO
{
    public int ProfessorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public List<CourseInfoDTO> Courses { get; set; } = new();
}

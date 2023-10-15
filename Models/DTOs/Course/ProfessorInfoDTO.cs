namespace StudentPortalAPI.Models.DTOs.Course;

public class ProfessorInfoDTO
{
    public int ProfessorId { get; set; } 
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int UserId { get; set; }
}

using StudentPortalAPI.Models;

namespace PortalNotas.Models.DTOs.Course
{
    public class GetCourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public Professor Professor { get; set; }
        public List<GetStudentFromCourseDTO>? Students { get; set; }
    }
}

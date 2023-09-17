using StudentPortalAPI.Models;

namespace PortalNotas.Models.DTOs.Student
{
    public class GetCourseFromStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semestre { get; set; } = new();
        public Professor? Professor { get; set; }
    }
}

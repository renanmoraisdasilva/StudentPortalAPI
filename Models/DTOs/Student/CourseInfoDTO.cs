using StudentPortalAPI.Models;

namespace StudentPortalAPI.Models.DTOs.Student
{
    public class CourseinfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semestre { get; set; } = new();
        public Professor? Professor { get; set; }
    }
}

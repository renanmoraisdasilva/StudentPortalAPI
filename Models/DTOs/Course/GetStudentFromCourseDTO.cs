namespace PortalNotas.Models.DTOs.Course
{
    public class GetStudentFromCourseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

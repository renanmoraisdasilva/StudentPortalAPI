using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Course
{
    public class UpdateCourseDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string CourseName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "O campo Semestre é obrigatório.")]
        public string Semester { get; set; } = string.Empty;
    }
}

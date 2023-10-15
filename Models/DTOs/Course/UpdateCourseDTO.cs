using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models.DTOs.Course
{
    public class UpdateCourseDTO
    {
        [Required(ErrorMessage = "CourseName is required.")]
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is required.")]
        public string Semester { get; set; } = string.Empty;
    }
}

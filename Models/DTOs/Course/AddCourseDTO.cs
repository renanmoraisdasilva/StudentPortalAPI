using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models.DTOs.Course
{
    public class AddCourseDTO
    {
        [Required(ErrorMessage = "Course name is required.")]
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public int? ProfessorId { get; set; }
    }
}

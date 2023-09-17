using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Student
{
    public class UpdateStudentDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
    }
}

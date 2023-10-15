using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models.DTOs.Professor;

public class UpdateProfessorDTO
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "ProfessorId is required.")]
    public int ProfessorId { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }
}

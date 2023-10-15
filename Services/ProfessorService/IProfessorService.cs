using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Professor;

namespace ProfessorPortalAPI.Services.ProfessorService;

public interface IProfessorService
{
    Task<ServiceResponse<List<GetProfessorDTO>>> GetAllProfessors();
    Task<ServiceResponse<GetProfessorDTO>> GetProfessorByUsername(string username);
    Task AddProfessor(AddProfessorDTO newProfessor);
    Task AssignProfessorToCourse(AssignProfessorDTO assignCourseProfessor);
    Task UnassignProfessorToCourse(AssignProfessorDTO unassignCourseProfessor);
    Task UpdateProfessor(UpdateProfessorDTO aluno);
}

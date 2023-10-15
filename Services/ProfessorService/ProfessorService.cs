using ProfessorPortalAPI.Services.ProfessorService;
using ProfessorPortalAPI.Services.ProfessorService.Commands;
using ProfessorPortalAPI.Services.ProfessorService.Queries;
using StudentPortalAPI.Data;
using StudentPortalAPI.Models.DTOs.Professor;

namespace StudentPortalAPI.Services.ProfessorService;

public class ProfessorService : IProfessorService
{
    private readonly ProfessorCommandService _commandService;
    private readonly ProfessorQueryService _queryService;

    public ProfessorService(IMapper mapper, IDbContextFactory<DataContext> context)
    {
        _commandService = new ProfessorCommandService(mapper, context);
        _queryService = new ProfessorQueryService(mapper, context);
    }
    public async Task AddProfessor(AddProfessorDTO newProfessor)
    {
        await _commandService.AddProfessor(newProfessor);
    }

    public async Task AssignProfessorToCourse(AssignProfessorDTO assignCourseProfessor)
    {
        await _commandService.AssignProfessorToCourse(assignCourseProfessor);
    }

    public async Task<Models.ServiceResponse<List<GetProfessorDTO>>> GetAllProfessors()
    {
        return await _queryService.GetAllProfessors();
    }

    public async Task<Models.ServiceResponse<GetProfessorDTO>> GetProfessorByUsername(string username)
    {
        return await _queryService.GetProfessorByUsername(username);
    }

    public async Task UnassignProfessorToCourse(AssignProfessorDTO unassignCourseProfessor)
    {
        await _commandService.UnassignProfessorToCourse(unassignCourseProfessor);
    }

    public async Task UpdateProfessor(UpdateProfessorDTO professor)
    {
        await _commandService.UpdateProfessor(professor);
    }
}

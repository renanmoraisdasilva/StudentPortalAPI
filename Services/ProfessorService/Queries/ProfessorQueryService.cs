

using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Professor;
using StudentPortalAPI.Models.DTOs.Student;

namespace ProfessorPortalAPI.Services.ProfessorService.Queries;

public class ProfessorQueryService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public ProfessorQueryService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetProfessorDTO>>> GetAllProfessors()
    {
        var serviceResponse = new ServiceResponse<List<GetProfessorDTO>>();

        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var professorsDto = await _context.Professors
                .Select(p => new GetProfessorDTO
                {
                    ProfessorId = p.ProfessorId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserId = p.UserId,
                    Username = p.User.Username,
                    Courses = p.Courses.Select(c => _mapper.Map<CourseInfoDTO>(c)).ToList()
                })
                .ToListAsync();

            serviceResponse.Data = professorsDto;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetProfessorDTO>> GetProfessorByUsername(string username)
    {
        var serviceResponse = new ServiceResponse<GetProfessorDTO>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var professor =
                await _context.Professors
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(item => item.User.Username == username)
                ?? throw new KeyNotFoundException("Professor not found.");

            var professorDto = _mapper.Map<GetProfessorDTO>(professor);

            serviceResponse.Data = professorDto;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}

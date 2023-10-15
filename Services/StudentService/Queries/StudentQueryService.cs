using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI.Services.StudentService.Queries;

public class StudentQueryService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public StudentQueryService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetStudentDTO>>> GetAllStudents()
    {
        var serviceResponse = new ServiceResponse<List<GetStudentDTO>>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();
           
            var dbStudents = await _context.Students
                .Include(a => a.CourseEnrollments)
                .Include(a => a.User)
                .ToListAsync();

            var alunosDto = dbStudents.Select(a => _mapper.Map<GetStudentDTO>(a)).ToList();

            serviceResponse.Data = alunosDto;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {          
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetStudentDTO>> GetStudentByUsername(string username)
    {
        var serviceResponse = new ServiceResponse<GetStudentDTO>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var aluno =
                await _context.Students
                    .Include(a => a.CourseEnrollments)
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(item => item.User.Username == username)
                ?? throw new KeyNotFoundException("Student not found.");

            var alunoDto = _mapper.Map<GetStudentDTO>(aluno);

            serviceResponse.Data = alunoDto;
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

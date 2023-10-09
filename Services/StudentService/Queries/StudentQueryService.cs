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
            // Retrieve all Students from the database, including related Courses and Professors
            var dbStudents = await _context.Students
                .Include(a => a.CourseEnrollments)
                .Include(a => a.User)
                .ToListAsync();

            // Map Student entities to GetStudentDTO objects
            var alunosDto = dbStudents.Select(a => _mapper.Map<GetStudentDTO>(a)).ToList();

            // Set the response data and success status
            serviceResponse.Data = alunosDto;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            // Handle any exceptions and set the appropriate error message and success status
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
            // Find the Student in the database by ID, including related Courses and Professors
            var aluno =
                await _context.Students
                    .Include(a => a.CourseEnrollments)
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(item => item.User.Username == username)
                ?? throw new KeyNotFoundException("Student not found.");

            // Map the Student entity to a GetStudentDTO object
            var alunoDto = _mapper.Map<GetStudentDTO>(aluno);

            // Set the response data and success status
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

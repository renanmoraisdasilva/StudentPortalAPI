using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Student;
using StudentPortalAPI.Services.StudentService.Commands;
using StudentPortalAPI.Services.StudentService.Queries;

namespace StudentPortalAPI.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly StudentCommandService _commandService;
    private readonly StudentQueryService _queryService;

    public StudentService(IMapper mapper, IDbContextFactory<DataContext> context)
    {
        _commandService = new StudentCommandService(mapper, context);
        _queryService = new StudentQueryService(mapper, context);
    }

    // Retrieve all Students from the database
    public async Task<ServiceResponse<List<GetStudentDTO>>> GetAllStudents()
    {
        return await _queryService.GetAllStudents();
    }

    // Add a new Student to the database
    public async Task AddStudent(AddStudentDTO newStudent)
    {
        await _commandService.AddStudent(newStudent);
    }

    // Retrieve a specific Student by ID from the database
    public async Task<ServiceResponse<GetStudentDTO>> GetStudentByUsername(string username)
    {
        return await _queryService.GetStudentByUsername(username);
    }

    // Update an existing Student in the database
    public async Task UpdateStudent(UpdateStudentDTO updatedStudent)
    {
        await _commandService.UpdateStudent(updatedStudent);
    }

    // Link an Student to a Course
    public async Task LinkStudentToCourse(EnrollStudentDTO newLinkCourseStudent)
    {
        await _commandService.LinkStudentToCourse(newLinkCourseStudent);
    }

    // Unlink an Student from a Course
    public async Task UnlinkStudentToCourse(EnrollStudentDTO UnlinkCourseStudent)
    {
        await _commandService.UnlinkStudentToCourse(UnlinkCourseStudent);
    }
}

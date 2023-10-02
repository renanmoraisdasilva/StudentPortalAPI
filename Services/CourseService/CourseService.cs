using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;
using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI.Services.CourseService;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CourseService(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    // Add a new Course
    public async Task<ServiceResponse<GetCourseFromStudentDTO>> AddCourse(AddCourseDTO newCourse)
    {
        var serviceResponse = new ServiceResponse<GetCourseFromStudentDTO>();

        if (newCourse is null)
            throw new ArgumentNullException(nameof(newCourse));

        if (newCourse.ProfessorId is not null)
        {
            // Check if Professor exists in the database
            _ =
                await _context.Professors.FindAsync(newCourse.ProfessorId)
                ?? throw new KeyNotFoundException("Professor não encontrado.");
        }

        var materia = Course.Create(newCourse);

        _context.Courses.Add(materia);
        await _context.SaveChangesAsync();

        // Map the added Course entity to GetCourseFromStudentDTO
        serviceResponse.Success = true;
        serviceResponse.Data = _mapper.Map<GetCourseFromStudentDTO>(materia);
        return serviceResponse;
    }

    // Get all Courses
    public async Task<ServiceResponse<List<GetCourseFromStudentDTO>>> GetAllCourses()
    {
        var serviceResponse = new ServiceResponse<List<GetCourseFromStudentDTO>>();

        try
        {
            // Retrieve all Courses from the database, including related Professor and Student entities
            var dbCourses = await _context.Courses
                .Include(materia => materia.Professor)
                .ToListAsync();

            // Map Courses to GetCourseFromStudentDTO objects
            var materias = dbCourses
                .Select(ma => _mapper.Map<GetCourseFromStudentDTO>(ma))
                .ToList();

            // Set the response data and success status
            serviceResponse.Data = materias;
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

    // Get a Course by ID
    public async Task<ServiceResponse<GetCourseFromStudentDTO>> GetCourseById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCourseFromStudentDTO>();

        try
        {
            // Retrieve the Course from the database by ID, including related Professor
            var materia =
                await _context.Courses
                    .Include(m => m.Professor)
                    .FirstOrDefaultAsync(item => item.Id == id)
                ?? throw new KeyNotFoundException("Course não encontrada");

            // Map the Course entity to GetCourseFromStudentDTO
            serviceResponse.Data = _mapper.Map<GetCourseFromStudentDTO>(materia);
        }
        catch (Exception ex)
        {
            // Handle any exceptions and set the appropriate error message and success status
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    // Update a Course
    public async Task<ServiceResponse<GetCourseFromStudentDTO>> UpdateCourse(
        UpdateCourseDTO updatedCourse,
        int id
    )
    {
        var serviceResponse = new ServiceResponse<GetCourseFromStudentDTO>();

        // Retrieve the Course from the database by ID
        var materia =
            await _context.Courses.SingleOrDefaultAsync(item => item.Id == id)
            ?? throw new Exception("Course não encontrada.");

        // Update the Course entity with the new values from the updated Course DTO
        _mapper.Map(updatedCourse, materia);

        await _context.SaveChangesAsync();

        // Map the updated Course entity to GetCourseFromStudentDTO
        serviceResponse.Data = _mapper.Map<GetCourseFromStudentDTO>(materia);

        return serviceResponse;
    }

    // Delete a Course
    public async Task<ServiceResponse<bool>> DeleteCourse(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            // Retrieve the Course from the database by ID
            var dbCourse =
                await _context.Courses.SingleOrDefaultAsync(item => item.Id == id)
                ?? throw new Exception("Course não encontrada");

            // Remove the Course from the database
            _context.Courses.Remove(dbCourse);
            await _context.SaveChangesAsync();

            // Set the response data and success status
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
}

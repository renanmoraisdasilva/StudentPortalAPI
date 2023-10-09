using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;

namespace StudentPortalAPI.Services.CourseService.Queries;

public class CourseQueryService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public CourseQueryService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _mapper = mapper;
        _contextFactory = contextFactory;
    }
    // Retrieve all Courses from the database, including related Professor and Student entities
    public async Task<ServiceResponse<List<GetCourseDTO>>> GetAllCourses()
    {
        var serviceResponse = new ServiceResponse<List<GetCourseDTO>>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var dbCourses = await _context.Courses
                .Include(c => c.CourseEnrollments)
                .ThenInclude(ce => ce.Student)
                .Include(c => c.Professor)
                .ToListAsync();

            var courses = dbCourses.Select(course => new GetCourseDTO
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Professor = course.Professor != null ? new ProfessorInfoDTO
                {
                    FirstName = course.Professor.FirstName,
                    LastName = course.Professor.LastName,
                    ProfessorId = course.Professor.ProfessorId,
                    UserId = course.Professor.UserId
                } : null,
                Semester = course.Semester,
                Students = course.CourseEnrollments.Select(ce => new StudentInfoDTO
                {
                    Id = ce.Student.StudentId,
                    FirstName = ce.Student.FirstName,
                    LastName = ce.Student.LastName
                })
                .ToList()
            }).ToList();

            serviceResponse.Data = courses;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
    // Retrieve a specific Course by ID from the database, including related Professor and Student entities
    public async Task<ServiceResponse<GetCourseDTO>> GetCourseById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCourseDTO>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbCourse = await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.CourseEnrollments)
                .ThenInclude(ce => ce.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbCourse == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Course not found.";
                return serviceResponse;
            }

            var course = new GetCourseDTO
            {
                Id = dbCourse.Id,
                CourseName = dbCourse.CourseName,
                Professor = dbCourse.Professor != null ? new ProfessorInfoDTO
                {
                    FirstName = dbCourse.Professor.FirstName,
                    LastName = dbCourse.Professor.LastName,
                    ProfessorId = dbCourse.Professor.ProfessorId,
                    UserId = dbCourse.Professor.UserId
                } : null,
                Semester = dbCourse.Semester,
                Students = dbCourse.CourseEnrollments.Select(ce => new StudentInfoDTO
                {
                    Id = ce.Student.StudentId,
                    FirstName = ce.Student.FirstName,
                    LastName = ce.Student.LastName
                })
            .ToList()
            };

            serviceResponse.Data = course;
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
    // Retrieve all Courses from the database, including related Professor and Student entities
    public async Task<ServiceResponse<List<GetCourseDTO>>> GetCoursesByProfessorId(int professorId)
    {
        var serviceResponse = new ServiceResponse<List<GetCourseDTO>>();
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbCourses = await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.CourseEnrollments)
                .ThenInclude(ce => ce.Student)
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();

            var courses = dbCourses.Select(course => new GetCourseDTO
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Professor = course.Professor != null ? new ProfessorInfoDTO
                {
                    FirstName = course.Professor.FirstName,
                    LastName = course.Professor.LastName,
                    ProfessorId = course.Professor.ProfessorId,
                    UserId = course.Professor.UserId
                } : null,
                Semester = course.Semester,
                Students = course.CourseEnrollments.Select(ce => new StudentInfoDTO
                {
                    Id = ce.Student.StudentId,
                    FirstName = ce.Student.FirstName,
                    LastName = ce.Student.LastName
                })
                .ToList()
            }).ToList();

            serviceResponse.Data = courses;
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


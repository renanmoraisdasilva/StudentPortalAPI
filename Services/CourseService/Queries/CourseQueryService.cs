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
                Professor = _mapper.Map<ProfessorInfoDTO>(course.Professor),
                Semester = course.Semester,
                Students = course.CourseEnrollments.Select(ce => _mapper.Map<StudentInfoDTO>(ce.Student)).ToList(),
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
                Professor = _mapper.Map<ProfessorInfoDTO>(dbCourse.Professor),
                Semester = dbCourse.Semester,
                Students = dbCourse.CourseEnrollments.Select(ce => _mapper.Map<StudentInfoDTO>(ce.Student)).ToList(),
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
                Professor = _mapper.Map<ProfessorInfoDTO>(course.Professor),
                Semester = course.Semester,
                Students = course.CourseEnrollments.Select(ce => _mapper.Map<StudentInfoDTO>(ce.Student)).ToList(),
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


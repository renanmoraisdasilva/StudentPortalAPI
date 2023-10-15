using StudentPortalAPI.Data;
using StudentPortalAPI.Models.DTOs.Course;

namespace StudentPortalAPI.Services.CourseService.Commands;

public class CourseCommandService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public CourseCommandService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task AddCourse(AddCourseDTO newCourse)
    {
        using var _context = _contextFactory.CreateDbContext();

        if (newCourse is null)
            throw new ArgumentNullException("There are missing fields.");

        if (newCourse.ProfessorId is not null)
        {
            // Check if Professor exists in the database
            _ =
                await _context.Professors.FindAsync(newCourse.ProfessorId)
                ?? throw new KeyNotFoundException("Professor not found.");
        }

        if (await _context.Courses.FirstOrDefaultAsync(Course => Course.CourseName == newCourse.CourseName) is not null)
            throw new ArgumentException("Course name already exists.");

        var Course = Models.Course.Create(newCourse);

        // Add the Course entity to the database
        _context.Courses.Add(Course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourse(UpdateCourseDTO updatedCourse, int id)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbCourse =
                await _context.Courses.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new KeyNotFoundException("Course not found.");

            _mapper.Map(updatedCourse, dbCourse);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteCourse(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == id)
            ?? throw new KeyNotFoundException("Course not found.");
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }
}


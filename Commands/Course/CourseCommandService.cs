using StudentPortalAPI.Data;
using StudentPortalAPI.Models.DTOs.Course;

namespace StudentPortalAPI.Commands.Course;

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

        if (await _context.Courses.FirstOrDefaultAsync(Course => Course.CourseName == newCourse.CourseName) is not null)
            throw new ArgumentException("Username already exists.");

        var course = Models.Course.Create(newCourse);


        // Add the Course entity to the database
        _context.Courses.Add(course);

        await _context.SaveChangesAsync();

    }
}


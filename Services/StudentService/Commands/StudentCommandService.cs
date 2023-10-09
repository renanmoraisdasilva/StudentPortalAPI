namespace StudentPortalAPI.Services.StudentService.Commands;

using Microsoft.EntityFrameworkCore;
using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Student;

public class StudentCommandService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public StudentCommandService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task AddStudent(AddStudentDTO newStudent)
    {
        var serviceResponse = new ServiceResponse<GetStudentDTO>();

        if (newStudent is null)
            throw new ArgumentNullException("There are missing fields.");

        using var _context = _contextFactory.CreateDbContext();

        if (await _context.Users.FirstOrDefaultAsync(Users => Users.Username == newStudent.Username) is not null)
            throw new ArgumentException("Username already exists.");

        var student = Student.Create(newStudent.Username, newStudent.Password, newStudent.Email, newStudent.FirstName, newStudent.LastName);

        // Add the Student entity to the database
        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        // Map the added Student entity back to a GetStudentDTO object
        serviceResponse.Data = _mapper.Map<GetStudentDTO>(student);
        serviceResponse.Success = true;
    }

    public async Task UpdateStudent(UpdateStudentDTO updatedStudent)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbStudent =
                await _context.Students.FirstOrDefaultAsync(item => item.StudentId == updatedStudent.StudentId)
                ?? throw new KeyNotFoundException("Student not found.");

            _mapper.Map(updatedStudent, dbStudent);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task LinkStudentToCourse(EnrollStudentDTO newCourseEnrollment)
    {
        using var _context = _contextFactory.CreateDbContext();

        var student =
            await _context.Students
                .FirstOrDefaultAsync(a => a.StudentId == newCourseEnrollment.StudentId)
            ?? throw new KeyNotFoundException("Student not found.");

        var course =
            await _context.Courses.FirstOrDefaultAsync(m => m.Id == newCourseEnrollment.CourseId)
            ?? throw new KeyNotFoundException("Course not found.");

        var enrollment = await _context.CourseEnrollments.FirstOrDefaultAsync(
            e =>
                e.StudentId == newCourseEnrollment.StudentId
                && e.CourseId == newCourseEnrollment.CourseId
                && (e.EnrollmentDate < DateTime.Now || e.DisenrollmentDate > DateTime.Now)
        );
        if (enrollment is not null)
            throw new ApplicationException("Student is already enrolled in this course.");

        var newEnrollment = CourseEnrollment.Create(newCourseEnrollment.CourseId, newCourseEnrollment.StudentId, DateTime.Now);

        // Transaction Handling
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.CourseEnrollments.AddAsync(newEnrollment);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UnlinkStudentToCourse(EnrollStudentDTO UnlinkCourseEnrollment)
    {
        using var _context = _contextFactory.CreateDbContext();

        var link =
            await _context.CourseEnrollments.FirstOrDefaultAsync(
                ma =>
                    ma.StudentId == UnlinkCourseEnrollment.StudentId
                    && ma.CourseId == UnlinkCourseEnrollment.CourseId
            ) ?? throw new KeyNotFoundException("Link not found.");

        _context.CourseEnrollments.Remove(link);
        await _context.SaveChangesAsync();
    }
}

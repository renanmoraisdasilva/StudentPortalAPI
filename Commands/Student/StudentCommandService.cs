namespace PortalNotas.Commands.Student;

using Microsoft.EntityFrameworkCore;
using PortalNotas.Models;
using StudentPortalAPI.Models;

public class StudentCommandService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public StudentCommandService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetStudentDTO>> AddStudent(AddStudentDTO newStudent)
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

        return serviceResponse;
    }

    public async Task UpdateStudent(UpdateStudentDTO updatedStudent)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbStudents =
                await _context.Students.FirstOrDefaultAsync(item => item.StudentId == updatedStudent.StudentId)
                ?? throw new KeyNotFoundException("Student not found.");

            _mapper.Map(updatedStudent, dbStudents);
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
            throw new ApplicationException("Student is already enrolled.");

        var newEnrollment = CourseEnrollment.Create(newCourseEnrollment.CourseId, newCourseEnrollment.StudentId, DateTime.Now);

        // Transaction Handling
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Add the new CourseEnrollment relationship to the database
            await _context.CourseEnrollments.AddAsync(newEnrollment);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            // Rollback the transaction in case of an error
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UnlinkStudentToCourse(EnrollStudentDTO UnlinkCourseEnrollment)
    {
        using var _context = _contextFactory.CreateDbContext();
        // Find the CourseEnrollment link in the database
        var link =
            await _context.CourseEnrollments.FirstOrDefaultAsync(
                ma =>
                    ma.StudentId == UnlinkCourseEnrollment.StudentId
                    && ma.CourseId == UnlinkCourseEnrollment.CourseId
            ) ?? throw new KeyNotFoundException("Link não encontrado.");

        // Remove the CourseEnrollment link from the database
        _context.CourseEnrollments.Remove(link);
        await _context.SaveChangesAsync();
    }

    //public async Task DeleteStudent(string username)
    //{
    //    try
    //    {
    //        // Find the Student in the database by ID
    //        var dbStudent =
    //            await _context.Students
    //            .Include(a => a.CourseEnrollments)
    //            .Include(a => a.User)
    //            .FirstOrDefaultAsync(item => item.User.Username == username)
    //            ?? throw new KeyNotFoundException("Student não encontrado");

    //        // Remove the Student from the database
    //        _context.Students.Remove(dbStudent);
    //        await _context.SaveChangesAsync();

    //        // Set the response data and success status
    //        serviceResponse.Success = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        serviceResponse.Success = false;
    //        serviceResponse.Message = ex.Message;
    //    }
    //    return serviceResponse;
    //}
}

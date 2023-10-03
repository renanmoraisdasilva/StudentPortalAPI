using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentPortalAPI.Commands.Student;
using StudentPortalAPI.Data;
using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI.Tests.Student;

[TestClass]
public class StudentCommandServiceTests
{
    private readonly IDbContextFactory<DataContext> _contextFactory;
    private readonly IMapper _mapper;

    public StudentCommandServiceTests()
    {
        var services = new ServiceCollection();

        // Using in-memory database for testing
        services.AddDbContextFactory<DataContext>(options => options.UseInMemoryDatabase("TestDatabase"));

        services.AddAutoMapper(typeof(AutoMapperProfile));

        var serviceProvider = services.BuildServiceProvider();

        _contextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();

    }


    [TestMethod]
    public async Task AddStudent_Should_AddStudentToDatabase()
    {
        // Arrange
        using var context = _contextFactory.CreateDbContext();
        var studentCommandService = new StudentCommandService(_mapper, _contextFactory);

        var newStudent = new AddStudentDTO
        {
            Username = "teststudent",
            Password = "password123",
            Email = "teststudent@example.com",
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        await studentCommandService.AddStudent(newStudent);

        // Assert
        using var assertContext = _contextFactory.CreateDbContext();

        // Checking that the new student exists in the database
        var addedStudent = await assertContext.Users.Include(user => user.StudentProfile)
            .FirstOrDefaultAsync(s => s.Username == newStudent.Username);

        Assert.IsNotNull(addedStudent);
        Assert.IsNotNull(addedStudent.StudentProfile);

        // Checking field values
        Assert.AreEqual(newStudent.Username, addedStudent.Username);
        Assert.AreEqual(newStudent.Password, addedStudent.Password);
        Assert.AreEqual(newStudent.Email, addedStudent.Email);
        Assert.AreEqual(newStudent.FirstName, addedStudent.StudentProfile.FirstName);
        Assert.AreEqual(newStudent.LastName, addedStudent.StudentProfile.LastName);
    }

    [TestMethod]
    public async Task UpdateStudent_Should_UpdateStudentInDatabase()
    {
        // Arrange
        using var context = _contextFactory.CreateDbContext();
        var studentCommandService = new StudentCommandService(_mapper, _contextFactory);

        await CreateStudentAsync(studentCommandService, "testStudentUpdate");
        var newStudent = await context.Students.Include(student => student.User).FirstOrDefaultAsync(s => s.User.Username == "testStudentUpdate") ?? throw new KeyNotFoundException("newStudent not found.");

        // New Student information
        var toUpdateStudent = new UpdateStudentDTO
        {
            FirstName = "Test-updated",
            StudentId = newStudent.StudentId,
            LastName = "User-updated"
        };

        // Act
        await studentCommandService.UpdateStudent(toUpdateStudent);

        // Assert
        using var assertContext = _contextFactory.CreateDbContext();
        var updatedStudent = await assertContext.Users.Include(user => user.StudentProfile).FirstOrDefaultAsync(s => s.Username == "testStudentUpdate");
        Assert.IsNotNull(updatedStudent);
        Assert.IsNotNull(updatedStudent.StudentProfile);

        // Check field values
        Assert.AreEqual(toUpdateStudent.FirstName, updatedStudent.StudentProfile.FirstName);
        Assert.AreEqual(toUpdateStudent.LastName, updatedStudent.StudentProfile.LastName);
    }

    private async Task CreateStudentAsync(StudentCommandService studentCommandService, string username)
    {
        var newStudent = new AddStudentDTO
        {
            Username = username,
            Password = "password123",
            Email = $"{username}@example.com",
            FirstName = "Test",
            LastName = "User"
        };

        await studentCommandService.AddStudent(newStudent);
    }

}



using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalNotas;
using PortalNotas.Commands.Student;

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
            Username = "testuser",
            Password = "password123",
            Email = "testuser@example.com",
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        Task result = studentCommandService.AddStudent(newStudent);
        result.Wait();

        // Assert
        Assert.IsTrue(result.IsCompletedSuccessfully); // Checking that the operation was successful

        // Checking that the new student exists in the database
        var addedStudent = await context.Users.Include(user => user.StudentProfile).FirstOrDefaultAsync(s => s.Username == newStudent.Username);
        Assert.IsNotNull(addedStudent);
        Assert.IsNotNull(addedStudent.StudentProfile);

        // Check field values
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

        // New Student information
        var toUpdateStudent = new UpdateStudentDTO
        {
            FirstName = "Test-updated",
            StudentId = 1,
            LastName = "User-updated"
        };

        // Act
        await studentCommandService.UpdateStudent(toUpdateStudent);

        var updatedStudent = await context.Users.Include(user => user.StudentProfile).FirstOrDefaultAsync(s => s.Username == "testuser");
        Assert.IsNotNull(updatedStudent);
        Assert.IsNotNull(updatedStudent.StudentProfile);

        // Check field values
        Assert.AreEqual(toUpdateStudent.FirstName, updatedStudent.StudentProfile.FirstName);
        Assert.AreEqual(toUpdateStudent.LastName, updatedStudent.StudentProfile.LastName);
    }

}



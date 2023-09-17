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
        var result = await studentCommandService.AddStudent(newStudent);

        // Assert
        Assert.IsTrue(result.Success); // Checking that the operation was successful
        Assert.IsNotNull(result.Data); // Checking that data was returned

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
}



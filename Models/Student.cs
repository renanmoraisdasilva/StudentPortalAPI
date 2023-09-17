using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models;

public class Student
{
    public int StudentId { get; private set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
    public string FirstName { get; private set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
    public string LastName { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; }
    public ICollection<CourseEnrollment> CourseEnrollments { get; private set; }

    private Student() { }

    public static Student Create(string username, string password, string email, string firstName, string lastName)
    {
        var student = new Student
        {
            User = User.Create(username, password, email, UserRole.Student),
            FirstName = firstName,
            LastName = lastName
        };
        return student;
    }
}


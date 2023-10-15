using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models;

public class Professor
{
    public int ProfessorId { get; private set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
    public string FirstName { get; private set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
    public string LastName { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; }

    public List<Course> Courses { get; private set; } = new List<Course>();

    private Professor() { }

    public static Professor Create(string username, string password, string email, string firstName, string lastName)
    {
        var professor = new Professor
        {
            User = User.Create(username, password, email, UserRole.Teacher),
            FirstName = firstName,
            LastName = lastName
        };
        return professor;
    }

    public void AddCourse(Course course)
    {
        // Check if the professor is not already associated with the course
        //if (!Courses.Contains(course))
        //{
        //    Courses.Add(course);
        //    course.AddProfessor(this); // Link the course to the professor
        //}
    }

    public void RemoveCourse(Course course)
    {
        // Check if the professor is associated with the course
        //if (Courses.Contains(course))
        //{
        //    Courses.Remove(course);
        //    course.RemoveProfessor(this); // Unlink the course from the professor
        //}
    }

}


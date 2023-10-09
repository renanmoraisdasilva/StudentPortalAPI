using StudentPortalAPI.Models.DTOs.Course;

namespace StudentPortalAPI.Models;

public class Course
{
    private Course(string CourseName, int? ProfessorId, string Semester)
    {
        this.CourseName = CourseName;
        this.ProfessorId = ProfessorId;
        this.Semester = Semester;
    }

    private Course(string CourseName, string Semester)
    {
        this.CourseName = CourseName;
        this.Semester = Semester;
    }

    public int Id { get; private set; }
    public string CourseName { get; private set; } = string.Empty;
    public int? ProfessorId { get; private set; }
    public Professor? Professor { get; set; }
    public string Semester { get; private set; }
    public ICollection<CourseEnrollment> CourseEnrollments { get; private set; } = new List<CourseEnrollment>();

    public static Course Create(AddCourseDTO newCourse)
    {
        if (newCourse.ProfessorId.HasValue)
        {
            return new Course(newCourse.CourseName, newCourse.ProfessorId, newCourse.Semester);
        }
        return new Course(newCourse.CourseName, newCourse.Semester);
    }
    public void AddProfessor(Professor professor)
    {
        // Check if the professor is not already associated with the course
        //if (!Professors.Contains(professor))
        //{
        //    Professors.Add(professor);
        //    professor.AddCourse(this); // Link the professor to the course
        //}
    }

    public void RemoveProfessor(Professor professor)
    {
        // Check if the professor is associated with the course
        //if (Professors.Contains(professor))
        //{
        //    Professors.Remove(professor);
        //    professor.RemoveCourse(this); // Unlink the professor from the course
        //}
    }


}

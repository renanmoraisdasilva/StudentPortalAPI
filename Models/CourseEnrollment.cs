using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models;

public class CourseEnrollment
{
    public int CourseId { get; private set; }
    public int StudentId { get; private set; }
    public Course Course { get; private set; }
    public Student Student { get; private set; }

    [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100.")]
    public decimal? Grade { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public DateTime DisenrollmentDate { get; private set; }
    public CourseStatus Status { get; private set; }

    private CourseEnrollment() { }

    public static CourseEnrollment Create(int courseId, int studentId, DateTime enrollmentDate, decimal? grade = null)
    {
        // Add validation logic if needed
        return new CourseEnrollment
        {
            CourseId = courseId,
            StudentId = studentId,
            Grade = grade,
            EnrollmentDate = enrollmentDate,
            Status = CourseStatus.Enrolled // Example: Calculate status based on grade
        };
    }

    public void UpdateGrade(decimal? newGrade)
    {
        // Add validation or business logic if needed
        Grade = newGrade;
        Status = CalculateStatus(newGrade);
    }

    private static CourseStatus CalculateStatus(decimal? grade)
    {
        if (grade.HasValue && grade >= 60)
            return CourseStatus.Passed;
        return CourseStatus.Failed;
    }
}

public enum CourseStatus
{
    Passed,
    Failed,
    Dropped,
    InProgress,
    Enrolled,
    Other
}

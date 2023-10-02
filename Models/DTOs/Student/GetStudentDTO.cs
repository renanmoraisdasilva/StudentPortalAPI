namespace StudentPortalAPI.Models.DTOs.Student
{
    public class GetStudentDTO
    {
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<GetCourseFromStudentDTO> CourseEnrollments { get; set; } = new();
    }
}

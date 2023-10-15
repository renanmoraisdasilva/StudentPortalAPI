namespace StudentPortalAPI.Models.DTOs.Course
{
    public class GetCourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public ProfessorInfoDTO? Professor { get; set; }
        public List<StudentInfoDTO>? Students { get; set; }
    }
}

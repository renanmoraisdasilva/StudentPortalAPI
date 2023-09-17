namespace PortalNotas.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<GetCourseFromStudentDTO>>> GetAllCourses();
        Task<ServiceResponse<GetCourseFromStudentDTO>> GetCourseById(int id);
        Task<ServiceResponse<GetCourseFromStudentDTO>> AddCourse(AddCourseDTO newCourse);
        Task<ServiceResponse<GetCourseFromStudentDTO>> UpdateCourse(
            UpdateCourseDTO course,
            int id
        );
        Task<ServiceResponse<bool>> DeleteCourse(int id);
    }
}

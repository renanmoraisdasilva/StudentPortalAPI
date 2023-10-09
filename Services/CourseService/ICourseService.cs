using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;

namespace StudentPortalAPI.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<GetCourseDTO>>> GetAllCourses();
        Task<ServiceResponse<GetCourseDTO>> GetCourseById(int id);
        Task<ServiceResponse<List<GetCourseDTO>>> GetCoursesByProfessorId(int id);
        Task AddCourse(AddCourseDTO newCourse);
        Task UpdateCourse(UpdateCourseDTO course, int id);
        Task DeleteCourse(int id);
    }
}

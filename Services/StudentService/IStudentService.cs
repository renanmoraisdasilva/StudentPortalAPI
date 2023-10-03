using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<GetStudentDTO>>> GetAllStudents();
        Task<ServiceResponse<GetStudentDTO>> GetStudentByUsername(string username);
        Task AddStudent(AddStudentDTO newStudent);
        Task LinkStudentToCourse(EnrollStudentDTO newLinkCourseStudent);
        Task UnlinkStudentToCourse(EnrollStudentDTO unlinkCourseStudent);
        Task UpdateStudent(UpdateStudentDTO aluno);
    }
}

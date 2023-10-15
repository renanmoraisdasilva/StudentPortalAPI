using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;
using StudentPortalAPI.Models.DTOs.Professor;
using StudentPortalAPI.Models.DTOs.Student;

namespace StudentPortalAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, GetStudentDTO>()
                .ForMember(dto => dto.Email, opt => opt.MapFrom(student => student.User.Email))
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(student => student.User.Username));
            CreateMap<UpdateStudentDTO, Student>();
            CreateMap<Student, StudentInfoDTO>();

            CreateMap<Course, GetCourseDTO>();
            CreateMap<Course, CourseInfoDTO>();
            CreateMap<AddCourseDTO, Course>();
            CreateMap<UpdateCourseDTO, Course>();

            CreateMap<Professor, ProfessorInfoDTO>();
            CreateMap<UpdateProfessorDTO, Professor>();
            CreateMap<Professor, GetProfessorDTO>()
                .ForMember(dto => dto.Username, opt => opt.MapFrom(professor => professor.User.Username))
                .ForMember(dto => dto.Courses, opt => opt.MapFrom(professor => professor.Courses));

        }
    }
}

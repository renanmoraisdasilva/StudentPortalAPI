using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;
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

            CreateMap<Course, GetCourseDTO>();
            CreateMap<Course, CourseinfoDTO>();
            CreateMap<Professor, ProfessorInfoDTO>();
            CreateMap<AddCourseDTO, Course>();
            CreateMap<UpdateCourseDTO, Course>();
        }
    }
}

using StudentPortalAPI.Models;

namespace PortalNotas
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
            CreateMap<Course, GetCourseFromStudentDTO>();
            CreateMap<AddCourseDTO, Course>();
            CreateMap<UpdateCourseDTO, Course>();
        }
    }
}

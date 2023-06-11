namespace PortalNotas
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Aluno, GetAlunoDTO>();
            CreateMap<AddAlunoDTO, Aluno>();
            CreateMap<UpdateAlunoDTO, Aluno>();
        }
    }
}

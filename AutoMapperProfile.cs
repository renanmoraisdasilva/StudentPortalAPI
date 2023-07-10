namespace PortalNotas
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Aluno, GetAlunoDTO>();
            CreateMap<AddAlunoDTO, Aluno>();
            CreateMap<UpdateAlunoDTO, Aluno>();
            CreateMap<Aluno, GetAlunoFromMateriaDTO>();

            CreateMap<Materia, GetMateriaDTO>();
            CreateMap<Materia, GetMateriaFromAlunoDTO>();
            CreateMap<AddMateriaDTO, Materia>();
            CreateMap<UpdateMateriaDTO, Materia>();
        }
    }
}

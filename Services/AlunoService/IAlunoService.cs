namespace PortalNotas.Services.AlunoService
{
    public interface IAlunoService
    {
        Task<ServiceResponse<List<GetAlunoDTO>>> GetAllAlunos();
        Task<ServiceResponse<GetAlunoDTO>> GetAlunoById(int id);
        Task<ServiceResponse<List<GetAlunoDTO>>> AddAluno(AddAlunoDTO newAluno);
        Task<ServiceResponse<GetAlunoDTO>> UpdateAluno(UpdateAlunoDTO aluno, int id);
        Task<ServiceResponse<bool>> DeleteAluno(int id);


    }
}

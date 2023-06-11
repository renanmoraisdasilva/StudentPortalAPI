namespace PortalNotas.Services.AlunoService
{
    public class AlunoService : IAlunoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AlunoService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetAlunoDTO>>> AddAluno(AddAlunoDTO newAluno)
        {
            var serviceResponse = new ServiceResponse<List<GetAlunoDTO>>();
            _context.Alunos.Add(_mapper.Map<Aluno>(newAluno));
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Alunos.Select(item => _mapper.Map<GetAlunoDTO>(item)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAlunoDTO>>> GetAllAlunos()
        {
            var serviceResponse = new ServiceResponse<List<GetAlunoDTO>>();
            var dbAlunos = await _context.Alunos.ToListAsync();
            serviceResponse.Data = dbAlunos.Select(item => _mapper.Map<GetAlunoDTO>(item)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAlunoDTO>> GetAlunoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAlunoDTO>();
            try
            {
                var dbAlunos = await _context.Alunos.ToListAsync();
                var aluno = dbAlunos.FirstOrDefault(item => item.Id == id) ?? throw new Exception("Aluno não encontrado"); ;
                serviceResponse.Data = _mapper.Map<GetAlunoDTO>(dbAlunos);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAlunoDTO>> UpdateAluno(UpdateAlunoDTO updatedAluno, int id)
        {
            var serviceResponse = new ServiceResponse<GetAlunoDTO>();
            try
            {
                var dbAlunos = await _context.Alunos.FirstOrDefaultAsync(item => item.Id == id) ?? throw new Exception("Aluno não encontrado");
                //aluno.Nome = updatedAluno.Nome;
                //aluno.Email = updatedAluno.Email;
                _mapper.Map(updatedAluno, dbAlunos);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetAlunoDTO>(dbAlunos);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteAluno(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var dbAluno = await _context.Alunos.FirstOrDefaultAsync(item => item.Id == id) ?? throw new Exception("Aluno não encontrado");
                _context.Alunos.Remove(dbAluno);
                await _context.SaveChangesAsync();
                serviceResponse.Data = true;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}

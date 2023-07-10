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

        public async Task<ServiceResponse<List<GetAlunoDTO>>> GetAllAlunos()
        {
            var serviceResponse = new ServiceResponse<List<GetAlunoDTO>>();

            try
            {
                var dbAlunos = await _context.Alunos.Include(a => a.Materias).ThenInclude(m => m.Professor).ToListAsync();
                var alunosDto = dbAlunos.Select(a => _mapper.Map<GetAlunoDTO>(a)).ToList();

                serviceResponse.Data = alunosDto;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetAlunoDTO>> AddAluno(AddAlunoDTO newAluno)
        {
            var serviceResponse = new ServiceResponse<GetAlunoDTO>();

            var aluno = _mapper.Map<Aluno>(newAluno);

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetAlunoDTO>(aluno);
            serviceResponse.Success = true;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAlunoDTO>> GetAlunoById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAlunoDTO>();
            try
            {
                var aluno = await _context.Alunos.Include(a => a.Materias).ThenInclude(m => m.Professor).FirstOrDefaultAsync(item => item.Id == id) ?? throw new KeyNotFoundException("Aluno não encontrado");
                var alunoDto = _mapper.Map<GetAlunoDTO>(aluno);
                serviceResponse.Data = alunoDto;
                serviceResponse.Success = true;
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
                _mapper.Map(updatedAluno, dbAlunos);
                await _context.SaveChangesAsync();
                var aluno = await _context.Alunos.Include(a => a.Materias).FirstOrDefaultAsync(item => item.Id == id);
                serviceResponse.Data = _mapper.Map<GetAlunoDTO>(aluno);
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

        public async Task LinkAlunoToMateria(LinkMateriaAlunoDTO newLinkMateriaAluno)
        {
            // Retrieve the Aluno
            var aluno = await _context.Alunos
                .Include(a => a.Materias)
                .FirstOrDefaultAsync(a => a.Id == newLinkMateriaAluno.AlunoId);
            if (aluno is null)
                throw new KeyNotFoundException("Aluno não encontrado.");

            // Retrieve the Matéria
            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.Id == newLinkMateriaAluno.MateriaId);
            if (materia is null)
                throw new KeyNotFoundException("Matéria não encontrada.");

            // Check if the Link already exists
            var link = await _context.MateriaAluno.FirstOrDefaultAsync(ma => ma.AlunoId == newLinkMateriaAluno.AlunoId && ma.MateriaId == newLinkMateriaAluno.MateriaId);
            if (link is not null)
                throw new ApplicationException("Link já existe.");

            // Create new MateriaAluno
            var materiaAluno = new MateriaAluno
            {
                AlunoId = aluno.Id,
                MateriaId = materia.Id
            };

            // Transaction Handling
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.MateriaAluno.AddAsync(materiaAluno);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to preserve the original stack trace
            }

            //await _context.MateriaAluno.AddAsync(materiaAluno);
            //await _context.SaveChangesAsync();
        }

        public async Task UnlinkAlunoToMateria(LinkMateriaAlunoDTO UnlinkMateriaAluno)
        {
            var link = await _context.MateriaAluno.FirstOrDefaultAsync(ma => ma.AlunoId == UnlinkMateriaAluno.AlunoId && ma.MateriaId == UnlinkMateriaAluno.MateriaId);

            if (link is null) throw new KeyNotFoundException("Link não encontrado.");

            _context.MateriaAluno.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}


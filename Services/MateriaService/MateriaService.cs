namespace PortalNotas.Services.MateriaService
{
    public class MateriaService : IMateriaService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MateriaService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetMateriaFromAlunoDTO>>> AddMateria(AddMateriaDTO newMateria)
        {
            var serviceResponse = new ServiceResponse<List<GetMateriaFromAlunoDTO>>();

            if (newMateria.ProfessorId is not null)
            {
                var professor = await _context.Professores.FindAsync(newMateria.ProfessorId);
                if (professor is null)
                {
                    // Handle the case where the related Professor entity doesn't exist
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Professor Inválido.";
                    return serviceResponse;
                }
                var materia = new Materia
                {
                    Name = newMateria.Name,
                    Semestre = newMateria.Semestre,
                    Professor = professor
                };
                _context.Materias.Add(_mapper.Map<Materia>(materia));
            }
            else
            {
                var materia = new Materia
                {
                    Name = newMateria.Name,
                    Semestre = newMateria.Semestre
                };
                _context.Materias.Add(_mapper.Map<Materia>(materia));
            }

            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Materias
                .Select(item => _mapper.Map<GetMateriaFromAlunoDTO>(item))
                .ToListAsync();

            return serviceResponse;
        }



        public async Task<ServiceResponse<List<GetMateriaFromAlunoDTO>>> GetAllMaterias()
        {

            var serviceResponse = new ServiceResponse<List<GetMateriaFromAlunoDTO>>();

            try
            {
                var dbMaterias = await _context.Materias
                    .Include(materia => materia.Professor)
                    .Include(materia => materia.Alunos)
                    .ToListAsync();


                var materiasDto = dbMaterias.Select(a => _mapper.Map<GetMateriaFromAlunoDTO>(a)).ToList();

                serviceResponse.Data = materiasDto;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMateriaFromAlunoDTO>> GetMateriaById(int id)
        {
            var serviceResponse = new ServiceResponse<GetMateriaFromAlunoDTO>();
            try
            {
                var dbMaterias = await _context.Materias.Include(m => m.Professor).ToListAsync();
                var materia = dbMaterias.FirstOrDefault(item => item.Id == id) ?? throw new Exception("Materia não encontrada"); ;
                serviceResponse.Data = _mapper.Map<GetMateriaFromAlunoDTO>(materia);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMateriaFromAlunoDTO>> UpdateMateria(UpdateMateriaDTO updatedMateria, int id)
        {
            var serviceResponse = new ServiceResponse<GetMateriaFromAlunoDTO>();
            try
            {
                var dbMaterias = await _context.Materias.FirstOrDefaultAsync(item => item.Id == id) ?? throw new Exception("Materia não encontrado");
                //aluno.Nome = updatedMateria.Nome;
                //aluno.Email = updatedMateria.Email;
                _mapper.Map(updatedMateria, dbMaterias);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetMateriaFromAlunoDTO>(dbMaterias);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteMateria(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var dbMateria = await _context.Materias.FirstOrDefaultAsync(item => item.Id == id) ?? throw new Exception("Materia não encontrado");
                _context.Materias.Remove(dbMateria);
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

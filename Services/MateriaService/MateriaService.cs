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

        // Add a new Materia
        public async Task<ServiceResponse<GetMateriaFromAlunoDTO>> AddMateria(AddMateriaDTO newMateria)
        {
            var serviceResponse = new ServiceResponse<GetMateriaFromAlunoDTO>();

            var materia = new Materia
            {
                Name = newMateria.Name,
                Semestre = newMateria.Semestre
            };

            if (newMateria.ProfessorId is not null)
            {
                // Retrieve the related Professor from the database
                var professor = await _context.Professores.FindAsync(newMateria.ProfessorId);
                if (professor is null)
                {
                    // Handle the case where the related Professor entity doesn't exist
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Professor Inválido.";
                    return serviceResponse;
                }
                materia.Professor = professor;
            }

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            // Map the added Materia entity to GetMateriaFromAlunoDTO
            serviceResponse.Data = _mapper.Map<GetMateriaFromAlunoDTO>(materia);
            return serviceResponse;
        }

        // Get all Materias
        public async Task<ServiceResponse<List<GetMateriaFromAlunoDTO>>> GetAllMaterias()
        {
            var serviceResponse = new ServiceResponse<List<GetMateriaFromAlunoDTO>>();

            try
            {
                // Retrieve all Materias from the database, including related Professor and Aluno entities
                var dbMaterias = await _context.Materias
                    .Include(materia => materia.Professor)
                    .Include(materia => materia.Alunos)
                    .ToListAsync();

                // Map Materias to GetMateriaFromAlunoDTO objects
                var materias = dbMaterias.Select(ma => _mapper.Map<GetMateriaFromAlunoDTO>(ma)).ToList();

                // Set the response data and success status
                serviceResponse.Data = materias;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions and set the appropriate error message and success status
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        // Get a Materia by ID
        public async Task<ServiceResponse<GetMateriaFromAlunoDTO>> GetMateriaById(int id)
        {
            var serviceResponse = new ServiceResponse<GetMateriaFromAlunoDTO>();

            try
            {
                // Retrieve the Materia from the database by ID, including related Professor
                var materia = await _context.Materias.Include(m => m.Professor).FirstOrDefaultAsync(item => item.Id == id)
                    ?? throw new KeyNotFoundException("Materia não encontrada");

                // Map the Materia entity to GetMateriaFromAlunoDTO
                serviceResponse.Data = _mapper.Map<GetMateriaFromAlunoDTO>(materia);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and set the appropriate error message and success status
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        // Update a Materia
        public async Task<ServiceResponse<GetMateriaFromAlunoDTO>> UpdateMateria(UpdateMateriaDTO updatedMateria, int id)
        {
            var serviceResponse = new ServiceResponse<GetMateriaFromAlunoDTO>();

            try
            {
                // Retrieve the Materia from the database by ID
                var dbMateria = await _context.Materias.SingleOrDefaultAsync(item => item.Id == id)
                    ?? throw new Exception("Materia não encontrada.");

                // Update the Materia entity with the new values from the updated Materia DTO
                _mapper.Map(updatedMateria, dbMateria);

                await _context.SaveChangesAsync();

                // Map the updated Materia entity to GetMateriaFromAlunoDTO
                serviceResponse.Data = _mapper.Map<GetMateriaFromAlunoDTO>(dbMateria);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and set the appropriate error message and success status
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        // Delete a Materia
        public async Task<ServiceResponse<bool>> DeleteMateria(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                // Retrieve the Materia from the database by ID
                var dbMateria = await _context.Materias.SingleOrDefaultAsync(item => item.Id == id)
                    ?? throw new Exception("Materia não encontrada");

                // Remove the Materia from the database
                _context.Materias.Remove(dbMateria);
                await _context.SaveChangesAsync();

                // Set the response data and success status
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions and set the appropriate error message and success status
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

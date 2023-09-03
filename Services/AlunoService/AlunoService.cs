namespace PortalNotas.Services.AlunoService;

public class AlunoService : IAlunoService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public AlunoService(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    // Retrieve all Alunos from the database
    public async Task<ServiceResponse<List<GetAlunoDTO>>> GetAllAlunos()
    {
        var serviceResponse = new ServiceResponse<List<GetAlunoDTO>>();

        try
        {
            // Retrieve all Alunos from the database, including related Materias and Professors
            var dbAlunos = await _context.Alunos
                .Include(a => a.Materias)
                .ThenInclude(m => m.Professor)
                .ToListAsync();

            // Map Aluno entities to GetAlunoDTO objects
            var alunosDto = dbAlunos.Select(a => _mapper.Map<GetAlunoDTO>(a)).ToList();

            // Set the response data and success status
            serviceResponse.Data = alunosDto;
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

    // Add a new Aluno to the database
    public async Task<ServiceResponse<GetAlunoDTO>> AddAluno(AddAlunoDTO newAluno)
    {
        var serviceResponse = new ServiceResponse<GetAlunoDTO>();

        if (newAluno is null)
            throw new ArgumentNullException(nameof(newAluno));

        var aluno = Aluno.Create(newAluno.Nome, newAluno.Email);
        // Map the new Aluno DTO to an Aluno entity
        //var aluno = _mapper.Map<Aluno>(newAluno);

        // Add the Aluno entity to the database
        _context.Alunos.Add(aluno);

        await _context.SaveChangesAsync();

        // Map the added Aluno entity back to a GetAlunoDTO object
        serviceResponse.Data = _mapper.Map<GetAlunoDTO>(aluno);
        serviceResponse.Success = true;

        return serviceResponse;
    }

    // Retrieve a specific Aluno by ID from the database
    public async Task<ServiceResponse<GetAlunoDTO>> GetAlunoById(int id)
    {
        var serviceResponse = new ServiceResponse<GetAlunoDTO>();
        try
        {
            // Find the Aluno in the database by ID, including related Materias and Professors
            var aluno =
                await _context.Alunos
                    .Include(a => a.Materias)
                    .ThenInclude(m => m.Professor)
                    .FirstOrDefaultAsync(item => item.Id == id)
                ?? throw new KeyNotFoundException("Aluno não encontrado");

            // Map the Aluno entity to a GetAlunoDTO object
            var alunoDto = _mapper.Map<GetAlunoDTO>(aluno);

            // Set the response data and success status
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

    // Update an existing Aluno in the database
    public async Task<ServiceResponse<GetAlunoDTO>> UpdateAluno(UpdateAlunoDTO updatedAluno, int id)
    {
        var serviceResponse = new ServiceResponse<GetAlunoDTO>();
        try
        {
            // Find the Aluno in the database by ID
            var dbAlunos =
                await _context.Alunos.FirstOrDefaultAsync(item => item.Id == id)
                ?? throw new KeyNotFoundException("Aluno não encontrado");

            // Update the Aluno entity with the new values from the updated Aluno DTO
            _mapper.Map(updatedAluno, dbAlunos);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Retrieve the updated Aluno with the related Materias
            var aluno = await _context.Alunos
                .Include(a => a.Materias)
                .FirstOrDefaultAsync(item => item.Id == id);

            // Map the updated Aluno entity to a GetAlunoDTO object
            serviceResponse.Data = _mapper.Map<GetAlunoDTO>(aluno);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    // Delete an existing Aluno from the database
    public async Task<ServiceResponse<bool>> DeleteAluno(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();
        try
        {
            // Find the Aluno in the database by ID
            var dbAluno =
                await _context.Alunos.FirstOrDefaultAsync(item => item.Id == id)
                ?? throw new KeyNotFoundException("Aluno não encontrado");

            // Remove the Aluno from the database
            _context.Alunos.Remove(dbAluno);
            await _context.SaveChangesAsync();

            // Set the response data and success status
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    // Link an Aluno to a Materia
    public async Task LinkAlunoToMateria(LinkMateriaAlunoDTO newLinkMateriaAluno)
    {
        // Retrieve the Aluno from the database
        var aluno =
            await _context.Alunos
                .Include(a => a.Materias)
                .FirstOrDefaultAsync(a => a.Id == newLinkMateriaAluno.AlunoId)
            ?? throw new KeyNotFoundException("Aluno não encontrado.");

        // Retrieve the Materia from the database
        var materia =
            await _context.Materias.FirstOrDefaultAsync(m => m.Id == newLinkMateriaAluno.MateriaId)
            ?? throw new KeyNotFoundException("Matéria não encontrada.");

        // Check if the Link already exists
        var link = await _context.MateriaAluno.FirstOrDefaultAsync(
            ma =>
                ma.AlunoId == newLinkMateriaAluno.AlunoId
                && ma.MateriaId == newLinkMateriaAluno.MateriaId
        );
        if (link is not null)
            throw new ApplicationException("Link já existe.");

        // Create new MateriaAluno relationship
        var materiaAluno = new MateriaAluno { AlunoId = aluno.Id, MateriaId = materia.Id };

        // Transaction Handling
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Add the new MateriaAluno relationship to the database
            await _context.MateriaAluno.AddAsync(materiaAluno);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            // Rollback the transaction in case of an error
            await transaction.RollbackAsync();
            throw;
        }
    }

    // Unlink an Aluno from a Materia
    public async Task UnlinkAlunoToMateria(LinkMateriaAlunoDTO UnlinkMateriaAluno)
    {
        // Find the MateriaAluno link in the database
        var link =
            await _context.MateriaAluno.FirstOrDefaultAsync(
                ma =>
                    ma.AlunoId == UnlinkMateriaAluno.AlunoId
                    && ma.MateriaId == UnlinkMateriaAluno.MateriaId
            ) ?? throw new KeyNotFoundException("Link não encontrado.");

        // Remove the MateriaAluno link from the database
        _context.MateriaAluno.Remove(link);
        await _context.SaveChangesAsync();
    }
}

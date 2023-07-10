using PortalNotas.Models.DTOs.Materia;

namespace PortalNotas.Services.MateriaService
{
    public interface IMateriaService
    {
        Task<ServiceResponse<List<GetMateriaFromAlunoDTO>>> GetAllMaterias();
        Task<ServiceResponse<GetMateriaFromAlunoDTO>> GetMateriaById(int id);
        Task<ServiceResponse<List<GetMateriaFromAlunoDTO>>> AddMateria(AddMateriaDTO newMateria);
        Task<ServiceResponse<GetMateriaFromAlunoDTO>> UpdateMateria(UpdateMateriaDTO materia, int id);
        Task<ServiceResponse<bool>> DeleteMateria(int id);


    }
}

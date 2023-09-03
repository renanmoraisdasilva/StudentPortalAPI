using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Materia
{
    public class AddMateriaDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "O campo Semestre é obrigatório.")]
        public int Semestre { get; set; }
        public int? ProfessorId { get; set; }
    }
}

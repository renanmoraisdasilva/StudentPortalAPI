using PortalNotas.Models.DTOs.Materia;
using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Aluno
{
    public class GetAlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        public string Email { get; set; } = string.Empty;

        public List<GetMateriaFromAlunoDTO> Materias { get; set; } = new();
    }
}

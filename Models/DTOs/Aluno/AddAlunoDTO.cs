using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Aluno
{
    public class AddAlunoDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; } = string.Empty;
    }
}

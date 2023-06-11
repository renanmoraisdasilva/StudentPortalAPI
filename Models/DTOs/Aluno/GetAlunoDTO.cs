using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Aluno
{
    public class GetAlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        public string? Email { get; set; }
    }
}

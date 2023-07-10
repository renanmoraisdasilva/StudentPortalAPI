using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortalNotas.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        public string? Email { get; set; }

        public List<Materia> Materias { get; } = new();

    }
}


//public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
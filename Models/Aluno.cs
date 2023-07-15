using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortalNotas.Models
{
    public class Aluno
    {
        public Aluno(
            string Nome,
            string Email
            )
        {
            this.Nome = Nome;
            this.Email = Email;
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Materia>? Materias { get; set; }

    }
}
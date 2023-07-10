using System.ComponentModel.DataAnnotations.Schema;

namespace PortalNotas.Models
{
    public class Materia
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [ForeignKey("Professor")]
        public int? ProfessorId { get; set; }
        public Professor? Professor { get; set; }

        public int Semestre { get; set; } = new();


        public List<Aluno>? Alunos { get; }

    }
}

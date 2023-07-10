using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalNotas.Models
{

    public class MateriaAluno
    {
        public int MateriaId { get; set; }
        //public Materia Materia { get; set; } = new Materia();

        public int AlunoId { get; set; }
        //public Aluno Aluno { get; set; } = new Aluno();

        public decimal Nota { get; set; }
        public DateTime DataMatricula { get; set; }
        public string Resultado { get; set; } = string.Empty;
    }
}

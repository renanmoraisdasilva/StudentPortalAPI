namespace PortalNotas.Models
{

    public class MateriaAluno
    {
        public int MateriaId { get; set; }
        public int AlunoId { get; set; }
        public decimal Nota { get; set; }
        public DateTime DataMatricula { get; set; }
        public string Resultado { get; set; } = string.Empty;
    }
}

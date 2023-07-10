namespace PortalNotas.Models.DTOs.Materia
{
    public class GetMateriaFromAlunoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semestre { get; set; } = new();
        public Professor? Professor { get; set; }
    }
}

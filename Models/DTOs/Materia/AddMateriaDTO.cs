namespace PortalNotas.Models.DTOs.Materia
{
    public class AddMateriaDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Semestre { get; set; } = new();
        public int? ProfessorId { get; set; }
        //public Professor? Professor { get; set; }
    }
}

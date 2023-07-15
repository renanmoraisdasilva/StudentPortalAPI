namespace PortalNotas.Models.DTOs.Materia
{
    public class UpdateMateriaDTO
    {
        public string Name { get; set; } = string.Empty; 
        public int Semestre { get; set; } = new();
    }
}

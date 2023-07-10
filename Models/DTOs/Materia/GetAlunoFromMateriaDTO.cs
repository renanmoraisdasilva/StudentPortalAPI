namespace PortalNotas.Models.DTOs.Materia
{
    public class GetAlunoFromMateriaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

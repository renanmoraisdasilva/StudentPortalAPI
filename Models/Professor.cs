using System.Text.Json.Serialization;

namespace PortalNotas.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }

        [JsonIgnore]
        public List<Materia> Materias { get; } = new List<Materia>();

    }
}

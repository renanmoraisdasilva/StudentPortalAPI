using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public string Professor { get; set; }

    }
}

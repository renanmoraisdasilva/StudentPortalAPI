using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models;

public class Nota
{
    public int Id { get; set; }
    public int Valor { get; set; }

    // Foreign Keys
    [Required(ErrorMessage = "O campo Aluno é obrigatório")]
    public int AlunoId { get; set; }

    [Required(ErrorMessage = "O campo Matéria é obrigatório")]
    public int MateriaId { get; set; }

    // Navigation Properties
    [Required(ErrorMessage = "O Aluno é obrigatório")]
    public virtual Aluno? Aluno { get; set; }

    [Required(ErrorMessage = "A Materia é obrigatório")]
    public virtual Materia? Materia { get; set; }
}

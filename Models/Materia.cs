using System.ComponentModel.DataAnnotations.Schema;

namespace PortalNotas.Models;

public class Materia
{
    private Materia(string Name, int? ProfessorId, int Semestre)
    {
        this.Name = Name;
        this.ProfessorId = ProfessorId;
        this.Semestre = Semestre;
    }

    private Materia(string Name, int Semestre)
    {
        this.Name = Name;
        this.Semestre = Semestre;
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    [ForeignKey("Professor")]
    public int? ProfessorId { get; private set; }
    public Professor? Professor { get; private set; }

    public int Semestre { get; private set; } = new();

    public List<Aluno>? Alunos { get; }

    public static Materia Create(AddMateriaDTO newMateria)
    {
        if (newMateria.ProfessorId.HasValue)
        {
            return new Materia(newMateria.Name, newMateria.ProfessorId, newMateria.Semestre);
        }
        return new Materia(newMateria.Name, newMateria.Semestre);
    }
}

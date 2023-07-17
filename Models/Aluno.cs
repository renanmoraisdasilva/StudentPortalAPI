namespace PortalNotas.Models;

public class Aluno
{
    private Aluno(string Nome, string Email)
    {
        this.Nome = Nome;
        this.Email = Email;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public List<Materia> Materias { get; private set; } = new();

    public static Aluno Create(string Nome, string Email)
    {
        return new Aluno(Nome, Email);
    }
}

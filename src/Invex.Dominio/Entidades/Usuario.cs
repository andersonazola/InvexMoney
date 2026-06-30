namespace Invex.Dominio.Entidades;

public class Usuario
{
    public int Id { get; private set; } //PK
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public List<Ativo> Ativos { get; set; } = new List<Ativo>();


    private Usuario() { }

    public Usuario(string nome, string email, string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome inválido");

        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new Exception("Senha inválida");

        if (!email.Contains("@"))
            throw new Exception("Email inválido");


        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        DataCriacao = DateTime.UtcNow;
    }

}
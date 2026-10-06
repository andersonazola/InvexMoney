namespace Invex.Domain.Entidades;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; } // Vai guardar hash
    public DateTime DataCriacao { get; private set; }

    // public List<Posicao> Posicoes { get; set; } = new List<Posicao>();

    private Usuario() { }

    public Usuario(string nome, string email, string senhaHash)
    {


        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome inválido");

        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("Senha inválida");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email inválido");

        email = email.Trim().ToLower();

        if (!email.Contains("@"))
            throw new ArgumentException("Email inválido");


        Nome = nome;
        Email = email;
        Senha = senhaHash;
        DataCriacao = DateTime.UtcNow;
    }
}

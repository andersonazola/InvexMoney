using Invex.Dominio.Enums;

namespace Invex.Dominio.Entidades;


public class Ativo
{
    public int Id { get; private set; } //Pk
    public int UsuarioId { get; private set; }//Fk
    public string Ticker { get; private set; }
    public string Nome { get; private set; }
    public TipoAtivo Tipo { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Usuario Usuario { get; private set; }
    public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();
    public List<Dividendo> Dividendos { get; private set; } = new List<Dividendo>();

    private Ativo() {}
    public Ativo(int usuarioid, string ticker, string nome, TipoAtivo tipo)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new Exception("Ticker inválido");

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome inválido");

        UsuarioId = usuarioid;
        Ticker = ticker;
        Nome = nome;
        Tipo = tipo;
        DataCriacao = DateTime.UtcNow;

    }


}
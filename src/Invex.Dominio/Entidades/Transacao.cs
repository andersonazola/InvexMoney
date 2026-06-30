using Invex.Dominio.Enums;

namespace Invex.Dominio.Entidades;

public class Transacao
{
    public int Id { get; private set; } //PK
    public int AtivoId { get; private set; } //FK
    public TipoTransacao Tipo { get; private set; }
    public decimal Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public DateTime Data { get; private set; }
    public string? Observacao { get; private set; }


    public Ativo Ativo { get; private set; }

    public decimal ValorTotal => Quantidade * PrecoUnitario; // Toda vez que acessar ele executa o calculo na hora com os valores atuais

    private Transacao(){}

    public Transacao(int ativoId, TipoTransacao tipo, decimal quantidade, decimal precoUnitario, string observacao = null)
    {
        AtivoId = ativoId;
        Tipo = tipo;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Observacao = observacao;
        Data = DateTime.UtcNow;
    }


}
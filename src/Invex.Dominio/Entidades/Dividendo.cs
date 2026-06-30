namespace Invex.Dominio.Entidades;

public class Dividendo
{
    public int Id { get; private set; } // PK
    public int AtivoId { get; private set; } // FK
    public decimal ValorPorCota { get; private set; }
    public decimal QuantidadeNaData { get; private set; }
    public DateTime DataPagamento { get; private set; }
    public decimal ValorTotal => QuantidadeNaData * ValorPorCota;


    public Ativo Ativo { get; private set; }


    private Dividendo() {}
    public Dividendo(int ativoId, decimal valorPorCota, decimal quantidade, DateTime dataPagamento)
    {
        AtivoId = ativoId;
        ValorPorCota = valorPorCota;
        QuantidadeNaData = quantidade;
        DataPagamento = dataPagamento;
    }
}
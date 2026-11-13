namespace Invex.Application.Dtos.Response;

//uma ação do ranking
public class AtivoResponse
{
    public string Ticker { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public decimal Variacao { get; set; } // %

}
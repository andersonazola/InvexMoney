namespace Invex.Services.Dtos.Response;

public class QuoteResponse
{
    public decimal? LastPrice { get; set; } //Ultimo Preço
    public decimal? ChangePercent { get; set; } //Variacão
    public long? Volume { get; set; } 
}